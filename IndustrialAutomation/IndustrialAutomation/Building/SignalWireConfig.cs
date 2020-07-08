using TUNING;
using System.Collections.Generic;
using UnityEngine;

namespace MattsMods.IndustrialAutomation.Building
{
    public class SignalWireConfig : IBuildingConfig
    {
        public const string ID = "SignalWire";

        public const KAnimGraphTileVisualizer.ConnectionSource CONNECTION_SOURCE = (KAnimGraphTileVisualizer.ConnectionSource) 8;

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 1,
                height: 1,
                anim: "logic_wires_kanim", // TODO
                hitpoints: BUILDINGS.HITPOINTS.TIER0,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0,
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER1,
                construction_materials: MATERIALS.REFINED_METALS,
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1,
                build_location_rule: BuildLocationRule.Anywhere,
                decor: DECOR.PENALTY.TIER2,
                noise: NOISE_POLLUTION.NONE
            );
            def.ViewMode = Network.SignalOverlayMode.ID;
            def.Floodable = false;
            def.Overheatable = false;
            def.Entombable = false;
            def.AudioCategory = AUDIO.METAL;
            def.AudioSize = "small";
            def.BaseTimeUntilRepair = -1f;
            def.isKAnimTile = true;
            def.DragBuild = true;

            // TODO own object layer
            def.ObjectLayer = ObjectLayer.LogicWire;
            def.TileLayer = ObjectLayer.LogicWireTile;
            def.ReplacementLayer = ObjectLayer.LogicWireTile;
            def.SceneLayer = Grid.SceneLayer.LogicWires;

            // TODO hookup
            // def.isUtility = true;

            GeneratedBuildings.RegisterWithOverlay(Network.SignalOverlayMode.HighlightItemIDs, ID);
            return def;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            GeneratedBuildings.MakeBuildingAlwaysOperational(go);
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof (RequiresFoundation), prefab_tag);
            KAnimGraphTileVisualizer graphTileVisualizer = go.AddOrGet<KAnimGraphTileVisualizer>();
            graphTileVisualizer.connectionSource = CONNECTION_SOURCE;
            graphTileVisualizer.isPhysicalBuilding = true;
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            base.DoPostConfigureUnderConstruction(go);
            go.GetComponent<Constructable>().isDiggingRequired = false;
            KAnimGraphTileVisualizer graphTileVisualizer = go.AddOrGet<KAnimGraphTileVisualizer>();
            graphTileVisualizer.connectionSource = CONNECTION_SOURCE;
            graphTileVisualizer.isPhysicalBuilding = false;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            // nothing to do
        }
    }
}
