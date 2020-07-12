using static STRINGS.UI;

namespace MattsMods.IndustrialStorage.STRINGS
{
    public static class UI
    {
        public static class UISIDESCREENS
        {

            public static class THRESHOLD_SWITCH_SIDESCREEN
            {

                public static LocString STORAGE = "Storage";
                public static LocString STORAGE_TOOLTIP_ABOVE = $"Will send a {FormatAsAutomationState("Green Signal", AutomationState.Active)} if the {FormatAsKeyWord("Storage")} is above <b>{{0}}</b>";
                public static LocString STORAGE_TOOLTIP_BELOW = $"Will send a {FormatAsAutomationState("Green Signal", AutomationState.Active)} if the {FormatAsKeyWord("Storage")} is below <b>{{0}}</b>";
            }

            public static class STORAGE_COLD_SIDESCREEN
            {
                public static LocString NAME = STRINGS.BUILDINGS.PREFABS.STORAGECONTAINERCOLD.NAME;
                public static LocString TOOLTIP = "Temperature at which the contents will be stored.";
            }

        }
    }
}
