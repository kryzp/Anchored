using Anchored.Debug.Console;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Anchored.Util.Math.Tween
{
	public static class Tween
	{
		private static List<TweenTask> tasks = new List<TweenTask>();

		public static TweenTask To(float to, float value, Action<float> set, float duration, Func<float, float> ease = null, float delay = 0)
		{
			if (ease == null)
				ease = Ease.QuadOut;

			var task = new TweenTask();
			tasks.Add(task);

			task.Delay = delay;
			task.Duration = duration;
			task.EaseFn = ease;
			task.Single = true;
			task.From = value;
			task.To = to;
			task.Set = set;

			return task;
		}

		public static TweenTask To<T>(T target, object values, float duration, Func<float, float> ease = null, float delay = 0)
		{
			if (ease == null)
				ease = Ease.QuadOut;

			var task = new TweenTask();
			tasks.Add(task);

			task.Delay = delay;
			task.Duration = duration;
			task.EaseFn = ease;

			if (values == null)
				return task;

			foreach (var property in values.GetType().GetTypeInfo().DeclaredProperties)
			{
				try
				{
					var info = new TweenValue(target, property.Name);
					var to = Convert.ToSingle(new TweenValue(values, property.Name, false).Value);

					float s = Convert.ToSingle(info.Value);
					float r = to - s;

					task.Vars.Add(info);
					task.Start.Add(s);
					task.Range.Add(r);
				}
				catch (Exception e)
				{
					DebugConsole.Error(e.Message);
				}
			}

			return task;
		}

		public static void Remove(TweenTask task)
		{
			if (task != null)
				tasks.Remove(task);
		}

		public static void Update()
		{
			int ii = tasks.Count - 1;

			try
			{
				for (; ii >= 0; ii -= 1)
				{
					var task = tasks[ii];

					if (task == null)
					{
						tasks.RemoveAt(ii);
						continue;
					}

					task.Update();

					if (task.Ended)
						tasks.RemoveAt(ii);
				}
			}
			catch (Exception e)
			{
				tasks.RemoveAt(ii);
				DebugConsole.Error(e.Message);
			}
		}
	}
}
