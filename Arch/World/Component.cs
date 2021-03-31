using Arch.Streams;
using System;

namespace Arch.World
{
	public class Component
	{
		public UInt16 ID;
		public UInt16 Index;
		public Entity Entity;

		public EntityWorld World => Entity.World;

		public bool Enabled;
		public Masks Mask;
		public Type Type;

		public Component()
		{
			this.Mask = Masks.None;
			this.Enabled = false;
		}

		public Component(UInt16 id, UInt16 index)
			: this()
		{
			this.ID = id;
			this.Index = index;
		}

		public bool IsValid()
		{
			return (
				World != null &&
				World.ValidComponent(this, Type)
			);
		}

		public virtual void Init()
		{
		}

		public virtual void Destroyed()
		{
		}

		public virtual void Save(FileWriter stream)
		{
		}

		public virtual void Load(FileReader stream)
		{
		}

		public T GetComponent<T>() where T : Component, new()
		{
			return Entity.GetComponent<T>();
		}
	}
}
