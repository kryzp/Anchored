using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Util
{
	public static class Calc
	{
		public static float Approach(float from, float to, float amount)
		{
			return (from < to) ? (MathF.Min(from + amount, to)) : (MathF.Max(from - amount, to));
		}
	}
}
