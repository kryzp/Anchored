using System;
using Arch.Assets.Maps.Serialization;
using Microsoft.Xna.Framework;

namespace Arch.Assets.Maps
{
    public class Tile
    {
        public UInt16 ID;
        public TileType Type;
        public Rectangle Bounds;

        public Tile(TileJson data)
		{
            this.ID = data.ID;
            this.Type = (TileType)Enum.Parse(typeof(TileType), data.Type);
            this.Bounds = data.Bounds.ToRectangle();
		}

        public bool IsEmpty() => Type == TileType.Empty;
        public bool IsSolid() => Type == TileType.Solid;
        public bool IsDiagonal() => Type == TileType.Diagonal;
    }
}
