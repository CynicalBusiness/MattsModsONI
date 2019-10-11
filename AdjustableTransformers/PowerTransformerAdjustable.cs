using Harmony;
using KSerialization;
using STRINGS;
using static MattsMods.AdjustableTransformers.STRINGS.UI;

namespace MattsMods.AdjustableTransformers
{
    public class PowerTransformerAdjustable : KMonoBehaviour, ISim200ms, ISingleSliderControl, ISliderControl
    {

        public const string KEY = "STRINGS.UI.UISIDESCREENS.POWERTRANSFORMERWATTAGESIDESCREEN";

        [MyCmpReq]
        public PowerTransformer powerTransformer;

        /// <summary>
        /// The value this slider is currently set to
        /// </summary>
        [Serialize]
        public float wattageVal = int.MaxValue;

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
            wattageVal = val;
        }

        public void SetWattage (float watts)
        {
            wattageVal = watts / 1000f;
        }

        protected override void OnSpawn ()
        {
            wattageVal = powerTransformer.BaseCapacity;
        }

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
    }
}
