using MonoGame.Extended.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.World
{
	public abstract class EntityType
	{
		public abstract void Create(Entity entity);
	}
}
