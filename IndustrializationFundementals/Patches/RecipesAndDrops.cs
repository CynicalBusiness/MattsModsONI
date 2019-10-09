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
                    crop_id = ElementConfig.WoodArbor.ID + "Solid";
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
                        time = TUNING.BUILDINGS.FABRICATION_TIME_SECONDS.SHORT,
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

        [HarmonyPatch(typeof(MetalRefineryConfig), "ConfigureBuildingTemplate")]
        private static class Patch_MetalRefineryConfig_ConfigureBuildingTemplate
        {
            public static void Postfix ()
            {
                #region Bronze Recipe
                var bronzeOut = TagManager.Create("BronzeSolid");
                var bronzeInCopper = TagManager.Create("Copper");
                var bronzeInTin = TagManager.Create("TinSolid");
                var bronzeInAlu = TagManager.Create("Aluminum");

                var amountIn1 = 80f;
                var amountIn2 = 20f;
                var amountOut = amountIn1 + amountIn2;

                var bronzeInputsTin = new ComplexRecipe.RecipeElement[2]
                {
                    new ComplexRecipe.RecipeElement(bronzeInTin, amountIn2),
                    new ComplexRecipe.RecipeElement(bronzeInCopper, amountIn1)
                };
                var bronzeInputsAlu = new ComplexRecipe.RecipeElement[2]
                {
                    new ComplexRecipe.RecipeElement(bronzeInAlu, amountIn2),
                    new ComplexRecipe.RecipeElement(bronzeInCopper, amountIn1)
                };
                var bronzeOutputs = new ComplexRecipe.RecipeElement[1]
                {
                    new ComplexRecipe.RecipeElement(bronzeOut, amountOut)
                };
                IndustrializationFundementalsMod.ModLogger.Debug("make ids");

                var bronzeTinId = ComplexRecipeManager.MakeRecipeID(MetalRefineryConfig.ID, bronzeInputsTin, bronzeOutputs);
                var bronzeAluId = ComplexRecipeManager.MakeRecipeID(MetalRefineryConfig.ID, bronzeInputsAlu, bronzeOutputs);

                IndustrializationFundementalsMod.ModLogger.Debug("make recipes");
                new ComplexRecipe(bronzeTinId, bronzeInputsTin, bronzeOutputs)
                {
                    time = TUNING.BUILDINGS.FABRICATION_TIME_SECONDS.MODERATE,
                    description = string.Format(STRINGS.BUILDINGS.PREFABS.METALREFINERY.RECIPE_ALLOY2_DESCRIPTION, ElementLoader.FindElementByName(bronzeOut.Name).name, ElementLoader.FindElementByName(bronzeInCopper.Name).name, ElementLoader.FindElementByName(bronzeInTin.Name)),
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                    fabricators = new List<Tag>(){ TagManager.Create(MetalRefineryConfig.ID) }
                };
                new ComplexRecipe(bronzeAluId, bronzeInputsAlu, bronzeOutputs)
                {
                    time = TUNING.BUILDINGS.FABRICATION_TIME_SECONDS.MODERATE,
                    description = string.Format(STRINGS.BUILDINGS.PREFABS.METALREFINERY.RECIPE_ALLOY2_DESCRIPTION, ElementLoader.FindElementByName(bronzeOut.Name).name, ElementLoader.FindElementByName(bronzeInCopper.Name).name, ElementLoader.FindElementByName(bronzeInAlu.Name)),
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
                    fabricators = new List<Tag>(){ TagManager.Create(MetalRefineryConfig.ID) }
                };

                IndustrializationFundementalsMod.ModLogger.Debug("map obsolete ids");
                ComplexRecipeManager.Get().AddObsoleteIDMapping(ComplexRecipeManager.MakeObsoleteRecipeID(MetalRefineryConfig.ID, TagManager.Create("Bronze")), bronzeTinId);
                // ComplexRecipeManager.Get().AddObsoleteIDMapping(ComplexRecipeManager.MakeObsoleteRecipeID(MetalRefineryConfig.ID, TagManager.Create("BronzeAlu")), bronzeAluId);
                #endregion
            }
        }
    }
}
