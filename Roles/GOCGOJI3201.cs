using AutoEvent;
using AutoEvent_5KMode.API;
using AutoEvent_5KMode.API.KeyBind;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using HintServiceMeow.UI.Extension;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Roles
{
    public class GOCGOJI3201
    {
        public static void Spawn3201(Player player)
        {
            player.GiveLoadout(MainGame._5KMode.Plugin.StaticConfig.GOC3201);
            player.Position = RoleTypeId.ChaosConscript.GetRandomSpawnLocation().Position;
        }
        public static void SpawnPTECN3201(Player player)
        {
            player.AddItem(ItemType.Medkit);
            player.Position = RoleTypeId.ChaosConscript.GetRandomSpawnLocation().Position;
            player.GiveLoadout(MainGame._5KMode.Plugin.StaticConfig.PTECN3201Loadout);
            foreach (Player player1 in PlayerExtension.PlayerSpecial.Keys.Where(x => x.IsSpecialRole(PlayerExtension.SpecialRolesName.GOC)&& x.IsSpecialRole(PlayerExtension.SpecialRolesName.PTECN3201)).ToList())
            {
                player1.GetPlayerUi().CommonHint.ShowOtherHint("伙计们，拿好枪，准备干点大的", 10);
            }
            player.GetPlayerUi().CommonHint.ShowRoleHint("PTE-CN-3201|支援组组长|临时\n丢医疗包给附近的队友恢复血量\n按[左ALT]键可以加伤害和血量, CD90秒", "",30);
            SettingBase settingBase = SimpleKeyBind.RegisterAbility(player);
            settingBase.Label = "PTE-CN-3201技能";
        }
    }
}
