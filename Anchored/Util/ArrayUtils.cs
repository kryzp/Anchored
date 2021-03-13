using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Util
{
	public static class ArrayUtils
	{
		public static float[] Clone(float[] array)
		{
			var clone = new float[array.Length];

			for (var ii = 0; ii < array.Length; ii++)
			{
				clone[ii] = array[ii];
			}

			return clone;
		}

		public static bool[] Clone(bool[] array)
		{
			var clone = new bool[array.Length];

			for (var ii = 0; ii < array.Length; ii++)
			{
				clone[ii] = array[ii];
			}

			return clone;
		}

		public static T[] Clone<T>(List<T> array)
		{
			var clone = new T[array.Count];

			for (var ii = 0; ii < array.Count; ii++)
			{
				clone[ii] = array[ii];
			}

			return clone;
		}

		public static void Remove<T>(List<T> array, Func<T, bool> filter)
		{
			for (var ii = array.Count - 1; ii >= 0; ii--)
			{
				if (filter(array[ii]))
				{
					array.RemoveAt(ii);
				}
			}
		}
	}
}
