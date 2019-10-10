using Harmony;
using UnityEngine;

namespace MattsMods.AdjustableTransformers
{
    public static class PowerTransformerPatches
    {
        public static void OnLoad ()
        {
            LocString.CreateLocStringKeys(typeof(MattsMods.AdjustableTransformers.STRINGS.UI));
        }

        // === Large Power Transformer
        [HarmonyPatch(typeof(PowerTransformerConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        private static class Patch_PowerTransformerConfig_CreateBuildingDef
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.GeneratorBaseCapacity = Wire.GetMaxWattageAsFloat(Wire.WattageRating.Max20000);
                __result.GeneratorWattageRating = __result.GeneratorBaseCapacity;
            }
        }

        [HarmonyPatch(typeof(PowerTransformerConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        private static class Patch_PowerTransformerConfig_ConfigureBuildingTemplate
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<PowerTransformerAdjustable>();
            }
        }

        // === Small Power Transformer
        [HarmonyPatch(typeof(PowerTransformerSmallConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        private static class Patch_PowerTransformerSmallConfig_CreateBuildingDef
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.GeneratorBaseCapacity = Wire.GetMaxWattageAsFloat(Wire.WattageRating.Max1000);
                __result.GeneratorWattageRating = __result.GeneratorBaseCapacity;
            }
        }

        [HarmonyPatch(typeof(PowerTransformerSmallConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        private static class Patch_PowerTransformerSmallConfig_ConfigureBuildingTemplate
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<PowerTransformerAdjustable>();
            }
        }
    }
}
