using Anchored.Streams;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Anchored.World
{
	public class Prefab
	{
		public PrefabData[] Data;

		public Entity Instantiate(int x, int y)
		{
			Entity entity = null;
			var reader = new FileReader(null);

			foreach (var d in Data)
			{
				EntityType entityType = (EntityType)Activator.CreateInstance(d.Type);
				entityType.Create(entity);
				entity.Load(reader);
				entity.Transform.Position = new Vector2(x, y);
			}

			return entity;
		}
	}
}
