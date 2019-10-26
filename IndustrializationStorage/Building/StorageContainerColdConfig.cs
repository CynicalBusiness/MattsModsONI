using PipLib;
using PipLib.Building;
using TUNING;
using System.Collections.Generic;

namespace MattsMods.Industrialization.Storage.Building
{
    [BuildingInfo.OnPlanScreen(ID, "Base", AfterId = StorageContainerConfig.ID)]
    [BuildingInfo.TechRequirement(ID, IndustrializationStorageMod.TECH_STORAGE2)]
    public class StorageContainerColdConfig : IBuildingConfig
    {
        public const string ID = "StorageContainerCold";

        public static readonly List<Tag> StorageFilters = new List<Tag>(PLUtil.ArrayConcat(
            TUNING.STORAGEFILTERS.NOT_EDIBLE_SOLIDS.ToArray(),
            TUNING.STORAGEFILTERS.FOOD.ToArray()
        ));

        private static readonly List<global::Storage.StoredItemModifier> StoredItemModifiers = new List<global::Storage.StoredItemModifier>() {
            global::Storage.StoredItemModifier.Hide,
            global::Storage.StoredItemModifier.Preserve,
            global::Storage.StoredItemModifier.Insulate,
            global::Storage.StoredItemModifier.Seal
        };

        public readonly Tag TAG = TagManager.Create(ID);

        private static readonly LogicPorts.Port[] INPUT_PORTS = new LogicPorts.Port[1]
        {
            LogicPorts.Port.InputPort(LogicOperationalController.PORT_ID, new CellOffset(-1, 1), STRINGS.BUILDINGS.PREFABS.STORAGECONTAINERCOLD.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.STORAGECONTAINERCOLD.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.STORAGECONTAINERCOLD.LOGIC_PORT_INACTIVE)
        };

        private static readonly LogicPorts.Port[] OUTPUT_PORTS = new LogicPorts.Port[1]
        {
            LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(1, 0), STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT_INACTIVE)
        };

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 6,
                height: 3,
                anim: "storage_container_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER3,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER6,
                construction_mass: new float[]{
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER4[0],
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0],
                    BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0]
                },
                construction_materials: new string[]{
                    GameTags.RefinedMetal.Name,
                    GameTags.Plastic.Name,
                    ElementLoader.FindElementByHash(SimHashes.Ceramic).tag.Name
                },
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER2,
                build_location_rule: BuildLocationRule.OnFloorOrBuildingAttachPoint,
                decor: DECOR.PENALTY.TIER3,
                noise: NOISE_POLLUTION.NOISY.TIER2
            );
            def.InputConduitType = ConduitType.Solid;
            def.attachablePosition = new CellOffset(0, 0);
            def.AttachmentSlotTag = StorageContainerConfig.TAG;
            def.AudioCategory = AUDIO.HOLLOW_METAL;
            def.RequiresPowerInput = true;
            def.ViewMode = OverlayModes.Power.ID;
            def.ExhaustKilowattsWhenActive = BUILDINGS.EXHAUST_ENERGY_ACTIVE.TIER2;
            def.EnergyConsumptionWhenActive = BUILDINGS.ENERGY_CONSUMPTION_WHEN_ACTIVE.TIER6;
            def.OverheatTemperature = BUILDINGS.OVERHEAT_TEMPERATURES.HIGH_2;
            def.PermittedRotations = PermittedRotations.FlipH;
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), TAG);
            Prioritizable.AddRef(go);

            var storage = go.AddOrGet<global::Storage>();
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = StorageFilters;
            storage.allowItemRemoval = true;
            storage.capacityKg = 12000;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = global::Storage.FetchCategory.GeneralStorage;
            storage.SetDefaultStoredItemModifiers(StoredItemModifiers);

            go.AddOrGet<EnergyConsumer>().powerSortOrder = 4; // TODO is there a TUNING enum for this?
            go.AddOrGet<StorageCold>();

            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = TAG;
            go.AddOrGet<StorageLocker>();
            go.AddOrGet<StorageContainer>();
            go.AddOrGet<LogicStorageSensor>();
            go.AddOrGet<DropAllWorkable>();

            go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[1]
            {
                new BuildingAttachPoint.HardPoint(new CellOffset(0, 3), StorageContainerConfig.TAG, null)
            };
        }

        public override void DoPostConfigurePreview(BuildingDef def, UnityEngine.GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, INPUT_PORTS, OUTPUT_PORTS);
        }

        public override void DoPostConfigureUnderConstruction(UnityEngine.GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, INPUT_PORTS, OUTPUT_PORTS);
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
            go.AddOrGetDef<PoweredActiveController.Def>();
            GeneratedBuildings.RegisterLogicPorts(go, INPUT_PORTS, OUTPUT_PORTS);
        }
    }
}
