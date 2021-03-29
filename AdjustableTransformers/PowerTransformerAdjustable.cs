using Harmony;
using KSerialization;
using STRINGS;
using UnityEngine;
using static MattsMods.AdjustableTransformers.STRINGS.UI;

namespace MattsMods.AdjustableTransformers
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class PowerTransformerAdjustable : KMonoBehaviour, IUserControlledCapacity
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

        public float Wattage => wattageVal;

        public float WattageRatio => Wattage / powerTransformer.BaseWattageRating;
        public float Efficiency => Traverse.Create(powerTransformer).Property<float>("Efficiency").Value;

        public float MinCapacity => 0;
        public float MaxCapacity => powerTransformer.BaseWattageRating;
        public bool WholeValues => true;
        public float AmountStored => powerTransformer.JoulesAvailable;

        public string SliderTitleKey => KEY + ".TITLE";
        public LocString CapacityUnits => UI.UNITSUFFIXES.ELECTRICAL.WATT;

        public float UserMaxCapacity {
            get {
                return wattageVal;
            }
            set {
                wattageVal = Mathf.Floor(value);
            }
        }

        public int SliderDecimalPlaces(int i)
        {
            return 8;
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
                UserMaxCapacity = GetPreferredDefaultWattage();
            }
            else if (wattageVal <= 1 && wattageVal > 0)
            {
                // legacy transformer settings
                UserMaxCapacity = wattageVal * GetPreferredDefaultWattage();
            }
        }

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
