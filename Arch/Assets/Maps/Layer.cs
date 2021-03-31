﻿using System;
using Arch.Assets.Maps.Serialization;
using Arch.Assets.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Assets.Maps
{
    public class Layer
    {
        public string Name = "";
        public string Type = "";
        public int ID = -1;
        public int Level = 0;
        
        public uint Width = 0;
        public uint Height = 0;
        public Tileset Tileset = null;
        
        public float Opacity = 1f;
        public bool Repeat = false;
        
        public int Distance = 0;
        public int YDistance = 0;
        
        public Vector2 MoveSpeed = Vector2.Zero;
        
        public UInt16[,] Data = null;

        public Layer()
		{
		}

        public Layer(LayerJson data)
		{
            Name = data.Name;
            Type = data.Type;
            ID = data.ID;
            Level = data.Level;
            Width = data.Width;
            Height = data.Height;
            Opacity = data.Opacity;
            Repeat = data.Repeat;
            Distance = data.Distance;
            YDistance = data.Distance;
            MoveSpeed = new Vector2(data.MoveSpeed.X, data.MoveSpeed.Y);
            Data = data.Data;
            Tileset = new Tileset();
            {
                Tileset.Texture = TextureManager.Get(data.TilesetName).Texture;
                Tileset.TileSize = data.TileSize;
            }
        }

        public void Update()
		{
            // todo: apply stuff like movepseed, update animating tiles
		}

        public void Draw(SpriteBatch sb)
        {
            for (int yy = 0; yy < Height; yy++)
			{
                for (int xx = 0; xx < Width; xx++)
				{
                    UInt16 id = Data[yy, xx];

                    if (id != 0)
					{
                        var tile = Tileset.GetTile(id);

                        int x = xx * Tileset.TileSize;
                        int y = yy * Tileset.TileSize;

                        var tileDest = new Rectangle(
                            x,
                            y,
                            Tileset.TileSize,
                            Tileset.TileSize
                        );

                        sb.Draw(
                            Tileset.Texture,
                            tileDest,
                            tile.Bounds,
                            Color.White,
                            0f,
                            Vector2.Zero,
                            SpriteEffects.None,
                            Level / Constants.LAYER_DEPTH_DIVIDER
                        );
					}
                }
			}
        }
    }
}
