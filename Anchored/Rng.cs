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
	}
}
