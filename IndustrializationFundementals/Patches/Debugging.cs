using Harmony;

namespace MattsMods.IndustrializationFundementals.Patches
{
    public static class Debugging
    {

        // [HarmonyPatch(typeof(ProcGenGame.WorldGenSimUtil), "DoSettleSim")]
        // private static class Patch_WorldGenSimUtil_DoSettleSim
        // {
        //     public static void Prefix ()
        //     {
        //         Debug.Log("DoSettleSim Prefix");
        //     }

        //     public static void Postfix ()
        //     {
        //         Debug.Log("DoSettleSim Postfix");
        //     }
        // }

        // [HarmonyPatch(typeof(ProcGenGame.WorldGen), "RenderOfflineThreadFn")]
        // private static class Patch_WorldGen_RenderOfflineThreadFn
        // {
        //     public static bool Prefix (ProcGenGame.WorldGen __instance)
        //     {
        //         Debug.Log("RenderOffline");
        //         Sim.DiseaseCell[] dc = null;
        //         __instance.RenderOffline(false, ref dc);
        //         return false;
        //     }
        // }

        public static void OnLoad ()
        {
            Debug.LogFormat("{0} {1}", System.Runtime.InteropServices.Marshal.SizeOf(typeof(int)), System.Runtime.InteropServices.Marshal.SizeOf(typeof(Sim.Element)));
        }
    }
}
