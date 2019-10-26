using KSerialization;
using System.Collections.Generic;
using UnityEngine;

namespace MattsMods.Industrialization.Storage.Building
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class StorageCold : KMonoBehaviour, ISaveLoadable, IIntSliderControl, ISim1000ms
    {
        public static readonly Operational.Flag OperationalFlagHasStorageItems = new Operational.Flag("HasStorageItems", Operational.Flag.Type.Functional);

        public static readonly EventSystem.IntraObjectHandler<StorageCold> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<StorageCold>(OnStorageChangedHandler);
        public static readonly EventSystem.IntraObjectHandler<StorageCold> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<StorageCold>(OnOperationalChangedHandler);
        public static readonly EventSystem.IntraObjectHandler<StorageCold> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<StorageCold>(OnCopySettingsHandler);

        public static void OnStorageChangedHandler (StorageCold comp, object data)
        {
            comp.OnStorageChanged();
        }

        public static void OnOperationalChangedHandler (StorageCold comp, object data)
        {
            comp.OnOperationalChanged();
        }

        public static void OnCopySettingsHandler (StorageCold comp, object data)
        {
            if (data != null)
            {
                comp.OnCopySettings(((GameObject) data).GetComponent<StorageCold>());
            }
        }

        [Serialize]
        public float temperatureCurrent = int.MaxValue;

        public float thermalEfficiency = 0.85f;
        public float energyMaxMultiplier = 0.1f;
        public float temperatureMin = 0;
        public float temperatureMax = 100 + Constants.CELSIUS2KELVIN;
        public float temperatureDefault = -4 + Constants.CELSIUS2KELVIN;

        [MyCmpReq]
        protected global::Storage storage;

        [MyCmpGet]
        protected global::Building building;

        [MyCmpGet]
        protected Operational operational;

        [MyCmpReq]
        protected EnergyConsumer energyConsumer;

        public string SliderUnits => GameUtil.GetTemperatureUnitSuffix();
        public string SliderTitleKey => $"STRINGS.BUILDINGS.PREFABS.{building.PrefabID().Name.ToUpper()}.NAME";

        private HandleVector<int>.Handle structureTemperature;

        public void Sim1000ms (float delta)
        {
            float toPull = GetCoolingEnergy(delta);
            energyConsumer.BaseWattageRating = Mathf.RoundToInt(toPull);
            if (toPull > 0)
            {
                GetCoolingEnergy(delta, doPull: true);
                GameComps.StructureTemperatures.ProduceEnergy(structureTemperature, toPull * 0.1f, global::STRINGS.BUILDING.STATUSITEMS.OPERATINGENERGY.EXHAUSTING, delta);
            }
        }

        public string GetSliderTooltip ()
        {
            return STRINGS.UI.UISIDESCREENS.STORAGE_COLD_SIDESCREEN.TOOLTIP;
        }

        public string GetSliderTooltipKey (int idx)
        {
            return "STRINGS.UI.UISIDESCREENS.STORAGE_COLD_SIDESCREEN.TOOLTIP";
        }

        public int SliderDecimalPlaces (int idx)
        {
            return 0;
        }

        public float GetSliderMin (int idx)
        {
            return GameUtil.GetConvertedTemperature(temperatureMin);
        }

        public float GetSliderMax (int idx)
        {
            return GameUtil.GetConvertedTemperature(temperatureMax);
        }

        public float GetSliderValue (int idx)
        {
            return GameUtil.GetConvertedTemperature(temperatureCurrent);
        }

        public void SetSliderValue (float val, int idx)
        {
            temperatureCurrent = GameUtil.GetTemperatureConvertedToKelvin(val);
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            structureTemperature = GameComps.StructureTemperatures.GetHandle(gameObject);

            Subscribe((int)GameHashes.CopySettings, OnCopySettingsDelegate);
            Subscribe((int)GameHashes.OperationalChanged, OnOperationalChangedDelegate);
            Subscribe((int)GameHashes.OnStorageChange, OnStorageChangedDelegate);

            if (temperatureCurrent == int.MaxValue)
            {
                temperatureCurrent = temperatureDefault;
            }
        }

        private void OnOperationalChanged ()
        {
            /* no-op */
        }

        private void OnStorageChanged ()
        {
            operational.SetFlag(OperationalFlagHasStorageItems, storage.MassStored() > 0);
            operational.SetActive(GetStorageHotMass() > 0);
        }

        private void OnCopySettings (StorageCold other)
        {
            temperatureCurrent = other.temperatureCurrent;
        }

        private float GetCoolingEnergy (float delta, bool doPull = false)
        {
            if (!operational.IsOperational || !operational.IsActive)
            {
                return 0;
            }

            float energyPulled = 0;
            var energyToPullPerKG = GetEnergyPerHotMass();
            if (energyToPullPerKG > 0)
            {
                energyToPullPerKG = Mathf.Min(energyToPullPerKG, GetAvailableCoolingEnergy() * energyMaxMultiplier);
                if (energyToPullPerKG > 0)
                {
                    foreach (var item in GetHotStorageItems())
                    {
                        var element = item.GetComponent<PrimaryElement>();

                        float energyToPullPerItem = energyToPullPerKG * element.Mass;
                        float deltaTemp = Mathf.Min(energyToPullPerItem, Mathf.Max(0, GameUtil.CalculateEnergyDeltaForElement(element, temperatureCurrent - 1, element.Temperature)));

                        energyPulled += deltaTemp;
                        if (doPull)
                        {
                            element.Temperature += GameUtil.CalculateTemperatureChange(
                                element.Element.specificHeatCapacity,
                                element.Mass,
                                -deltaTemp
                            );
                        }
                    }
                }
            }
            return energyPulled;
        }

        private float GetEnergyPerHotMass ()
        {
            var hotMass = GetStorageHotMass();
            return hotMass > 0 ? GetAvailableCoolingEnergy() / hotMass : 0;
        }

        private float GetStorageHotMass ()
        {
            if (storage.MassStored() <= 0)
            {
                return 0;
            }

            float hotMass = 0;

            foreach (var item in GetHotStorageItems())
            {
                hotMass += item.GetComponent<PrimaryElement>().Mass;
            }

            return hotMass;
        }

        private List<GameObject> GetHotStorageItems ()
        {
            return storage.items.FindAll(s => s.GetComponent<PrimaryElement>().Temperature > temperatureCurrent);
        }

        private float GetAvailableCoolingEnergy ()
        {
            return energyConsumer.WattsNeededWhenActive - building.Def.ExhaustKilowattsWhenActive;
        }
    }
}
