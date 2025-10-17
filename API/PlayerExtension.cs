using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API
{
    public static class PlayerExtension
    {
        public static Dictionary<Player, SpecialRolesName> PlayerSpecial = new Dictionary<Player, SpecialRolesName>();
        public static void AddSpecialRole(this Player player, SpecialRolesName name)
        {
            if (PlayerSpecial.ContainsKey(player))
            {
                PlayerSpecial[player] = name;
            }
            PlayerSpecial.Add(player, name);
        }
        public static bool IsSpecialRole(this Player player, SpecialRolesName name)
        {
            if (!PlayerSpecial.TryGetValue(player, out var special))
                return false;
            if (PlayerSpecial[player] != special)
                return false;
            return true;
        }
        public static bool RemoveSpecialRole(this Player player)
        {
            return PlayerSpecial.Remove(player);
        }
        public enum SpecialRolesName
        {
            GOC,
            Nu22,
            Scp1440,
            PTECN3201,
            GOC3201
        }
    }
}
