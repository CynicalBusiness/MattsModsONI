
using Harmony;

namespace MattsMods.Industrialization.Storage
{
    public static class Patches
    {

        [HarmonyPatch(typeof(global::Storage), "MakeItemTemperatureInsulated")]
        private static class Patch_Storage_MakeItemTemperatureInsulated
        {
            public static void Prefix (UnityEngine.GameObject go)
            {
                if (go.GetComponent<SimTemperatureTransfer>() == null)
                {
                    Debug.LogFormat("No SimTemperatureTransfer on {0}", go.GetComponent<PrimaryElement>().GetProperName());
                }
            }
        }

    }
}
