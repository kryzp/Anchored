using Microsoft.Xna.Framework;
using System;

namespace Anchored
{
	public static class Time
	{
		public static double Seconds;
		public static double PrevSeconds;
		public static float Delta;
		public static float PauseTimer;
		public static GameTime GameTime;

		public static void PauseFor(float duration)
		{
			if (duration >= PauseTimer)
				PauseTimer = duration;
		}

		public static bool OnInterval(double time, float delta, float interval, float offset)
		{
			var last = (long)((time - offset - delta) / interval);
			var next = (long)((time - offset) / interval);
			return last < next;
		}

		public static bool OnInterval(float delta, float interval, float offset)
		{
			return OnInterval(Seconds, delta, interval, offset);
		}

		public static bool OnInterval(float interval, float offset)
		{
			return OnInterval(Seconds, Delta, interval, offset);
		}

		public static bool OnTime(double time, double timestamp)
		{
			float c = (float)time - Delta;
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
			return BetweenInterval(Seconds, interval, offset);
		}
	}
}
