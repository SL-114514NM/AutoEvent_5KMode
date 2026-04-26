using HarmonyLib;
using LabApi.Features.Wrappers;
using NorthwoodLib.Pools;
using PlayerRoles.FirstPersonControl.NetworkMessages;
using PlayerRoles.Visibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API.Patches
{
    [HarmonyPatch(typeof(FpcServerPositionDistributor))]
    public class GostVisiblePatch
    {
        [HarmonyPatch(nameof(FpcServerPositionDistributor.WriteAll))]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            int validateCallIndex = newInstructions.FindIndex(
                instruction => instruction.Calls(AccessTools.Method(typeof(VisibilityController), nameof(VisibilityController.ValidateVisibility))));
            if (validateCallIndex == -1)
            {
                foreach (var instruction in newInstructions)
                    yield return instruction;
                ListPool<CodeInstruction>.Shared.Return(newInstructions);
                yield break;
            }
            const int offset = 6;
            int insertIndex = validateCallIndex + offset;
            if (insertIndex >= newInstructions.Count)
            {
                insertIndex = newInstructions.Count - 1;
            }
            var handleGhostModeMethod = AccessTools.Method(typeof(GostVisiblePatch), nameof(ShouldBeInvisible),
                new[] { typeof(ReferenceHub), typeof(ReferenceHub), typeof(bool).MakeByRefType() });
            newInstructions.InsertRange(insertIndex, new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldloc_S, (byte)5),
                new CodeInstruction(OpCodes.Ldloca_S, (byte)7),
                new CodeInstruction(OpCodes.Call, handleGhostModeMethod),
            });
            for (int i = 0; i < newInstructions.Count; i++)
                yield return newInstructions[i];
            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
        private static bool ShouldBeInvisible(ReferenceHub hubReceiver, ReferenceHub hubTarget, ref bool isInvisible)
        {
            if (SAPI.VisibilityPlayers.Contains(Player.Get(hubTarget)))
            {
                isInvisible = true;
                return true;
            }
            isInvisible = false;
            return false;
        }
    }
}
