using static TUNING.BUILDINGS;
using UnityEngine;

namespace MattsMods.Industrialization.Agriculture
{
    public class AutoHarvesterConfig : IBuildingConfig
    {

        public const string ID = "AutoHarvester";

        public const int DEFAULT_RANGE = 4;

        public static void AddVisualizer (GameObject go, bool movable = true)
        {
            var harvester = go.GetComponent<AutoHarvester>();
            if (harvester) {
                var vis = go.AddOrGet<StationaryChoreRangeVisualizer>();
                vis.x = -harvester.range;
                vis.y = -harvester.range;
                vis.width = (harvester.range * 2) + 1;
                vis.height = (harvester.range * 2) + 1;
                vis.movable = movable;
            }
        }

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 3,
                height: 1,
                anim: "conveyor_transferarm_kanim",
                hitpoints: HITPOINTS.TIER1,
                construction_time: CONSTRUCTION_TIME_SECONDS.TIER2,
                construction_mass: CONSTRUCTION_MASS_KG.TIER3,
                construction_materials: TUNING.MATERIALS.REFINED_METALS,
                melting_point: MELTING_POINT_KELVIN.TIER1,
                build_location_rule: BuildLocationRule.Anywhere,
                decor: DECOR.PENALTY.TIER2,
                noise: TUNING.NOISE_POLLUTION.NOISY.TIER0
            );
            def.Floodable = false;
            def.AudioCategory = TUNING.AUDIO.METAL;
            def.RequiresPowerInput = true;
            def.EnergyConsumptionWhenActive = ENERGY_CONSUMPTION_WHEN_ACTIVE.TIER2;
            def.ExhaustKilowattsWhenActive = EXHAUST_ENERGY_ACTIVE.TIER0;
            def.SelfHeatKilowattsWhenActive = SELF_HEAT_KILOWATTS.TIER1;
            def.PermittedRotations = PermittedRotations.R360;
            def.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
            GeneratedBuildings.RegisterWithOverlay(OverlayScreen.HarvestableIDs, ID);
            return def;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGet<AutoHarvester>();
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            AddVisualizer(go);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            AddVisualizer(go);
            go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.IncreaseBotanyMedium.Id;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            AddVisualizer(go);
        }
    }
}
