using Microsoft.Xna.Framework;

namespace Arch
{
	public static class Time
	{
		public static float TotalSeconds = 0f;
		public static float PrevTotalSeconds = 0f;
		public static float DeltaModifier = 1f;
		public static float RawDelta = 0f;
		public static float Delta => RawDelta * DeltaModifier;
		public static float PauseTimer = 0f;
		public static GameTime GameTime = null;

		public static void PauseFor(float duration)
		{
			if (duration >= PauseTimer)
				PauseTimer = duration;
		}

		public static bool OnInterval(float time, float delta, float interval, float offset)
		{
			var last = (long)((time - offset - delta) / interval);
			var next = (long)((time - offset) / interval);
			return last < next;
		}

		public static bool OnInterval(float delta, float interval, float offset)
		{
			return OnInterval(TotalSeconds, delta, interval, offset);
		}

		public static bool OnInterval(float interval, float offset)
		{
			return OnInterval(TotalSeconds, Delta, interval, offset);
		}

		public static bool OnTime(float time, double timestamp)
		{
			float c = time - Delta;
			return (
				time >= timestamp &&
				c < timestamp
			);
		}

		public static bool BetweenInterval(double time, float interval, float offset)
		{
			return ((time - offset) % (((double)interval) * 2)) >= interval;
		}

		public static bool BetweenInterval(float interval, float offset)
		{
			return BetweenInterval(TotalSeconds, interval, offset);
		}
	}
}
