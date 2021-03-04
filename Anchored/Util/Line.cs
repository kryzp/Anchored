using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace Anchored.Util
{
	public struct Line
	{
		public Vector2 A;
		public Vector2 B;

		public float AngleRadians
		{
			get
			{
				return (float)(Math.Atan2(A.Y - B.Y, A.X - B.X) + (180f * (Math.PI / 180f)));
			}
		}

		public float AngleDegrees
		{
			get
			{
				// engineer gaming
				return (float)((Math.Atan2(A.Y - B.Y, A.X - B.X) + (180f * (Math.PI / 180f))) * (180f / Math.PI));
			}
		}

		public Line(Vector2 a, Vector2 b)
		{
			this.A = a;
			this.B = b;
		}

		public Line(float x0, float y0, float x1, float y1)
		{
			this.A.X = x0;
			this.A.Y = y0;
			this.B.X = x1;
			this.B.Y = y1;
		}

		public RectangleF Bounds()
		{
			Vector2 position = new Vector2(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y));
			return new RectangleF(
				position.X,
				position.Y,
				Math.Max(A.X, B.X) - position.X,
				Math.Max(A.Y, B.Y) - position.Y
			);
		}

		public void Project(Vector2 axis, ref float min, ref float max)
		{
			float dot = (A.X * axis.X) + (A.Y * axis.Y);
			min = dot;
			max = dot;

			dot = (B.X * axis.X) + (B.Y * axis.Y);
			min = Math.Min(dot, min);
			max = Math.Max(dot, max);
		}
	}
}
