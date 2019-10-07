using Harmony;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MattsMods.IndustrializationFundementals.Patches
{
    public static class Scaffolding
    {
        private static readonly MethodInfo OnCompleteWork = AccessTools.Method(typeof(Deconstructable), "OnCompleteWork", new Type[1]{ typeof(Worker) });
        private static readonly FieldInfo Chore = AccessTools.Field(typeof(Deconstructable), "chore");

        private static readonly List<GameObject> pendingDeconstruct = new List<GameObject>();

        [HarmonyPatch(typeof(Deconstructable), "OnCompleteWork")]
        private static class Patch_Deconstructable_OnCompleteWork
        {
            // deconstruct neighboring scaffolding marked for deconstruction
            private static bool Prefix (Deconstructable __instance, Worker worker)
            {
                if (worker == null)
                {
                    // don't try and handle insta-build deletes
                    return true;
                }

                var building = __instance.GetComponent<global::Building>();
                var s = building.gameObject.GetComponent<Building.Scaffolding>();
                if (s != null)
                {
                    if (!pendingDeconstruct.Contains(building.gameObject))
                    {
                        var chore = (Chore)Chore.GetValue(__instance);
                        chore.runUntilComplete = true;

                        var last = s.GetFurthestMarkedForDeconstruct();
                        if (last == building.gameObject)
                        {
                            return true;
                        }
                        else
                        {
                            pendingDeconstruct.Add(last);
                            OnCompleteWork.Invoke(last.GetComponent<Deconstructable>(), new []{ worker });
                            chore.Fail("More to deconstruct");
                            return false;
                        }
                    }
                    else
                    {
                        pendingDeconstruct.Remove(building.gameObject);
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
