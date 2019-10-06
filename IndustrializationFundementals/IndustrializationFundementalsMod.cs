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

            var techStorage2 = TechTree.CreateTech("HeavyStorage");
            TechTree.AddRequirement(techStorage2, TechTree.GetTech("Smelting"));
            TechTree.AddRequirement(techStorage2, TechTree.GetTech("SmartStorage"));

            // add buildings to their appropriate tech and plan screen
            BuildingManager.AddToPlanMenu(StorageCrateConfig.ID, "Base", "StorageLockerSmart");
            BuildingManager.AddToTech(StorageCrateConfig.ID, techStorage.Id);

            BuildingManager.AddToPlanMenu(StorageSiloConfig.ID, "Base", StorageCrateConfig.ID);
            BuildingManager.AddToTech(StorageSiloConfig.ID, techStorage.Id);

            BuildingManager.AddToPlanMenu(StorageSkipConfig.ID, "Base", StorageSiloConfig.ID);
            BuildingManager.AddToTech(StorageSkipConfig.ID, techStorage.Id);

            BuildingManager.AddToPlanMenu(StorageContainerConfig.ID, "Base", StorageSkipConfig.ID);
            BuildingManager.AddToTech(StorageContainerConfig.ID, techStorage2.Id);
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
        }
    }
}
