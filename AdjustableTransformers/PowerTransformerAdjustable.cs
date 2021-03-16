using Harmony;
using KSerialization;
using STRINGS;
using UnityEngine;
using static MattsMods.AdjustableTransformers.STRINGS.UI;

namespace MattsMods.AdjustableTransformers
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class PowerTransformerAdjustable : KMonoBehaviour, ISingleSliderControl, ISliderControl
    {
        private static readonly EventSystem.IntraObjectHandler<PowerTransformerAdjustable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<PowerTransformerAdjustable>(OnCopySettings);

        private static void OnCopySettings (PowerTransformerAdjustable comp, object data)
        {
            comp.OnCopySettings(data);
        }

        public const string KEY = "STRINGS.UI.UISIDESCREENS.POWERTRANSFORMERWATTAGESIDESCREEN";

        [MyCmpReq]
        public PowerTransformer powerTransformer;

        [MyCmpAdd]
        public CopyBuildingSettings copyBuildingSettings;

        /// <summary>
        /// The value this slider is currently set to
        /// </summary>
        [Serialize]
        public float wattageVal = int.MaxValue;

        public float preferredDefaultWattage = int.MaxValue;

        // public float Wattage => wattageVal * 1000f;
        public float Wattage => wattageVal;
        public float WattageRatio => Wattage / powerTransformer.BaseWattageRating;
        public float Efficiency => Traverse.Create(powerTransformer).Property<float>("Efficiency").Value;

        public int SliderDecimalPlaces(int i)
        {
            return 8;
        }

        public float GetSliderMin(int i)
        {
            return 0;
        }

        public float GetSliderMax(int i)
        {
            // return powerTransformer.BaseWattageRating / 1000f;
            return powerTransformer.BaseWattageRating;
        }

        public float GetSliderValue(int i)
        {
            return wattageVal;
        }

        public string GetSliderTooltipKey(int i)
        {
            return KEY + ".TOOLTIP";
        }

        public string GetSliderTooltip()
        {
            return string.Format(UISIDESCREENS.POWERTRANSFORMERWATTAGESIDESCREEN.TOOLTIP, wattageVal, SliderUnits);
        }

        public string SliderTitleKey => KEY + ".TITLE";
        public string SliderUnits => UI.UNITSUFFIXES.ELECTRICAL.KILOWATT;

        public void SetSliderValue(float val, int i)
        {
            SetWattage(val);
        }

        public void SetWattage (float watts)
        {
            // wattageVal = watts / 1000f;
            wattageVal = Mathf.Floor(watts);
        }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            Subscribe<PowerTransformerAdjustable>((int)GameHashes.CopySettings, OnCopySettingsDelegate);
        }

        protected override void OnSpawn ()
        {
            if (wattageVal == int.MaxValue)
            {
                SetWattage(GetPreferredDefaultWattage());
            }
            else if (wattageVal <= 1 && wattageVal > 0)
            {
                // legacy transformer settings
                SetWattage(wattageVal * GetPreferredDefaultWattage());
            }
        }

        /*
        public void Sim200ms(float delta)
        {
            var pt = Traverse.Create(powerTransformer);
            var battery = pt.Field<Battery>("battery").Value;
            battery.capacity = wattageVal * 1000f;
            // battery.chargeWattage = WattageVal * 1000f;
            if (battery.JoulesAvailable > battery.Capacity)
            {
                battery.ConsumeEnergy(battery.JoulesAvailable - battery.Capacity, false);
            }
        }
        */

        private float GetPreferredDefaultWattage ()
        {
            return Mathf.Min(powerTransformer.BaseWattageRating, preferredDefaultWattage);
        }

        internal void OnCopySettings (object data)
        {
            PowerTransformerAdjustable comp = ((UnityEngine.GameObject)data).GetComponent<PowerTransformerAdjustable>();
            if (comp != null)
            {
                this.wattageVal = comp.wattageVal;
            }
        }
    }
}
