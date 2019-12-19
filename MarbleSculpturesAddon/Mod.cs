using Harmony;
using UnityEngine;

namespace MattsMods.MarbleSculpturesAddon
{
    public class Mod
    {
        // This is a joke mod, so I'm not super concerned about formatting it.
        // Falls into the "10-minute-mod" category, so we're going with only one file.

        // This mod is a good example of what happens when people are really bored and a joke goes too far. :3

        [HarmonyPatch(typeof(MarbleSculptureConfig), "DoPostConfigureComplete")]
        private static class Patch_MarbleSculptureConfig_DoPostConfigureComplete
        {
            public static void Postfix (GameObject go)
            {
                Artable artable = go.AddOrGet<Sculpture>();
                artable.stages.Add(new Artable.Stage(
                    "Good4",
                    STRINGS.BUILDINGS.PREFABS.MARBLESCULPTURE.EXCELLENTQUALITYNAME,
                    "amazing_4",
                    15,
                    true,
                    Artable.Status.Great));
                artable.stages.Add(new Artable.Stage(
                    "Good5",
                    STRINGS.BUILDINGS.PREFABS.MARBLESCULPTURE.EXCELLENTQUALITYNAME,
                    "amazing_5",
                    15,
                    true,
                    Artable.Status.Great
                ));
            }
        }
    }
}
