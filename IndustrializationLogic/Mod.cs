using PipLib.Mod;
using PipLib.Tech;

namespace MattsMods.Industrialization.Logic
{
    public sealed class Mod : PipMod
    {
        public const int MAX_WIRE_TYPES = 4;

        public const int MAX_RELEVANT_BRIDGES = MAX_WIRE_TYPES;

        public const int MAX_WIRE_BIT_DEPTH = 32;

        public const string TECH_HIGHDENSITYAUTOMATION = "HighDensityAutomation";

        public override string Name => "Industrialization: Logic";
        public override string Prefix => "MM:IL";

        public override void Initialize()
        {
            var highDensityAutomation = TechTree.CreateTech(TECH_HIGHDENSITYAUTOMATION);
            TechTree.AddRequirement(highDensityAutomation, TechTree.GetTech("ParallelAutomation"));
            TechTree.AddRequirement(highDensityAutomation, TechTree.GetTech("DupeTrafficControl"));
        }

        public override void Load()
        {
            LocString.CreateLocStringKeys(typeof(Strings.BUILDINGS));
            LocString.CreateLocStringKeys(typeof(Strings.RESEARCH));
        }

    }
}
