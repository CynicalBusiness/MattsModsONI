using System;
using System.Reflection.Emit;
using System.Collections.Generic;
using Harmony;

namespace MattsMods.Industrialization.Logic.LogicNetwork
{

    [HarmonyPatch(typeof(UtilityNetwork), MethodType.Constructor, new Type[0])]
    public static class Patch_LogicCircuitNetwork_CTOR
    {

        // we have to expand the `wireGroups` array out to be big enough
        private static void Postfix (UtilityNetwork __instance)
        {
            Debug.Log(__instance.GetType().FullName);
            if (__instance is LogicCircuitNetwork) {
                Debug.LogFormat("Updating logic circuit network wireGroups to length of {0} (via ctor)", Mod.MAX_WIRE_TYPES);
                Traverse.Create(__instance).Field("wireGroups").SetValue(new List<LogicWire>[Mod.MAX_WIRE_TYPES]);
            }
        }
    }

    [HarmonyPatch(typeof(LogicCircuitManager), MethodType.Constructor, new Type[]{ typeof(UtilityNetworkManager<LogicCircuitNetwork, LogicWire>) })]
    public static class Patch_LogicCircuitManager_CTOR
    {
        private static void Postfix (LogicCircuitManager __instance, ref List<LogicUtilityNetworkLink>[] ___bridgeGroups)
        {
            ___bridgeGroups = new List<LogicUtilityNetworkLink>[Mod.MAX_WIRE_TYPES];
            for (var i = 0; i < ___bridgeGroups.Length; i++)
            {
                ___bridgeGroups[i] = new List<LogicUtilityNetworkLink>();
            }
        }
    }

    [HarmonyPatch(typeof(LogicCircuitNetwork), "WireCount", MethodType.Getter)]
    public static class Patch_LogicCircuitNetwork_WireCount
    {
        // replace this getter so it respects our wire type...
        public static bool Prefix (ref int __result, ref List<LogicWire>[] ___wireGroups)
        {
            var num = 0;
            foreach (var wireList in ___wireGroups)
            {
                if (wireList != null) num += 1;
            }
            __result = num;
            return false;
        }
    }

    [HarmonyPatch(typeof(LogicCircuitNetwork), "GetBitsUsed")]
    public static class Patch_LogicCircuitNetwork_GetBitsUsed
    {
        // in order to have all 32 bits, we need to replace this method to support it
        private static bool Prefix (ref int __result, int ___outputValue)
        {
            if (___outputValue > 4)
            {
                __result = Mod.MAX_WIRE_BIT_DEPTH;
                return false;
            }
            else return true;
        }
    }

    [HarmonyPatch(typeof(LogicCircuitNetwork), "Reset")]
    public static class Patch_LogicCircuitNetwork_Reset
    {

        // we have to pick that annoying hard-coded `index < 2` out of some for loops
        internal static IEnumerable<CodeInstruction> TranspileHardcodedIndex (IEnumerable<CodeInstruction> instructions, CodeInstruction var, OpCode val)
        {
            var instructionList = new List<CodeInstruction>(instructions);


            for (var i = 0; i < instructionList.Count; i++)
            {
                if (i >= 2
                    && instructionList[i - 2].opcode == var.opcode && instructionList[i - 2].operand == var.operand
                    && instructionList[i - 1].opcode == val
                    && instructionList[i].opcode == OpCodes.Clt)
                {
                    // we found it
                    instructionList[i - 1].opcode = OpCodes.Ldc_I4_S;
                    instructionList[i - 1].operand = Mod.MAX_WIRE_TYPES;
                    break;
                }
            }

            return instructionList;
        }

        internal static void UpdateWireGroupsLength (ref List<LogicWire>[] wireGroups, string via)
        {
            if (wireGroups.Length < Mod.MAX_WIRE_TYPES)
            {
                #if DEBUG
                    Debug.LogFormat("Updated logic network wire groups {0}->{1} (via {2})", wireGroups.Length, Mod.MAX_WIRE_TYPES, via);
                #endif

                wireGroups = PipLib.PLUtil.ArrayConcat(wireGroups, new List<LogicWire>[Mod.MAX_WIRE_TYPES - wireGroups.Length]);
            }
        }

        internal static void UpdateRelevantBridgesLength (ref List<LogicUtilityNetworkLink>[] relevantBridges, string via)
        {
            if (relevantBridges.Length < Mod.MAX_RELEVANT_BRIDGES)
            {
                #if DEBUG
                    Debug.LogFormat("Updated logic network wire groups {0}->{1} (via {2})", relevantBridges.Length, Mod.MAX_RELEVANT_BRIDGES, via);
                #endif

                relevantBridges = PipLib.PLUtil.ArrayConcat(relevantBridges, new List<LogicUtilityNetworkLink>[Mod.MAX_RELEVANT_BRIDGES - relevantBridges.Length]);
            }
        }

        private static void Prefix (ref List<LogicWire>[] ___wireGroups, ref List<LogicUtilityNetworkLink>[] ___relevantBridges)
        {
            UpdateRelevantBridgesLength(ref ___relevantBridges, "Reset");
            UpdateWireGroupsLength(ref ___wireGroups, "Reset");
        }

        private static IEnumerable<CodeInstruction> Transpiler (IEnumerable<CodeInstruction> instructions)
        {
            return TranspileHardcodedIndex(instructions, new CodeInstruction(OpCodes.Ldloc_0), OpCodes.Ldc_I4_2);
        }
    }

    /*
    [HarmonyPatch(typeof(LogicCircuitNetwork), "GetNetworkBitDepth")]
    public static class Patch_LogicCircuitNetwork_GetNetworkBitDepth
    {
        private static void Prefix (ref List<LogicWire>[] ___wireGroups)
        {
            Patch_LogicCircuitNetwork_Reset.UpdateWireGroupsLength(ref ___wireGroups, "GetNetworkBitDepth");
        }

        private static IEnumerable<CodeInstruction> Transpiler (IEnumerable<CodeInstruction> instructions)
        {
            return Patch_LogicCircuitNetwork_Reset.TranspileHardcodedIndex(instructions, new CodeInstruction(OpCodes.Ldloc_1), OpCodes.Ldc_I4_2);
        }
    }
    */

    [HarmonyPatch(typeof(LogicCircuitNetwork), "TriggerAudio")]
    public static class Patch_LogicCircuitNetwork_TriggerAudio
    {
        private static void Prefix (ref List<LogicWire>[] ___wireGroups, ref List<LogicUtilityNetworkLink>[] ___relevantBridges)
        {
            Patch_LogicCircuitNetwork_Reset.UpdateRelevantBridgesLength(ref ___relevantBridges, "TriggerAudio");
            Patch_LogicCircuitNetwork_Reset.UpdateWireGroupsLength(ref ___wireGroups, "TriggerAudio");
        }

        private static IEnumerable<CodeInstruction> Transpiler (IEnumerable<CodeInstruction> instructions)
        {
            return Patch_LogicCircuitNetwork_Reset.TranspileHardcodedIndex(instructions, new CodeInstruction(OpCodes.Ldloc_S, "index1"), OpCodes.Ldc_I4_2);
        }
    }

    [HarmonyPatch(typeof(LogicCircuitNetwork), "UpdateOverloadTime")]
    public static class Patch_LogicCircuitNetwork_UpdateOverloadTime
    {
        private static void Prefix (ref List<LogicWire>[] ___wireGroups, ref List<LogicUtilityNetworkLink>[] ___relevantBridges)
        {
            Patch_LogicCircuitNetwork_Reset.UpdateRelevantBridgesLength(ref ___relevantBridges, "UpdateOverloadTime");
            Patch_LogicCircuitNetwork_Reset.UpdateWireGroupsLength(ref ___wireGroups, "UpdateOverloadTime");
        }

        private static IEnumerable<CodeInstruction> Transpiler (IEnumerable<CodeInstruction> instructions)
        {
            return Patch_LogicCircuitNetwork_Reset.TranspileHardcodedIndex(instructions, new CodeInstruction(OpCodes.Ldloc_3), OpCodes.Ldc_I4_2);
        }
    }

    [HarmonyPatch(typeof(LogicCircuitNetwork), "AddItem")]
    public static class Patch_LogicCircuitNetwork_AddItem
    {
        private static void Prefix (ref List<LogicWire>[] ___wireGroups, ref List<LogicUtilityNetworkLink>[] ___relevantBridges)
        {
            Patch_LogicCircuitNetwork_Reset.UpdateRelevantBridgesLength(ref ___relevantBridges, "AddItem");
            Patch_LogicCircuitNetwork_Reset.UpdateWireGroupsLength(ref ___wireGroups, "AddItem");
        }
    }


    [HarmonyPatch(typeof(LogicWire), "GetBitDepthAsInt")]
    public static class Patch_LogicWire_GetBitDepthAsInt
    {
        // the bit depth enum does not support bigger things, so here we are
        private static bool Prefix(ref int __result, LogicWire.BitDepth rating)
        {
            if ((int)rating == 3)
            {
                __result = Mod.MAX_WIRE_BIT_DEPTH;
                return false;
            }
            else return true;
        }
    }

}
