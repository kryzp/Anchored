using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Util.Math
{
	public static class Calc
	{
		public static float Approach(float from, float to, float amount)
		{
			return from < to ? MathF.Min(from + amount, to) : MathF.Max(from - amount, to);
		}

		public static float MapValue(float a0, float a1, float b0, float b1, float v)
		{
			return ((a0 - a1) / (b0 - b1) * (v - b0)) + 1f;
		}
	}
}
