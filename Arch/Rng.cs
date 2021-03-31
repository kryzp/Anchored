using Microsoft.Xna.Framework;
using System;

namespace Arch
{
	public static class Rng
	{
		private static Random random;

		static Rng()
		{
			random = new Random(Engine.SEED);
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
			var buffer = new byte[4];
			random.NextBytes(buffer);
			return BitConverter.ToSingle(buffer, 0);
		}

		public static float Float(float max)
		{
			return Float(0, max);
		}

		public static float AngleDegrees()
		{
			return Float(0, 360);
		}

		public static float AngleRadians()
		{
			return MathHelper.ToRadians(AngleDegrees());
		}

		public static Vector2 UnitVector()
		{
			float angle = AngleRadians();
			float x = MathF.Cos(angle);
			float y = MathF.Sin(angle);
			Vector2 vector = new Vector2(x, y);
			return vector;
		}
	}
}
