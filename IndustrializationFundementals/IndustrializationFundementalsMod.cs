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
        }

        public override void Initialize()
        {
            // create techs
            var techStorage = TechTree.CreateTech("IndustrialStorage");
            TechTree.AddRequirement(techStorage, TechTree.GetTech("BasicRefinement"));

            var techStorage2 = TechTree.CreateTech("HeavyStorage");
            TechTree.AddRequirement(techStorage2, TechTree.GetTech("Smelting"));
            TechTree.AddRequirement(techStorage2, TechTree.GetTech(techStorage.Id));

            var techCarpentry1 = TechTree.CreateTech("CarpentryI");
            TechTree.AddRequirement(techCarpentry1, TechTree.GetTech("BasicRefinement"));

            // add buildings to plan screen
            BuildingManager.AddToPlanMenu(StorageCrateConfig.ID, "Base", StorageLockerSmartConfig.ID);
            BuildingManager.AddToPlanMenu(StorageSiloConfig.ID, "Base", StorageCrateConfig.ID);
            BuildingManager.AddToPlanMenu(StorageSkipConfig.ID, "Base", StorageSiloConfig.ID);
            BuildingManager.AddToPlanMenu(StorageContainerConfig.ID, "Base", StorageSkipConfig.ID);
            BuildingManager.AddToPlanMenu(SawmillConfig.ID, "Refining", KilnConfig.ID);

            // add buildings to tech
            BuildingManager.AddToTech(StorageSkipConfig.ID, techStorage.Id);
            BuildingManager.AddToTech(StorageCrateConfig.ID, techStorage.Id);
            BuildingManager.AddToTech(StorageSiloConfig.ID, techStorage.Id);
            BuildingManager.AddToTech(StorageContainerConfig.ID, techStorage2.Id);
            BuildingManager.AddToTech(SawmillConfig.ID, techCarpentry1.Id);
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
