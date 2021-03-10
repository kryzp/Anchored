using Anchored.Debug.Console;
using Microsoft.Xna.Framework;
using System;

namespace Anchored
{
	public static class Time
	{
		public static double Seconds = 0.0;
		public static double PrevSeconds = 0.0;
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
