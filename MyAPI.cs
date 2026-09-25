using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using LabApi.Features.Enums;
using LabApi.Features.Extensions;
using LabApi.Features.Wrappers;
using MEC;
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
        public static List<Room> SecledRooms = new List<Room>();
        public static Room GetRandomRoom()
        {
            List<Room> newlist = Room.List.Where(x =>!SecledRooms.Contains(x)).ToList();
            int nr = Random.Next(0, newlist.Count);
            Room room = newlist[nr];
            SecledRooms.Add(room);
            Timing.CallDelayed(3, () =>
            {
                SecledRooms.Remove(room);
            });
            return room;
        }
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
        /// <summary>
        /// 给玩家发送打字机提示
        /// </summary>
        /// <param name="player"></param>
        /// <param name="Ypos"></param>
        /// <param name="hintAlignment"></param>
        /// <param name="texts"></param>
        /// <param name="dtime">每条内容的间隔时长</param>
        public static void SendPlayerTimeTypeHint(Player player, float Ypos, HintAlignment hintAlignment, List<string> texts, float dtime)
        {
            int totalChars = 0;
            foreach (string text in texts)
            {
                totalChars += text.Length;
            }
            float totalTime = totalChars * dtime;
            int textIndex = 0;      
            int charIndex = 0;     
            string currentText = "";
            Hint hint = new Hint()
            {
                YCoordinate = Ypos,
                Alignment = hintAlignment,
                AutoText = at =>
                {
                    if (textIndex >= texts.Count)
                    {
                        at.NextUpdateDelay = TimeSpan.FromMilliseconds(-1);
                        return currentText;
                    }
                    string currentLine = texts[textIndex];
                    if (charIndex < currentLine.Length)
                    {
                        currentText += currentLine[charIndex];
                        charIndex++;
                        at.NextUpdateDelay = TimeSpan.FromSeconds(dtime);
                        return currentText;
                    }
                    else
                    {
                        textIndex++;
                        charIndex = 0;
                        currentText = ""; 

                        if (textIndex < texts.Count)
                        {
                            at.NextUpdateDelay = TimeSpan.FromSeconds(dtime);
                            return currentText;
                        }
                        else
                        {
                            at.NextUpdateDelay = TimeSpan.FromMilliseconds(-1);
                            return currentText;
                        }
                    }
                }
            };
            player.AddHint(hint);
            player.GetPlayerDisplay().RemoveAfter(hint, totalTime + 2);
        }

    }
}
