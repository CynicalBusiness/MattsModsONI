using PipLib.Building;
using System.Collections.Generic;
using TUNING;

namespace MattsMods.IndustrialStorage.Building
{
    [BuildingInfo.TechRequirement(ID, Mod.TECH_STORAGE1)]
    [BuildingInfo.OnPlanScreen(ID, "Base", AfterId = StoragePalletConfig.ID)]
    public class StorageSiloConfig : IBuildingConfig
    {

        public const string ID = "StorageSilo";

        public static readonly Tag TAG = TagManager.Create(ID);

        public static readonly List<Tag> STORAGE_TAG = new List<Tag>()
        {
            GameTags.Farmable,
            GameTags.Filter
        };

        public override BuildingDef CreateBuildingDef ()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 2,
                height: 4,
                anim: "liquidreservoir_kanim", // TODO
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

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            var storage = go.AddOrGet<global::Storage>();
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = STORAGE_TAG;
            storage.allowItemRemoval = true;
            storage.capacityKg *= 6;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = global::Storage.FetchCategory.GeneralStorage;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = TAG;
            go.AddOrGet<StorageLocker>();
            go.AddOrGet<DropAllWorkable>();

            Prioritizable.AddRef(go);
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
        }

    }
}
