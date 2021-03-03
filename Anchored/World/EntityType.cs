using System;

namespace Anchored.World
{
	public abstract class EntityType
	{
		public abstract void Create(Entity entity, UInt32 instance);
	}
}
