using AutoEvent_5KMode.API;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Roles.GOC
{
    public class Jd
    {
        public static void Spawn(Player player)
        {
            if(RoleManager.CheckRole(player))
            {
                RoleManager.Remove(player);
            }
            RoleManager.Add(player, RoleManager.RoleName.GOC间谍);
            player.SetRole(PlayerRoles.RoleTypeId.ClassD);
            player.AddItem(ItemType.KeycardGuard);
            player.Health = 120;
            player.MaxHealth = 120;
        }
    }
}
