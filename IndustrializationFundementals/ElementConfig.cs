using PipLib.Elements;
using Klei.AI;
using UnityEngine;

namespace MattsMods.IndustrializationFundementals
{
    public static class ElementConfig
    {
        public const string PREFIX_WOOD = "Wood";
        public const string PREFIX_LUMBER = "Lumber";

        public class Charcoal : IElementConfig
        {
            public const string ID = "Charcoal";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);
                def.AddOrGetState(Element.State.Solid).anim = "carbon_kanim";
                def.color = new Color32(50, 50, 50, 255);
                return def;
            }
        }

        public class Ash : IElementConfig
        {
            public const string ID = "Ash";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);
                def.AddOrGetState(Element.State.Solid).anim = "limestone_kanim";
                def.color = new Color32(200, 200, 200, 255);
                return def;
            }
        }

        public class Sawdust : IElementConfig
        {
            public const string ID = "Sawdust";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);
                def.AddOrGetState(Element.State.Solid).anim = "limestone_kanim";
                def.color = new Color32(204, 164, 121, 255);
                return def;
            }
        }

        public class WoodArbor : IElementConfig
        {
            public const string ID = PREFIX_WOOD + "Arbor";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);

                var solid = def.AddOrGetState(Element.State.Solid);
                solid.attributes.Add(db => new AttributeModifier(db.BuildingAttributes.Decor.Id, 0.2f, db.BuildingAttributes.Decor.Description, true));
                solid.attributes.Add(db => new AttributeModifier(db.BuildingAttributes.OverheatTemperature.Id, -25f, db.BuildingAttributes.OverheatTemperature.Description));

                TUNING.CROPS.CROP_TYPES.Add(new Crop.CropVal(def.GetStateID(Element.State.Solid), 2700f, 300, true));

                def.color = new Color32(117, 72, 40, 255);
                return def;
            }

        }

        public class LumberArbor : IElementConfig
        {
            public const string ID = PREFIX_LUMBER + "Arbor";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);
                def.AddOrGetState(Element.State.Solid);
                def.color = new Color32(204, 164, 121, 255);
                return def;
            }
        }

        public class BrickRed : IElementConfig
        {
            public const string ID = "BrickRed";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);

                var solid = def.AddOrGetState(Element.State.Solid);
                solid.attributes.Add(db => new AttributeModifier(db.BuildingAttributes.OverheatTemperature.Id, TUNING.BUILDINGS.OVERHEAT_TEMPERATURES.HIGH_2));
                // solid.attributes.Add(db => new AttributeModifier(db.BuildingAttributes.Decor.Id, TUNING.BUILDINGS.DECOR_MATERIAL_MOD.HIGH_1, is_multiplier: true));

                def.color = new Color32(255, 115, 87, 255);
                return def;
            }
        }

        public class BrickGray : IElementConfig
        {
            public const string ID = "BrickGray";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);

                var solid = def.AddOrGetState(Element.State.Solid);
                solid.attributes.Add(db => new AttributeModifier(db.BuildingAttributes.OverheatTemperature.Id, TUNING.BUILDINGS.OVERHEAT_TEMPERATURES.HIGH_2));

                def.color = new Color32(210, 210, 210, 255);
                return def;
            }
        }

        public class Concrete : IElementConfig
        {
            public const string ID = "Concrete";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);

                var solid = def.AddOrGetState(Element.State.Solid);
                solid.attributes.Add(db => new AttributeModifier(db.BuildingAttributes.OverheatTemperature.Id, TUNING.BUILDINGS.OVERHEAT_TEMPERATURES.HIGH_2));

                def.color = new Color32(230, 230, 230, 255);
                return def;
            }
        }

        public class Slag : IElementConfig
        {
            public const string ID = "Slag";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);
                def.AddOrGetState(Element.State.Solid);
                def.color = new Color32(220, 180, 180, 255);
                return def;
            }
        }

        public class Cement : IElementConfig
        {
            public const string ID = "Cement";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);
                def.AddOrGetState(Element.State.Solid);
                def.color = new Color32(200, 200, 200, 255);
                return def;
            }
        }
    }
}
