using Cassie;
using LabApi.Features.Wrappers;
using MapGeneration;
using PlayerRoles;
using PlayerRoles.FirstPersonControl.Spawnpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API
{
    public static class SAPI
    {
        public static List<Player> VisibilityPlayers = new List<Player>();
        public static void CassieMessage(string message, bool isHeld = false, bool isNoisy = true, bool isSubtitles = false)
        {
            CassieAnnouncementDispatcher.AddToQueue(new CassieAnnouncement(new CassieTtsPayload(message, isSubtitles, isHeld), 0f));
        }
        public static void MessageTranslated(string message, string translation, bool isHeld = false, bool isNoisy = true, bool isSubtitles = true)
        {
            CassieAnnouncementDispatcher.AddToQueue(new CassieAnnouncement(new CassieTtsPayload(message, translation, isHeld), 0f));
        }
        public static Vector3 GetRoleSpawnPosition(RoleTypeId roleTypeId)
        {
            RoleSpawnpointManager.TryGetSpawnpointForRole(roleTypeId, out var spawnpoint);
            spawnpoint.TryGetSpawnpoint(out Vector3 position, out float hr);
            return position;
        }
        public static Player GetRandomPlayer(List<Player> players)
        {
            System.Random random = new System.Random();
            int i = random.Next(players.Count);
            return players[i];
        }
        public static RoomName GetRandomRoom()
        {
            System.Random random = new System.Random();
            int i = random.Next(Room.List.ToList().Count);
            return Room.List.ToList()[i].Name;
        }
        public static string GetBlockProgressBar(int current, int max, int totalCells)
        {
            double percentage = (double)current / max;
            int filledCells = (int)Math.Round(percentage * totalCells);
            filledCells = Math.Max(0, Math.Min(filledCells, totalCells));
            StringBuilder progressBar = new StringBuilder();
            for (int i = 0; i < filledCells; i++)
            {
                progressBar.Append("■");
            }
            for (int i = filledCells; i < totalCells; i++)
            {
                progressBar.Append("□");
            }

            return progressBar.ToString();
        }
        public static Door GetDoor(LabApi.Features.Enums.DoorName roomName)
        {
            return Door.List.FirstOrDefault(x => x.DoorName == roomName);
        }
        public static string GetLastChar(string input)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            return input.Substring(input.Length - 1);
        }
        public static string RoleTypeToChinese(RoleTypeId roleType)
        {
            switch (roleType)
            {
                case RoleTypeId.None:
                    return "无";
                case RoleTypeId.Scp173:
                    return "SCP-173";
                case RoleTypeId.ClassD:
                    return "D级人员";
                case RoleTypeId.Spectator:
                    return "观众";
                case RoleTypeId.Scp106:
                    return "SCP-106";
                case RoleTypeId.NtfSpecialist:
                    return "九尾狐专家";
                case RoleTypeId.Scp049:
                    return "SCP-049";
                case RoleTypeId.Scientist:
                    return "科学家";
                case RoleTypeId.Scp079:
                    return "SCP-079";
                case RoleTypeId.ChaosConscript:
                    return "混沌征召兵";
                case RoleTypeId.Scp096:
                    return "SCP-096";
                case RoleTypeId.Scp0492:
                    return "SCP-049-2";
                case RoleTypeId.NtfSergeant:
                    return "九尾狐中士";
                case RoleTypeId.NtfCaptain:
                    return "九尾狐上尉";
                case RoleTypeId.NtfPrivate:
                    return "九尾狐列兵";
                case RoleTypeId.Tutorial:
                    return "教程角色";
                case RoleTypeId.FacilityGuard:
                    return "设施警卫";
                case RoleTypeId.Scp939:
                    return "SCP-939";
                case RoleTypeId.CustomRole:
                    return "自定义角色";
                case RoleTypeId.ChaosRifleman:
                    return "混沌步枪手";
                case RoleTypeId.ChaosMarauder:
                    return "混沌掠夺者";
                case RoleTypeId.ChaosRepressor:
                    return "混沌压制者";
                case RoleTypeId.Overwatch:
                    return "监管者";
                case RoleTypeId.Filmmaker:
                    return "导演";
                case RoleTypeId.Scp3114:
                    return "SCP-3114";
                case RoleTypeId.Destroyed:
                    return "已摧毁";
                case RoleTypeId.Flamingo:
                    return "火烈鸟";
                case RoleTypeId.AlphaFlamingo:
                    return "Alpha火烈鸟";
                case RoleTypeId.ZombieFlamingo:
                    return "僵尸火烈鸟";
                case RoleTypeId.NtfFlamingo:
                    return "九尾狐火烈鸟";
                case RoleTypeId.ChaosFlamingo:
                    return "混沌火烈鸟";
                default:
                    return "未知角色";
            }
        }
    }

}
