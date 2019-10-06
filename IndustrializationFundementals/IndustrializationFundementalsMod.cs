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
        }

        public override string Name => "Matt's Mods: Industrialization Fundementals";

        public override string Prefix => "MM:IndyFundementals";

        internal static PipLib.Logging.ILogger ModLogger { get; private set; }

        public override void Load()
        {
            ModLogger = Logger;
        }

        public override void Initialize()
        {
            // register strings
            LocString.CreateLocStringKeys(typeof(STRINGS.BUILDINGS));
            LocString.CreateLocStringKeys(typeof(STRINGS.RESEARCH));
            LocString.CreateLocStringKeys(typeof(STRINGS.MISC));

            // create techs
            var techStorage = TechTree.CreateTech("IndustrialStorage");
            TechTree.AddRequirement(techStorage, TechTree.GetTech("BasicRefinement"));

            // add buildings to their appropriate tech and plan screen
            BuildingManager.AddToPlanMenu(StorageCrateConfig.ID, "Base", "StorageLockerSmart");
            BuildingManager.AddToTech(StorageCrateConfig.ID, techStorage.Id);

            BuildingManager.AddToPlanMenu(StorageSiloConfig.ID, "Base", StorageCrateConfig.ID);
            BuildingManager.AddToTech(StorageSiloConfig.ID, techStorage.Id);

            BuildingManager.AddToPlanMenu(StorageSkipConfig.ID, "Base", StorageSiloConfig.ID);
            BuildingManager.AddToTech(StorageSkipConfig.ID, techStorage.Id);
        }

        public override void PostInitialize()
        {
            ElementManager.AddTags(new Dictionary<Element, Tag>(){
                {
                    // Sand is a powder
                    ElementLoader.FindElementByHash(SimHashes.Sand),
                    Tags.Powder
                },
                {
                    // Regolith is a silt
                    ElementLoader.FindElementByHash(SimHashes.Regolith),
                    Tags.Silt
                },
                {
                    // Salt is a silt
                    ElementLoader.FindElementByHash(SimHashes.Salt),
                    Tags.Silt
                },
                {
                    // Fertilizer is a soil
                    ElementLoader.FindElementByHash(SimHashes.Fertilizer),
                    GameTags.Farmable
                }
            });

            ElementLoader.FindElementByHash(SimHashes.Diamond).tag = Tags.Gems;
        }
    }
}
