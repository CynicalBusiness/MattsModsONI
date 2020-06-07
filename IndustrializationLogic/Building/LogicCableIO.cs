using System;
using KSerialization;

namespace MattsMods.Industrialization.Logic.Building
{

    [SerializationConfig(MemberSerialization.OptIn)]
    public class LogicCableIO : KMonoBehaviour, ILogicBundledBitSelector, IRender200ms
    {
        public static readonly HashedString PORT_INPUT_READER_ID = new HashedString("LogicCableIOReader");
        public static readonly HashedString PORT_OUTPUT_READER_ID = new HashedString("LogicCableIOReader");

        public static readonly HashedString PORT_INPUT_WRITER_ID = new HashedString("LogicCableIOWriter");
        public static readonly HashedString PORT_OUTPUT_WRITER_ID = new HashedString("LogicCableIOWriter");

        public int bitDepth = 32;

        public int bitMultiplier = 4;

        public bool isReader = true;

        [Serialize]
        public int selectedBit = 0;
        [Serialize]
        private int currentValue = 0;

        public string SideScreenTitle => $"STRINGS.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.CABLE_{(isReader ? "READER" : "WRITER")}_TITLE";
        public string SideScreenDescription => isReader ? Strings.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.CABLE_READER_DESC : Strings.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.CABLE_WRITER_DESC;
        public string BundleTitle => Strings.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.BUNDLED_BIT_TITLE;
        public int BundleSize => bitMultiplier;
        public int BundleMask => (int)(Math.Pow(2, bitMultiplier)) - 1;
        public HashedString InputPortID => isReader ? PORT_INPUT_READER_ID : PORT_INPUT_WRITER_ID;
        public HashedString OutputPortID => isReader ? PORT_OUTPUT_READER_ID : PORT_OUTPUT_WRITER_ID;

        protected override void OnSpawn()
        {
            base.OnSpawn();
            Subscribe<LogicCableIO>((int) GameHashes.LogicEvent, new EventSystem.IntraObjectHandler<LogicCableIO>((comp, data) => comp.OnLogicValueChanged(data)));
        }

        public void Render200ms(float delta)
        {
            UpdateVisuals();
        }

        public string GetBundleState (int active)
        {
            return active > 0 ? active == BundleMask ? Strings.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.BUNDLED_BIT_STATE_ACTIVE : Strings.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.BUNDLED_BIT_STATE_ACTIVE_PARTIAL : Strings.UI.UISIDESCREENS.LOGICBITSELECTORSIDESCREEN.BUNDLED_BIT_STATE_INACTIVE;
        }

        public void OnLogicValueChanged(object data)
        {
            var logicValueChanged = (LogicValueChanged)data;
            if (logicValueChanged.portID == InputPortID && currentValue != logicValueChanged.newValue)
            {
                currentValue = logicValueChanged.newValue;
                UpdateLogicCircuit();
                UpdateVisuals();
            }
        }

        public void SetBitSelection (int bit)
        {
            selectedBit = bit;
            UpdateLogicCircuit();
        }

        public int GetBitSelection ()
        {
            return selectedBit;
        }

        public int GetBitDepth ()
        {
            return bitDepth / bitMultiplier;
        }

        public bool SideScreenDisplayWriterDescription()
        {
            return false;
        }

        public bool SideScreenDisplayReaderDescription()
        {
            return true;
        }

        public bool IsBitActive (int bit)
        {
            return GetBitsAtSelection(bit) > 0;
        }

        public int GetInputValue ()
        {
            return GetComponent<LogicPorts>()?.GetInputValue(InputPortID) ?? 0;
        }

        public int GetOutputValue ()
        {
            return GetComponent<LogicPorts>()?.GetOutputValue(OutputPortID) ?? 0;
        }

        public void UpdateVisuals ()
        {
            // TODO
        }

        public int GetBitsAtSelection (int selection)
        {
            return isReader ? (currentValue >> (selection * bitMultiplier)) & BundleMask : currentValue << (selection * bitMultiplier);
        }

        private void UpdateLogicCircuit()
        {
            GetComponent<LogicPorts>()?.SendSignal(OutputPortID, GetBitsAtSelection(selectedBit));
        }
    }

}
