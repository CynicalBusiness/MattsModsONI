using Harmony;
using UnityEngine;

namespace MattsMods.AdjustableTransformers
{
    public static class PowerTransformerPatches
    {
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
                go.AddComponent<PowerTransformerAdjustable>().SetWattage(4000f);
            }
        }

        // === Small Power Transformer
        [HarmonyPatch(typeof(PowerTransformerSmallConfig))]
        [HarmonyPatch("CreateBuildingDef")]
        private static class Patch_PowerTransformerSmallConfig_CreateBuildingDef
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.GeneratorBaseCapacity = Wire.GetMaxWattageAsFloat(Wire.WattageRating.Max2000);
                __result.GeneratorWattageRating = __result.GeneratorBaseCapacity;
            }
        }

        [HarmonyPatch(typeof(PowerTransformerSmallConfig))]
        [HarmonyPatch("ConfigureBuildingTemplate")]
        private static class Patch_PowerTransformerSmallConfig_ConfigureBuildingTemplate
        {
            public static void Postfix(GameObject go)
            {
                go.AddComponent<PowerTransformerAdjustable>();
            }
        }

        [HarmonyPatch(typeof(PowerTransformerSmallConfig))]
        [HarmonyPatch("DoPostConfigureComplete")]
        private static class Patch_PowerTransformerSmallConfig_DoPostConfigureComplete
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<PowerTransformerAdjustable>().SetWattage(1000f);
            }
        }
    }
}
