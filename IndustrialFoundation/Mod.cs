using System.Collections.Generic;
using PipLib.Elements;
using PipLib.Mod;
using PipLib.Tech;

namespace MattsMods.IndustrialFoundation
{
    public class Mod : PipMod
    {
        public static class Tags
        {
            public static readonly Tag Gem = TagManager.Create("Gem");
            public static readonly Tag Crystal = TagManager.Create("Crystal");
            public static readonly Tag Powder = TagManager.Create("Powder");
            public static readonly Tag Silt = TagManager.Create("Silt");
            public static readonly Tag Log = TagManager.Create("Log");
            public static readonly Tag RawLumber = TagManager.Create("RawLumber");
            public static readonly Tag Waste = TagManager.Create("Waste");
        }

        public static class Techs
        {
            public const string CHEMICAL_SYNTHESIS = "ChemicalSynthesis";
        }

        public override string Name => "Matt's Mods: Industrial Foundation";

        public override string Prefix => "MMIFoundation";

        internal static PipLib.Logging.ILogger ModLogger { get; private set; }

        public override void Load()
        {
            ModLogger = Logger;

            // register strings
            LocString.CreateLocStringKeys(typeof(STRINGS.BUILDINGS));
            LocString.CreateLocStringKeys(typeof(STRINGS.ELEMENTS));
            LocString.CreateLocStringKeys(typeof(STRINGS.MISC));

            GameTags.MaterialCategories.Add(Tags.Gem);
            GameTags.MaterialCategories.Add(Tags.Crystal);
            GameTags.MaterialCategories.Add(Tags.Silt);
            GameTags.MaterialCategories.Add(Tags.Powder);
            GameTags.MaterialCategories.Add(Tags.Log);
            GameTags.MaterialCategories.Add(Tags.RawLumber);
            GameTags.MaterialCategories.Add(Tags.Waste);

            TUNING.STORAGEFILTERS.NOT_EDIBLE_SOLIDS.AddRange(new Tag[]{
                Tags.Gem,
                Tags.Crystal,
                Tags.Silt,
                Tags.Powder,
                Tags.Log,
                Tags.RawLumber,
                Tags.Waste
            });
            TUNING.CROPS.CROP_TYPES.Add(new Crop.CropVal("LogArbor", 2700f, 300, true));
        }

        public override void Initialize()
        {
            // create techs
            var techChemSynth = TechTree.CreateTech(Techs.CHEMICAL_SYNTHESIS);
            TechTree.AddRequirement(techChemSynth, TechTree.GetTech("Plastics"));


            // var techCarpentry1 = TechTree.CreateTech(TECH_CARPENTRY1);
            // TechTree.AddRequirement(techCarpentry1, TechTree.GetTech("BasicRefinement"));
            // TechTree.AddRequirement(techCarpentry1, TechTree.GetTech("Agriculture"));
        }

        public override void PostInitialize()
        {
            ElementManager.AddBulkTags(new Dictionary<Element, Tag[]>(){
                {
                    // Sand is a powder
                    ElementLoader.FindElementByHash(SimHashes.Sand),
                    new Tag[] { Tags.Powder }
                },
                {
                    // Regolith is a silt
                    ElementLoader.FindElementByHash(SimHashes.Regolith),
                    new Tag[] { Tags.Silt }
                },
                {
                    // Steel is an alloy
                    ElementLoader.FindElementByHash(SimHashes.Steel),
                    new Tag[] { GameTags.Alloy }
                },
                {
                    // Thermium is an alloy
                    ElementLoader.FindElementByHash(SimHashes.TempConductorSolid),
                    new Tag[] { GameTags.Alloy }
                }
            });

            ElementLoader.FindElementByHash(SimHashes.Diamond).tag = Tags.Gem;
            ElementLoader.FindElementByHash(SimHashes.Salt).tag = Tags.Silt;
        }
    }
}
