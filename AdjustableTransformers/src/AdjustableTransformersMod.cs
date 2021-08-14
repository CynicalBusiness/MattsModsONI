using HarmonyLib;
using KMod;

namespace MattsMods.AdjustableTransformers
{
    public class AdjustableTransformersMod : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            LocString.CreateLocStringKeys(typeof(MattsMods.AdjustableTransformers.STRINGS.UI));
        }
    }
}
