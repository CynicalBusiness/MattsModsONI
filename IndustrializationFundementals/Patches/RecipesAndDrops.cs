using Harmony;
using System.Collections.Generic;
using UnityEngine;

namespace MattsMods.IndustrializationFundementals.Patches
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
                    crop_id = ElementConfig.LumberArbor.ID + "Solid";
                }
            }

        }

        [HarmonyPatch(typeof(EthanolDistilleryConfig), "ConfigureBuildingTemplate")]
        private static class Patch_EthanolDistilleryConfig_ConfigureBuildingTemplate
        {
            // make the ethanol distillery use our lumber instead
            public static void Postfix(GameObject go)
            {
                var tag = IndustrializationFundementalsMod.Tags.WoodLogs;
                go.AddOrGet<ManualDeliveryKG>().requestedItemTag = tag;
                go.AddOrGet<ElementConverter>().consumedElements[0].tag = tag;
            }
        }

        [HarmonyPatch(typeof(WoodGasGeneratorConfig), "DoPostConfigureComplete")]
        private static class Patch_WoodGasGeneratorConfig_DoPostConfigureComplete
        {
            // make the wood burner use our lumber instead
            public static void Postfix(GameObject go)
            {
                var tag = IndustrializationFundementalsMod.Tags.WoodLogs;
                go.AddOrGet<ManualDeliveryKG>().requestedItemTag = tag;
                go.AddOrGet<EnergyGenerator>().formula.inputs[0].tag = tag;
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
                var charcoalInput1 = IndustrializationFundementalsMod.Tags.WoodLogs;
                var charcoalOutput1 = TagManager.Create("CharcoalSolid");
                var charcoalOutput2 = TagManager.Create("AshSolid");
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
                        time = 40f,
                        description = string.Format(STRINGS.BUILDINGS.PREFABS.KILN.RECIPE_CHARCOAL_DESCRIPTION, element.name, ElementLoader.FindElementByName("CharcoalSolid").name, ElementLoader.FindElementByName("AshSolid").name),
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
    }
}
