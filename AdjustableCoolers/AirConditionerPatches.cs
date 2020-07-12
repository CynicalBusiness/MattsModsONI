using Harmony;
using UnityEngine;

namespace MattsMods.AdjustableCoolers
{
    public static class AirConditionerPatches
    {
        public const float ENERGY_MODIFIER = 20f / 14f;

        private static HarmonyInstance harmonyInstance;

        public static void PrePatch (HarmonyInstance harmonyInstance)
        {
            AirConditionerPatches.harmonyInstance = harmonyInstance;
        }

        public static void OnLoad ()
        {
            LocString.CreateLocStringKeys(typeof(MattsMods.AdjustableCoolers.STRINGS.UI));
        }

        [HarmonyPatch(typeof(AirConditionerConfig), "ConfigureBuildingTemplate")]
        private static class Patch_AirConditionerConfig_ConfigureBuildingTemplate
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<AirConditionerAdjustable>();
            }
        }

        [HarmonyPatch(typeof(LiquidConditionerConfig), "ConfigureBuildingTemplate")]
        private static class Patch_LiquidConditionerConfig_ConfigureBuildingTemplate
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<AirConditionerAdjustable>();
            }
        }



        [HarmonyPatch(typeof(AirConditionerConfig), "CreateBuildingDef")]
        private static class Patch_AirConditionerConfig_CreateBuildingDef
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 340;
            }
        }

        [HarmonyPatch(typeof(LiquidConditionerConfig), "CreateBuildingDef")]
        private static class Patch_LiquidConditionerConfig_CreateBuildingDef
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.EnergyConsumptionWhenActive = 1700;
            }
        }
    }
}
