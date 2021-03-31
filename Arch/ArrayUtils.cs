using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arch
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

		public static void Populate<T>(this T[] arr, T value)
		{
			for (int ii = 0; ii < arr.Length; ii++)
			{
				arr[ii] = value;
			}
		}

		public static void Populate<T>(this T[,] arr, T value)
		{
			for (int yy = 0; yy < arr.GetLength(0); yy++)
			{
				for (int xx = 0; xx < arr.GetLength(1); xx++)
				{
					arr[xx, yy] = value;
				}
			}
		}

		public static void Populate<T>(this List<T> list, int length, T value)
		{
			for (int ii = 0; ii < length; ii++)
			{
				list.Add(value);
			}
		}

		public static void Resize<T>(this List<T> list, int sz, T c)
		{
			int cur = list.Count;

			if (sz < cur)
			{
				list.RemoveRange(sz, cur - sz);
			}
			else if (sz > cur)
			{
				if (sz > list.Capacity)
				{
					list.Capacity = sz;
				}

				list.AddRange(Enumerable.Repeat(c, sz - cur));
			}
		}

		public static void Resize<T>(this List<T> list, int sz) where T : new()
		{
			Resize(list, sz, new T());
		}
	}
}
