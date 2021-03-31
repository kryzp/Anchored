using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.Math
{
	public class Polygon
	{
		public Vector2[] Vertices;

		public Polygon(Vector2[] vertices)
		{
			this.Vertices = vertices;
		}

		public Polygon(List<Vector2> vertices)
		{
			this.Vertices = vertices.ToArray();
		}

		public void Project(Vector2 axis, ref float min, ref float max)
		{
			float dot = Vector2.Dot(Vertices[0], axis);
			min = dot;
			max = dot;

			for (int ii = 1; ii < Vertices.Length; ii++)
			{
				dot = Vector2.Dot(Vertices[ii], axis);
				min = MathF.Min(dot, min);
				max = MathF.Max(dot, max);
			}
		}

		public void ForeachPoint(Action<int, Vector2, Vector2> onVertex)
		{
			for (int ii = 0; ii < Vertices.Length; ii++)
			{
				Vector2 currentVertex = Vertices[ii];
				Vector2 nextVertex = Vertices[((ii+1)>=Vertices.Length)?0:(ii+1)];
				onVertex(ii, currentVertex, nextVertex);
			}
		}
	}
}
