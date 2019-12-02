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

        #region Ingredient Materials
        public static class ASH
        {
            public static LocString NAME = FormatAsLink("Ash", nameof(ASH));
            public static LocString DESC = $"A left-over inflammable powder from burning {FormatAsLink("Charcoal", nameof(CHARCOAL))}";
        }

        public static class SAWDUST
        {
            public static LocString NAME = FormatAsLink("Sawdust", nameof(SAWDUST));
            public static LocString DESC = $"A fine powder produced as a byproduct from processing {FormatAsLink("Wood", Tags.WoodLogs.Name)}.";
        }

        public static class SLAG
        {
            public static LocString NAME = FormatAsLink("Slag", nameof(SLAG));
            public static LocString DESC = $"Left-over impurities and rock from the metal refining process.";
        }

        public static class CEMENT
        {
            public static LocString NAME = FormatAsLink("Cement", nameof(CEMENT));
            public static LocString DESC = $"A fine powder formed from baking {FormatAsLink("Lime", nameof(global::STRINGS.ELEMENTS.LIME))} and {FormatAsLink("Clay", nameof(global::STRINGS.ELEMENTS.CLAY))} with a secondary ingredient such as {FormatAsLink("Ash", nameof(ASH))} or {FormatAsLink("Slag", nameof(SLAG))}";
        }
        #endregion

        #region Wood
        #region Wood Products
        public static class CHARCOAL
        {
            public static LocString NAME = FormatAsLink("Charcoal", nameof(CHARCOAL));
            public static LocString DESC = $"A coal-like material made mostly of carbon created from baking {FormatAsLink("Wood", Tags.Lumber.Name)} in a low-{FormatAsLink("Oxygen", nameof(global::STRINGS.ELEMENTS.OXYGEN))} environment.";
        }
        #endregion
        #region Arbor Wood
        public static class WOODARBOR
        {
            public static LocString NAME = FormatAsLink("Arbor Wood", nameof(WOODARBOR));
            public static LocString DESC = $"The raw wood of an {FormatAsLink("Arbor Tree", "FOREST_TREE")}.";
        }

        public static class LUMBERARBOR
        {
            public static LocString NAME = FormatAsLink("Arbor Lumber", nameof(LUMBERARBOR));
            public static LocString DESC = $"Cut lumber from {FormatAsLink("Arbor Wood", nameof(WOODARBOR))}.";
        }
        #endregion
        #endregion

        #region Building Blocks
        public static class BRICKRED
        {
            public static LocString NAME = FormatAsLink("Red Brick", nameof(BRICKRED));
            public static LocString DESC = $"The result of baking a formed block of {FormatAsLink("Clay", nameof(global::STRINGS.ELEMENTS.CLAY))}";
        }

        public static class BRICKGRAY
        {
            public static LocString NAME = FormatAsLink("Cinder Block", nameof(BRICKGRAY));
            public static LocString DESC = $"A light-weight and cheap block formed from {FormatAsLink("Cement", nameof(CEMENT))}, {FormatAsLink("Water", nameof(global::STRINGS.ELEMENTS.WATER))}, and left-over {FormatAsLink("Ash", nameof(ASH))}.";
        }

        public static class CONCRETE
        {
            public static LocString NAME = FormatAsLink("Concrete Block", nameof(CONCRETE));
            public static LocString DESC = $"A heavy-duty building material made up of {FormatAsLink("Cement", nameof(CEMENT))}, {FormatAsLink("Water", nameof(global::STRINGS.ELEMENTS.WATER))}, and an aggregate {FormatAsLink("Silt", "Silt")} with {FormatAsLink("Steel", nameof(global::STRINGS.ELEMENTS.STEEL))} reinforcements throughout.";
        }
        #endregion
    }
}
