using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Linq;

namespace Anchored.Util
{
    public static class Extensions
    {
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

        public static void Project(this CircleF circ, Vector2 axis, ref float min, ref float max)
        {
            min = Vector2.Dot(circ.Center - (axis * circ.Radius), axis);
            max = Vector2.Dot(circ.Center + (axis * circ.Radius), axis);
        }
    }
}