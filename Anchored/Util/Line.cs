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
				return MathF.Atan2(A.Y - B.Y, A.X - B.X) + (180f * (MathF.PI / 180f));
			}
		}

		public float AngleDegrees
		{
			get
			{
				// engineer gaming
				return (MathF.Atan2(A.Y - B.Y, A.X - B.X) + (180f * (MathF.PI / 180f))) * (180f / MathF.PI);
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
			Vector2 position = new Vector2(MathF.Min(A.X, B.X), MathF.Min(A.Y, B.Y));
			return new RectangleF(
				position.X,
				position.Y,
				MathF.Max(A.X, B.X) - position.X,
				MathF.Max(A.Y, B.Y) - position.Y
			);
		}

		public void Project(Vector2 axis, ref float min, ref float max)
		{
			float dot = (A.X * axis.X) + (A.Y * axis.Y);
			min = dot;
			max = dot;

			dot = (B.X * axis.X) + (B.Y * axis.Y);
			min = MathF.Min(dot, min);
			max = MathF.Max(dot, max);
		}
	}
}
