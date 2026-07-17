using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using Mirror;
using NetworkManagerUtils.Dummies;
using NVorbis;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VoiceChat;
using VoiceChat.Codec;
using VoiceChat.Networking;
using Logger = LabApi.Features.Console.Logger;

namespace AutoEvent_5KMode.API.Featrues.AudioManager
{
    public class DummyMusic
    {
        private static readonly byte[] EmptyVoiceData = Array.Empty<byte>();

        public DummyMusic(ReferenceHub owner)
        {
            Owner = owner;
            Encoder = new OpusEncoder(VoiceChat.Codec.Enums.OpusApplicationType.Voip);
        }

        public static Dictionary<ReferenceHub, DummyMusic> DummyList = new Dictionary<ReferenceHub, DummyMusic>();
        public ReferenceHub Owner;
        public bool IsSpeaking = false;
        public OpusEncoder Encoder { get; private set; }
        private CoroutineHandle _playCoroutine;
        private CoroutineHandle _loopCoroutine;
        private bool _stopRequested = false;

        public List<string> Paths { get; set; } = new List<string>();
        public int CurrentIndex { get; private set; } = 0;
        public string CurrentPath => Paths.Count > 0 ? Paths[CurrentIndex] : null;
        public bool IsLoopPlaying { get; private set; } = false;

        #region 静态工厂方法

        /// <summary>
        /// 创建一个音乐 Dummy 并返回其 DummyMusic 控制器
        /// </summary>
        public static DummyMusic Create(string name)
        {
            ReferenceHub hub = DummyUtils.SpawnDummy(name);
            VoiceChatMutes.SetFlags(hub, VcMuteFlags.GlobalIntercom);
            return GetOrAdd(hub);
        }

        /// <summary>
        /// 根据名称获取 DummyMusic
        /// </summary>
        public static DummyMusic Get(string Name)
        {
            foreach (var dummy in DummyList.Values)
            {
                if (dummy.Owner.nicknameSync.MyNick == Name)
                {
                    return dummy;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取或创建 DummyMusic
        /// </summary>
        public static DummyMusic GetOrAdd(ReferenceHub referenceHub)
        {
            if (DummyList.ContainsKey(referenceHub))
            {
                return DummyList[referenceHub];
            }
            DummyMusic dummy = new DummyMusic(referenceHub);
            DummyList.Add(referenceHub, dummy);
            return dummy;
        }

        /// <summary>
        /// 销毁所有 DummyMusic 实例
        /// </summary>
        public static void DestroyAll()
        {
            foreach (var kvp in new Dictionary<ReferenceHub, DummyMusic>(DummyList))
            {
                try
                {
                    kvp.Value.Destroy();
                }
                catch (Exception ex)
                {
                    Logger.Error($"销毁 DummyMusic 失败: {ex.Message}");
                }
            }
            DummyList.Clear();
        }

        #endregion

        #region 播放列表管理

        /// <summary>
        /// 添加单个音频文件到播放列表
        /// </summary>
        public void AddToPlaylist(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Logger.Error($"文件不存在: {filePath}");
                return;
            }
            if (Path.GetExtension(filePath).ToLower() != ".ogg")
            {
                Logger.Error($"{filePath} 不是ogg音频");
                return;
            }
            Paths.Add(filePath);
            Logger.Info($"已添加到播放列表: {Path.GetFileName(filePath)}");
        }

        /// <summary>
        /// 添加多个音频文件到播放列表
        /// </summary>
        public void AddToPlaylist(List<string> filePaths)
        {
            foreach (string path in filePaths)
            {
                AddToPlaylist(path);
            }
        }

        /// <summary>
        /// 清空播放列表
        /// </summary>
        public void ClearPlaylist()
        {
            Paths.Clear();
            CurrentIndex = 0;
            Logger.Info("播放列表已清空");
        }

        /// <summary>
        /// 从播放列表中移除指定索引的音频
        /// </summary>
        public void RemoveFromPlaylist(int index)
        {
            if (index >= 0 && index < Paths.Count)
            {
                string removedPath = Paths[index];
                Paths.RemoveAt(index);
                Logger.Info($"已从播放列表移除: {Path.GetFileName(removedPath)}");

                if (CurrentIndex >= Paths.Count)
                {
                    CurrentIndex = 0;
                }
            }
        }

        #endregion

        #region 播放控制

        /// <summary>
        /// 播放单个音频文件
        /// </summary>
        public void Play(string FilePath, bool destroyOnStopOrEnd = false)
        {
            if (!File.Exists(FilePath))
            {
                Logger.Error($"文件不存在: {FilePath}");
                return;
            }
            if (Path.GetExtension(FilePath).ToLower() != ".ogg")
            {
                Logger.Error($"{FilePath} 不是ogg音频");
                return;
            }

            Stop(false);
            StopLoop(false);

            _stopRequested = false;
            _playCoroutine = Timing.RunCoroutine(PlayCoroutine(FilePath, destroyOnStopOrEnd));
        }

        /// <summary>
        /// 开始循环播放列表中的所有音频
        /// </summary>
        public void StartLoopPlay(bool destroyOnStop = false)
        {
            if (Paths.Count == 0)
            {
                Logger.Error("播放列表为空，无法开始循环播放");
                return;
            }

            Stop(false);
            StopLoop(false);

            _stopRequested = false;
            IsLoopPlaying = true;
            CurrentIndex = 0;
            _loopCoroutine = Timing.RunCoroutine(LoopPlayCoroutine(destroyOnStop));
            Logger.Info($"开始循环播放，共 {Paths.Count} 首音频");
        }

        /// <summary>
        /// 播放播放列表中的指定音频
        /// </summary>
        public void PlayFromPlaylist(int index, bool destroyOnStopOrEnd = false)
        {
            if (Paths.Count == 0)
            {
                Logger.Error("播放列表为空");
                return;
            }

            if (index < 0 || index >= Paths.Count)
            {
                Logger.Error($"索引超出范围: {index}，播放列表大小: {Paths.Count}");
                return;
            }

            CurrentIndex = index;
            Play(Paths[CurrentIndex], destroyOnStopOrEnd);
        }

        /// <summary>
        /// 播放下一首
        /// </summary>
        public void PlayNext(bool destroyOnStop = false)
        {
            if (Paths.Count == 0)
            {
                Logger.Error("播放列表为空");
                return;
            }

            CurrentIndex = (CurrentIndex + 1) % Paths.Count;
            Play(Paths[CurrentIndex], destroyOnStop);
        }

        /// <summary>
        /// 播放上一首
        /// </summary>
        public void PlayPrevious(bool destroyOnStop = false)
        {
            if (Paths.Count == 0)
            {
                Logger.Error("播放列表为空");
                return;
            }

            CurrentIndex = (CurrentIndex - 1 + Paths.Count) % Paths.Count;
            Play(Paths[CurrentIndex], destroyOnStop);
        }

        /// <summary>
        /// 停止单曲播放
        /// </summary>
        public void Stop(bool willDestroy)
        {
            _stopRequested = true;
            if (_playCoroutine.IsRunning)
                Timing.KillCoroutines(_playCoroutine);

            if (IsSpeaking)
            {
                IsSpeaking = false;
                SendSilencePacket();
            }

            if (willDestroy)
            {
                Destroy();
            }
        }

        /// <summary>
        /// 停止循环播放
        /// </summary>
        public void StopLoop(bool willDestroy)
        {
            _stopRequested = true;
            if (_loopCoroutine.IsRunning)
            {
                Timing.KillCoroutines(_loopCoroutine);
            }
            IsLoopPlaying = false;

            if (willDestroy)
            {
                Destroy();
            }
        }

        /// <summary>
        /// 停止所有播放
        /// </summary>
        public void StopAll(bool willDestroy)
        {
            Stop(false);
            StopLoop(false);

            if (willDestroy)
            {
                Destroy();
            }
        }

        /// <summary>
        /// 销毁此 DummyMusic 并踢出 Dummy 玩家
        /// </summary>
        public void Destroy()
        {
            StopAll(false);
            DummyList.Remove(Owner);
            NetworkServer.Destroy(Owner.gameObject);
        }

        #endregion

        #region 内部协程

        /// <summary>
        /// 循环播放协程
        /// </summary>
        private IEnumerator<float> LoopPlayCoroutine(bool destroyOnStop)
        {
            while (!_stopRequested && Paths.Count > 0)
            {
                string currentPath = Paths[CurrentIndex];
                Logger.Info($"正在播放 [{CurrentIndex + 1}/{Paths.Count}]: {Path.GetFileName(currentPath)}");

                // 播放当前音频并等待完成
                yield return Timing.WaitUntilDone(Timing.RunCoroutine(PlayCoroutine(currentPath, false)));

                if (_stopRequested) break;

                // 移动到下一首
                CurrentIndex = (CurrentIndex + 1) % Paths.Count;

                // 曲间停顿
                yield return Timing.WaitForSeconds(0.5f);
            }

            IsLoopPlaying = false;

            if (destroyOnStop)
            {
                Destroy();
            }
        }

        /// <summary>
        /// 单曲播放协程
        /// </summary>
        private IEnumerator<float> PlayCoroutine(string filePath, bool destroyOnStopOrEnd)
        {
            VorbisReader reader = null;
            MemoryStream stream = null;

            byte[] fileData = File.ReadAllBytes(filePath);
            stream = new MemoryStream(fileData);
            reader = new VorbisReader(stream);

            // 校验格式：必须是单声道 48000Hz
            if (reader.Channels >= 2)
            {
                Logger.Error($"音频必须为单声道: {filePath}");
                yield break;
            }
            if (reader.SampleRate != 48000)
            {
                Logger.Error($"音频采样率必须为48000，当前为{reader.SampleRate}: {filePath}");
                yield break;
            }

            IsSpeaking = true;

            // Opus 编码参数
            int samplesPerFrame = 480;        // 每帧 480 样本
            float[] readBuffer = new float[samplesPerFrame];
            byte[] encodedBuffer = new byte[512];
            int samplesRead;

            // 批量发送缓冲：每积累5帧（50ms）发送一次
            const int framesPerBatch = 5;
            List<VoiceMessage> frameBuffer = new List<VoiceMessage>(framesPerBatch);
            int frameCount = 0;

            Logger.Info($"开始播放: {Path.GetFileName(filePath)}");

            while ((samplesRead = reader.ReadSamples(readBuffer, 0, samplesPerFrame)) > 0)
            {
                if (_stopRequested) break;

                // 最后一帧不足时补零
                if (samplesRead < samplesPerFrame)
                {
                    Array.Clear(readBuffer, samplesRead, samplesPerFrame - samplesRead);
                }

                // 编码音频帧
                int encodedLength = Encoder.Encode(readBuffer, encodedBuffer, samplesPerFrame);

                // 复制编码数据（避免引用被覆盖）
                byte[] frameData = new byte[encodedLength];
                Array.Copy(encodedBuffer, frameData, encodedLength);

                // 创建语音消息
                VoiceMessage msg = new VoiceMessage(
                    Owner,
                    VoiceChatChannel.Intercom,
                    frameData,
                    encodedLength,
                    false
                );

                frameBuffer.Add(msg);
                frameCount++;

                // 每积累 framesPerBatch 帧，批量发送
                if (frameCount >= framesPerBatch)
                {
                    foreach (var frame in frameBuffer)
                    {
                        SendVoiceMessageToAll(frame);
                    }
                    frameBuffer.Clear();
                    frameCount = 0;

                    // 等待 50ms（5帧 × 10ms/帧）
                    yield return Timing.WaitForSeconds(0.05f);
                }
            }

            // 发送剩余不足一批的帧
            if (frameBuffer.Count > 0)
            {
                foreach (var frame in frameBuffer)
                {
                    SendVoiceMessageToAll(frame);
                }
                frameBuffer.Clear();
            }

            Logger.Info($"播放完成: {Path.GetFileName(filePath)}");

            // 播放结束后销毁（如果不是循环播放模式）
            if (destroyOnStopOrEnd && !IsLoopPlaying)
            {
                Destroy();
            }
        }

        #endregion

        #region 网络通信

        /// <summary>
        /// 安全发送语音消息给所有在线玩家
        /// </summary>
        private static void SendVoiceMessageToAll(VoiceMessage msg)
        {
            foreach (Player player in Player.List)
            {
                if (player?.ConnectionToClient != null)
                {
                    try
                    {
                        player.ConnectionToClient.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"发送语音消息到 {player.Nickname} 失败: {ex.Message}");
                    }
                }
            }
        }

        /// <summary>
        /// 发送静音包，告知客户端语音传输结束
        /// </summary>
        private void SendSilencePacket()
        {
            // 使用空字节数组而非 null，避免序列化时 ArgumentNullException
            VoiceMessage endMsg = new VoiceMessage(
                Owner,
                VoiceChatChannel.Intercom,
                EmptyVoiceData,
                0,
                true
            );
            SendVoiceMessageToAll(endMsg);
        }

        #endregion
    }
}