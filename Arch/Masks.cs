using System;

namespace Arch
{
	[Flags]
	public enum Masks : Byte
	{
		None = 0 << 0,
		Player = 1 << 0,
		Solid = 1 << 1
	}
}
