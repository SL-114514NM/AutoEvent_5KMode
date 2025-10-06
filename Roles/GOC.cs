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
    public class GOC
    {
        public static void Spawn(Player player)
        {
            player.AddSpecialRole(PlayerExtension.SpecialRolesName.GOC);
            player.GiveLoadout(MainGame._5KMode.Plugin.StaticConfig.GOCLoadout);
            AudioPlayer audioPlayer = AudioPlayer.CreateOrGet("goc-Join");
            string gocmusic = Path.Combine(ConfigPath.MusicPath, "gocjoin.ogg");
            audioPlayer.AddClip(gocmusic, 50, false, true);
            Cassie.MessageTranslated("", MainGame._5KMode.Plugin.G5K.Translation.GOCJoinCASSIE);
            player.Position = Room.Get(Exiled.API.Enums.RoomType.EzIntercom).Position + UnityEngine.Vector3.up;
            player.GetPlayerUi().CommonHint.ShowRoleHint("GOC小队", "你们与基金会为敌, 你们启动核弹即可启动Omega", 30);
        }
    }
}
