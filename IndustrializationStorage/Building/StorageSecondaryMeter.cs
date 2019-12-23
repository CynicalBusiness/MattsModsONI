using UnityEngine;

namespace MattsMods.Industrialization.Storage.Building
{
    public class StorageSecondaryMeter : KMonoBehaviour
    {

        public const string METER_NAME = "meter2";

        public static readonly int OnOriginalMeterUpdate = new HashedString(nameof(OnOriginalMeterUpdate)).HashValue;

        private static readonly EventSystem.IntraObjectHandler<StorageSecondaryMeter> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<StorageSecondaryMeter>(OnStorageChangeHandler);
        private static readonly EventSystem.IntraObjectHandler<StorageSecondaryMeter> OnOriginalMeterUpdateDelegate = new EventSystem.IntraObjectHandler<StorageSecondaryMeter>(OnOriginalMeterUpdateHandler);

        private static void OnStorageChangeHandler (StorageSecondaryMeter comp, object data)
        {
            comp.UpdateMeter();
        }

        private static void OnOriginalMeterUpdateHandler (StorageSecondaryMeter comp, object data)
        {
            comp.UpdateMeter();
        }

        public global::Storage storage;

        [MyCmpGet]
        private KBatchedAnimController animController;

        [MyCmpGet]
        private StorageLocker storageLocker;

        private MeterController storageMeter;

        protected override void OnPrefabInit ()
        {
            this.Subscribe((int)GameHashes.OnStorageChange, OnStorageChangeDelegate);
            this.Subscribe(OnOriginalMeterUpdate, OnOriginalMeterUpdateDelegate);
        }

        protected override void OnSpawn()
        {
            this.UpdateMeter();
        }

        private bool HasMeter {
            get
            {
                return this.storageMeter != null;
            }
        }

        private bool HasStorage
        {
            get
            {
                return this.storage != null;
            }
        }

        private float MaxStorage
        {
            get
            {
                return storageLocker != null
                    ? storageLocker.UserMaxCapacity
                    : HasStorage
                        ? storage.capacityKg
                        : 0;
            }
        }

        private float PercentFull {
            get
            {
                return HasStorage ? Mathf.Clamp01(storage.MassStored() / MaxStorage) : 0;
            }
        }

        private void UpdateMeter ()
        {
            if (!HasMeter)
            {
                this.CreateMeter();
            }
            storageMeter.SetPositionPercent(PercentFull);
        }

        private void CreateMeter ()
        {
            storageMeter = new MeterController(
                animController,
                METER_NAME + "_target",
                METER_NAME,
                Meter.Offset.Behind,
                Grid.SceneLayer.NoLayer,
                new string[2]
                {
                    METER_NAME + "_frame",
                    METER_NAME + "_level"
                }
            );
        }

    }
}
