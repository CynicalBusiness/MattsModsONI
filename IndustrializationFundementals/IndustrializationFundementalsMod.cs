using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PipLib.Elements;
using PipLib.Mod;
using PipLib.Tech;
using PipLib.Building;

using MattsMods.IndustrializationFundementals.Building;

namespace MattsMods.IndustrializationFundementals
{
    public class IndustrializationFundementalsMod : PipMod
    {
        public static class Tags
        {
            public static readonly Tag Gems = TagManager.Create(nameof(Gems));
            public static readonly Tag Crystal = TagManager.Create(nameof(Crystal));
            public static readonly Tag Powder = TagManager.Create(nameof(Powder));
            public static readonly Tag Silt = TagManager.Create(nameof(Silt));
            public static readonly Tag WoodLogs = TagManager.Create(nameof(WoodLogs));
            public static readonly Tag Lumber = TagManager.Create(nameof(Lumber));
        }

        public override string Name => "Matt's Mods: Industrialization Fundementals";

        public override string Prefix => "MM:IndyFundementals";

        public const string TECH_STORAGE1 = "IndustrialStorageI";
        public const string TECH_STORAGE2 = "IndustrialStorageII";
        public const string TECH_CARPENTRY1 = "CarpentryI";

        internal static PipLib.Logging.ILogger ModLogger { get; private set; }

        public override void Load()
        {
            ModLogger = Logger;

            // register strings
            LocString.CreateLocStringKeys(typeof(STRINGS.BUILDINGS));
            LocString.CreateLocStringKeys(typeof(STRINGS.ELEMENTS));
            LocString.CreateLocStringKeys(typeof(STRINGS.RESEARCH));
            LocString.CreateLocStringKeys(typeof(STRINGS.MISC));

            GameTags.MaterialCategories.Add(Tags.Gems);
            GameTags.MaterialCategories.Add(Tags.Crystal);
            GameTags.MaterialCategories.Add(Tags.Silt);
            GameTags.MaterialCategories.Add(Tags.WoodLogs);
            GameTags.MaterialCategories.Add(Tags.Lumber);

            TUNING.STORAGEFILTERS.NOT_EDIBLE_SOLIDS.AddRange(new Tag[]{
                Tags.Gems,
                Tags.Crystal,
                Tags.Silt,
                Tags.WoodLogs,
                Tags.Lumber
            });
        }

        public override void Initialize()
        {
            // create techs
            var techStorage = TechTree.CreateTech(TECH_STORAGE1);
            TechTree.AddRequirement(techStorage, TechTree.GetTech("BasicRefinement"));

            var techStorage2 = TechTree.CreateTech(TECH_STORAGE2);
            TechTree.AddRequirement(techStorage2, TechTree.GetTech(techStorage.Id));
            TechTree.AddRequirement(techStorage2, TechTree.GetTech("Smelting"));

            var techCarpentry1 = TechTree.CreateTech(TECH_CARPENTRY1);
            TechTree.AddRequirement(techCarpentry1, TechTree.GetTech("BasicRefinement"));
        }

        public override void PostInitialize()
        {
            ElementManager.AddTags(new Dictionary<Element, Tag>(){
                {
                    // Sand is a powder
                    ElementLoader.FindElementByHash(SimHashes.Sand),
                    Tags.Silt
                },
                {
                    // Regolith is a silt
                    ElementLoader.FindElementByHash(SimHashes.Regolith),
                    Tags.Silt
                },
                {
                    // Fertilizer is a soil
                    ElementLoader.FindElementByHash(SimHashes.Fertilizer),
                    GameTags.Farmable
                },
                {
                    // Steel is an alloy
                    ElementLoader.FindElementByHash(SimHashes.Steel),
                    GameTags.Alloy
                },
                {
                    // Thermium is an alloy
                    ElementLoader.FindElementByHash(SimHashes.TempConductorSolid),
                    GameTags.Alloy
                }
            });

            ElementLoader.FindElementByHash(SimHashes.Diamond).tag = Tags.Gems;
            ElementLoader.FindElementByHash(SimHashes.Salt).tag = Tags.Silt;
        }
    }
}
