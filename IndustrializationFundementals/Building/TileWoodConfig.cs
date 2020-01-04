using System.IO;
using System;
using TUNING;
using PipLib.Building;
using UnityEngine;

namespace MattsMods.IndustrializationFundementals.Building
{

    [BuildingInfo.OnPlanScreen(ID, "Base")]
    public class TileWoodConfig : IBuildingConfig
    {

        public static readonly int BlockTileConnectorID = Hash.SDBMLower("tiles_carpet_tops");
        public const string ID = "TileWood";

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: ID,
                width: 1,
                height: 1,
                anim: "floor_carpet_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER2,
                construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER2,
                construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER2,
                construction_materials: new string[1]{ IndustrializationFundementalsMod.Tags.WoodLogs.Name },
                melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1,
                build_location_rule: BuildLocationRule.Tile,
                decor: BUILDINGS.DECOR.BONUS.TIER1,
                noise: NOISE_POLLUTION.NONE
            );
            BuildingTemplates.CreateFoundationTileDef(def);
            def.Floodable = false;
            def.Overheatable = false;
            def.Entombable = false;
            def.UseStructureTemperature = false;
            def.AudioCategory = AUDIO.METAL;
            def.AudioSize = "small"; // what's the constant for this?
            def.BaseTimeUntilRepair = -1f; // same here
            def.SceneLayer = Grid.SceneLayer.TileMain;
            def.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
            def.isKAnimTile = true;
            def.isSolidTile = true;
            def.BlockTileAtlas = GetCustomBlockTileAtlas("tiles_wood"); // this is the important bit, see method below
            def.BlockTilePlaceAtlas = Assets.GetTextureAtlas("tiles_carpet_place"); // if you want custom place/tops, these need to change too
            def.BlockTileMaterial = Assets.GetMaterial("tiles_solid");
            def.DecorBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_carpet_tops_decor_info"); // absolutely no clue what these two do or how to change them
            def.DecorPlaceBlockTileInfo = Assets.GetBlockTileDecorInfo("tiles_carpet_tops_decor_place_info");
            return def;
        }

        public override void ConfigureBuildingTemplate(UnityEngine.GameObject go, Tag prefab_tag)
        {
            GeneratedBuildings.MakeBuildingAlwaysOperational(go);
            var sco = go.AddOrGet<SimCellOccupier>();
            sco.doReplaceElement = true;
            sco.movementSpeedMultiplier = DUPLICANTSTATS.MOVEMENT.BONUS_1;
            go.AddOrGet<TileTemperature>();
            go.AddOrGet<KAnimGridTileVisualizer>().blockTileConnectorID = BlockTileConnectorID;
            go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
        }

        public override void DoPostConfigureComplete(UnityEngine.GameObject go)
        {
            GeneratedBuildings.RemoveLoopingSounds(go);
            go.GetComponent<KPrefabID>().AddTag(GameTags.FloorTiles, false);
        }

        public override void DoPostConfigureUnderConstruction(UnityEngine.GameObject go)
        {
            go.AddOrGet<KAnimGridTileVisualizer>();
        }

        /// <summary>
        /// Gets a custom <see cref="TextureAtlas"/> for a tile.
        /// </summary>
        /// <param name="name">The name of the atlas to get</param>
        /// <returns></returns>
        private TextureAtlas GetCustomBlockTileAtlas (string name)
        {
            // THIS IS A PROOF OF CONCEPT
            // in a real application this should probably be a static method or similar
            // ...or use my library, that is going to include this now. :3
            var dir = Path.Combine(PipLib.PLUtil.GetAssemblyDir(this.GetType()), "atlas");
            var texFile = Path.Combine(dir, name + ".png");

            // TL;DR load a texture from disk, then create a new atlas borrowing settings from an existing tile atlas
            // ...then swap in our texture
            TextureAtlas atlas = null;
            if (File.Exists(texFile)) {
                var data = File.ReadAllBytes(texFile);
                var tex = new Texture2D(2, 2);
                tex.LoadImage(data);

                var tileAtlas = Assets.GetTextureAtlas("tiles_metal");
                atlas = ScriptableObject.CreateInstance<TextureAtlas>();
                atlas.texture = tex;
                atlas.vertexScale = tileAtlas.vertexScale;
                atlas.items = tileAtlas.items;
            }

            return atlas;
        }

    }

}
