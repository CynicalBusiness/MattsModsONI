using Harmony;
using UnityEngine;

namespace MattsMods.AdjustableTransformers
{
    public static class PowerTransformerPatches
    {
        private static HarmonyInstance harmonyInstance;

        public static void PrePatch (HarmonyInstance harmonyInstance)
        {
            PowerTransformerPatches.harmonyInstance = harmonyInstance;
        }

        public static void OnLoad ()
        {
            LocString.CreateLocStringKeys(typeof(MattsMods.AdjustableTransformers.STRINGS.UI));
        }

        // === Large Power Transformer
        [HarmonyPatch(typeof(PowerTransformerConfig), "CreateBuildingDef")]
        private static class Patch_PowerTransformerConfig_CreateBuildingDef
        {
            public static void Postfix(BuildingDef __result)
            {
                __result.GeneratorBaseCapacity = Wire.GetMaxWattageAsFloat(Wire.WattageRating.Max20000);
                __result.GeneratorWattageRating = __result.GeneratorBaseCapacity;
            }
        }

        [HarmonyPatch(typeof(PowerTransformerConfig), "ConfigureBuildingTemplate")]
        private static class Patch_PowerTransformerConfig_ConfigureBuildingTemplate
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<PowerTransformerAdjustable>().preferredDefaultWattage = Wire.GetMaxWattageAsFloat(Wire.WattageRating.Max2000) * 2;
            }
        }

        [HarmonyPatch(typeof(PowerTransformerSmallConfig), "ConfigureBuildingTemplate")]
        private static class Patch_PowerTransformerSmallConfig_ConfigureBuildingTemplate
        {
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<PowerTransformerAdjustable>();
            }
        }

        [HarmonyPatch(typeof(PowerTransformer), "UpdateJoulesLostPerSecond")]
        private static class Patch_PowerTransformer_UpdateJoulesLostPerSecond
        {
            public static void Postfix(PowerTransformer __instance, Battery ___battery)
            {
                var pta = __instance.gameObject.GetComponent<PowerTransformerAdjustable>();
                if (pta != null && ___battery.joulesLostPerSecond > 0)
                {
                    ___battery.joulesLostPerSecond *= pta.Efficiency;
                }
            }
        }

        [HarmonyPatch(typeof(Generator), "Efficiency", MethodType.Getter)]
        private static class Patch_Generator_Efficiency
        {
            public static bool Prefix(Generator __instance, ref float __result)
            {
                var pta = __instance.gameObject.GetComponent<PowerTransformerAdjustable>();
                if (__instance is PowerTransformer && pta != null)
                {
                    __result = Mathf.Max(pta.WattageRatio, 0);
                    return false;
                }
                else return true;
            }
        }

        [HarmonyPatch(typeof(CapacityControlSideScreen), "SetTarget")]
        private static class Patch_CapacityControlSideScreen_SetTarget
        {
            public static void Postfix(ref string ___titleKey, IUserControlledCapacity ___target)
            {
                if (___target != null && ___target is PowerTransformerAdjustable)
                {
                    ___titleKey = ((PowerTransformerAdjustable)___target).SliderTitleKey;
                }
            }
        }
    }
}
