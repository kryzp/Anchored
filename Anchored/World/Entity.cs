﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.World
{
	public class Entity
	{
		private UInt32 transformStamp;

		public UInt16 ID;
		public UInt16 Index;

		public List<SimpleComponentHandle> Components = new List<SimpleComponentHandle>();
		public EntityWorld World = null;

		public UInt32 TransformStamp => transformStamp;
		public Transform Transform;

		public Entity()
		{
			this.World = null;
			this.ID = 0;
			this.Index = 0;
			this.Transform = new Transform();
			this.Transform.OnTransformed += Transformed;
		}

		public Entity(EntityWorld world, UInt16 id, UInt16 index)
		{
			this.World = world;
			this.ID = id;
			this.Index = index;
			this.Transform = new Transform();
			this.Transform.OnTransformed += Transformed;
		}

		public T AddComponent<T>() where T : Component, new()
		{
			return World.AddComponent<T>(this, new T());
		}

		public T AddComponent<T>(T component) where T : Component, new()
		{
			return World.AddComponent<T>(this, component);
		}

		public T GetComponent<T>() where T : Component, new()
		{
			foreach (var it in Components)
			{
				if (it.Type == typeof(T))
				{
					return World.GetComponent<T>(it);
				}
			}

			return new T();
		}

		public bool IsValid()
		{
			return (
				World != null &&
				World.ValidEntity(this)
			);
		}

		private void Transformed()
		{
			transformStamp += 1;
		}
	}
}