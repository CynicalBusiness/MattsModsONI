using Harmony;
using KSerialization;
using STRINGS;

namespace MattsMods.AdjustableTransformers
{
    public class PowerTransformerAdjustable : KMonoBehaviour, ISim200ms, ISingleSliderControl
    {

        public const string KEY = "STRINGS.UI.UISIDESCREENS.POWERTRANSFORMERWATTAGESIDESCREEN";

        public readonly LocString TOOLTIP = new LocString("Select Maximum Wattage", KEY + ".TOOLTIP");
        public readonly LocString TITLE = new LocString("Wattage", KEY + ".TITLE");

        /// <summary>
        /// The value this slider is currently set to
        /// </summary>
        [field: Serialize]
        public float WattageVal { get; set; }

        [MyCmpGet]
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
            return TOOLTIP.key.ToString();
        }

        public string GetSliderTooltip()
        {
            return $"Transformer output wattage will be capped at {UI.PRE_KEYWORD}{WattageVal} kW{UI.PST_KEYWORD}.";
        }

        public string SliderTitleKey => TITLE.key.ToString();
        public string SliderUnits => " kW";

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
