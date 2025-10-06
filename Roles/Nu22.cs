using AutoEvent;
using AutoEvent_5KMode.API;
using AutoEvent_5KMode.Main;
using Exiled.API.Features;
using HintServiceMeow.UI.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Roles
{
    public class Nu22
    {
        public static void Spawn(Player player)
        {
            player.AddSpecialRole(PlayerExtension.SpecialRolesName.Nu22);
            player.GiveLoadout(MainGame._5KMode.Plugin.StaticConfig.Nu22Loadout);
            AudioPlayer audioPlayer = AudioPlayer.CreateOrGet("nu22-Join");
            string n22music = Path.Combine(ConfigPath.MusicPath, "nu22join.ogg");
            audioPlayer.AddClip(n22music, 50, false, true);
            Cassie.MessageTranslated("", MainGame._5KMode.Plugin.G5K.Translation.Nu22JoinCASSIE);
            player.GetPlayerUi().CommonHint.ShowRoleHint("Nu-22|火箭侠", "目标: 收容SCP1440", 30);
        }
    }
}
