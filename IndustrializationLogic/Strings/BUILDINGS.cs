
using STRINGS;

namespace MattsMods.Industrialization.Logic.Strings
{
    public static class BUILDINGS
    {

        public static class PREFABS {

            private static readonly string AUTO_GREEN = UI.FormatAsAutomationState("Green", UI.AutomationState.Active);
            private static readonly string AUTO_RED = UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);
            private static readonly string AUTO_GREEN_SIG = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);
            private static readonly string AUTO_RED_SIG = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

            public static class LOGICCABLE
            {
                public static LocString NAME = UI.FormatAsLink("Automation Cable", Building.LogicCableConfig.ID);
                public static LocString DESC = "This is as dense as duplicants are able to get these things due to potentional space-time inconsistencies. Cannot be placed within tiles due to its diameter.";
                public static LocString EFFECT = $"Densely-packed automation wires capable of carrying 32 bits via eight {UI.FormatAsLink("Ribbons", LogicRibbonConfig.ID)}.";
            }

            public static class LOGICCABLEJOINT
            {
                public static LocString NAME = UI.FormatAsLink("Automation Cable Joint-Plate", Building.LogicCableJointConfig.ID);
                public static LocString DESC = $"Not a whole lot different from its cousin, the {UI.FormatAsLink("Heavi-Watt Joint-Plate", WireBridgeHighWattageConfig.ID)}.";
                public static LocString EFFECT = "Allows logic cable to cross through tiles at pre-defined points.";
            }
        }

    }
}
