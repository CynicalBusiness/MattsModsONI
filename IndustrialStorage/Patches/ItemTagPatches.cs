using Harmony;
using UnityEngine;

namespace MattsMods.IndustrialStorage.Patches
{
    public static class ItemTagPatches
    {
        [HarmonyPatch(typeof(GeneShufflerRechargeConfig), "CreatePrefab")]
        static class Patch_GeneShufflerRechargeConfig_CreatePrefab
        {
            public static void Postfix (GameObject __result)
            {
                __result.AddTag(GameTags.MiscPickupable);
            }
        }
    }
}
