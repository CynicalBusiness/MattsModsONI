using System;
using PipLib.Mod;
using PipLib.Tech;
using Harmony;
using UnityEngine;

namespace MattsMods.IndustrialAutomation
{
    public class Mod : PipMod
    {
        public const string TECH_SIGNALNETWORKS1 = "SignalNetworks1";

        public override string Name => "Matt's Mods: Industrial Automation";

        public override string Prefix => "MMIAutomation";

        public override void Load()
        {
            // strings
            LocString.CreateLocStringKeys(typeof(Strings.RESEARCH));
            LocString.CreateLocStringKeys(typeof(Strings.UI));
        }

        public override void Initialize()
        {
            var techItems = Db.Get().TechItems;
            techItems.AddTechItem(
                "AutomationOverlay",
                STRINGS.RESEARCH.OTHER_TECH_ITEMS.AUTOMATION_OVERLAY.NAME, // TODO
                STRINGS.RESEARCH.OTHER_TECH_ITEMS.AUTOMATION_OVERLAY.DESC,
                Traverse.Create(techItems)
                    .Method("GetSpriteFnBuilder")
                    .GetValue<Func<string, bool, Sprite>>(techItems, "overlay_logic"));

            // techs
            var techLogicCircuits = TechTree.GetTech("LogicCircuits");
            var techSignalNetworks1 = TechTree.CreateTech(TECH_SIGNALNETWORKS1);
            TechTree.AddRequirement(techSignalNetworks1, techLogicCircuits);
            TechTree.AddTechItem(TECH_SIGNALNETWORKS1, Building.SignalWireConfig.ID);
        }
    }
}
