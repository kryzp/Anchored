using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Anchored.Util
{
    public static class Extensions
    {
        public static void Project(this Polygon poly, Vector2 axis, ref float min, ref float max)
		{
            float dot = Vector2.Dot(poly.Vertices[0], axis);
            min = dot;
            max = dot;

            for (int ii = 1; ii < poly.Vertices.Length; ii++)
			{
                dot = Vector2.Dot(poly.Vertices[ii], axis);
                min = MathF.Min(dot, min);
                max = MathF.Max(dot, max);
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

        public static void Project(this CircleF circ, Vector2 axis, ref float min, ref float max)
        {
            min = Vector2.Dot(circ.Center - (axis * circ.Radius), axis);
            max = Vector2.Dot(circ.Center + (axis * circ.Radius), axis);
        }

        public static void DrawRoundedRectangle(
            this SpriteBatch sb,
            RectangleF rectangle,
            float r,
            Color colour,
            int sides = 15,
            float layer = 0.95f
        )
		{
            DrawUtil.DrawRoundedRectangle(rectangle, r, colour, layer, sides, sb);
		}

        public static void DrawOutlinedRoundedRectangle(
            this SpriteBatch sb,
            RectangleF rectangle,
            float r,
            float t,
            Color colour,
            float layer = 0.95f,
            int sides = 15
        )
        {
            DrawUtil.DrawOutlinedRoundedRectangle(rectangle, r, t, colour, layer, sides, sb);
        }

        public static Texture2D Crop(
            this Texture2D tex,
            Rectangle rect,
            bool set = false
        )
		{
            Texture2D croppedTexture = new Texture2D(Game1.GraphicsDevice, rect.Width, rect.Height);
            Color[] data = new Color[rect.Width * rect.Height];
            tex.GetData(0, rect, data, 0, rect.Width * rect.Height);
            croppedTexture.SetData(data);
            if (set)
                tex = croppedTexture;
            return croppedTexture;
		}
    }
}