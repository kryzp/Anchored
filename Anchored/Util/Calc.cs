using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Util
{
	public static class Calc
	{
		public static float Approach(float from, float to, float amount)
		{
			return (from < to) ? (Math.Min(from + amount, to)) : (Math.Max(from - amount, to));
		}
	}
}
