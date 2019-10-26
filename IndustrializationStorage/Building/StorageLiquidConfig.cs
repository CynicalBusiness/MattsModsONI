using PipLib.Building;
using TUNING;
using System.Collections.Generic;
using UnityEngine;

namespace MattsMods.Industrialization.Storage.Building
{
    [BuildingInfo.OnPlanScreen(ID, "Base", AfterId = GasReservoirConfig.ID)]
    [BuildingInfo.TechRequirement(ID, IndustrializationStorageMod.TECH_STORAGE_FLUIDS)]
    public class StorageLiquidConfig : IBuildingConfig
    {
        public const string ID = "StorageLiquid";
        public const int KG_PER_TILE = 1200;
        private const ConduitType CONDUIT = ConduitType.Liquid;

        public static readonly Tag TAG = TagManager.Create(ID);

        public static readonly List<global::Storage.StoredItemModifier> StoredItemModifiers = new List<global::Storage.StoredItemModifier>()
        {
            global::Storage.StoredItemModifier.Hide,
            global::Storage.StoredItemModifier.Seal
        };

        private static readonly LogicPorts.Port[] INPUT_PORTS = new LogicPorts.Port[1]
        {
            LogicPorts.Port.InputPort(LogicOperationalController.PORT_ID, new CellOffset(-1, 1), STRINGS.BUILDINGS.PREFABS.STORAGEGAS.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.STORAGEGAS.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.STORAGEGAS.LOGIC_PORT_INACTIVE)
        };

        private static readonly LogicPorts.Port[] OUTPUT_PORTS = new LogicPorts.Port[1]
        {
            LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(1, 0), global::STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.LOGICPRESSURESENSORGAS.LOGIC_PORT_INACTIVE)
        };

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 3,
                height: 3,
                anim: "storage_skip_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER1,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER3,
                construction_mass: new float[]{ BUILDINGS.CONSTRUCTION_MASS_KG.TIER5[0], BUILDINGS.CONSTRUCTION_MASS_KG.TIER1[0] },
                construction_materials: new string[]{ MATERIALS.REFINED_METALS[0], MATERIALS.PLASTICS[0] },
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER0,
                build_location_rule: BuildLocationRule.OnFloorOrBuildingAttachPoint,
                decor: BUILDINGS.DECOR.PENALTY.TIER3,
                noise: NOISE_POLLUTION.NOISY.TIER1
            );
            def.InputConduitType = CONDUIT;
            def.OutputConduitType = CONDUIT;
            def.Floodable = false;
            def.Entombable = true;
            def.ViewMode = OverlayModes.LiquidConduits.ID;
            def.AudioCategory = AUDIO.HOLLOW_METAL;
            def.UtilityInputOffset = new CellOffset(-1, 1);
            def.UtilityOutputOffset = new CellOffset(1, 0);
            def.PermittedRotations = PermittedRotations.FlipH;
            def.attachablePosition = new CellOffset(1, 0);
            def.AttachmentSlotTag = TAG;
            return def;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), TAG);
            var buildingDef = go.GetComponent<global::Building>().Def;

            var storage = go.AddOrGet<global::Storage>();
            storage.showDescriptor = true;
            storage.storageFilters = STORAGEFILTERS.GASES;
            storage.capacityKg = buildingDef.WidthInCells * buildingDef.HeightInCells * KG_PER_TILE;
            storage.SetDefaultStoredItemModifiers(StoredItemModifiers);

            var consumer = go.AddOrGet<ConduitConsumer>();
            consumer.conduitType = CONDUIT;
            consumer.ignoreMinMassCheck = true;
            consumer.forceAlwaysSatisfied = true;
            consumer.alwaysConsume = true;
            consumer.capacityKG = storage.capacityKg;

            var dispenser = go.AddOrGet<ConduitDispenser>();
            dispenser.conduitType = CONDUIT;
            dispenser.elementFilter = null;

            var storageSensor = go.AddOrGet<LogicStorageSensor>();
            storageSensor.massFormat = GameUtil.MetricMassFormat.Tonne;
            storageSensor.unit = global::STRINGS.UI.UNITSUFFIXES.MASS.TONNE;

            go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
            {
                new BuildingAttachPoint.HardPoint(new CellOffset(1, buildingDef.HeightInCells), TAG, null)
            };

            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery);
            go.AddOrGet<StorageFluid>();
            go.AddOrGet<LogicOperationalController>();
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, INPUT_PORTS, OUTPUT_PORTS);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, INPUT_PORTS, OUTPUT_PORTS);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
            GeneratedBuildings.RegisterLogicPorts(go, INPUT_PORTS, OUTPUT_PORTS);
        }
    }
}
