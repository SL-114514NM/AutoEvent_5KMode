using AutoEvent_5KMode.API;
using AutoEvent_5KMode.Main;
using Exiled.API.Extensions;
using Exiled.API.Features;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace AutoEvent_5KMode.MainGame._5KMode
{
    public class Plugin : AutoEvent.Interfaces.Event
    {
        public override string Name { get; set; } = "SCP:5K";
        public override string Description { get; set; } = "SCP:5K";
        public override string Author { get; set; } = "HUI";
        public override string CommandName { get; set; } = "5K";

        protected override bool IsRoundDone()
        {
            return IsFisish();
        }
        protected override void OnFinished()
        {
            foreach (Player player in Player.List)
            {
                player.Role.Set(PlayerRoles.RoleTypeId.ClassD);
                player.Position = RoleTypeId.Tutorial.GetRandomSpawnLocation().Position;
            }
        }

        protected override void OnStart()
        {
            API.VoiceDummy.Base Base = API.VoiceDummy.Base.Create("广播紧急通知");
            Base.Play(ConfigPath.MusicPath+"/StartBC.ogg", false, true);
            foreach(Player player in Player.List)
            {
                Hint hint = new Hint()
                { 
                    YCoordinate = 100,
                    Alignment = HintServiceMeow.Core.Enum.HintAlignment.Center,
                    AutoText = a =>
                    {
                        string Text;
                        if (IsFisish())
                        {
                            Text = "";
                        }
                        else
                        {
                            Text = "[<color=red>SCP-5000</color>]|[<color=green>为什么?</color>]";
                            Text += "\n>>借助GOC或其他的力量逃离这里<<";
                            Text += $"\n人类数量[{Player.List.Where(x => x.IsNTF&&x.IsCHI&&x.Role.Type == RoleTypeId.Scientist).Count()}]|Scp数量[{Player.List.Where(x => x.IsScp).Count()}]";
                        }
                        return Text;
                    }
                };
                player.AddHint(hint);
            }
            Cassie.MessageTranslated("For those who are still unaware of our existence, we represent an organization called the SXP Foundation", "那些目前仍未注意到我们的存在的人，我们代表着一个称作 SXP 基金会的组织");
            Cassie.MessageTranslated("Our previous missions revolved around containing and studying anomalous objects, entities, and various other phenomena", "我们之前的任务都是围绕着收容与研究异常物体、实体以及其它各种各样的现象展开的");
            Cassie.MessageTranslated("These tasks have been the focus of our organization for hundreds of years", "上百年以来，这些任务一直都是我们组织的工作重点");
            Cassie.MessageTranslated("This directive has now changed due to circumstances beyond our control", "由于出现了超出我们控制的情况，此指令现已更改");
            Cassie.MessageTranslated("Our new mission will be", "我们的新任务将为----");
            Cassie.MessageTranslated("Eliminate all humanity", "<color=red>灭除全部人类</color>");
            List<Player> FRoles = StarAPI.SelectHalfOfPlayers();
            List<Player> Then = StarAPI.SelectHalfOfPlayers();
            List<Player> SCPS = StarAPI.SelectHalfOfPlayers(FRoles);
            List<Player> Choas = StarAPI.SelectHalfOfPlayers(FRoles);
            foreach (var DD in StarAPI.SelectHalfOfPlayers(Then))
            {
                DD.Role.Set(RoleTypeId.ClassD);
            }
            foreach (var Scien in StarAPI.SelectHalfOfPlayers(Then))
            {
                Scien.Role.Set(RoleTypeId.Scientist);
            }
            foreach (var p in SCPS)
            {
                p.Role.Set(StarAPI.GetRandomSCP());
            }
            foreach (var p in Choas)
            {
                p.Role.Set(StarAPI.GetRandomChoas());
            }
            foreach (Player player in FRoles)
            {
                player.Role.Set(RoleTypeId.FacilityGuard);
            }
        }
        public bool IsFisish()
        {
            if (PlayerExtension.PlayerSpecial.Any())
                return false;
            if (Player.List.Any(x => x.IsAlive))
                return false;
            return true;
        }
    }
}
