using PipLib.Building;
using TUNING;
using System.Collections.Generic;

namespace MattsMods.IndustrialStorage.Building
{
    [BuildingInfo.TechRequirement(ID, Mod.TECH_STORAGE1)]
    [BuildingInfo.OnPlanScreen(ID, "Base", AfterId = StorageSiloConfig.ID)]
    public class StorageSkipConfig : IBuildingConfig
    {

        public const string ID = "StorageSkip";

        public static readonly Tag TAG = TagManager.Create(ID);

        public static readonly List<Tag> STORAGE_TAG = new List<Tag>()
        {
            GameTags.BuildableRaw,
            GameTags.Metal,
            GameTags.ConsumableOre
        };

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 3,
                height: 2,
                anim: "storageSkip_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER2,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER2,
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER5,
                construction_materials: MATERIALS.RAW_METALS,
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1,
                build_location_rule: BuildLocationRule.OnFloor,
                decor: DECOR.PENALTY.TIER3,
                noise: NOISE_POLLUTION.NOISY.TIER0
            );
            def.Floodable = false;
            def.AudioCategory = AUDIO.METAL;
            def.Overheatable = false;
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag tag)
        {
            Prioritizable.AddRef(go);
            var storage = go.AddOrGet<global::Storage>();
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = STORAGE_TAG;
            storage.allowItemRemoval = true;
            storage.capacityKg *= 5;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = global::Storage.FetchCategory.GeneralStorage;

            go.AddOrGet<StorageSecondaryMeter>().storage = storage;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = TAG;

            go.AddOrGet<StorageLocker>();
            go.AddOrGet<DropAllWorkable>();
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
        }
    }

}
