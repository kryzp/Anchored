using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.World
{
	public class SimpleComponentHandle
	{
		private EntityWorld world;
		public Type Type;
		public UInt16 Index;
		public UInt16 ID;

		public SimpleComponentHandle(EntityWorld world, Type type, UInt16 index, UInt16 id)
		{
			this.world = world;
			this.Type = type;
			this.Index = index;
			this.ID = id;
		}

		public Component Get()
		{
			return world.Components[Type][Index];
		}

		public static implicit operator bool(SimpleComponentHandle ci1)
		{
			return (
				ci1.Index >= 0 &&
				ci1.ID > 0 &&
				ci1.world.Components[ci1.Type] != null
			);
		}
	}
}
