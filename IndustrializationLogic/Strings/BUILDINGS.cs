
using static STRINGS.UI;

namespace MattsMods.Industrialization.Logic.Strings
{
    public static class BUILDINGS
    {

        public static class PREFABS {

            private static readonly string AUTO_GREEN = FormatAsAutomationState("Green", AutomationState.Active);
            private static readonly string AUTO_RED = FormatAsAutomationState("Red", AutomationState.Standby);
            private static readonly string AUTO_GREEN_SIG = FormatAsAutomationState("Green Signal", AutomationState.Active);
            private static readonly string AUTO_RED_SIG = FormatAsAutomationState("Red Signal", AutomationState.Standby);

            public static class LOGICCABLE
            {
                public static LocString NAME = FormatAsLink("Automation Cable", Building.LogicCableConfig.ID);
                public static LocString DESC = "This is as dense as duplicants are able to get these things due to potentional space-time inconsistencies. Cannot be placed within tiles due to its diameter.";
                public static LocString EFFECT = $"Densely-packed automation wires capable of carrying 32 bits via eight {FormatAsLink("Ribbons", LogicRibbonConfig.ID)}.";
            }

            public static class LOGICCABLEJOINT
            {
                public static LocString NAME = FormatAsLink("Automation Cable Joint-Plate", Building.LogicCableJointConfig.ID);
                public static LocString DESC = $"Not a whole lot different from its cousin, the {FormatAsLink("Heavi-Watt Joint-Plate", WireBridgeHighWattageConfig.ID)}.";
                public static LocString EFFECT = "Allows logic cable to cross through tiles at pre-defined points.";
            }

            public static class LOGICCABLEREADER
            {
                public static LocString NAME = FormatAsLink("Automation Cable Reader", Building.LogicCableReaderConfig.ID);
                public static LocString DESC = "Will output a block of four bits at a time.";
                public static LocString EFFECT = $"Reads four bits off an automation cable for output onto a {FormatAsLink("Ribbons", LogicRibbonConfig.ID)}.";
            }

            public static class LOGICBUNDLEDEMITTER
            {
                public static LocString NAME = FormatAsLink("Bundled Automation Switch", Building.LogicBundledEmitterConfig.ID);
                public static LocString DESC = "Emits a signal on all channels of any wire connected to it";
                public static LocString EFFECT = "Wires below the maximum output of this switch will not overload when used.";
                public static LocString SIDESCREEN_TITLE = StripLinkFormatting(NAME);
            }
        }

    }
}
