
using MattsMods.IndustrialStorage.Building;
using Harmony;
using System.Reflection;
using UnityEngine;

namespace MattsMods.Industrialization.Storage
{
    public static class Patches
    {

        [HarmonyPatch(typeof(RefrigeratorConfig), "DoPostConfigureComplete")]
        private static class Patch_RefrigeratorConfig_DoPostConfigureComplete
        {

            public static void Postfix (RefrigeratorConfig __instance, GameObject go)
            {
                GameObject.Destroy(go.GetComponent<Refrigerator>());
                GameObject.Destroy(go.GetComponent<TreeFilterable>());
                go.AddOrGet<StorageLocker>();
                go.AddOrGet<StorageCold>();
            }

        }

        [HarmonyPatch(typeof(RefrigeratorConfig), "CreateBuildingDef")]
        private static class Patch_RefrigeratorConfig_CreateBuildingDef
        {
            public static void Postfix (BuildingDef __result)
            {
                __result.MaterialCategory = TUNING.MATERIALS.ALL_METALS;
            }
        }

        [HarmonyPatch(typeof(Rottable), nameof(Rottable.IsRefrigerated))]
        private static class Patch_Rottable_IsRefrigerated
        {
            public static bool Prefix (GameObject gameObject, ref bool __result)
            {
                var cell = Grid.PosToCell(gameObject);
                if (Grid.IsValidCell(cell))
                {
                    var element = gameObject.GetComponent<PrimaryElement>();
                    if (element.Temperature <= (Constants.CELSIUS2KELVIN + 4))
                    {
                        __result = true;
                        return false;
                    }
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(FilteredStorage), "UpdateMeter")]
        private static class Patch_FilteredStorage_UpdateMeter
        {
            public static void Postfix (FilteredStorage __instance, StorageLocker ___root)
            {
                ___root.Trigger(StorageSecondaryMeter.OnOriginalMeterUpdate, null);
            }
        }

    }
}
