using AdminToys;
using HarmonyLib;
using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Patches
{
    public class AdminToyPatches
    {
        public static Dictionary<Player, AdminToy> KanSeeToyPlayer = new Dictionary<Player, AdminToy>();
        public static void Spawn(AdminToy adminToy,Player Owner)
        {
            KanSeeToyPlayer.Add(Owner, adminToy);
        }
        public static void RemoveA(AdminToy adminToy)
        {
            if(KanSeeToyPlayer.ContainsValue(adminToy))
            {
                Player player = KanSeeToyPlayer.FirstOrDefault(x => x.Value == adminToy).Key;
                KanSeeToyPlayer.Remove(player);
            }
        }
        [HarmonyPatch(typeof(AdminToyBase),nameof(AdminToyBase.NetworkPosition),MethodType.Getter)]
        public class PosGetterFix
        {
            [HarmonyPrefix]
            public static bool Prefix(AdminToyBase __instance, ref Vector3 NetworkPosition)
            {
                if(KanSeeToyPlayer.ContainsValue(AdminToy.Get(__instance)))
                {
                    Player player = KanSeeToyPlayer.FirstOrDefault(x => x.Value == AdminToy.Get(__instance)).Key;
                    if(ItemManager.CheckItem(player.CurrentItem, ItemManager.SpecifiItem.炮台放置器))
                    {
                        NetworkPosition = player.Position + player.Camera.forward;
                    }
                    NetworkPosition = player.Position + Vector3.up * 2;
                    return false;
                }
                return true;
            }
        }
    }
}
