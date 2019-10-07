using System.Collections.Generic;
using TUNING;

namespace MattsMods.IndustrializationFundementals.Building
{
    public class SawmillConfig : IBuildingConfig
    {
        public const string ID = "Sawmill";

        public static readonly Tag TAG_WOOD = IndustrializationFundementalsMod.Tags.WoodLogs;
        public static readonly Tag TAG_LUMBER = IndustrializationFundementalsMod.Tags.Lumber;
        public static readonly Tag TAG_SAWDUST = TagManager.Create(ElementConfig.Sawdust.ID + Element.State.Solid);

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 4,
                height: 3,
                anim: "ethanoldistillery_kanim", // TODO
                hitpoints: BUILDINGS.HITPOINTS.TIER1,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER3,
                construction_mass: new float[2]{ BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0], BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0] },
                construction_materials: new string[2]{ MATERIALS.ALL_METALS[0], TAG_WOOD.Name },
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER0,
                build_location_rule: BuildLocationRule.OnFloor,
                decor: DECOR.PENALTY.TIER2,
                noise: NOISE_POLLUTION.NOISY.TIER6
            );

            def.RequiresPowerInput = true;
            def.EnergyConsumptionWhenActive = BUILDINGS.ENERGY_CONSUMPTION_WHEN_ACTIVE.TIER5;
            def.SelfHeatKilowattsWhenActive = BUILDINGS.SELF_HEAT_KILOWATTS.TIER5;
            def.ViewMode = OverlayModes.Power.ID;
            def.AudioCategory = AUDIO.HOLLOW_METAL;
            def.AudioSize = "large"; // TODO find this contstant

            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
            var fab = go.AddOrGet<ComplexFabricator>();
            fab.outputOffset = new UnityEngine.Vector3(2f, 0.5f);
            fab.duplicantOperated = true;
            fab.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
            go.AddOrGet<ComplexFabricatorWorkable>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, fab);
            ConfigureRecipes();
            Prioritizable.AddRef(go);
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            SymbolOverrideControllerUtil.AddToPrefab(go);
            go.AddOrGetDef<PoweredActiveStoppableController.Def>();
            go.GetComponent<KPrefabID>().prefabSpawnFn += gameObject =>
            {
                var comp = gameObject.GetComponent<ComplexFabricatorWorkable>();
                comp.WorkerStatusItem = Db.Get().DuplicantStatusItems.Processing;
                comp.AttributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
                comp.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
                comp.SkillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
                comp.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
            };
        }

        private void ConfigureRecipes ()
        {
            // all wood that has a lumber variant
            foreach (var woodE in ElementLoader.elements.FindAll(e => e.IsSolid && e.HasTag(TAG_WOOD)))
            {
                var woodId = woodE.tag.Name;
                IndustrializationFundementalsMod.ModLogger.Debug(woodId);
                if (woodId.StartsWith(ElementConfig.PREFIX_WOOD))
                {
                    var lumberId = ElementConfig.PREFIX_LUMBER + woodId.Substring(ElementConfig.PREFIX_WOOD.Length);
                    IndustrializationFundementalsMod.ModLogger.Debug(lumberId);
                    var lumberE = ElementLoader.FindElementByName(lumberId);

                    if (lumberE != null)
                    {
                        var inputs = new ComplexRecipe.RecipeElement[1] {
                            new ComplexRecipe.RecipeElement(woodE.tag, 100f)
                        };
                        var outputs = new ComplexRecipe.RecipeElement[2] {
                            new ComplexRecipe.RecipeElement(lumberE.tag, 90f),
                            new ComplexRecipe.RecipeElement(TAG_SAWDUST, 10f)
                        };
                        var idObsolete = ComplexRecipeManager.MakeObsoleteRecipeID(ID, woodE.tag);
                        var id = ComplexRecipeManager.MakeRecipeID(ID, inputs, outputs);

                        new ComplexRecipe(id, inputs, outputs)
                        {
                            time = BUILDINGS.FABRICATION_TIME_SECONDS.SHORT,
                            description = string.Format(STRINGS.BUILDINGS.PREFABS.KILN.RECIPE_CHARCOAL_DESCRIPTION, woodE.name, lumberE.name, STRINGS.ELEMENTS.SAWDUSTSOLID.NAME),
                            nameDisplay = ComplexRecipe.RecipeNameDisplay.Ingredient,
                            fabricators = new List<Tag>(){ TagManager.Create(ID) }
                        };
                        ComplexRecipeManager.Get().AddObsoleteIDMapping(idObsolete, id);
                    }
                }
            }
        }
    }
}
