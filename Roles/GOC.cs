using AutoEvent;
using AutoEvent_5KMode.API;
using AutoEvent_5KMode.Main;
using Exiled.API.Features;
using HintServiceMeow.UI.Extension;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Extensions;

namespace AutoEvent_5KMode.Roles
{
    public class GOC
    {
        public static bool CanStartMRG = false;
        public static void Spawn(Player player)
        {
            player.AddSpecialRole(PlayerExtension.SpecialRolesName.GOC);
            player.GiveLoadout(MainGame._5KMode.Plugin.StaticConfig.GOCLoadout);
            AudioPlayer audioPlayer = AudioPlayer.CreateOrGet("goc-Join");
            string gocmusic = Path.Combine(ConfigPath.MusicPath, "gocjoin.ogg");
            audioPlayer.AddClip(gocmusic, 50, false, true);
            Cassie.MessageTranslated("", MainGame._5KMode.Plugin.G5K.Translation.GOCJoinCASSIE);
            player.Position = RoleTypeId.ChaosMarauder.GetRandomSpawnLocation().Position;
            player.GetPlayerUi().CommonHint.ShowRoleHint("Goc评估小组", "你们与基金会为敌, 到广播室把基金会灭绝人类的资料上传至ganzir，上传之后启动MRG召唤以太风暴", 30);
        }
    }
}
