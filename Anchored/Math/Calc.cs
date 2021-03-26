using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Math
{
	public static class Calc
	{
		public static float Approach(float from, float to, float amount)
		{
			return from < to ? MathF.Min(from + amount, to) : MathF.Max(from - amount, to);
		}

		public static float MapValue(float a0, float a1, float b0, float b1, float v)
		{
			return ((a1 - a0) / (b1 - b0) * (v - b1)) + a1;
		}

		public static float NormalizeRange(float min, float max, float v)
		{
			return MapValue(0, 1, min, max, v);
		}
	}
}
