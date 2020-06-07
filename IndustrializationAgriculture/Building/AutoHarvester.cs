using KSerialization;
using UnityEngine;

namespace MattsMods.Industrialization.Agriculture
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class AutoHarvester : StateMachineComponent<SolidTransferArm.SMInstance>
    {

        public const string ARM_TARGET = "arm_target";

        private static readonly EventSystem.IntraObjectHandler<AutoHarvester> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<AutoHarvester>((AutoHarvester comp, object data) => comp.OnOperationalChanged(data));
        private static readonly EventSystem.IntraObjectHandler<AutoHarvester> OnEndChoreDelegate = new EventSystem.IntraObjectHandler<AutoHarvester>((AutoHarvester comp, object data) => comp.OnEndChore(data));

        [Serialize]
        public int range = AutoHarvesterConfig.DEFAULT_RANGE;

        [MyCmpReq]
        private Operational operational;

        [MyCmpGet]
        private KSelectable selectable;

        [MyCmpAdd]
        private Worker worker;

        [MyCmpAdd]
        private ChoreConsumer choreConsumer;

        [MyCmpAdd]
        private ChoreDriver choreDriver;

        private GameObject arm;
        private KBatchedAnimController armAnimController;
        private KAnimLink armAnimLink;
        private int serialNumber;

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();
            choreConsumer.AddProvider(GlobalChoreProvider.Instance);
            choreConsumer.SetReach(this.range);
            worker.usesMultiTool = false;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();

            var animController = GetComponent<KBatchedAnimController>();
            string armName = animController.name + ".arm";
            arm = new GameObject(armName);
            arm.SetActive(false);
            arm.transform.parent = animController.transform;
            arm.AddComponent<KPrefabID>().PrefabTag = new Tag(armName);

            armAnimController = arm.AddComponent<KBatchedAnimController>();
            armAnimController.AnimFiles = new KAnimFile[1]
            {
                animController.AnimFiles[0]
            };
            armAnimController.initialAnim = "arm";
            armAnimController.isMovable = true;
            armAnimController.sceneLayer = Grid.SceneLayer.TransferArm;
            animController.SetSymbolVisiblity(ARM_TARGET, false);

            var column = animController.GetSymbolTransform(ARM_TARGET, out var isVisible).GetColumn(3);
            column.z = Grid.GetLayerZ(Grid.SceneLayer.TransferArm);
            arm.transform.SetPosition(column);
            arm.SetActive(true);

            armAnimLink = new KAnimLink(animController, armAnimController);

            var choreGroups = Db.Get().ChoreGroups;
            for (int i = 0; i < choreGroups.Count; i++)
            {
                choreConsumer.SetPermittedByUser(choreGroups[i], true);
            }

            Subscribe((int)GameHashes.OperationalChanged, OnOperationalChangedDelegate);
            Subscribe((int)GameHashes.EndChore, OnEndChoreDelegate);

            // dunno why this is needed, but it is?
            animController.enabled = false;
            animController.enabled = true;

            MinionGroupProber.Get().SetValidSerialNos(this, serialNumber, serialNumber);
            smi.StartSM();
        }

        protected override void OnCleanUp()
        {
            MinionGroupProber.Get().ReleaseProber(this);
            base.OnCleanUp();
        }

        private void OnOperationalChanged (object data)
        {

        }

        private void OnEndChore (object data)
        {

        }

    }
}
