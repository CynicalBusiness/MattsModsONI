
using PipLib.Mod;
using PipLib.Tech;

namespace MattsMods.IndustrialStorage
{
    public sealed class Mod : PipMod
    {

        public override string Name => "Industrial Storage";

        public override string Prefix => "MMIStorage";

        public const string TECH_STORAGE1 = "IndustrialStorageI";
        public const string TECH_STORAGE2 = "IndustrialStorageII";
        public const string TECH_STORAGE_FLUIDS = "CompressedFluidStorage";

        public override void Load()
        {
            LocString.CreateLocStringKeys(typeof(STRINGS.BUILDINGS));
            LocString.CreateLocStringKeys(typeof(STRINGS.RESEARCH));
            LocString.CreateLocStringKeys(typeof(STRINGS.UI));
        }

        public override void Initialize()
        {
            // create techs
            var techStorage = TechTree.CreateTech(TECH_STORAGE1);
            TechTree.AddRequirement(techStorage, TechTree.GetTech("BasicRefinement"));

            var techStorage2 = TechTree.CreateTech(TECH_STORAGE2);
            TechTree.AddRequirement(techStorage2, techStorage);
            TechTree.AddRequirement(techStorage2, TechTree.GetTech("Smelting"));

            var techStorageFluids = TechTree.CreateTech(TECH_STORAGE_FLUIDS);
            TechTree.AddRequirement(techStorageFluids, techStorage);
            TechTree.AddRequirement(techStorageFluids, TechTree.GetTech("HVAC"));
            TechTree.AddRequirement(techStorageFluids, TechTree.GetTech("LiquidTemperature"));
        }

    }
}
