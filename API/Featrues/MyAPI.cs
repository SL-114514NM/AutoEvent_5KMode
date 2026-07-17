using LabApi.Features.Enums;
using LabApi.Features.Extensions;
using LabApi.Features.Wrappers;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UserSettings.ServerSpecific;
using Random = System.Random;

namespace AutoEvent_5KMode.API.Featrues
{
    public static class MyAPI
    {
        public static Random Random = new Random();
        public static List<Player> SecledPlayers = new List<Player>();

        public static Player GetRandomPlayer(List<Player> players)
        {
            List<Player> newlist = players.Where(x => !SecledPlayers.Contains(x)).ToList();
            int RInt = Random.Next(1, newlist.Count);
            Player player = newlist[RInt];
            SecledPlayers.Add(player);
            return player;
        }
        public static List<Player> GetRandomPlayers(List<Player> players,int Count)
        {
            List<Player> newlist = new List<Player>();
            for(int x =0; x < Count; x++)
            {
                newlist.Add(GetRandomPlayer(players));
            }
            return newlist;
        }
        public static Vector3 GetDoorPos(DoorName doorName)
        {
            Door door = Door.Get(doorName);
            if (door == null) return Vector3.zero;
            return door.Position + Vector3.right;
        }
        public static Vector3 GetRolePos(RoleTypeId roleTypeId)
        {
            roleTypeId.TryGetRandomSpawnPoint(out Vector3 pos, out float hr);
            if (pos == null) return Vector3.zero;
            return pos;
        }
        public static void AddItem(this Player player,ItemType itemType, int ammout)
        {
            for(int x =0; x<ammout;x++)
            {
                player.AddItem(itemType);
            }
        }
        public static void AddItem(this Player player, List<ItemType> itemTypes)
        {
            foreach(ItemType itemType in itemTypes)
            {
                player.AddItem(itemType);
            }
        }
        public static ServerSpecificSettingBase GetPlayerSSSBase(this Player player,int id)
        {
            return ServerSpecificSettingsSync.GetSettingOfUser<ServerSpecificSettingBase>(player.ReferenceHub, id);
        }

    }
}
