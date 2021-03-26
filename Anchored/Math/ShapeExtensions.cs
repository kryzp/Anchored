using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Math
{
	public static class ShapeExtensions
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

		public static void Project(this CircleF circle, Vector2 axis, ref float min, ref float max)
		{
			min = Vector2.Dot(circle.Center - (axis * circle.Radius), axis);
			max = Vector2.Dot(circle.Center + (axis * circle.Radius), axis);
		}
	}
}
