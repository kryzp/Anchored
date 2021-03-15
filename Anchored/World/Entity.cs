using Anchored.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.World
{
	public class Entity
	{
		private UInt32 transformStamp;

		public UInt16 ID;
		public UInt16 Index;

		public string Name;

		public List<SimpleComponentHandle> Components = new List<SimpleComponentHandle>();
		public EntityWorld World = null;

		public UInt32 TransformStamp => transformStamp;
		public Transform Transform;

		public EntityType Type;

		public Entity()
		{
			this.World = null;
			this.ID = 0;
			this.Index = 0;
			this.Transform = new Transform();
			this.Transform.OnTransformed += Transformed;
			this.Name = "";
		}

		public Entity(EntityWorld world, UInt16 id, UInt16 index, string name)
		{
			this.World = world;
			this.ID = id;
			this.Index = index;
			this.Transform = new Transform();
			this.Transform.OnTransformed += Transformed;
			this.Name = name;
		}

		public T AddComponent<T>() where T : Component, new()
		{
			return World.AddComponent<T>(this, new T());
		}

		public T AddComponent<T>(T component) where T : Component, new()
		{
			return World.AddComponent<T>(this, component);
		}

		public void RemoveComponent<T>(SimpleComponentHandle component) where T : Component
		{
			World.RemoveComponent<T>(this, component);
		}

		public void RemoveComponent<T>(T component) where T : Component
		{
			World.RemoveComponent<T>(this, component);
		}

		public void RemoveComponent<T>() where T : Component
		{
			World.RemoveComponent<T>(this);
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

		public void Save(FileWriter stream)
		{
			Transform.Save(stream);

			foreach (var component in Components)
			{
				World.GetComponent(component, component.Type).Save(stream);
			}
		}

		public void Load(FileReader stream)
		{
			Transform.Load(stream);

			foreach (var component in Components)
			{
				World.GetComponent(component, component.Type).Load(stream);
			}
		}

		private void Transformed()
		{
			transformStamp += 1;
		}
	}
}
