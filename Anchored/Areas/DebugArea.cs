using System;
using System.Collections.Generic;
using Anchored.Assets;
using Anchored.World;
using Arch.Assets.Maps;
using Arch.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.Areas
{
    public class DebugArea : GameArea
    {
        private Map testMap;
        
        public DebugArea(EntityWorld world)
            : base(world)
        {
            /*
            testMap = new Map();
            testMap.Name = "Debug Map";
            testMap.MapWidth = 7;
            testMap.MapHeight = 5;
            testMap.MasterLevel = 0;
            {
                var tileset = new Tileset();
                tileset.Texture = AssetManager.Content.Load<Texture2D>("txrs\\sheets\\tutorial_island_beach");
                tileset.TileSize = 16;
                tileset.Tiles = new List<Tile>()
                {
                    new Tile()
                    {
                        ID = 1,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(0, 0, 16, 16)
                    },
                    new Tile()
                    {
                        ID = 2,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(16, 0, 16, 16)
                    },
                    new Tile()
                    {
                        ID = 3,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(32, 0, 16, 16)
                    },
                    new Tile()
                    {
                        ID = 4,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(0, 16, 16, 16)
                    },
                    new Tile()
                    {
                        ID = 5,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(16, 16, 16, 16)
                    },
                    new Tile()
                    {
                        ID = 6,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(32, 16, 16, 16)
                    },
                    new Tile()
                    {
                        ID = 7,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(0, 32, 16, 16)
                    },
                    new Tile()
                    {
                        ID = 8,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(16, 32, 16, 16)
                    },
                    new Tile()
                    {
                        ID = 9,
                        Type = TileType.Empty,
                        Bounds = new Rectangle(32, 32, 16, 16)
                    },
                };

                var layer1 = new Layer()
                {
                    Name = "Main",
                    Tileset = tileset,
                    Width = 7,
                    Height = 5,
                    ID = 0,
                    Level = 0,
                    Data = new UInt16[]
                    {
                        0, 0, 0, 0, 0, 0, 0,
                        0, 1, 0, 0, 0, 1, 0,
                        0, 0, 0, 0, 0, 0, 0,
                        0, 2, 2, 2, 2, 2, 0,
                        0, 0, 2, 2, 2, 0, 0,
                    }
                };

                var layer2 = new Layer()
                {
                    Name = "Upper",
                    Tileset = tileset,
                    Width = 7,
                    Height = 5,
                    ID = 1,
                    Level = 16,
                    Data = new UInt16[]
                    {
                        0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 4, 0,
                        0, 0, 0, 3, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0,
                    }
                };

                testMap.AddLayer(layer1);
                testMap.AddLayer(layer2);
            }
            */
        }

        public override void Update()
        {
            base.Update();
            //testMap.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
            /*
            sb.Begin(
                SpriteSortMode.FrontToBack,
                samplerState: SamplerState.PointClamp,
                transformMatrix: Camera.GetViewMatrix()
            );

            testMap.Draw(sb);
            
            sb.End();
            */
        }
    }
}