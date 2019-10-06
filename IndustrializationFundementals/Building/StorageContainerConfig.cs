using System;
using TUNING;

namespace MattsMods.IndustrializationFundementals.Building
{

    public class StorageContainerConfig : IBuildingConfig
    {

        public const string ID = "StorageContainer";

        public readonly Tag TAG = TagManager.Create(ID);

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 6,
                height: 3,
                anim: "storage_container_kanim", // TODO
                hitpoints: BUILDINGS.HITPOINTS.TIER3,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER5,
                construction_mass: new float[]{
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0],
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0],
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
                },
                construction_materials: new string[]{
                    // Since most alloys are also refined metals, this leads to some interesting options
                    GameTags.Alloy.Name,
                    GameTags.RefinedMetal.Name,
                    GameTags.Plastic.Name
                },
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER2,
                build_location_rule: BuildLocationRule.OnFloorOrBuildingAttachPoint,
                decor: DECOR.PENALTY.TIER4,
                noise: NOISE_POLLUTION.NOISY.TIER0
            );
            def.InputConduitType = ConduitType.Solid;
            def.attachablePosition = new CellOffset(0, 0);
            def.AttachmentSlotTag = TAG;
            def.Floodable = false;
            def.Overheatable = false;
            def.AudioCategory = AUDIO.HOLLOW_METAL;
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            Prioritizable.AddRef(go);
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), TAG);
            var storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = TUNING.STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
            storage.allowItemRemoval = true;
            storage.capacityKg *= 10;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = TAG;
            go.AddOrGet<StorageLocker>();
            go.AddOrGet<StorageContainer>();
            go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[1]
            {
                new BuildingAttachPoint.HardPoint(new CellOffset(0, 3), TAG, null)
            };
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
        }
    }

}
