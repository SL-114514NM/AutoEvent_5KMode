using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.MusicAPI
{
    public class SpeakerAudioPlayer
    {
        public static Dictionary<SpeakerToy, SpeakerAudioPlayer> List = new Dictionary<SpeakerToy, SpeakerAudioPlayer>();
        public SpeakerAudioPlayer(SpeakerToy speakerToy)
        {
            this.Speaker = speakerToy;
            List.Add(speakerToy, this);
        }
        public static SpeakerAudioPlayer Create(Vector3 pos,string name)
        {
            SpeakerToy speakerToy = SpeakerToy.Create(pos);
            speakerToy.Spawn();
            speakerToy.GameObject.name = name;
            SpeakerAudioPlayer speakerAudioPlayer = new SpeakerAudioPlayer(speakerToy);
            return speakerAudioPlayer;
        }
        public SpeakerToy Speaker{ get; set; }
        public bool IsPlaying => Speaker.IsPlaying;
        public bool IsPaused => Speaker.IsPaused;

        public void Play(float[] samples, bool queue = true,bool loop = false)
        {
            SpeakerToy.GetTransmitter(Speaker.ControllerId).Play(samples, queue, loop);
        }
        public void Stop()
        {
            SpeakerToy.GetTransmitter(Speaker.ControllerId).Stop();
        }
        public void Destroy()
        {
            Speaker.Destroy();
            List.Remove(Speaker);
        }
    }
}
