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

        public static class SLAGSOLID
        {
            public static LocString NAME = FormatAsLink("Slag", nameof(SLAGSOLID));
            public static LocString DESC = $"Left-over impurities and rock from the metal refining process.";
        }

        public static class CEMENTSOLID
        {
            public static LocString NAME = FormatAsLink("Cement", nameof(CEMENTSOLID));
            public static LocString DESC = $"A fine powder formed from baking {FormatAsLink("Lime", nameof(global::STRINGS.ELEMENTS.LIME))} and {FormatAsLink("Clay", nameof(global::STRINGS.ELEMENTS.CLAY))} with a secondary ingredient such as {FormatAsLink("Ash", nameof(ASHSOLID))} or {FormatAsLink("Slag", nameof(SLAGSOLID))}";
        }
        #endregion

        #region Wood
        #region Wood Products
        public static class CHARCOALSOLID
        {
            public static LocString NAME = FormatAsLink("Charcoal", nameof(CHARCOALSOLID));
            public static LocString DESC = $"A coal-like material made mostly of carbon created from baking {FormatAsLink("Wood", Tags.Lumber.Name)} in a low-{FormatAsLink("Oxygen", nameof(global::STRINGS.ELEMENTS.OXYGEN))} environment.";
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

        #region Building Blocks
        public static class BRICKREDSOLID
        {
            public static LocString NAME = FormatAsLink("Red Brick", nameof(BRICKREDSOLID));
            public static LocString DESC = $"The result of baking a formed block of {FormatAsLink("Clay", nameof(global::STRINGS.ELEMENTS.CLAY))}";
        }

        public static class BRICKGRAYSOLID
        {
            public static LocString NAME = FormatAsLink("Cinder Block", nameof(BRICKGRAYSOLID));
            public static LocString DESC = $"A light-weight and cheap block formed from {FormatAsLink("Cement", nameof(CEMENTSOLID))}, {FormatAsLink("Water", nameof(global::STRINGS.ELEMENTS.WATER))}, and left-over {FormatAsLink("Ash", nameof(ASHSOLID))}.";
        }

        public static class CONCRETESOLID
        {
            public static LocString NAME = FormatAsLink("Concrete Block", nameof(CONCRETESOLID));
            public static LocString DESC = $"A heavy-duty building material made up of {FormatAsLink("Cement", nameof(CEMENTSOLID))}, {FormatAsLink("Water", nameof(global::STRINGS.ELEMENTS.WATER))}, and an aggregate {FormatAsLink("Silt", "Silt")} with {FormatAsLink("Steel", nameof(global::STRINGS.ELEMENTS.STEEL))} reinforcements throughout.";
        }
        #endregion
    }
}
