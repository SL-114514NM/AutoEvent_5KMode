using MEC;
using Mirror;
using NVorbis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VoiceChat;
using VoiceChat.Codec;
using VoiceChat.Networking;

namespace AutoEvent_5KMode.API.VoiceDummy
{
    public class Base:MonoBehaviour
    {
        public static Dictionary<string, Base> VoiceBases = new Dictionary<string, Base>();
        public ReferenceHub Owner { get; set; }
        public string Name { get; set; }
        public VorbisReader VorbisReader { get; set; }
        public float[] SendBuffer { get; set; }
        public float[] ReadBuffer { get; set; }
        public const int HeadSamples = 1920;
        public OpusEncoder Encoder { get; } = new OpusEncoder(VoiceChat.Codec.Enums.OpusApplicationType.Voip);
        public PlaybackBuffer PlaybackBuffer { get; } = new PlaybackBuffer();
        public byte[] EncodedBuffer { get; } = new byte[512];
        public CoroutineHandle PlaybackCoroutine;
        public MemoryStream CurrentPlayStream;
        public bool IsPlaying { get; set; } = false;
        public bool DestroyOnComplete { get; set; } = false;
        public string CurrentAudioPath { get; set; }
        public float Volume { get; set; } = 1.0f;
        public FakeConnection FakeConnection { get; set; }
        private int _samplesPerPacket = 480; 
        private float[] _encodeBuffer;
        private static int _nextConnectionId = 1000; 
        public bool Loop = false;
        public VoiceChatChannel BroadcastChannel { get; set; } = VoiceChatChannel.Proximity;
        public Base(string name, ReferenceHub owner = null)
        {
            Create(name, owner);
        }
        public static Base Create(string name, ReferenceHub hub = null)
        {
            Base gameObject = new GameObject(name).AddComponent<Base>();
            gameObject.Owner = hub;
            gameObject.Name = name;
            gameObject.Owner.nicknameSync.DisplayName = name;
            VoiceBases.Add(name, gameObject);
            return gameObject;
        }

        public static Base Get(string name)
        {
            return VoiceBases[name];
        }
        public static bool TryGet(string name, out Base result)
        {
            var a = Get(name);
            if (a == null)
            {
                result = null;
                return false;
            }
            result = a;
            return true;
        }
        public bool Play(string audioPath, bool isLoop = false, bool isDestroy = false)
        {
            if (IsPlaying)
            {
                Debug.LogWarning($"[VoiceDummy] {Name} 正在播放音频，无法播放新音频");
                return false;
            }
            if (!File.Exists(audioPath))
            {
                Debug.LogError($"[VoiceDummy] 音频文件不存在: {audioPath}");
                return false;
            }
            if (!audioPath.EndsWith(".ogg", StringComparison.OrdinalIgnoreCase))
            {
                Debug.LogError($"[VoiceDummy] 只支持 .ogg 格式的音频文件: {audioPath}");
                return false;
            }
            VorbisReader = new VorbisReader(audioPath);
            if (VorbisReader.SampleRate != 48000)
            {
                Debug.LogError($"[VoiceDummy] 音频采样率必须为48000，当前采样率: {VorbisReader.SampleRate}");
                VorbisReader.Dispose();
                VorbisReader = null;
                return false;
            }
            if (VorbisReader.Channels != 1)
            {
                Debug.LogWarning($"[VoiceDummy] 建议使用单轨道音频，当前轨道数: {VorbisReader.Channels}");
            }
            Loop = isLoop;
            CurrentAudioPath = audioPath;
            IsPlaying = true;
            SendBuffer = new float[HeadSamples];
            ReadBuffer = new float[HeadSamples];
            _encodeBuffer = new float[_samplesPerPacket];
            PlaybackCoroutine = Timing.RunCoroutine(PlaybackRoutine());
            DestroyOnComplete = isDestroy;
            Debug.Log($"[VoiceDummy] 开始播放音频: {Path.GetFileName(audioPath)} (循环: {isLoop})");
            return true;
        }
        public void Stop(bool isDestroy = false)
        {
            if (PlaybackCoroutine.IsRunning)
            {
                Timing.KillCoroutines(PlaybackCoroutine);
            }
            if (VorbisReader != null)
            {
                VorbisReader.Dispose();
                VorbisReader = null;
            }

            if (CurrentPlayStream != null)
            {
                CurrentPlayStream.Dispose();
                CurrentPlayStream = null;
            }
            IsPlaying = false;
            Loop = false;
            CurrentAudioPath = null;
            SendBuffer = null;
            ReadBuffer = null;
            _encodeBuffer = null;
            Debug.Log($"[VoiceDummy] 停止播放音频: {Name}");
            if (isDestroy)
            {
                OnDestoy();
            }
        }
        private IEnumerator<float> PlaybackRoutine()
        {
            while (IsPlaying && VorbisReader != null)
            {
                int samplesRead = VorbisReader.ReadSamples(ReadBuffer, 0, HeadSamples);
                if (samplesRead > 0)
                {
                    ProcessAndSendAudioData(samplesRead);
                    yield return Timing.WaitForOneFrame;
                }
                else
                {
                    if (Loop)
                    {
                        VorbisReader.SeekTo(TimeSpan.Zero);
                        Debug.Log($"[VoiceDummy] 循环播放音频: {Path.GetFileName(CurrentAudioPath)}");
                    }
                    else
                    {
                        if (DestroyOnComplete)
                        {
                            OnDestoy();
                        }
                        else
                        {
                            Stop();
                        } 
                        Debug.Log($"[VoiceDummy] 音频播放完成: {Path.GetFileName(CurrentAudioPath)}");
                        yield break;
                    }
                }
            }
        }
        private void ProcessAndSendAudioData(int samplesRead)
        {
            if (Owner == null)
            {
                Debug.LogWarning($"[VoiceDummy] 无法发送音频数据: Owner为null");
                return;
            }
            for (int i = 0; i < samplesRead; i++)
            {
                ReadBuffer[i] *= Volume;
            }
            PlaybackBuffer.Write(ReadBuffer, samplesRead);
            SendVoiceData();
        }
        private void SendVoiceData()
        {
            while (PlaybackBuffer.Length >= _samplesPerPacket)
            {
                PlaybackBuffer.ReadTo(_encodeBuffer, _samplesPerPacket, 0L);
                int encodedLength = Encoder.Encode(_encodeBuffer, EncodedBuffer, _samplesPerPacket);
                if (encodedLength > 0)
                {
                    SendVoiceMessage(EncodedBuffer, encodedLength);
                }
            }
        }
        private void SendVoiceMessage(byte[] data, int dataLength)
        {
            if (Owner == null)
                return;

            try
            {
                var voiceMessage = new VoiceMessage(Owner, BroadcastChannel, data, dataLength, false);
                if (NetworkServer.active)
                {
                    BroadcastToAllClients(voiceMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"[VoiceDummy] 发送语音消息时发生错误: {ex.Message}");
            }
        }
        private void BroadcastToAllClients(VoiceMessage voiceMessage)
        {
            foreach (var hub in ReferenceHub.AllHubs)
            {
                if (hub.connectionToClient != null && hub != Owner)
                {
                    try
                    {
                        hub.connectionToClient.Send(voiceMessage, 0);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogWarning($"[VoiceDummy] 发送给客户端 {hub.nicknameSync.DisplayName} 失败: {ex.Message}");
                    }
                }
            }
        }
        public void OnDestoy()
        {
            GameObject.Destroy(gameObject);
            VoiceBases.Remove(Name);
        }
    }
}
