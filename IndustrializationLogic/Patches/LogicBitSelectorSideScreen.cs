
using System;
using Harmony;
using PipLib;
using System.Collections.Generic;
using UnityEngine;

namespace MattsMods.Industrialization.Logic.Patches
{
    [HarmonyPatch(typeof(LogicBitSelectorSideScreen), "RefreshToggles")]
    public static class Patch_LogicBitSelectorSideScreen_RefreshToggles
    {
        private static void Postfix (Dictionary<int, MultiToggle> ___toggles_by_int, ILogicRibbonBitSelector ___target)
        {
            foreach ((var idx, var toggle) in ___toggles_by_int)
            {
                toggle.enabled = idx < ___target.GetBitDepth();
                toggle.gameObject.SetActive(toggle.enabled);

                if (toggle.enabled)
                {
                    bool active = ___target.IsBitActive(idx);

                    string title;
                    if (___target is Building.ILogicBundledBitSelector)
                    {
                        var target = (Building.ILogicBundledBitSelector)___target;
                        var bundleIdx = idx * target.BundleSize + 1;
                        title = String.Format(target.BundleTitle, bundleIdx, bundleIdx + target.BundleSize - 1);
                    }
                    else
                    {
                        title = String.Format(STRINGS.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.BIT, idx + 1);
                    }
                    toggle.GetComponent<HierarchyReferences>().GetReference<LocText>("bitName").SetText(title);
                }
            }
        }
    }

    [HarmonyPatch(typeof(LogicBitSelectorSideScreen), "UpdateStateVisuals")]
    public static class Patch_LogicBitSelectorSideScreen_UpdateStateVisuals
    {
        private static void Postfix (int bit, Dictionary<int, MultiToggle> ___toggles_by_int, ILogicRibbonBitSelector ___target, Color ___activeColor, Color ___inactiveColor)
        {
            var toggle = ___toggles_by_int[bit];
            toggle.enabled = bit < ___target.GetBitDepth();

            if (toggle.enabled)
            {
                bool active = ___target.IsBitActive(bit);

                string state;
                Color color;
                if (___target is Building.ILogicBundledBitSelector)
                {
                    var target = (Building.ILogicBundledBitSelector)___target;
                    var activeBits = target.GetBitsAtSelection(bit);

                    state = target.GetBundleState(activeBits);
                    color = active ? activeBits == target.BundleMask ? ___activeColor : Color.yellow : ___inactiveColor;
                }
                else
                {
                    state = active ? STRINGS.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.STATE_ACTIVE : STRINGS.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.STATE_INACTIVE;
                    color = active ? ___activeColor : ___inactiveColor;
                }
                toggle.GetComponent<HierarchyReferences>().GetReference<LocText>("stateText").SetText(state);
                toggle.GetComponent<HierarchyReferences>().GetReference<KImage>("stateIcon").color = color;
            }
        }
    }
}
