using AutoEvent;
using AutoEvent_5KMode.API;
using Exiled.API.Enums;
using Exiled.API.Features;
using HintServiceMeow.UI.Extension;
using MEC;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Extensions;

namespace AutoEvent_5KMode.Roles
{
    public class SCP1440
    {
        public static bool IsAny = false;
        public static void Spawn(Player player)
        {
            player.AddSpecialRole(PlayerExtension.SpecialRolesName.Scp1440);
            StarAPI.SetPlayerRank(player, "SCP1440","red");
            player.GetPlayerUi().CommonHint.ShowRoleHint("<color=red>SCP1440</color>", "你是[SCP-1440]无法拾取武器BUT,10米内的其他玩家会受到你的影响", 30);
            IsAny = true;
            Main.Plugin.Instance.NBCoroutine = Timing.RunCoroutine(YINXIANG(player));
            Cassie.MessageTranslated("", MainGame._5KMode.Plugin.G5K.Translation.SCP1440Joincassie);
            player.GiveLoadout(MainGame._5KMode.Plugin.StaticConfig.SCP1440);
            player.Position = RoleTypeId.Scientist.GetRandomSpawnLocation().Position;
        }
        private static IEnumerator<float> YINXIANG(Player Attacker)
        {
            while(true)
            {
                List<Player> players = Player.List.Where(x => Vector3.Distance(Attacker.Position, x.Position) <= 10&&x!=Attacker).ToList();
                int R = new System.Random().Next(1, 5);
                if (R == 1)
                {
                    foreach (Player p in players)
                    {
                        int H = new System.Random().Next(20, 80);
                        Hurting(p, H);
                    }
                }
                else if (R == 2)
                {
                    HEIPING();
                }
                else if (R== 3)
                {
                    foreach (Player p in players)
                    {
                        BadEffect(p);
                    }
                }
                else
                {
                    foreach (Player p in players)
                    {
                        p.Hurt(p.Health/2);
                    }
                }
                yield return Timing.WaitForSeconds(30f);
            }
        }
        public static void HEIPING()
        {
            if (IsAny)
            {
                Map.ChangeLightsColor(UnityEngine.Color.black);
                foreach (Player player in Player.List.Where(x => !x.IsSpecialRole(PlayerExtension.SpecialRolesName.Nu22)))
                {
                    player.EnableEffect(EffectType.Blinded);
                    player.GetPlayerUi().CommonHint.ShowOtherHint("你感觉头有点晕, 并且设施停电了", 10);
                }
                Timing.CallDelayed(10f, () =>
                {
                    Map.ResetLightsColor();
                });
            }
        }
        public static void Hurting(Player Target, int ammo)
        {
            if (IsAny)
            {
                if (!Target.IsSpecialRole(PlayerExtension.SpecialRolesName.Nu22))
                {
                    Target.Hurt(ammo);
                    Target.ShowHint("你感觉身体一股疼痛", 10);
                }
            }
        }
        public static void BadEffect(Player Target)
        { 
            List<EffectType> effectTypes = new List<EffectType>().Where(x => x.OutCategory() == EffectCategory.Negative).ToList();
            if (!Target.IsSpecialRole(PlayerExtension.SpecialRolesName.Nu22))
            {
                Target.EnableEffect(effectTypes.GetRandomItem());
            }
        }
    }
}
