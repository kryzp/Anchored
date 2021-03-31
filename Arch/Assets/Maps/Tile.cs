using System;
using Microsoft.Xna.Framework;

namespace Arch.Assets.Maps
{
    public class Tile
    {
        public UInt16 ID;
        public TileType Type;
        public Rectangle Bounds;

        public bool IsEmpty() => Type == TileType.Empty;
        public bool IsSolid() => Type == TileType.Solid;
        public bool IsDiagonal() => Type == TileType.Diagonal;
    }
}
