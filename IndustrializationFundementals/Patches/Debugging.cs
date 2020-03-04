using System.Reflection;
using System.IO;
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

        #if DEBUG
            // this is a test to see *just* how many elements we can make happen
            [HarmonyPatch(typeof(PipLib.Elements.ElementManager), "CollectElements")]
            public static class Patch_ElementManager_CollectElements
            {
                public static void Prefix (string dir, System.Collections.Generic.List<ElementLoader.ElementEntry> results)
                {
                    if (Path.GetDirectoryName(dir).EndsWith("IndustrializationFundementals"))
                    {
                        for (var i = 0; i < 94; i++)
                        {
                            var entry = new PipLib.Elements.ElementEntryExtended()
                            {
                                elementId = "Debug" + i,
                                anim = "neutronium",
                                state = Element.State.Solid,
                                isDisabled = false,
                                materialCategory = "Special",
                                localizationID = "STRINGS.ELEMENTS.DEBUG" + i + ".NAME"
                            };
                            PipLib.Elements.ElementManager.loadedElements.Add((PipLib.Elements.ElementEntryExtended) entry);
                            results.Add(entry);
                        }
                    }
                }
            }
        #endif
    }
}
