using System;
using System.Collections;

namespace MattsMods.Industrialization.Logic.Building
{

    public class LogicBundledEmitter : Switch, IPlayerControlledToggle
    {

        [MyCmpReq]
        protected global::Building building;

        public static readonly HashedString PORT_ID = nameof(LogicBundledEmitter);
        private System.Action firstFrameCallback = (System.Action) null;

        protected override void OnSpawn()
        {
            base.OnSpawn();
            this.UpdateVisualization();
            this.UpdateLogicCircuit();
        }

        protected override void Toggle()
        {
            base.Toggle();
            this.UpdateVisualization();
            this.UpdateLogicCircuit();
        }

        private void UpdateVisualization()
        {
            KBatchedAnimController component = this.GetComponent<KBatchedAnimController>();
            component.Play((HashedString) (this.switchedOn ? "on_pre" : "on_pst"), KAnim.PlayMode.Once, 1f, 0.0f);
            component.Queue((HashedString) (this.switchedOn ? "on" : "off"), KAnim.PlayMode.Once, 1f, 0.0f);
        }

        private void UpdateLogicCircuit()
        {
            var wire = Grid.Objects[building.GetCell(), (int)ObjectLayer.LogicWire];
            this.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn
                ? (int)Math.Pow(2, LogicWire.GetBitDepthAsInt(wire?.GetComponent<LogicWire>()?.GetMaxBitRating() ?? LogicWire.BitDepth.OneBit)) - 1
                : 0);
        }

        protected override void UpdateSwitchStatus()
        {
            this.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, this.switchedOn ? Db.Get().BuildingStatusItems.LogicSwitchStatusActive : Db.Get().BuildingStatusItems.LogicSwitchStatusInactive, (object) null);
        }

        public void SetFirstFrameCallback(System.Action ffCb)
        {
            this.firstFrameCallback = ffCb;
            this.StartCoroutine(this.RunCallback());
        }

        private IEnumerator RunCallback()
        {
            yield return (object) null;
            if (this.firstFrameCallback != null)
            {
                this.firstFrameCallback();
                this.firstFrameCallback = (System.Action) null;
            }
            yield return (object) null;
        }

        public void ToggledByPlayer()
        {
            this.Toggle();
        }

        public bool ToggledOn()
        {
            return this.switchedOn;
        }

        public KSelectable GetSelectable()
        {
            return this.GetComponent<KSelectable>();
        }

        public string SideScreenTitleKey
        {
            get
            {
                return "STRINGS.BUILDINGS.PREFABS.LOGICBUNDLEDEMITTER.SIDESCREEN_TITLE";
            }
        }
    }

}
