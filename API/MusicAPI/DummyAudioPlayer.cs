using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;
using NetworkManagerUtils.Dummies;
using NVorbis;
using NVorbis.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceChat.Networking;

namespace AutoEvent_5KMode.API.MusicAPI
{
    public class DummyAudioPlayer
    {
        public DummyAudioPlayer(int id,ReferenceHub bot)
        {
            this.Bot = bot;
            this.Id = id;
        }
        public int Id { get; set; }
        public ReferenceHub Bot;
        public bool IsSpeaking = false;
        public static List<DummyAudioPlayer> List = new List<DummyAudioPlayer>();
        public static DummyAudioPlayer Create(string Name, int? Id=null)
        {
            int BotId;
            ReferenceHub referenceHub = DummyUtils.SpawnDummy(Name);
            if(Id == null)
            {
                BotId = referenceHub.PlayerId;
            }
            else
            {
                BotId = (int)Id;
            }
            DummyAudioPlayer audioPlayer = new DummyAudioPlayer(BotId,referenceHub);
            List.Add(audioPlayer);
            return audioPlayer;
        }
        public void Play(byte[] bytes, int lenght, bool isnull, bool UsedWillDestroy = true)
        {
            foreach(Player player in Player.List)
            {
                VoiceMessage vm = new VoiceMessage(Bot, VoiceChat.VoiceChatChannel.Intercom, bytes, lenght, isnull);
                player.ReferenceHub.connectionToClient.Send(vm);
            }
            this.IsSpeaking = true;
            if (UsedWillDestroy == true)
            {
                Timing.CallDelayed(lenght + 3, () =>
                {
                    foreach (Player player in Player.List)
                    {
                        VoiceMessage vm = new VoiceMessage(Bot, VoiceChat.VoiceChatChannel.None, null, 0, isnull);
                        player.ReferenceHub.connectionToClient.Send(vm);
                    }
                    Destroy();
                });
            }
        }
        public void Play(string FilePath,bool isnull = false)
        {
            if(!File.Exists(FilePath))
            {
                return;
            }
            if(Path.GetExtension(FilePath) != ".ogg")
            {
                Logger.Error($"{FilePath}不是ogg音频");
                return;
            }
            VorbisReader vb = new VorbisReader(FilePath);
            float lengthInSeconds = (float)((double)vb.TotalSamples / (vb.Channels * vb.SampleRate));
            byte[] bytes = FloatArrayToByteArray(ReadAllSamples(vb));
            Play(bytes, (int)lengthInSeconds, isnull);
        }
        private float[] ReadAllSamples(VorbisReader reader)
        {
            int totalSamples = (int)(reader.TotalSamples * reader.Channels);
            float[] samples = new float[totalSamples];

            int samplesRead = 0;
            float[] buffer = new float[4096];

            while (true)
            {
                int read = reader.ReadSamples(buffer, 0, buffer.Length);
                if (read == 0) break;

                System.Array.Copy(buffer, 0, samples, samplesRead, read);
                samplesRead += read;
            }

            if (samplesRead < totalSamples)
            {
                System.Array.Resize(ref samples, samplesRead);
            }

            return samples;
        }
        private byte[] FloatArrayToByteArray(float[] floatArray)
        {
            byte[] byteArray = new byte[floatArray.Length * sizeof(float)];
            System.Buffer.BlockCopy(floatArray, 0, byteArray, 0, byteArray.Length);
            return byteArray;
        }
        public void Destroy()
        {
            Server.KickPlayer(Player.Get(Bot), "666");
            List.Remove(this);
        }
    }
}
