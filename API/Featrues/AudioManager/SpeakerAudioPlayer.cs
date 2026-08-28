using LabApi.Features.Wrappers;
using NVorbis;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.AudioManager
{
    public class SpeakerAudioPlayer
    {
        public static Dictionary<SpeakerToy, SpeakerAudioPlayer> List = new Dictionary<SpeakerToy, SpeakerAudioPlayer>();

        public SpeakerToy Speaker { get; set; }
        public bool IsPlaying => Speaker?.IsPlaying ?? false;
        public bool IsPaused => Speaker?.IsPaused ?? false;
        public bool UseCameraPlayAudio = false;

        private List<SpeakerToy> _allCameraSpeakers = new List<SpeakerToy>();
        private int? _controllerId;

        public SpeakerAudioPlayer(SpeakerToy speakerToy)
        {
            Speaker = speakerToy ?? throw new ArgumentNullException(nameof(speakerToy));
            _controllerId = speakerToy.ControllerId;
            List.Add(speakerToy, this);
        }

        public static SpeakerAudioPlayer Create(Vector3 pos, string name)
        {
            try
            {
                SpeakerToy speakerToy = SpeakerToy.Create(pos);
                if (speakerToy == null) return null;

                speakerToy.Spawn();
                speakerToy.GameObject.name = name;
                return new SpeakerAudioPlayer(speakerToy);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to create speaker: {ex.Message}");
                return null;
            }
        }

        public void SpawnSpeakersInAllCameras()
        {
            try
            {
                foreach (var speaker in _allCameraSpeakers)
                {
                    speaker?.Destroy();
                }
                _allCameraSpeakers.Clear();
                foreach (LabApi.Features.Wrappers.Camera camera in LabApi.Features.Wrappers.Camera.List)
                {
                    if (camera == null) continue;

                    SpeakerToy speaker = SpeakerToy.Create(camera.Position);
                    if (speaker != null)
                    {
                        speaker.Spawn();
                        speaker.GameObject.name = $"{Speaker.GameObject.name}_Camera_{camera.Base.Label}";
                        _allCameraSpeakers.Add(speaker);
                    }
                }

                Debug.Log($"Created {_allCameraSpeakers.Count} camera speakers");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to spawn camera speakers: {ex.Message}");
            }
        }

        public void Play(float[] samples, bool queue = true, bool loop = false)
        {
            if (samples == null || samples.Length == 0)
            {
                Debug.LogWarning("Cannot play empty audio samples");
                return;
            }

            try
            {
                if (UseCameraPlayAudio && _allCameraSpeakers.Count > 0)
                {
                    foreach (var speaker in _allCameraSpeakers)
                    {
                        if (speaker?.ControllerId != null)
                        {
                            var transmitter = SpeakerToy.GetTransmitter(speaker.ControllerId);
                            transmitter?.Play(samples, queue, loop);
                        }
                    }
                }
                else if (Speaker?.ControllerId != null)
                {
                    var transmitter = SpeakerToy.GetTransmitter(Speaker.ControllerId);
                    transmitter?.Play(samples, queue, loop);
                }
                else
                {
                    Debug.LogWarning("No valid speaker controller available");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to play audio: {ex.Message}");
            }
        }

        public void Play(string path, bool loop = false)
        {
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogWarning("Audio file path is empty");
                return;
            }

            if (!System.IO.File.Exists(path))
            {
                Debug.LogWarning($"Audio file not found: {path}");
                return;
            }

            try
            {
                using (var vorbisReader = new VorbisReader(path))
                {
                    float[] samples = ReadAllSamples(vorbisReader);
                    Play(samples, true, loop);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to play audio file {path}: {ex.Message}");
            }
        }

        public void Play(byte[] audioData, bool queue = true, bool loop = false)
        {
            if (audioData == null || audioData.Length == 0)
            {
                Debug.LogWarning("Cannot play empty audio data");
                return;
            }

            try
            {
                float[] samples = ByteArrayToFloatArray(audioData);
                Play(samples, queue, loop);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to play audio data: {ex.Message}");
            }
        }

        public void Stop()
        {
            try
            {
                if (UseCameraPlayAudio && _allCameraSpeakers.Count > 0)
                {
                    foreach (var speaker in _allCameraSpeakers)
                    {
                        if (speaker?.ControllerId != null)
                        {
                            var transmitter = SpeakerToy.GetTransmitter(speaker.ControllerId);
                            transmitter?.Stop();
                        }
                    }
                }
                else if (Speaker?.ControllerId != null)
                {
                    var transmitter = SpeakerToy.GetTransmitter(Speaker.ControllerId);
                    transmitter?.Stop();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to stop audio: {ex.Message}");
            }
        }

        public void Pause()
        {
            try
            {
                if (UseCameraPlayAudio && _allCameraSpeakers.Count > 0)
                {
                    foreach (var speaker in _allCameraSpeakers)
                    {
                        if (speaker?.ControllerId != null)
                        {
                            var transmitter = SpeakerToy.GetTransmitter(speaker.ControllerId);
                            if (transmitter != null && transmitter.IsPlaying)
                                transmitter.Stop();
                        }
                    }
                }
                else if (Speaker?.ControllerId != null)
                {
                    var transmitter = SpeakerToy.GetTransmitter(Speaker.ControllerId);
                    if (transmitter != null && transmitter.IsPlaying)
                        transmitter.Stop();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to pause audio: {ex.Message}");
            }
        }

        private float[] ReadAllSamples(VorbisReader reader)
        {
            if (reader == null) return new float[0];

            try
            {
                int totalSamples = (int)(reader.TotalSamples * reader.Channels);
                if (totalSamples <= 0) return new float[0];

                float[] samples = new float[totalSamples];
                int samplesRead = 0;
                float[] buffer = new float[4096];

                while (samplesRead < totalSamples)
                {
                    int remaining = totalSamples - samplesRead;
                    int toRead = Math.Min(buffer.Length, remaining);
                    int read = reader.ReadSamples(buffer, 0, toRead);

                    if (read == 0) break;

                    Array.Copy(buffer, 0, samples, samplesRead, read);
                    samplesRead += read;
                }

                if (samplesRead < totalSamples)
                {
                    Array.Resize(ref samples, samplesRead);
                }

                return samples;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to read audio samples: {ex.Message}");
                return new float[0];
            }
        }

        private float[] ByteArrayToFloatArray(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return new float[0];

            int floatCount = byteArray.Length / sizeof(float);
            float[] floatArray = new float[floatCount];
            Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
            return floatArray;
        }

        private byte[] FloatArrayToByteArray(float[] floatArray)
        {
            if (floatArray == null || floatArray.Length == 0) return new byte[0];

            byte[] byteArray = new byte[floatArray.Length * sizeof(float)];
            Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
            return byteArray;
        }

        public void Destroy()
        {
            try
            {
                Stop();
                foreach (var speaker in _allCameraSpeakers)
                {
                    speaker?.Destroy();
                }
                _allCameraSpeakers.Clear();
                if (Speaker != null)
                {
                    List.Remove(Speaker);
                    Speaker.Destroy();
                    Speaker = null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to destroy audio player: {ex.Message}");
            }
        }
    }
}