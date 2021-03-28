using Anchored.Streams;
using System;

namespace Anchored.World
{
	public abstract class EntityType
	{
		public bool Serializable = false;

		public virtual void Create(Entity entity)
		{
			entity.Type = this;
		}

		public virtual void Save(FileWriter stream)
		{
		}
		
		public virtual void Load(FileReader stream)
		{
		}

		public static explicit operator EntityType(Type v)
		{
			throw new NotImplementedException();
		}
	}
}
