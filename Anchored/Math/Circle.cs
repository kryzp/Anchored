using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Math
{
	public struct Circle
	{
		public Vector2 Position;

		public Vector2 Center;
		public float Radius;
		
		public float Diameter
		{
			get
			{
				return Radius * 2f;
			}

			set
			{
				Radius = value / 2f;
			}
		}

		public Circle(Vector2 center, float radius)
		{
			this.Position = Vector2.Zero;
			this.Center = center;
			this.Radius = radius;
		}

		public void Project(Vector2 axis, ref float min, ref float max)
		{
			min = Vector2.Dot(Center - (axis * Radius), axis);
			max = Vector2.Dot(Center + (axis * Radius), axis);
		}
	}
}
