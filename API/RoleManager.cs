using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API
{
    public class RoleManager
    {
        public static Dictionary<int, RoleName> RoleByIds = new Dictionary<int, RoleName>();
        public static void Add(int id, RoleName roleName)
        {
            if(RoleByIds.ContainsKey(id))
            {
                RoleByIds[id] = roleName;
                return;
            }
            RoleByIds.Add(id, roleName);
        }
        public static void Add(Player player,RoleName roleName)
        {
            Add(player.PlayerId, roleName);
        }
        public static bool CheckRole(int id)
        {
            if (RoleByIds.ContainsKey(id))
                return true;
            return false;
        }
        public static bool CheckRole(int id, RoleName roleName)
        {
            if(CheckRole(id))
            {
                if (RoleByIds[id] == roleName)
                    return true;
            }
            return false;
        }
        public static bool CheckRole(Player player)
        {
            return CheckRole(player.PlayerId);
        }
        public static bool CheckRole(Player player, RoleName roleName)
        {
            return CheckRole(player.PlayerId, roleName);
        }
        public static void Remove(int id)
        {
            RoleByIds.Remove(id);
        }
        public static void Remove(Player player)
        {
            Remove(player.PlayerId);
        }
        public enum RoleName
        {
            Nu22队长,
            Nu22中士,
            Nu22下士,
            SCP1440,
            CIGRU队长,
            CIGRU士兵,
            CIGRU特工,
            CIGRU重盔甲兵,
            SCP682,
            SCP008母体,
            SCP008感染者,
            GOC间谍,
            GOC破译员,
            GOC奇术打击二组组长,
            GOC奇术打击二组组员,
            SCP181,
            SCP550,
            UIU特遣,
            Nu7落锤肃杀B连连长,
            Nu7落锤肃杀B连士兵,

        }
    }
}
