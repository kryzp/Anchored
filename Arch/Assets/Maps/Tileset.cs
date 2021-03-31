using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Assets.Maps
{
    public class Tileset
    {
        public List<Tile> Tiles = new List<Tile>();
        public Texture2D Texture;
        public int TileSize;

        public Tile GetTile(UInt16 id)
        {
            return Tiles.Find(x => x.ID == id);
        }
    }
}
