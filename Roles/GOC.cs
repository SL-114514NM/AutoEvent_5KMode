using AutoEvent_5KMode.API;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
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
        }
    }
}
