using static STRINGS.UI;
using static MattsMods.IndustrializationFundementals.IndustrializationFundementalsMod;

namespace MattsMods.IndustrializationFundementals.STRINGS
{
    public static class BUILDINGS
    {

        public static class PREFABS
        {

            public static class STORAGESKIP
            {
                public static LocString NAME = FormatAsLink("Mining Skip", nameof(STORAGESKIP));
                public static LocString DESC = $"Efficent bulk storage of raw {FormatAsLink("Minerals", "RawMineral")}, {FormatAsLink("Ore", "Metal")}, {FormatAsLink("Gems", "Gems")}, and {FormatAsLink("Crystal", "Crystal")}";
                public static LocString EFFECT = "Bulk storage for raw materials.";
            }


            public static class STORAGECRATE
            {
                public static LocString NAME = FormatAsLink("Storage Crate", nameof(STORAGECRATE));
                public static LocString DESC = $"Efficient bulk storage of {FormatAsLink("Refined Metals", GameTags.RefinedMetal.Name)} and {FormatAsLink("Manufactured Materials", GameTags.ManufacturedMaterial.Name)}";
                public static LocString EFFECT = "Bulk storage for manufactured goods.";
            }

            public static class STORAGESILO
            {
                public static LocString NAME = FormatAsLink("Storage Silo", nameof(STORAGESILO));
                public static LocString DESC = $"Efficient bulk storage of {FormatAsLink("Powders", Tags.Powder.Name)}, {FormatAsLink("Silts", Tags.Silt.Name)}, and {FormatAsLink("Soil", GameTags.Farmable.Name)}";
                public static LocString EFFECT = "Bulk storage of loose materials.";
            }

            public static class STORAGECONTAINER
            {
                public static LocString NAME = FormatAsLink("Shipping Container", nameof(STORAGECONTAINER));
                public static LocString DESC = "A huge container designed to store resources in bulk. These containers can be stacked atop one-another, can be walked on, and have a ladder for easy access.";
                public static LocString EFFECT = "A large modular storage solution.";
            }

            public static class SAWMILL
            {
                public static LocString NAME = FormatAsLink("Sawmill", nameof(SAWMILL));
                public static LocString DESC = $"Processes {FormatAsLink("Wood", Tags.WoodLogs.Name)} into {FormatAsLink("Lumber", Tags.Lumber.Name)} and a small amount of {FormatAsLink("Sawdust", ElementConfig.Sawdust.ID + Element.State.Solid)}";
                public static LocString EFFECT = $"Initial processing station for {FormatAsLink("Wood", Tags.WoodLogs.Name)}.";
            }

            public static class SCAFFOLDING
            {
                public static LocString NAME = FormatAsLink("Scaffolding", nameof(SCAFFOLDING));
                public static LocString DESC = "A climable structure that can be easily removed in bulk. Great for temporary pathways during construction.";
                public static LocString EFFECT = "Desconstructing a scaffold will also deconstruct all scaffolds marked for deconstruct connected to it.";
            }

            #region Vanilla Overrides
            public static class KILN
            {
                public static LocString RECIPE_CHARCOAL_DESCRIPTION = "Processes {0} into {1} and a small amount of {2}.";
            }

            public static class METALREFINERY
            {
                public static LocString RECIPE_ALLOY2_DESCRIPTION = "Forges {0} from {1} and {2}.";
            }
            #endregion
        }

    }
}
