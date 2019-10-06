using static STRINGS.UI;
using static MattsMods.IndustrializationFundementals.IndustrializationFundementalsMod;

namespace MattsMods.IndustrializationFundementals.STRINGS
{
    public static class ELEMENTS
    {
        public static class CHARCOALSOLID
        {
            public static LocString NAME = FormatAsLink("Charcoal", nameof(CHARCOALSOLID));
            public static LocString DESC = $"A coal-like material made mostly of carbon created from baking {FormatAsLink("Wood", Tags.Lumber.Name)} in a low-{FormatAsLink("Oxygen", nameof(global::STRINGS.ELEMENTS.OXYGEN))} environment.";
        }

        public static class ASHSOLID
        {
            public static LocString NAME = FormatAsLink("Ash", nameof(ASHSOLID));
            public static LocString DESC = $"A left-over inflammable powder from burning {FormatAsLink("Charcoal", nameof(CHARCOALSOLID))}";
        }

        public static class LUMBERARBORSOLID
        {
            public static LocString NAME = FormatAsLink("Arbor Wood", nameof(LUMBERARBORSOLID));
            public static LocString DESC = $"The raw wood of an {FormatAsLink("Arbor Tree", "FOREST_TREE")}.";
        }
    }
}
