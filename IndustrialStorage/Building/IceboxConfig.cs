
using PipLib.Building;
using TUNING;
using System.Collections.Generic;

namespace MattsMods.IndustrialStorage.Building
{

    [BuildingInfo.OnPlanScreen(ID, "Food", AfterId = RefrigeratorConfig.ID)]
    [BuildingInfo.TechRequirement(ID, Mod.TECH_STORAGE1)]
    public class IceboxConfig : IBuildingConfig
    {

        public const string ID = "Icebox";

        private static readonly List<LogicPorts.Port> OUTPUT_PORTS = new List<LogicPorts.Port>()
        {
            LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(1, 0), STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT, STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT_ACTIVE, STRINGS.BUILDINGS.PREFABS.STORAGECONTAINER.LOGIC_PORT_INACTIVE)
        };

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 3,
                height: 2,
                anim: "storage_skip_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER1,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER3,
                construction_mass: new float[]{ BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0], BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0] },
                construction_materials: new string[]{
                    GameTags.Metal.Name,
                    ElementLoader.FindElementByHash(SimHashes.Ceramic).tag.Name
                },
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER0,
                build_location_rule: BuildLocationRule.OnFloorOrBuildingAttachPoint,
                decor: BUILDINGS.DECOR.PENALTY.TIER3,
                noise: NOISE_POLLUTION.NOISY.TIER1
            );
            def.AudioCategory = AUDIO.HOLLOW_METAL;
            def.EnergyConsumptionWhenActive = BUILDINGS.ENERGY_CONSUMPTION_WHEN_ACTIVE.TIER5;
            def.PermittedRotations = PermittedRotations.FlipH;
            def.RequiresPowerInput = true;
            def.ViewMode = OverlayModes.Power.ID;
            def.OverheatTemperature = BUILDINGS.OVERHEAT_TEMPERATURES.HIGH_1;
            def.LogicOutputPorts = OUTPUT_PORTS;
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            Prioritizable.AddRef(go);

            var storage = go.AddOrGet<global::Storage>();
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = new List<Tag>(
                PipLib.PLUtil.ArrayConcat(STORAGEFILTERS.FOOD.ToArray(),
                new Tag[]{ GameTags.Liquifiable }));
            storage.allowItemRemoval = true;
            storage.capacityKg = 800;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = global::Storage.FetchCategory.GeneralStorage;
            storage.SetDefaultStoredItemModifiers(global::Storage.StandardInsulatedStorage);

            go.AddOrGet<EnergyConsumer>().powerSortOrder = 4; // TODO is there a TUNING enum for this?
            go.AddOrGet<StorageCold>();

            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = prefab_tag;
            go.AddOrGet<StorageLocker>();
            go.AddOrGet<LogicStorageSensor>();
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
        }
    }
}
