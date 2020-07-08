using Harmony;
using System.Collections.Generic;
using UnityEngine;

namespace MattsMods.IndustrialFoundation.Patches
{
    public static class RecipesAndDrops
    {
        [HarmonyPatch(typeof(EntityTemplates), "ExtendEntityToBasicPlant")]
        private static class Patch_EntityTemplates_ExtendEntityToBasicPlant
        {
            // make the arbor tree drop our new lumber instead of the default wood
            public static void Prefix (ref string crop_id)
            {
                if (crop_id == "WoodLog")
                {
                    crop_id = "LogArbor";
                }
            }

        }

        [HarmonyPatch(typeof(EthanolDistilleryConfig), "ConfigureBuildingTemplate")]
        private static class Patch_EthanolDistilleryConfig_ConfigureBuildingTemplate
        {
            // make the ethanol distillery use our lumber instead
            public static void Postfix(GameObject go)
            {
                Component.Destroy(go.GetComponent<ManualDeliveryKG>());

                var tag = Mod.Tags.Log;
                go.AddOrGet<ElementConverter>().consumedElements[0].tag = tag;

                var storage = go.AddOrGet<Storage>();
                storage.showInUI = true;
                storage.storageFilters = new List<Tag>(){ tag };
            }
        }

        [HarmonyPatch(typeof(WoodGasGeneratorConfig), "DoPostConfigureComplete")]
        private static class Patch_WoodGasGeneratorConfig_DoPostConfigureComplete
        {
            // make the wood burner use our lumber instead
            public static void Postfix(GameObject go)
            {
                Component.Destroy(go.GetComponent<ManualDeliveryKG>());

                var tag = Mod.Tags.Log;
                go.AddOrGet<EnergyGenerator>().formula.inputs[0].tag = tag;

                var storage = go.AddOrGet<Storage>();
                storage.showInUI = true;
                storage.storageFilters = new List<Tag>(){ tag };
            }
        }

        [HarmonyPatch(typeof(GeneratorConfig), "ConfigureBuildingTemplate")]
        private static class Patch_GeneratorConfig_ConfigureBuildingTemplate
        {
            // make the coal generator burn anything that is coal-like
            // by default, it's delivery and generator tags are different
            public static void Postfix(GameObject go)
            {
                go.AddOrGet<EnergyGenerator>().formula.inputs[0].tag = go.AddOrGet<ManualDeliveryKG>().requestedItemTag;
            }
        }

        [HarmonyPatch(typeof(KilnConfig), "ConfigureBuildingTemplate")]
        private static class Patch_KilnConfig_ConfigureBuildingTemplate
        {
            // add a recipe in the kiln to make charcoal/ash from wood
            public static void Postfix()
            {
                var charcoalInput1 = Mod.Tags.Log;
                var charcoalOutput1 = TagManager.Create("Charcoal");
                var charcoalOutput2 = TagManager.Create("Ash");
                var amountIn = 100f;
                var amountOutMost = 90f;
                var amountOutLeast = 10f;

                foreach (var element in ElementLoader.elements.FindAll(e => e.IsSolid && e.HasTag(charcoalInput1)))
                {
                    var charcoalInputs = new ComplexRecipe.RecipeElement[1]{
                        new ComplexRecipe.RecipeElement(element.tag, amountIn)
                    };
                    var charcoalOutputs = new ComplexRecipe.RecipeElement[2]{
                        new ComplexRecipe.RecipeElement(charcoalOutput1, amountOutMost),
                        new ComplexRecipe.RecipeElement(charcoalOutput2, amountOutLeast)
                    };

                    var idObsolete = ComplexRecipeManager.MakeObsoleteRecipeID(KilnConfig.ID, charcoalOutput1);
                    var id = ComplexRecipeManager.MakeRecipeID(KilnConfig.ID, charcoalInputs, charcoalOutputs);

                    var charcoalRecipe = new ComplexRecipe(id, charcoalInputs, charcoalOutputs)
                    {
                        time = TUNING.BUILDINGS.FABRICATION_TIME_SECONDS.SHORT,
                        description = string.Format(
                            STRINGS.BUILDINGS.PREFABS.METALREFINERY.RECIPE_BYPRODUCT,
                            element.name,
                            ElementLoader.FindElementByName(charcoalOutput1.Name).name,
                            ElementLoader.FindElementByName(charcoalOutput2.Name).name),
                        fabricators = new List<Tag>()
                        {
                            TagManager.Create(KilnConfig.ID)
                        },
                        nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
                    };
                    ComplexRecipeManager.Get().AddObsoleteIDMapping(idObsolete, id);
                }
            }
        }

        [HarmonyPatch(typeof(MetalRefineryConfig), "ConfigureBuildingTemplate")]
        private static class Patch_MetalRefineryConfig_ConfigureBuildingTemplate
        {
            // additional recipies in the metal refinery
            public static void Postfix()
            {
                // nothing right now
            }
        }
    }
}
