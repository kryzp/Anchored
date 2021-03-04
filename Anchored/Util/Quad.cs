using Microsoft.Xna.Framework;
using System;

namespace Anchored.Util
{
	public class Quad
	{
		public Vector2 A;
		public Vector2 B;
		public Vector2 C;
		public Vector2 D;

		public Quad()
		{
			this.A = Vector2.Zero;
			this.B = Vector2.Zero;
			this.C = Vector2.Zero;
			this.D = Vector2.Zero;
		}

		public Quad(Vector2 x)
		{
			this.A = x;
			this.B = x;
			this.C = x;
			this.D = x;
		}

		public Quad(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
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
			min = Math.Min(dot, min);
			max = Math.Max(dot, max);

			dot = Vector2.Dot(C, axis);
			min = Math.Min(dot, min);
			max = Math.Max(dot, max);
			
			dot = Vector2.Dot(D, axis);
			min = Math.Min(dot, min);
			max = Math.Max(dot, max);
		}
	}
}
