using Arch.Assets.Maps.Serialization;
using Arch.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Arch.Assets.Maps
{
    public class MapEntity
    {
        public string Name;
        public EntityType Type;
        public int Level;

        public Vector2 Position;
        public float Z;

        public Dictionary<string, object> Settings = new Dictionary<string, object>();

        public MapEntity()
		{
		}

        public MapEntity(MapEntityJson data)
		{
            this.Name = data.Name;
            this.Type = Load(data.Type);
            this.Level = data.Level;
            this.Position = new Vector2(data.X, data.Y);
            this.Z = data.Z;
            this.Settings = data.Settings;
        }

        protected virtual EntityType Load(string type)
		{
            throw new NotImplementedException();
		}
    }
}
