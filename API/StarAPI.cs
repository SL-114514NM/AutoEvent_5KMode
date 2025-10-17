using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using PlayerRoles;
using PlayerRoles.Blood;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API
{
    public class StarAPI
    {
        public static List<Player> SelectPlayer = new List<Player>();
        public static Dictionary<Player, string> PlayerRanks = new Dictionary<Player, string>();
        public static bool SetPlayerRank(Player player,string rank, string Color)
        {
            if (PlayerRanks.ContainsKey(player))
            {
                PlayerRanks[player] = rank;
                player.RankName = $"({PlayerRanks[player]}){player.RankName}";
                player.RankColor = Color;
                return true;
            }
            PlayerRanks.Add(player, rank);
            player.RankName = $"({PlayerRanks[player]}){player.RankName}";
            player.RankColor = Color;
            return true;
        }
        public static bool RemoveRank(Player player)
        {
            player.RankColor = "white";
            player.RankName = $"{player.RankName}";
            return PlayerRanks.Remove(player);
        }
        /// <summary>
        /// 从指定的列表里随机一个玩家
        /// </summary>
        /// <param name="ts">列表</param>
        /// <returns></returns>
        public static Player GetRandomPlayer(List<Player> ts)
        {
            Player player = ts.RandomItem();
            if (SelectPlayer.Contains(player))
            {
                return null;
            }
            return player;
        }
        /// <summary>
        /// 从所有玩家中选择一半的玩家并返回列表
        /// </summary>
        public static List<Player> SelectHalfOfPlayers()
        {
            List<Player> allPlayers = Player.List.ToList();
            if (allPlayers.Count == 0)
                return new List<Player>();
            int halfCount = (int)System.Math.Round(allPlayers.Count / 2f);
            return SelectRandomPlayers(allPlayers, halfCount);
        }
        /// <summary>
        /// 从指定玩家列表中选择一半的玩家
        /// </summary>
        public static List<Player> SelectHalfOfPlayers(List<Player> playerList)
        {
            if (playerList == null || playerList.Count == 0)
                return new List<Player>();

            int halfCount = (int)System.Math.Round(playerList.Count / 2f);
            return SelectRandomPlayers(playerList, halfCount);
        }
        private static List<Player> SelectRandomPlayers(List<Player> sourcePlayers, int count)
        {
            if (count >= sourcePlayers.Count)
                return new List<Player>(sourcePlayers);
            List<Player> shuffledList = new List<Player>(sourcePlayers);
            System.Random random = new System.Random();
            for (int i = shuffledList.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(i + 1);
                Player temp = shuffledList[i];
                shuffledList[i] = shuffledList[randomIndex];
                shuffledList[randomIndex] = temp;
            }
            return shuffledList.Take(count).ToList();
        }
        public static RoleTypeId GetRandomSCP()
        {
            List<RoleTypeId> roleTypeIds = new List<RoleTypeId>().Where(x => x.IsScp()).ToList();
            return roleTypeIds.GetRandomItem();
        }
        public static RoleTypeId GetRandomChoas()
        {
            List<RoleTypeId> roleTypeIds = new List<RoleTypeId>().Where(x => x.IsChaos()).ToList();
            return roleTypeIds.GetRandomItem();
        }
        public static Pickup Get(ushort id)
        {
            return Pickup.List.FirstOrDefault(x => x.Serial == id);
        }
    }
}
