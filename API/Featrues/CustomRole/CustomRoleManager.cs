using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Featrues.CustomRole
{
    public class CustomRoleManager
    {
        public static List<CustomRole> CustomRoleList = new List<CustomRole>();

        public static void RegisterAll()
        {
            foreach(CustomRole role in CustomRoleList)
            {
                role.Register();
            }
        }
        public static void UnRegisterAll()
        {
            foreach(CustomRole role in CustomRoleList)
            {
                role.Unregister();
            }
        }
        public static CustomRole GetCustomRole(int Id)
        {
            CustomRole role = CustomRoleList.FirstOrDefault(x => x.Id == Id);
            return role;
        }
        public static List<CustomRole> CheckAndCreateInstance()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            List<CustomRole> newlist = new List<CustomRole>();
            foreach (Type type in types)
            {
                if(type.IsAbstract&&type.GetInterfaces().Any(x => x == typeof(CustomRole)))
                {
                    CustomRole customRole = (CustomRole)Activator.CreateInstance(type);
                    newlist.Add(customRole);
                }
            }
            if(CustomRoleList.Count<=0)
            {
                CustomRoleList = newlist;
            }
            else
            {
                foreach(CustomRole customRole in newlist)
                {
                    AddInstance(customRole);
                }
            }
            return newlist;
        }
        public static List<CustomRole> GetCustomRoleFromOtherDLL(string filepath)
        {
            Type[] types = Assembly.LoadFrom(filepath).GetTypes();
            List<CustomRole> newlist = new List<CustomRole>();
            foreach (Type type in types)
            {
                if (type.IsAbstract && type.GetInterfaces().Any(x => x == typeof(CustomRole)))
                {
                    CustomRole customRole = (CustomRole)Activator.CreateInstance(type);
                    newlist.Add(customRole);
                }
            }
            return newlist;
        }
        public static void AddInstance(CustomRole role)
        {
            if (CustomRoleList.Any(x => x.Id == role.Id))
            {
                return;
            }
            if (CustomRoleList.Contains(role))
            {
                return;
            }
            CustomRoleList.Add(role);
        }
        public static void RemoveInstance(CustomRole role)
        {
            if (!CustomRoleList.Any(x => x.Id == role.Id))
            {
                return;
            }
            if (!CustomRoleList.Contains(role))
            {
                return;
            }
            CustomRoleList.Remove(role);
        }
    }
}
