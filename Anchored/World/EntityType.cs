using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.World
{
	public abstract class EntityType
	{
		public bool Serializable = false;

		public virtual void Create(Entity entity)
		{
			entity.Type = this;
		}
	}
}
