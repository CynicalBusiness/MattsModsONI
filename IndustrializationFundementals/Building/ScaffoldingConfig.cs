using TUNING;

namespace MattsMods.IndustrializationFundementals.Building
{
    public class ScaffoldingConfig : IBuildingConfig
    {
        public const string ID = "Scaffolding";

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 1,
                height: 1,
                anim: "ladder_kanim", // TODO
                hitpoints: BUILDINGS.HITPOINTS.TIER0,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0,
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER0,
                construction_materials: new string[]{ IndustrializationFundementalsMod.Tags.Lumber.Name },
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER0,
                build_location_rule: BuildLocationRule.Anywhere,
                decor: BUILDINGS.DECOR.PENALTY.TIER0,
                noise: NOISE_POLLUTION.NONE
            );
            BuildingTemplates.CreateLadderDef(def);
            def.Floodable = false;
            def.Overheatable = false;
            def.Entombable = false;
            def.AudioCategory = AUDIO.METAL;
            def.BaseTimeUntilRepair = -1f;
            def.DragBuild = true;
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            GeneratedBuildings.MakeBuildingAlwaysOperational(go);
            var scaffolding = go.AddOrGet<Scaffolding>();
            scaffolding.upwardsMovementSpeedMultiplier = 0.8f;
            scaffolding.downwardsMovementSpeedMultiplier = 0.8f;
            go.AddOrGet<AnimTileable>();
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            /* no-op */
        }
    }
}
