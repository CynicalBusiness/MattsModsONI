using PipLib.Building;
using TUNING;
using UnityEngine;

namespace MattsMods.IndustrialFoundation.Building
{
    [BuildingInfo.TechRequirement(ID, Mod.Techs.CHEMICAL_SYNTHESIS)]
    [BuildingInfo.OnPlanScreen(ID, "Refinement", AfterId = OilRefineryConfig.ID)]
    public class ChemicalPlantConfig : IBuildingConfig
    {
        public const string ID = "ChemicalPlant";
        public static readonly Tag TAG = TagManager.Create(ID);

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 3,
                height: 3,
                anim: "generatorphos_kanim", // TODO
                hitpoints: BUILDINGS.HITPOINTS.TIER2,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER3,
                construction_mass: new float[]{
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0],
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0]
                },
                construction_materials: new string[]{
                    GameTags.RefinedMetal.Name,
                    GameTags.Plastic.Name
                },
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER2,
                build_location_rule: BuildLocationRule.OnFloor,
                decor: DECOR.PENALTY.TIER3,
                noise: NOISE_POLLUTION.NOISY.TIER2
            );
            def.PermittedRotations = PermittedRotations.FlipH;
            def.AudioCategory = AUDIO.HOLLOW_METAL;
            def.EnergyConsumptionWhenActive = BUILDINGS.ENERGY_CONSUMPTION_WHEN_ACTIVE.TIER3;
            return def;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = false;

            var fab = go.AddOrGet<ComplexFabricator>();
            fab.resultState = ComplexFabricator.ResultState.Heated;
            fab.heatedTemperature = KilnConfig.OUTPUT_TEMP;
            fab.duplicantOperated = false;
            fab.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            fab.storeProduced = true;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = TAG;

            BuildingTemplates.CreateComplexFabricatorStorage(go, fab);
            ConfigureRecipes();


        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            Prioritizable.AddRef(go);

            // TODO figure out fluid I/O
        }

        private void ConfigureRecipes ()
        {
            // battery cell
            var inputMetal = MATERIALS.METAL;
            var inputSulfur = SimHashes.Sulfur.CreateTag();


        }
    }
}
