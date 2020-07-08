using System.Collections.Generic;

namespace MattsMods.IndustrialAutomation.Network
{
    public class SignalOverlayMode : OverlayModes.Mode
    {
        public const string ID = "Signal";
        public static HashSet<Tag> HighlightItemIDs = new HashSet<Tag>();

        public override HashedString ViewMode()
        {
            return ID;
        }

        public override string GetSoundName()
        {
            return ID;
        }
    }
}
