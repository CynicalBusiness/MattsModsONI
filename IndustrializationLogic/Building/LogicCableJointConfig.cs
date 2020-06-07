using PipLib.Building;
using System.Collections.Generic;
using UnityEngine;

using static TUNING.BUILDINGS;
using static TUNING.MATERIALS;

namespace MattsMods.Industrialization.Logic.Building
{

    [BuildingInfo.OnPlanScreen(ID, "Automation", AfterId = LogicCableConfig.ID)]
    [BuildingInfo.TechRequirement(ID, Mod.TECH_HIGHDENSITYAUTOMATION)]
    public class LogicCableJointConfig : IBuildingConfig
    {

        public const string ID = "LogicCableJoint";

        public static readonly HashedString PORT_IO_ID = new HashedString("LogicCableJointIO");


        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 1,
                height: 1,
                anim: "heavywatttile_kanim", // TODO
                hitpoints: HITPOINTS.TIER2,
                construction_time: CONSTRUCTION_TIME_SECONDS.TIER1,
                construction_mass: CONSTRUCTION_MASS_KG.TIER3,
                construction_materials: REFINED_METALS,
                melting_point: MELTING_POINT_KELVIN.TIER1,
                build_location_rule: BuildLocationRule.HighWattBridgeTile, // TODO perhaps our own build rule?
                decor: DECOR.PENALTY.TIER2,
                noise: TUNING.NOISE_POLLUTION.NONE
            );

            def.Overheatable = false;
            def.Floodable = false;
            def.Entombable = false;
            def.ViewMode = OverlayModes.Logic.ID;
            def.AudioCategory = TUNING.AUDIO.METAL;
            def.AudioSize = "small"; // constant?
            def.BaseTimeUntilRepair = -1f;
            def.PermittedRotations = PermittedRotations.R360;
            def.UtilityInputOffset = new CellOffset(0, 0);
            def.UtilityOutputOffset = new CellOffset(0, 2);
            def.ObjectLayer = ObjectLayer.Building;
            def.SceneLayer = Grid.SceneLayer.LogicGates;
            def.ForegroundLayer = Grid.SceneLayer.TileMain;
            def.AlwaysOperational = true;
            def.LogicInputPorts = new List<LogicPorts.Port>()
            {
                LogicCableConfig.CableInputPort(
                    PORT_IO_ID,
                    new CellOffset(-1, 0),
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_ACTIVE,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_INACTIVE,
                    false, false),
                LogicCableConfig.CableInputPort(
                    PORT_IO_ID,
                    new CellOffset(1, 0),
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_ACTIVE,
                    STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_INACTIVE,
                    false, false)
            };
            GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, ID);
            return def;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);

            var sco = go.AddOrGet<SimCellOccupier>();
            sco.doReplaceElement = true;
            sco.movementSpeedMultiplier = TUNING.DUPLICANTSTATS.MOVEMENT.PENALTY_3;

            go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
            go.AddOrGet<TileTemperature>();
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            base.DoPostConfigurePreview(def, go);
            AddNetworkLink(go).visualizeOnly = true;
            go.AddOrGet<BuildingCellVisualizer>();
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            base.DoPostConfigureUnderConstruction(go);
            AddNetworkLink(go).visualizeOnly = true;
            go.AddOrGet<BuildingCellVisualizer>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            AddNetworkLink(go).visualizeOnly = false;
            go.AddOrGet<BuildingCellVisualizer>();
            go.AddOrGet<LogicRibbonBridge>();
        }

        private LogicUtilityNetworkLink AddNetworkLink (GameObject go)
        {
            var link = go.AddOrGet<LogicUtilityNetworkLink>();
            link.bitDepth = LogicCableConfig.BIT_DEPTH;
            link.link1 = new CellOffset(-1, 0);
            link.link2 = new CellOffset(1, 0);
            return link;
        }

    }

}
