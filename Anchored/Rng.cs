using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace Anchored
{
	public static class Rng
	{
		private static Random random;

		static Rng()
		{
			random = new Random(Game1.Seed);
		}

		public static int Int(int min, int max)
		{
			return random.Next(min, max);
		}

		public static int Int(int max)
		{
			return Int(0, max);
		}

		public static float Float(float min, float max)
		{
			return random.NextSingle(min, max);
		}

		public static float Float(float max)
		{
			return Float(0, max);
		}

		public static float Angle()
		{
			return random.NextAngle();
		}

		public static Vector2 UnitVector()
		{
			random.NextUnitVector(out Vector2 vector);
			return vector;
		}
	}
}
