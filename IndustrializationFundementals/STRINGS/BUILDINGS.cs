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
                public static LocString DESC = "Bulk storage for raw materials.";
                public static LocString EFFECT = $"Efficent bulk storage of raw {FormatAsLink("Minerals", "RawMineral")}, {FormatAsLink("Ore", "Metal")}, {FormatAsLink("Gems", "Gems")}, and {FormatAsLink("Crystal", "Crystal")}";
            }


            public static class STORAGECRATE
            {
                public static LocString NAME = FormatAsLink("Storage Crate", nameof(STORAGECRATE));
                public static LocString DESC = "Bulk storage for manufactured goods.";
                public static LocString EFFECT = $"Efficient bulk storage of {FormatAsLink("Refined Metals", GameTags.RefinedMetal.Name)} and {FormatAsLink("Manufactured Materials", GameTags.ManufacturedMaterial.Name)}";
            }

            public static class STORAGESILO
            {
                public static LocString NAME = FormatAsLink("Storage Silo", nameof(STORAGESILO));
                public static LocString DESC = "Bulk storage of loose materials.";
                public static LocString EFFECT = $"Efficient bulk storage of {FormatAsLink("Powders", Tags.Powder.Name)}, {FormatAsLink("Silts", Tags.Silt.Name)}, and {FormatAsLink("Soil", GameTags.Farmable.Name)}";
            }
        }

    }
}
