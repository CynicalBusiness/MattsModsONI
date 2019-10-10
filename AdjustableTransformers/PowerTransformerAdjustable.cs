using Harmony;
using KSerialization;
using STRINGS;
using System.Diagnostics;
using static MattsMods.AdjustableTransformers.STRINGS.UI;

namespace MattsMods.AdjustableTransformers
{
    public class PowerTransformerAdjustable : KMonoBehaviour, ISim200ms, ISingleSliderControl
    {

        public const string KEY = "STRINGS.UI.UISIDESCREENS.POWERTRANSFORMERWATTAGESIDESCREEN";

        /// <summary>
        /// The value this slider is currently set to
        /// </summary>
        [Serialize]
        public float WattageVal { get; set; }

        [MyCmpReq]
        public PowerTransformer powerTransformer;

        public int SliderDecimalPlaces(int i)
        {
            return 1;
        }

        public float GetSliderMin(int i)
        {
            return 0;
        }

        public float GetSliderMax(int i)
        {
            return powerTransformer.BaseWattageRating / 1000f;
        }

        public float GetSliderValue(int i)
        {
            return WattageVal;
        }

        public string GetSliderTooltipKey(int i)
        {
            return KEY + ".TOOLTIP";
        }

        public string GetSliderTooltip()
        {
            return string.Format(UISIDESCREENS.POWERTRANSFORMERWATTAGESIDESCREEN.TOOLTIP, WattageVal, SliderUnits);
        }

        public string SliderTitleKey => KEY + ".TITLE";
        public string SliderUnits => UI.UNITSUFFIXES.ELECTRICAL.KILOWATT;

        public void SetSliderValue(float val, int i)
        {
            WattageVal = val;
        }

        public void SetWattage (float watts)
        {
            WattageVal = watts / 1000f;
        }

        public void Sim200ms(float delta)
        {
            var pt = Traverse.Create(powerTransformer);
            var battery = pt.Field<Battery>("battery").Value;
            battery.capacity = WattageVal * 1000f;
            // battery.chargeWattage = WattageVal * 1000f;
            if (battery.JoulesAvailable > battery.Capacity)
            {
                battery.ConsumeEnergy(battery.JoulesAvailable - battery.Capacity, false);
            }
        }
    }
}
