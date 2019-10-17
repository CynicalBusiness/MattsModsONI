using static STRINGS.UI;

namespace MattsMods.Industrialization.Storage.STRINGS
{
    public static class UI
    {
        public static class UISIDESCREENS
        {

            public static class THRESHOLD_SWITCH_SIDESCREEN
            {

                public static LocString STORAGE = (LocString) "Storage";
                public static LocString STORAGE_TOOLTIP_ABOVE = $"Will send a {FormatAsAutomationState("Green Signal", AutomationState.Active)} if the {FormatAsKeyWord("Storage")} is above <b>{{0}}</b>";
                public static LocString STORAGE_TOOLTIP_BELOW = $"Will send a {FormatAsAutomationState("Green Signal", AutomationState.Active)} if the {FormatAsKeyWord("Storage")} is below <b>{{0}}</b>";
            }

        }
    }
}
