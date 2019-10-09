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
                solid.attributes.Add(db => new Klei.AI.AttributeModifier(db.BuildingAttributes.Decor.Id, 0.2f, db.BuildingAttributes.Decor.Description, true));
                solid.attributes.Add(db => new Klei.AI.AttributeModifier(db.BuildingAttributes.OverheatTemperature.Id, -25f, db.BuildingAttributes.OverheatTemperature.Description));

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

        public class Tin : IElementConfig
        {
            public const string ID = "Tin";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);

                var solid = def.AddOrGetState(Element.State.Solid);
                solid.attributes.Add(db => new Klei.AI.AttributeModifier(db.BuildingAttributes.OverheatTemperature.Id, TUNING.BUILDINGS.OVERHEAT_TEMPERATURES.LOW_1));

                def.AddOrGetState(Element.State.Liquid);
                def.AddOrGetState(Element.State.Gas);
                def.color = new Color32(180, 180, 210, 255);
                return def;
            }
        }

        public class TinOre : IElementConfig
        {
            public const string ID = "TinOre";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);

                var solid = def.AddOrGetState(Element.State.Solid);
                solid.attributes.Add(db => new Klei.AI.AttributeModifier(db.BuildingAttributes.OverheatTemperature.Id, TUNING.BUILDINGS.OVERHEAT_TEMPERATURES.LOW_1));

                def.AddOrGetState(Element.State.Liquid);
                def.color = new Color32(100, 100, 120, 255);
                return def;
            }
        }

        public class Bronze : IElementConfig
        {
            public const string ID = "Bronze";

            public override ElementDef CreateElementDef()
            {
                var def = ElementTemplates.CreateElementDef(ID);

                var solid = def.AddOrGetState(Element.State.Solid);
                solid.attributes.Add(db => new Klei.AI.AttributeModifier(db.BuildingAttributes.OverheatTemperature.Id, TUNING.BUILDINGS.OVERHEAT_TEMPERATURES.HIGH_2));

                def.color = new Color32(250, 154, 80, 255);
                return def;
            }
        }
    }
}
