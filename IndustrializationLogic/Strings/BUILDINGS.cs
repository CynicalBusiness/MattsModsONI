
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

            public static class LOGICGATENAND
            {
                public static LocString OUTPUT_NAME = "OUTPUT";
                public static LocString OUTPUT_ACTIVE = $"Sends a {AUTO_GREEN_SIG} if both Inputs are receiving {AUTO_RED}.";
                public static LocString OUTPUT_INACTIVE = $"Sends a {AUTO_RED_SIG} if any Input is {AUTO_GREEN}.";
            }


            public static class LOGICCABLE
            {
                public static LocString NAME = UI.FormatAsLink("Bundled Automation Cable", Building.LogicCableConfig.ID);
                public static LocString DESC = "This is as dense as duplicants are able to get these things due to potentional space-time inconsistencies.";
                public static LocString EFFECT = $"Densely-packed automation cable capable of carrying 32 bits via eight {UI.FormatAsLink("Ribbons", LogicRibbonConfig.ID)}.";
            }
        }

    }
}
