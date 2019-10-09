using static STRINGS.UI;
using static MattsMods.IndustrializationFundementals.IndustrializationFundementalsMod;

namespace MattsMods.IndustrializationFundementals.STRINGS
{
    public static class ELEMENTS
    {
        #region Vanilla Overrides
        public static class CUPERITE {
            public static LocString NAME = FormatAsLink("Cuperite", nameof(CUPERITE));
            public static LocString DESC = $"A raw metal ore that can be refined into {FormatAsLink("Copper", nameof(global::STRINGS.ELEMENTS.COPPER))}";
        }

        public static class HEMATITE {
            public static LocString NAME = FormatAsLink("Hematite", nameof(HEMATITE));
            public static LocString DESC = $"A raw metal ore that can be refined into {FormatAsLink("Iron", nameof(global::STRINGS.ELEMENTS.IRON))}";
        }

        public static class ALUMINUMORE
        {
            public static LocString NAME = FormatAsLink("Bauxite", nameof(ALUMINUMORE));
            public static LocString DESC = $"A raw metal ore that can be refined into {FormatAsLink("Aluminum", nameof(global::STRINGS.ELEMENTS.ALUMINUM))}";
        }
        #endregion
        #region Wood
        #region Wood Products
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

        public static class SAWDUSTSOLID
        {
            public static LocString NAME = FormatAsLink("Sawdust", nameof(SAWDUSTSOLID));
            public static LocString DESC = $"A fine powder produced as a byproduct from processing {FormatAsLink("Wood", Tags.WoodLogs.Name)}.";
        }
        #endregion

        #region Arbor Wood
        public static class WOODARBORSOLID
        {
            public static LocString NAME = FormatAsLink("Arbor Wood", nameof(WOODARBORSOLID));
            public static LocString DESC = $"The raw wood of an {FormatAsLink("Arbor Tree", "FOREST_TREE")}.";
        }

        public static class LUMBERARBORSOLID
        {
            public static LocString NAME = FormatAsLink("Arbor Lumber", nameof(LUMBERARBORSOLID));
            public static LocString DESC = $"Cut lumber from {FormatAsLink("Arbor Wood", nameof(WOODARBORSOLID))}.";
        }
        #endregion
        #endregion
        #region Metals
        #region Tin
        public static class TINSOLID
        {
            public static LocString NAME = FormatAsLink("Tin", nameof(TINSOLID));
            public static LocString DESC = $"A light-weight and mallable metal.";
        }

        public static class TINLIQUID
        {
            public static LocString NAME = FormatAsLink("Molten Tin", nameof(TINLIQUID));
            public static LocString DESC = $"{FormatAsLink("Tin", nameof(TINSOLID))} melted down into a liquid.";
        }

        public static class TINGAS
        {
            public static LocString NAME = FormatAsLink("Tin", nameof(TINGAS));
            public static LocString DESC = $"{FormatAsLink("Molten Tin", nameof(TINSOLID))} boiled into a gas.";
        }

        public static class TINORESOLID
        {
            public static LocString NAME = FormatAsLink("Casserite", nameof(TINORESOLID));
            public static LocString DESC = $"A raw ore that can be extracted for {FormatAsLink("Tin", nameof(TINSOLID))}";
        }
        #endregion
        #region Bronze
        public static class BRONZESOLID
        {
            public static LocString NAME = FormatAsLink("Bronze", nameof(BRONZESOLID));
            public static LocString DESC = $"A strong alloy forged from {FormatAsLink("Copper", nameof(global::STRINGS.ELEMENTS.COPPER))} a either {FormatAsLink("Tin", nameof(TINSOLID))} or {FormatAsLink("Aluminum", nameof(global::STRINGS.ELEMENTS.ALUMINUM))}";
        }
        #endregion
        #endregion
    }
}
