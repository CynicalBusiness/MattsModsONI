
using KSerialization;
using UnityEngine;

namespace MattsMods.Industrialization.Storage.Building
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class LogicStorageSensor : Switch, IThresholdSwitch, ISaveLoadable, ISim200ms
    {
        private static readonly EventSystem.IntraObjectHandler<LogicStorageSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicStorageSensor>(OnCopySettingsHandler);
        public static readonly EventSystem.IntraObjectHandler<LogicStorageSensor> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<LogicStorageSensor>(OnOperationalChangedHandler);
        private static readonly EventSystem.IntraObjectHandler<LogicStorageSensor> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<LogicStorageSensor>(OnStorageChangedHandler);

        private static void OnCopySettingsHandler (LogicStorageSensor comp, object data)
        {
            var other = ((GameObject) data).GetComponent<LogicStorageSensor>();
            if (other != null)
            {
                comp.OnCopySettings(other);
            }
        }
        private static void OnStorageChangedHandler (LogicStorageSensor comp, object data)
        {
            comp.OnStorageChanged();
        }

        public static void OnOperationalChangedHandler (LogicStorageSensor comp, object data)
        {
            comp.OnOperationalChanged();
        }

        [MyCmpGet]
        public Operational operational;

        [MyCmpReq]
        public global::Storage storage;

        [MyCmpReq]
        public LogicPorts logicPorts;

        [MyCmpAdd]
        public CopyBuildingSettings copyBuildingSettings;

        public float rangeMin = 0;
        public float rangeMax;
        public float rangeMultiplier = 1;

        public GameUtil.MetricMassFormat massFormat = GameUtil.MetricMassFormat.Kilogram;
        public LocString unit = global::STRINGS.UI.UNITSUFFIXES.MASS.KILOGRAM;

        private bool wasOn;

        [field:Serialize]
        public float Threshold { get; set; }

        [field:Serialize]
        public bool ActivateAboveThreshold { get; set; }

        public float CurrentValue => storage.MassStored();
        public float RangeMin => rangeMin;
        public float RangeMax => rangeMax;
        public LocString Title => global::STRINGS.UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TITLE;
        public LocString ThresholdValueName => STRINGS.UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.STORAGE;
        public string AboveToolTip => STRINGS.UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.STORAGE_TOOLTIP_ABOVE;
        public string BelowToolTip => STRINGS.UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.STORAGE_TOOLTIP_BELOW;
        public ThresholdScreenLayoutType LayoutType => ThresholdScreenLayoutType.SliderBar;
        public int IncrementScale => Mathf.RoundToInt(rangeMultiplier);
        public NonLinearSlider.Range[] GetRanges => NonLinearSlider.GetDefaultRange(RangeMax);

        public void Sim200ms (float delta)
        {
            if (ActivateAboveThreshold)
            {
                if ((CurrentValue >= Threshold && !IsSwitchedOn) || (CurrentValue < Threshold && IsSwitchedOn))
                {
                    Toggle();
                }
            }
            else
            {
                if ((CurrentValue >= Threshold && IsSwitchedOn) || (CurrentValue < Threshold && !IsSwitchedOn))
                {
                    Toggle();
                }
            }
        }

        public float GetRangeMinInputField ()
        {
            return RangeMin * rangeMultiplier;
        }

        public float GetRangeMaxInputField ()
        {
            return RangeMax * rangeMultiplier;
        }

        public string Format (float val, bool units)
        {
            return GameUtil.GetFormattedMass(mass: val, massFormat: massFormat, includeSuffix: units);
        }

        public float ProcessedSliderValue (float input)
        {
            // TODO range multiplier
            return input;
        }

        public float ProcessedInputValue (float input)
        {
            // TODO range multiplier
            return input;
        }

        public LocString ThresholdValueUnits ()
        {
            return unit;
        }

        protected override void OnPrefabInit ()
        {
            base.OnPrefabInit();
            Subscribe((int)GameHashes.CopySettings, OnCopySettingsDelegate);
            Subscribe((int)GameHashes.OnStorageChange, OnStorageChangedDelegate);
            rangeMax = storage.capacityKg;
            manuallyControlled = false;
        }

        protected override void OnSpawn ()
        {
            base.OnSpawn();
            OnToggle += OnSwitchToggled;
            UpdateLogicCircuit();
            UpdateVisualState(true);
            wasOn = IsSwitchedOn;
        }

        protected override void UpdateSwitchStatus()
        {
            GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
        }

        private void OnCopySettings (LogicStorageSensor other)
        {
            Threshold = other.Threshold;
            ActivateAboveThreshold = other.ActivateAboveThreshold;
        }

        private void OnSwitchToggled (bool newState)
        {
            UpdateLogicCircuit();
            UpdateVisualState(false);
        }

        private void OnStorageChanged ()
        {
            UpdateLogicCircuit();
            UpdateVisualState(false);
        }

        private void OnOperationalChanged ()
        {
            UpdateLogicCircuit();
            UpdateVisualState(false);
        }

        private void UpdateLogicCircuit ()
        {
            logicPorts.SendSignal(LogicSwitch.PORT_ID, switchedOn && (operational == null || operational.IsOperational) ? 1 : 0);
        }

        private void UpdateVisualState (bool force)
        {
            if (wasOn == switchedOn && !force)
            {
                return;
            }

            wasOn = switchedOn;

            KBatchedAnimController component = this.GetComponent<KBatchedAnimController>();
            component.Play(!this.switchedOn ? "on_pst" : "on_pre", KAnim.PlayMode.Once, 1f, 0.0f);
            component.Queue(!this.switchedOn ? "off" : "on", KAnim.PlayMode.Once, 1f, 0.0f);
        }
    }
}
