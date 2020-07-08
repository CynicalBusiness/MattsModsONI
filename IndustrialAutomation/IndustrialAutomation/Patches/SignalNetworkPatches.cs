using Harmony;
using System;
using System.Collections.Generic;

namespace MattsMods.IndustrialAutomation.Patches
{
    public static class SignalNetworkPatches
    {
        [HarmonyPatch(typeof(OverlayScreen), "RegisterModes")]
        private static class Patch_OverlayScreen_RegisterModes
        {
            // add our overlay screen to the list of overlays
            public static void Postfix (OverlayScreen __instance)
            {
                AccessTools
                    .Method(
                        typeof(OverlayScreen),
                        "RegisterMode",
                        new Type[]{ typeof(OverlayModes.Mode) }
                    )
                    .Invoke(
                        __instance,
                        new object[]{ new Network.SignalOverlayMode() }
                    );
            }
        }

        private static class Patch_OverlayMenu_InitializeToggles
        {
            public static void Postfix(OverlayMenu __instance, List<KIconToggleMenu.ToggleInfo> ___overlayToggleInfos)
            {
                ___overlayToggleInfos.Add(
                    AccessTools.Constructor(AccessTools.TypeByName("OverlayMenu.OverlayToggleInfo"))
                        .Invoke(
                            new object[]{
                                Strings.UI.OVERLAYS.SIGNAL.BUTTON,
                                "overlay_logic",
                                Network.SignalOverlayMode.ID,
                                Mod.TECH_SIGNALNETWORKS1,
                                Action.Overlay13, // TODO custom action here
                                Strings.UI.TOOLTIPS.SIGNAL_OVERLAY_STRING,
                                Strings.UI.OVERLAYS.SIGNAL.BUTTON
                            }
                        )
                );
            }
        }
    }
}
