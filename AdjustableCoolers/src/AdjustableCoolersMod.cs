using HarmonyLib;
using KMod;

namespace MattsMods.AdjustableCoolers
{
    public class AdjustableCoolersMod : UserMod2
    {

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            LocString.CreateLocStringKeys(typeof(MattsMods.AdjustableCoolers.STRINGS.UI));
        }

    }
}
