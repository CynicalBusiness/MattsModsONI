using KSerialization;
using static MattsMods.AdjustableCoolers.STRINGS.UI;

namespace MattsMods.AdjustableCoolers
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class AirConditionerAdjustable : KMonoBehaviour, ISingleSliderControl, ISliderControl
    {
        public const float MAX_DELTA = -20f;
        public const float TEMP_MODIFIER_F = 1.8f;

        private static readonly EventSystem.IntraObjectHandler<AirConditionerAdjustable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<AirConditionerAdjustable>(OnCopySettings);

        private static void OnCopySettings (AirConditionerAdjustable comp, object data)
        {
            comp.OnCopySettings(data);
        }

        public const string KEY = "STRINGS.UI.UISIDESCREENS.AIRCONDITIONERTEMPERATURESIDESCREEN";

        [MyCmpReq]
        public AirConditioner airConditioner;

        [MyCmpReq]
        public EnergyConsumer energyConsumer;

        [MyCmpAdd]
        public CopyBuildingSettings copyBuildingSettings;

        /// <summary>
        /// The value this slider is currently set to
        /// </summary>
        [Serialize]
        public float coolingDelta = int.MaxValue;

        public int SliderDecimalPlaces(int i)
        {
            return 10;
        }

        public float GetSliderMin(int i)
        {
            return MAX_DELTA * (GameUtil.temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit ? TEMP_MODIFIER_F : 1);
        }

        public float GetSliderMax(int i)
        {
            return 0;
        }

        public float GetSliderValue(int i)
        {
            return coolingDelta * (GameUtil.temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit ? TEMP_MODIFIER_F : 1);
        }

        public string GetSliderTooltipKey(int i)
        {
            return KEY + ".TOOLTIP";
        }

        public string GetSliderTooltip()
        {
            return string.Format(UISIDESCREENS.AIRCONDITIONERTEMPERATURESIDESCREEN.TOOLTIP, coolingDelta, SliderUnits);
        }

        public string SliderTitleKey => KEY + ".TITLE";
        public string SliderUnits => GameUtil.GetTemperatureUnitSuffix();

        public void SetSliderValue(float val, int i)
        {
            coolingDelta = GameUtil.temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit ? val / TEMP_MODIFIER_F : val;
            Update();
        }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            Subscribe<AirConditionerAdjustable>((int)GameHashes.CopySettings, OnCopySettingsDelegate);
        }

        protected override void OnSpawn ()
        {
            if (coolingDelta == int.MaxValue)
            {
                coolingDelta = airConditioner.temperatureDelta;
            }
            Update();
        }

        internal void OnCopySettings (object data)
        {
            AirConditionerAdjustable comp = ((UnityEngine.GameObject)data).GetComponent<AirConditionerAdjustable>();
            if (comp != null)
            {
                this.coolingDelta = comp.coolingDelta;
            }
        }

        internal void Update ()
        {
            var ratio = coolingDelta / MAX_DELTA;

            #if DEBUG
                Debug.LogFormat("Updated temperature delta: {0}C ({1})", coolingDelta, ratio);
            #endif
            airConditioner.temperatureDelta = coolingDelta;
            energyConsumer.BaseWattageRating = energyConsumer.WattsNeededWhenActive * ratio;
        }
    }
}
