using PipLib.Building;

namespace MattsMods.Industrialization.Logic.Building
{
    [BuildingInfo.OnPlanScreen(ID, "Automation", AfterId = LogicRibbonConfig.ID)]
    [BuildingInfo.TechRequirement(ID, "HighDensityAutomation")]
    public class LogicCableConfig : BaseLogicWireConfig
    {

        public static LogicPorts.Port CableInputPort(
            HashedString id,
            CellOffset cell_offset,
            string description,
            string activeDescription,
            string inactiveDescription,
            bool show_wire_missing_icon = false,
            bool display_custom_name = false)
        {
            return new LogicPorts.Port(
                id,
                cell_offset,
                description,
                activeDescription,
                inactiveDescription,
                show_wire_missing_icon,
                LogicPortSpriteType.RibbonInput, // TODO
                display_custom_name);
        }

        public static LogicPorts.Port CableOutputPort(
            HashedString id,
            CellOffset cell_offset,
            string description,
            string activeDescription,
            string inactiveDescription,
            bool show_wire_missing_icon = false,
            bool display_custom_name = false)
        {
            return new LogicPorts.Port(
                id,
                cell_offset,
                description,
                activeDescription,
                inactiveDescription,
                show_wire_missing_icon,
                LogicPortSpriteType.RibbonOutput, // TODO
                display_custom_name);
        }

        public const string ID = "LogicCable";

        public const LogicWire.BitDepth BIT_DEPTH = (LogicWire.BitDepth)3;
        public const int BITS = 32;

        public override BuildingDef CreateBuildingDef()
        {
            var def = base.CreateBuildingDef(
                id: ID,
                anim: "utilities_electric_insulated_kanim",
                construction_time: TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER2,
                construction_mass: TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2,
                decor: TUNING.DECOR.PENALTY.TIER2,
                noise: TUNING.NOISE_POLLUTION.NONE);
            def.BuildLocationRule = BuildLocationRule.NotInTiles;
            return def;
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            base.DoPostConfigureComplete(BIT_DEPTH, go);
        }
    }
}
