using static STRINGS.UI;

namespace MattsMods.IndustrialStorage.STRINGS
{
    public static class BUILDINGS
    {

        public static class PREFABS
        {

            public static class STORAGESKIP
            {
                public static LocString NAME = FormatAsLink("Mining Skip", nameof(STORAGESKIP));
                public static LocString DESC = $"Efficent bulk storage of raw {FormatAsLink("Minerals", "RawMineral")}, {FormatAsLink("Ore", "Metal")}, {FormatAsLink("Gems", "Gems")}, and {FormatAsLink("Crystal", "Crystal")}";
                public static LocString EFFECT = "Bulk storage for raw materials.";
            }


            public static class STORAGEPALLET
            {
                public static LocString NAME = FormatAsLink("Storage Pallet", nameof(STORAGEPALLET));
                public static LocString DESC = $"An empty pallet for bulk {FormatAsLink("Refined Metals", GameTags.RefinedMetal.Name)}, {FormatAsLink("Manufactured Materials", GameTags.ManufacturedMaterial.Name)}, and other manufactured goods.";
                public static LocString EFFECT = "Bulk storage for manufactured goods.";
            }

            public static class STORAGESILO
            {
                public static LocString NAME = FormatAsLink("Storage Silo", nameof(STORAGESILO));
                public static LocString DESC = $"Efficient bulk storage of {FormatAsLink("Powders", "Powder")}, {FormatAsLink("Silts", "Silt")}, and {FormatAsLink("Soil", GameTags.Farmable.Name)}";
                public static LocString EFFECT = "Bulk storage of loose materials.";
            }

            public static class STORAGECONTAINER
            {
                public static LocString NAME = FormatAsLink("Shipping Container", nameof(STORAGECONTAINER));
                public static LocString DESC = $"A huge container designed to store resources in bulk. These containers can be stacked atop one-another, can be walked on, and have a ladder for easy access as well as a input port for a {(global::STRINGS.BUILDINGS.PREFABS.SOLIDCONDUIT.NAME.text)}.";
                public static LocString EFFECT = "A large modular solid storage solution.";
                public static LocString LOGIC_PORT = "Storage Threshold";
        public static LocString LOGIC_PORT_ACTIVE = $"Sends a {(FormatAsAutomationState("Green Signal", AutomationState.Active))} when storage threshold is met";
        public static LocString LOGIC_PORT_INACTIVE = $"Otherwise, sends a {(FormatAsAutomationState("Red Signal", AutomationState.Standby))}.";
            }

            public static class STORAGECONTAINERCOLD
            {
                public static LocString NAME = FormatAsLink("Insulated Shipping Container", nameof(STORAGECONTAINERCOLD));
                public static LocString DESC = $"A variant of the {(STORAGECONTAINER.NAME.text)} that is insulated and has a configurable cooler to keep it below a set temperature. Heat removed from storage is pulled into the container itself.";
                public static LocString EFFECT = "A large modular and insulated solid storage solution.";
                public static LocString LOGIC_PORT = "Enable/Disable Cooling";
                public static LocString LOGIC_PORT_ACTIVE = FormatAsLink("Green Signal", AutomationState.Active.ToString()) + ": Cooling Active";
                public static LocString LOGIC_PORT_INACTIVE = FormatAsLink("Red Signal", AutomationState.Standby.ToString()) + ": Cooling Inactive";
            }

            public static class STORAGEGAS
            {
                public static LocString NAME = FormatAsLink("High-Pressure Gas Tank", nameof(STORAGEGAS));
                public static LocString DESC = $"A container that holds {FormatAsLink("Gasses", "ELEMENTS_GAS")} at high pressures to increase space efficiency. Can be stacked.";
                public static LocString EFFECT = "A large modular gas storage solution.";
                public static LocString LOGIC_PORT = "Enable/Disable Output";
                public static LocString LOGIC_PORT_ACTIVE = FormatAsLink("Green Signal", AutomationState.Active.ToString()) + ": Allow output";
                public static LocString LOGIC_PORT_INACTIVE = FormatAsLink("Red Signal", AutomationState.Standby.ToString()) + ": Prevent output";
            }

            public static class STORAGELIQUID
            {
                public static LocString NAME = FormatAsLink("Heavy Liquid Tank", nameof(STORAGELIQUID));
                public static LocString DESC = $"A container that holds {FormatAsLink("Liquids", "ELEMENTS_LIQUID")} under pressure to increase space efficiency. Can be stacked.";
                public static LocString EFFECT = "A large modular liquid storage solution.";
            }

            public static class ICEBOX
            {
                public static LocString NAME = FormatAsLink("Icebox", nameof(ICEBOX));
                public static LocString DESC = $"A larger and more space efficient variant of the {(global::STRINGS.BUILDINGS.PREFABS.REFRIGERATOR.NAME.text)} with a few more options.";
                public static LocString EFFECT = "Larger bulk storage solution for food.";
            }
        }

    }
}
