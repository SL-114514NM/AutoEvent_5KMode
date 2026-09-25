using AdminToys;
using AutoEvent_5KMode.API.Featrues.GameObjectScripts;
using HarmonyLib;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AutoEvent_5KMode.API.Featrues.Patches
{
    [HarmonyPatch(typeof(FirstPersonMovementModule))]
    public class WayPointPatch
    {
        [HarmonyPatch("OnWaypointMoved")]
        [HarmonyPrefix]
        public static bool Prefix(Transform transform, Vector3 deltaPos, Quaternion deltaRot, FirstPersonMovementModule __instance)
        {
            ReferenceHub.TryGetHub(__instance.gameObject, out ReferenceHub hub);
            if (hub == null) return true;
            if (transform == null) return false;
            if (!transform.TryGetComponent<SpecifiWaypoint>(out SpecifiWaypoint component)) return true;
            if (!component.AllowedHubs.Contains(hub)) return true;
            return false;
        }
    }
}
