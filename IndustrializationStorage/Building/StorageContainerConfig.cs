using PipLib.Building;
using TUNING;
using System.Collections.Generic;

namespace MattsMods.Industrialization.Storage.Building
{
    [BuildingInfo.TechRequirement(ID, IndustrializationStorageMod.TECH_STORAGE2)]
    [BuildingInfo.OnPlanScreen(ID, "Base", AfterId = StorageSkipConfig.ID)]
    public class StorageContainerConfig : IBuildingConfig
    {

        public const string ID = "StorageContainer";

        public static readonly Tag TAG = TagManager.Create(ID);

        private static readonly LogicPorts.Port OUTPUT_PORT = LogicPorts.Port.OutputPort(
            LogicSwitch.PORT_ID,
            new CellOffset(1, 0),
            STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT,
            STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT_ACTIVE,
            STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT_INACTIVE);

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
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0],
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0],
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
                },
                construction_materials: new string[]{
                    GameTags.Steel.Name,
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
            def.LogicOutputPorts = new List<LogicPorts.Port>(){OUTPUT_PORT};
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            // BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), TAG);

            var storage = go.AddOrGet<global::Storage>();
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = TUNING.STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
            storage.allowItemRemoval = true;
            storage.capacityKg *= 10;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = global::Storage.FetchCategory.GeneralStorage;

            go.AddOrGet<StorageLocker>();
            go.AddOrGet<StorageContainer>();
            go.AddOrGet<LogicStorageSensor>();
            go.AddOrGet<DropAllWorkable>();

            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = TAG;
            go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[1]
            {
                new BuildingAttachPoint.HardPoint(new CellOffset(0, 3), TAG, null)
            };
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
            Prioritizable.AddRef(go);
        }
    }

}
