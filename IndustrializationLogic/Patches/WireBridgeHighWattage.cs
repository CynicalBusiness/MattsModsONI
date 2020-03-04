using Harmony;
using MattsMods.Industrialization.Logic.Building;
using System.Collections.Generic;
using UnityEngine;

namespace MattsMods.Industrialization.Logic.Patches
{
    public static class WireBridgeHighWattagePatches
    {

        public static readonly HashedString BRIDGE_LOGIC_CABLE_IO_ID = new HashedString("BRIDGE_LOGIC_CABLE_IO");

        private static LogicUtilityNetworkLink AddNetworkLink (GameObject go)
        {
            var link = go.AddOrGet<LogicUtilityNetworkLink>();
            link.bitDepth = LogicCableConfig.BIT_DEPTH;
            link.link1 = new CellOffset(-1, 0);
            link.link2 = new CellOffset(1, 0);
            return link;
        }

        [HarmonyPatch(typeof(WireBridgeHighWattageConfig), "CreateBuildingDef")]
        public static class Patch_WireBridgeHighWattageConfig_CreateBuildingDef
        {
            private static void Postfix (BuildingDef __result)
            {
                __result.SceneLayer = Grid.SceneLayer.LogicGates;
                __result.LogicInputPorts = new List<LogicPorts.Port>()
                {
                    LogicCableConfig.CableInputPort(
                        BRIDGE_LOGIC_CABLE_IO_ID,
                        new CellOffset(-1, 0),
                        STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT,
                        STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_ACTIVE,
                        STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_INACTIVE,
                        false, false),
                    LogicCableConfig.CableInputPort(
                        BRIDGE_LOGIC_CABLE_IO_ID,
                        new CellOffset(1, 0),
                        STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT,
                        STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_ACTIVE,
                        STRINGS.BUILDINGS.PREFABS.LOGICRIBBONBRIDGE.LOGIC_PORT_INACTIVE,
                        false, false)
                };
                GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, WireBridgeHighWattageConfig.ID);
            }
        }

        [HarmonyPatch(typeof(WireBridgeHighWattageConfig), "DoPostConfigureComplete")]
        public static class Patch_WireBridgeHighWattageConfig_DoPostConfigureComplete
        {
            private static void Prefix (GameObject go)
            {
                AddNetworkLink(go).visualizeOnly = false;
            }

        }

        [HarmonyPatch(typeof(WireBridgeHighWattageConfig), "DoPostConfigureUnderConstruction")]
        public static class Patch_WireBridgeHighWattageConfig_DoPostConfigureUnderConstruction
        {
            private static void Prefix (GameObject go)
            {
                AddNetworkLink(go).visualizeOnly = true;
            }

        }

        [HarmonyPatch(typeof(WireBridgeHighWattageConfig), "DoPostConfigurePreview")]
        public static class Patch_WireBridgeHighWattageConfig_DoPostConfigurePreview
        {
            private static void Prefix (GameObject go)
            {
                AddNetworkLink(go).visualizeOnly = true;
            }

        }

    }

}
