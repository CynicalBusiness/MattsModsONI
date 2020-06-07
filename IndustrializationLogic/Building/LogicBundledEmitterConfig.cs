using PipLib.Building;
using System.Collections.Generic;

using static TUNING.BUILDINGS;
using static TUNING.MATERIALS;

namespace MattsMods.Industrialization.Logic.Building
{
    [BuildingInfo.OnPlanScreen(ID, "Automation", AfterId = "LogicSwitch")]
    [BuildingInfo.TechRequirement(ID, "ParallelAutomation")]
    public class LogicBundledEmitterConfig : IBuildingConfig
    {
        public const string ID = "LogicBundledEmitter";

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 1,
                height: 1,
                anim: "switchdupecontrolledpower_kanim", // TODO
                hitpoints: HITPOINTS.TIER1,
                construction_time: CONSTRUCTION_TIME_SECONDS.TIER2,
                construction_mass: CONSTRUCTION_MASS_KG.TIER2,
                construction_materials: REFINED_METALS,
                melting_point: MELTING_POINT_KELVIN.TIER1,
                build_location_rule: BuildLocationRule.NotInTiles,
                decor: TUNING.DECOR.PENALTY.TIER2,
                noise: TUNING.NOISE_POLLUTION.NONE
            );
            def.Overheatable = false;
            def.Floodable = false;
            def.Entombable = false;
            def.PermittedRotations = PermittedRotations.Unrotatable;
            def.ViewMode = OverlayModes.Logic.ID;
            def.AudioCategory = TUNING.AUDIO.METAL;
            def.ObjectLayer = ObjectLayer.LogicGate;
            def.SceneLayer = Grid.SceneLayer.LogicGates;
            def.AlwaysOperational = true;
            def.LogicOutputPorts = new List<LogicPorts.Port>()
            {
                LogicCableConfig.CableOutputPort(
                    LogicCableIO.PORT_OUTPUT_WRITER_ID,
                    new CellOffset(0, 0),
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_ACTIVE,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_INACTIVE,
                    false, false)
            };
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
            go.AddOrGet<LogicBundledEmitter>();
        }
    }
}
