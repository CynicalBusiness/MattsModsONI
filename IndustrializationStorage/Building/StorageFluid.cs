
namespace MattsMods.Industrialization.Storage.Building
{
    public class StorageFluid : KMonoBehaviour
    {
        private static readonly EventSystem.IntraObjectHandler<StorageFluid> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<StorageFluid>(OnStorageChangeHandler);
        private static void OnStorageChangeHandler (StorageFluid comp, object data)
        {
            comp.OnStorageChange();
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            Subscribe((int)GameHashes.OnStorageChange, OnStorageChangeDelegate);
            OnStorageChange();
        }

        private void OnStorageChange ()
        {
            // todo set meter
        }
    }
}
