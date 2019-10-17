using static STRINGS.UI;

namespace MattsMods.Industrialization.Storage.STRINGS
{

    public static class RESEARCH
    {

        public static class TECHS
        {

            public static class INDUSTRIALSTORAGEI
            {
                public static LocString NAME = FormatAsLink("Bulk Storage", nameof(INDUSTRIALSTORAGEI));
                public static LocString DESC = "Larger structures designed for specialized bulk storage.";
            }

            public static class INDUSTRIALSTORAGEII
            {
                public static LocString NAME = FormatAsLink("Industrial Storage", nameof(INDUSTRIALSTORAGEII));
                public static LocString DESC = "Huge industrial storage options for when most of the colony would otherwise be storage bins.";
            }

            public static class COMPRESSEDFLUIDSTORAGE
            {
                public static LocString NAME = FormatAsLink("Fluid Compression", nameof(COMPRESSEDFLUIDSTORAGE));
                public static LocString DESC = "Compression of fluids for more efficient bulk storage.";
            }

        }

    }

}
