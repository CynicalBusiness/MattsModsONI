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
                public static LocString EFFECT = $"Efficent bulk storage of raw {FormatAsLink("Minerals", "RawMineral")}, {FormatAsLink("Ore", "Metal")}, {FormatAsLink("Gems", "Gems")}, and {FormatAsLink("Crystal", "Crystal")}";
                public static LocString DESC = "A big bin to put all your rocks in.";
            }


            public static class STORAGEPALLET
            {
                public static LocString NAME = FormatAsLink("Storage Pallet", nameof(STORAGEPALLET));
                public static LocString EFFECT = $"An empty pallet for bulk {FormatAsLink("Refined Metals", GameTags.RefinedMetal.Name)}, {FormatAsLink("Manufactured Materials", GameTags.ManufacturedMaterial.Name)}, and other industrial goods.";
                public static LocString DESC = "Nobody said Duplicants were particularly good at stacking things.";
            }

            public static class STORAGESILO
            {
                public static LocString NAME = FormatAsLink("Storage Silo", nameof(STORAGESILO));
                public static LocString EFFECT = $"Efficient bulk storage of {FormatAsLink("Powders", "Powder")}, {FormatAsLink("Silts", "Silt")}, and {FormatAsLink("Soil", GameTags.Farmable.Name)}";
                public static LocString DESC = "Don't question how things are kept separate. Duplicants find a way.";
            }

            public static class STORAGECONTAINER
            {
                public static LocString NAME = FormatAsLink("Shipping Container", nameof(STORAGECONTAINER));
                public static LocString EFFECT = $"A huge container designed to store just about anything in bulk. These can be stacked atop one-another, can be walked on, have a ladder for easy access, and ports for a {(global::STRINGS.BUILDINGS.PREFABS.SOLIDCONDUIT.NAME.text)}.";
                public static LocString DESC = "The ultimate storage solution.";
                public static LocString LOGIC_PORT = "Storage Threshold";
                public static LocString LOGIC_PORT_ACTIVE = $"Sends a {(FormatAsAutomationState("Green Signal", AutomationState.Active))} when storage threshold is met";
                public static LocString LOGIC_PORT_INACTIVE = $"Otherwise, sends a {(FormatAsAutomationState("Red Signal", AutomationState.Standby))}.";
            }

            public static class STORAGECONTAINERCOLD
            {
                public static LocString NAME = FormatAsLink("Climate-Controlled Shipping Container", nameof(STORAGECONTAINERCOLD));
                public static LocString EFFECT = $"An insulated variant of the {(STORAGECONTAINER.NAME.text)} that also features a fully-configurable cooler to keep it below a set temperature. Heat removed from storage is pulled into the container itself.";
                public static LocString DESC = "The ultimate storage solution, now with a cooling unit stuck to the side. Ya know, in case you wanted to box up that glacier you found.";
                public static LocString LOGIC_PORT = "Enable/Disable Cooling";
                public static LocString LOGIC_PORT_ACTIVE = FormatAsLink("Green Signal", AutomationState.Active.ToString()) + ": Cooling Active";
                public static LocString LOGIC_PORT_INACTIVE = FormatAsLink("Red Signal", AutomationState.Standby.ToString()) + ": Cooling Inactive";
            }

            public static class STORAGEGAS
            {
                public static LocString NAME = FormatAsLink("High-Pressure Gas Tank", nameof(STORAGEGAS));
                public static LocString EFFECT = $"A container that holds {FormatAsLink("Gasses", "ELEMENTS_GAS")} at high pressures to increase space efficiency. Can be stacked.";
                public static LocString DESC = "Caution: high-pressure gasses can be hazardous to Duplicant eardrums.";
                public static LocString LOGIC_PORT = "Enable/Disable Output";
                public static LocString LOGIC_PORT_ACTIVE = FormatAsLink("Green Signal", AutomationState.Active.ToString()) + ": Allow output";
                public static LocString LOGIC_PORT_INACTIVE = FormatAsLink("Red Signal", AutomationState.Standby.ToString()) + ": Prevent output";
            }

            public static class STORAGELIQUID
            {
                public static LocString NAME = FormatAsLink("Heavy Liquid Tank", nameof(STORAGELIQUID));
                public static LocString EFFECT = $"A container that holds {FormatAsLink("Liquids", "ELEMENTS_LIQUID")} under pressure to increase space efficiency. Can be stacked.";
                public static LocString DESC = "Did you know spheres are the most energy-efficient 3D shape?";
            }

            public static class ICEBOX
            {
                public static LocString NAME = FormatAsLink("Icebox", nameof(ICEBOX));
                public static LocString EFFECT = $"A larger and more space efficient variant of the {(global::STRINGS.BUILDINGS.PREFABS.REFRIGERATOR.NAME.text)} with a few more options.";
                public static LocString DESC = "It's like a fridge, but bigger.";
            }
        }

    }
}
