using Arch.Assets.Maps.Serialization;
using Arch.World;
using Microsoft.Xna.Framework;
using System;

namespace Arch.Assets.Maps
{
    public class MapEntity
    {
        public string Name;
        public EntityType Type;
        public Transform Transform;
        public int Level;

        public MapEntity()
		{
		}

        public MapEntity(MapEntityJson data)
		{
            Name = data.Name;
            Type = Load(data.Type);
            Level = data.Level;
            Transform = new Transform();
            Transform.Position = new Vector2(data.X, data.Y);
            Transform.Z = data.Z;
        }

        protected virtual EntityType Load(string type)
		{
            throw new NotImplementedException();
		}
    }
}
