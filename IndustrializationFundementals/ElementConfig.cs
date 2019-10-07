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
    }
}
