using AutoEvent_5KMode.API.Featrues.GameObjectScripts;
using AutoEvent_5KMode.API.Featrues.MoreProjectObjects;
using HarmonyLib;
using NetworkManagerUtils.Dummies;
using PlayerRoles;
using ProjectMER;
using ProjectMER.Features.Objects;
using ProjectMER.Features.Serializable.Schematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.Patches
{
    [HarmonyPatch(typeof(ProjectMER.Features.ObjectSpawner), "SpawnSchematic",new Type[] { typeof(SerializableSchematic) })]
    public class PMERPatch
    {
        [HarmonyPostfix]
        public static void Postfix(SerializableSchematic serializableSchematic, SchematicObject __result)
        {
            if(serializableSchematic.SchematicName == "gocft")
            {
                __result.gameObject.AddComponent<ReboundGameObject>();
                return;
            }
        }
    }
    [HarmonyPatch(typeof(SchematicBlockData))]
    public class SchematicBlockDataPatch
    {
        [HarmonyPatch("Create")]
        [HarmonyPrefix]
        public static bool Prefix(SchematicObject schematicObject, Transform parentTransform, GameObject __result, SchematicBlockData __instance)
        {
            if (!__instance.Properties.ContainsKey("GameObjectId")) return true;
            if ((int)__instance.Properties["GameObjectId"] != 40) return true;
            GameObject gameObject = CreateDummyPlayer(__instance);
            gameObject.name = __instance.Name;
            gameObject.transform.SetParent(parentTransform);
            gameObject.transform.SetPositionAndRotation(__instance.Position, Quaternion.Euler(__instance.Rotation));
            __result = gameObject;
            return false;
        }
        public static GameObject CreateDummyPlayer(SchematicBlockData schematicBlockData)
        {
            string nickname = (string)schematicBlockData.Properties["DummyNickName"];
            RoleTypeId roleTypeId = (RoleTypeId)schematicBlockData.Properties["DefaultRoleTypeId"];
            MPortjectDummyPlayer mPortjectDummyPlayer = MPortjectDummyPlayer.CreateDummyPlayer(DummyUtils.SpawnDummy(nickname));
            mPortjectDummyPlayer.dummy.roleManager.ServerSetRole(roleTypeId, RoleChangeReason.None);
            return mPortjectDummyPlayer.dummy.gameObject;
        }
    }
}
