using Microsoft.Xna.Framework;
using System;

namespace Anchored.Math
{
	public class QuadF
	{
		public Vector2 A;
		public Vector2 B;
		public Vector2 C;
		public Vector2 D;

		public QuadF()
		{
			this.A = Vector2.Zero;
			this.B = Vector2.Zero;
			this.C = Vector2.Zero;
			this.D = Vector2.Zero;
		}

		public QuadF(Vector2 x)
		{
			this.A = x;
			this.B = x;
			this.C = x;
			this.D = x;
		}

		public QuadF(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
		{
			this.A = a;
			this.B = b;
			this.C = c;
			this.D = d;
		}

		public void Project(Vector2 axis, ref float min, ref float max)
		{
			float dot = Vector2.Dot(A, axis);
			min = dot;
			max = dot;
			
			dot = Vector2.Dot(B, axis);
			min = MathF.Min(dot, min);
			max = MathF.Max(dot, max);

			dot = Vector2.Dot(C, axis);
			min = MathF.Min(dot, min);
			max = MathF.Max(dot, max);
			
			dot = Vector2.Dot(D, axis);
			min = MathF.Min(dot, min);
			max = MathF.Max(dot, max);
		}
	}
}
