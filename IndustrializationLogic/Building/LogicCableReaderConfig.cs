using PipLib.Building;
using System.Collections.Generic;

using static TUNING.BUILDINGS;
using static TUNING.MATERIALS;

namespace MattsMods.Industrialization.Logic.Building
{

    [BuildingInfo.OnPlanScreen(ID, "Automation", AfterId = "LogicRibbonWriterConfig")]
    [BuildingInfo.TechRequirement(ID, Mod.TECH_HIGHDENSITYAUTOMATION)]
    public class LogicCableReaderConfig : IBuildingConfig
    {

        public const string ID = "LogicCableReader";

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 2,
                height: 1,
                anim: "logic_ribbon_reader_kanim", // TODO
                hitpoints: HITPOINTS.TIER1,
                construction_time: CONSTRUCTION_TIME_SECONDS.TIER2,
                construction_mass: PipLib.PLUtil.ArrayConcat(CONSTRUCTION_MASS_KG.TIER3, CONSTRUCTION_MASS_KG.TIER1),
                construction_materials: PipLib.PLUtil.ArrayConcat(REFINED_METALS, PLASTICS),
                melting_point: MELTING_POINT_KELVIN.TIER1,
                build_location_rule: BuildLocationRule.NotInTiles,
                decor: TUNING.DECOR.PENALTY.TIER2,
                noise: TUNING.NOISE_POLLUTION.NOISY.TIER1
            );
            def.Overheatable = false;
            def.Floodable = false;
            def.Entombable = false;
            def.PermittedRotations = PermittedRotations.R360;
            def.ViewMode = OverlayModes.Logic.ID;
            def.AudioCategory = TUNING.AUDIO.METAL;
            def.ObjectLayer = ObjectLayer.LogicGate;
            def.SceneLayer = Grid.SceneLayer.LogicGates;
            def.AlwaysOperational = true;
            def.LogicInputPorts = new List<LogicPorts.Port>()
            {
                LogicCableConfig.CableInputPort(
                    LogicCableIO.PORT_INPUT_READER_ID,
                    new CellOffset(0, 0),
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_ACTIVE,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_INACTIVE,
                    false, false),
            };
            def.LogicOutputPorts = new List<LogicPorts.Port>()
            {
                LogicPorts.Port.RibbonOutputPort(
                    LogicCableIO.PORT_OUTPUT_READER_ID,
                    new CellOffset(1, 0),
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_ACTIVE,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_INACTIVE,
                    false, false)
            };
            GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, ID);
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            go.AddOrGet<LogicCableIO>().isReader = true;
            go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
        }

    }

}
