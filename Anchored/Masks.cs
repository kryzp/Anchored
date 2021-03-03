using System;

namespace Anchored
{
	[Flags]
	public enum Masks : UInt32
	{
		None = 0 << 0,
		Player = 1 << 0,
		Solid = 1 << 1,
		NPC = 1 << 2,
		Enemy = 1 << 3,
	}
}
