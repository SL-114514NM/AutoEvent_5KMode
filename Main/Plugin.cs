using AutoEvent_5KMode.API;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Main
{
    public class Plugin:Plugin<Config>
    {
        public override string Author => "HUI";
        public override string Name => "AutoEvent_MiniGame_5K";
        public override Version Version => new Version(0,1);
        public static Plugin Instance {  get; set; }
        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Player.Died += OnDied;
            Main.ConfigPath.Int();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            Instance = null;
            Exiled.Events.Handlers.Player.Died -= OnDied;
            base.OnDisabled();
        }
        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Player != null)
            {
                if (StarAPI.SelectPlayer.Contains(ev.Player))
                {
                    StarAPI.SelectPlayer.Remove(ev.Player);
                }
            }
        }
    }
}
