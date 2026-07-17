using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace AutoEvent_5KMode.API.Featrues.CustomRole
{
    public abstract class CustomRole
    {
        public abstract string Name { get; set; }
        public abstract int Id { get; set; }
        public abstract RoleTypeId RoleType { get; set; }
        public abstract List<ItemType> ItemTypes { get; set; }
        public abstract float MaxHealthy { get; set; }
        public abstract Vector3 SpawnPosition { get; set; }
        public Dictionary<int, int> PlayerById = new Dictionary<int, int>();
        public virtual void Spawn(Player player)
        {
            player.SetRole(RoleType);
            player.MaxHealth = MaxHealthy;
            player.Health = MaxHealthy;
            player.AddItem(ItemTypes);
            player.Position = SpawnPosition;

        }
        public bool IsRole(int Id, Player player)
        {
            if(!PlayerById.ContainsKey(Id)) return false;
            if (PlayerById[player.PlayerId] ==  Id) return true;
            return false;
        }
        public virtual void Register()
        {
            Logger.Debug($"{Name}注册成功");

        }
        public virtual void Unregister()
        {
            Logger.Debug($"{Name}注销成功");

        }
    }
}
