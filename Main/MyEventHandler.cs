using AutoEvent_5KMode.API;
using HintServiceMeow.Core.Models.Hints;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Main
{
    public class MyEventHandler:CustomEventsHandler
    {
        public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
        {
            PlayerHardLineText.AddNew(ev.Player, "");
            PackHints(ev.Player);
            base.OnPlayerJoined(ev);
        }
        public override void OnPlayerLeft(PlayerLeftEventArgs ev)
        {
            if(PlayerHardLineText.ToysByPlayer.ContainsKey(ev.Player.PlayerId))
            {
                PlayerHardLineText.ToysByPlayer.Remove(ev.Player.PlayerId);
            }
            base.OnPlayerLeft(ev);
        }
        public void PackHints(Player player)
        {
            Hint RoleHint = new Hint()
            {
                YCoordinate = 860,
                Alignment = HintServiceMeow.Core.Enum.HintAlignment.Center,
                FontSize = 10,
                AutoText = awa =>
                {
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.SCP181))
                    {
                        return "你是[SCP-181]\n你的运气很好，可以概率开启一个门或柜子";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.GOC间谍))
                    {
                        return "你是[GOC-间谍]\n到达广播室可召唤GOC部队";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.SCP1440))
                    {
                        return "你是[SCP-1440]\n靠近你的人会有不好的事发生";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.GOC学者))
                    {
                        return "你是[<color=blue>GOC-学者</color>]\n在GOC部队的协助下到达设施的四个地方回答学术问题获得激活RGM的密钥然后激活RGM撤离";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.GOC奇术打击二组组长))
                    {
                        return "你是[<color=blue>奇术打击二组-组长</color>]\n你必须和你的组员门掩护GOC学者获取激活RGM的密钥激活它然后撤离\n<color=green>技能</color>一: 在你的前面生成一个巨大的电离子盾,CD90秒\n<color=blue>技能</color>二: 使你和组员们获得反重力效果";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.GOC奇术打击二组组长))
                    {
                        return "你是[<color=blue>奇术打击二组-组员</color>]\n你拥有较快得速度";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.CIGRU特工))
                    {
                        return "你是[<color=green>CIGRU-特工</color>]\n到达广播室向GRU大部队发送站点坐标使他们到来";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.CIGRU队长))
                    {
                        return "你是[<color=green>CIGRU-队长</color>]\n不惜一切代价找到SCP基金会那群狗日的发疯的原因,并和GOC团队一起毁了这里!";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.CIGRU士兵))
                    {
                        return "你是[<color=green>CIGRU-队员</color>]\n不惜一切代价找到SCP基金会那群狗日的发疯的原因,并和GOC团队一起毁了这里!";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.CIGRU重盔甲兵))
                    {
                        return "你是[<color=green>CIGRU-重盔甲兵</color>]\n不惜一切代价找到SCP基金会那群狗日的发疯的原因,并和GOC团队一起毁了这里!\n你拥有强大的防御力,并拥有一个可以防御一切的盾牌";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.SCP550))
                    {
                        return "你是[<color=red>SCP-550</color>]\n基金会将你释放，而你只有一个目标: 杀死所有人\n<color=red>技能一</color>: 使自身回复30滴血，增加10%的移速\n<color=red>技能二</color>: 使附近5M内的人受到<color=red>压迫</color>效果持续扣血";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.SCP682))
                    {
                        return "你是[<color=red>SCP-682</color>]\n你拥有较快的移速，强大的杀伤力，贼厚的血并拥有4次的复活机会";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.SCP0082))
                    {
                        return "你是[<color=red>SCP008-2</color>]\n你伤害的人会沦落到和你一样的下场";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.Nu7落锤肃杀B连连长))
                    {
                        return "你是[<color=red>Nu-7肃杀B连连长</color>]\n依照O5议会的指令，你们的任务是无效化该设施\n技能一: 使全体肃杀B连成员获得速度加成和回血效果";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.Nu7落锤肃杀B连士兵))
                    {
                        return "你是[<color=red>Nu-7肃杀B连士兵</color>]\n依照O5议会的指令，你们的任务是无效化该设施\n你拥有高伤害高回血，免疫掉落伤害";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.Nu22队长))
                    {
                        return "你是[<color=yellow>Nu-22队长</color>]\n你们的任务为保护SCP-1440并无效化该设施";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.Nu22中士))
                    {
                        return "你是[<color=yellow>Nu-22中士</color>]\n你们的任务为保护SCP-1440并无效化该设施\n你的枪可以为SCP-1440回血, 你也拥有非常高的伤害";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.Nu22下士))
                    {
                        return "你是[<color=yellow>Nu-22中士</color>]\n你们的任务为保护SCP-1440并无效化该设施\n你拥有非常高的伤害";
                    }
                    if(RoleManager.CheckRole(player, RoleManager.RoleName.UIU特遣))
                    {
                        return "你是[<color=blue>UIU特遣队员</color>]\n你们需要黑入设施的中心控制中心以得到“<color=green>真相</color>”";
                    }
                    return "no";
                }
            };
            Hint ItemHint = new Hint()
            {
                Alignment = HintServiceMeow.Core.Enum.HintAlignment.Center,
                YCoordinate = 760,
                AutoText =awa =>
                {
                    if(ItemManager.CheckItem(player.CurrentItem, ItemManager.SpecifiItem.斯克兰顿现实稳定锚))
                    {
                        return "[<color=blue>斯克兰顿现实稳定锚</color>]\n使用后，会使周围5m内的物体无法移动,维持20s";
                    }
                    if(ItemManager.CheckItem(player.CurrentItem, ItemManager.SpecifiItem.火箭筒))
                    {
                        return "[<color=green>火箭筒</color>]\n使用时会发送火箭弹";
                    }
                    if(ItemManager.CheckItem(player.CurrentItem, ItemManager.SpecifiItem.M110大狙))
                    {
                        return "<color=blue>M110-Gun</color>\n共5发子弹，高伤害";
                    }
                    if(ItemManager.CheckItem(player.CurrentItem, ItemManager.SpecifiItem.OmegaKeycard))
                    {
                        return "<color=gray>Omega授权卡</color>\n到达GATE-C的Omega核弹授权室进行授权后到达地底核弹室开启Omega核弹";
                    }
                    if(ItemManager.CheckItem(player.CurrentItem, ItemManager.SpecifiItem.轨道炮))
                    {
                        return "<color=blue>轨道炮</color>\n会发送一条轨道激光到达尽头会爆炸";
                    }
                    if(ItemManager.CheckItem(player.CurrentItem, ItemManager.SpecifiItem.炮台放置器))
                    {
                        return "<color=blue>炮台放置器</color>]\n可以在指定位置放置自动射击炮台";
                    }
                    return "i";
                }
            };
        }
    }
}
