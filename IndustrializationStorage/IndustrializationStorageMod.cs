
using PipLib.Mod;
using PipLib.Tech;

namespace MattsMods.Industrialization.Storage
{
    public sealed class IndustrializationStorageMod : PipMod
    {

        public override string Name => "Industrialization: Heavy Storage";

        public override string Prefix => "MM:Indy:Storage";

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
            TechTree.AddRequirement(techStorageFluids, TechTree.GetTech("HVAC"));
            TechTree.AddRequirement(techStorageFluids, TechTree.GetTech("LiquidTemperature"));
            TechTree.AddRequirement(techStorageFluids, techStorage);
        }

    }
}
