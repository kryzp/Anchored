using System;
using System.Diagnostics;
using System.IO;

namespace Arch
{
	public class Log
	{
		private const string LogName = "arch_log.txt";

		private static System.Numerics.Vector2 size = new System.Numerics.Vector2(300, 400);
		private static StreamWriter writer;

		public static void Open()
		{
			if (File.Exists(LogName))
				File.Delete(LogName);

			writer = new StreamWriter(new FileStream(LogName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
		}

		public static void Close()
		{
			try
			{
				writer?.Close();
				writer = null;
			}
			catch (Exception e)
			{
				Log.Error(e);
			}
		}

		public static void Info(object message)
		{
			Print(message, ConsoleColor.Green, "INF");
		}

		public static void Debug(object message)
		{
			Print(message, ConsoleColor.Blue, "DBG");
		}

		public static void Error(object message)
		{
			Print(message, ConsoleColor.DarkRed, "ERR");
		}

		public static void Engine(object message)
		{
			Print(message, ConsoleColor.DarkYellow, "ENG");
		}

		private static void Print(object message, ConsoleColor color, string type)
		{

			try
			{
				var stackTrace = new StackTrace(true);
				var frame = stackTrace.GetFrame(2);
				var prev = stackTrace.GetFrame(3);

#if DEBUG
				var text = prev == null ? $"{DateTime.Now:h:mm:ss} {Path.GetFileName(frame.GetFileName())}:{frame.GetMethod().Name}():{frame.GetFileLineNumber()}" : $"{DateTime.Now:h:mm:ss} {Path.GetFileName(frame.GetFileName())}:{frame.GetMethod().Name}():{frame.GetFileLineNumber()} <= {Path.GetFileName(prev.GetFileName())}:{prev.GetMethod().Name}():{prev.GetFileLineNumber()} ";
#else
				var text = prev == null ?  $"{DateTime.Now:h:mm:ss} {frame.GetMethod().Name}()" :  $"{DateTime.Now:h:mm:ss} {frame.GetMethod().Name}() <= {prev.GetMethod().Name}() ";
#endif

				writer?.Write($"[{type}] {text}");
				writer?.WriteLine(message == null ? "null" : message.ToString());

				Console.Write("[");
				Console.ForegroundColor = color;
				Console.Write(type);
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine($"] {text}");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
