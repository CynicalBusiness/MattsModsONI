
namespace MattsMods.Industrialization.Logic.Building
{

    public interface ILogicBundledBitSelector : ILogicRibbonBitSelector
    {

        string BundleTitle { get; }

        int BundleSize { get; }

        int BundleMask { get; }

        string GetBundleState (int active);

        int GetBitsAtSelection (int selection);

    }

}
