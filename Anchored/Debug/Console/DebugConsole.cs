using Anchored.Debug.Console.Commands;
using Anchored.World;
using Anchored.World.Components;
using ImGuiNET;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Anchored.Debug.Console
{
	public static unsafe class DebugConsole
	{
		private static System.Numerics.Vector2 size = new System.Numerics.Vector2(323, 435);
		private static System.Numerics.Vector2 spacer = new System.Numerics.Vector2(4, 1);
		private static System.Numerics.Vector4 color = new System.Numerics.Vector4(1, 0.4f, 0.4f, 1f);

		private static ImGuiTextFilterPtr filter = new ImGuiTextFilterPtr(ImGuiNative.ImGuiTextFilter_ImGuiTextFilter(null));
		private static string input = "";

		private static List<ConsoleVariable> variables = new List<ConsoleVariable>();
		private static bool showVariables;

		private static bool forceFocus;

		public static List<ConsoleCommand> Commands = new List<ConsoleCommand>();
		public static List<ConsoleLine> Lines = new List<ConsoleLine>();

		public static EntityWorld World;
		public static bool Open;

		public const int MAX_COMMAND_LENGTH = 128;

		static DebugConsole()
		{
			variables.Add(new ConsoleVariableOf<bool>("showcolliders", true));
			variables.Add(new ConsoleVariableOf<float>("deltamod", Time.DeltaModifier, (x) => Time.DeltaModifier = x));

			Commands.Add(new HelpCommand());
			Commands.Add(new SetCommand());
			Commands.Add(new NoClipCommand());
			Commands.Add(new ReloadCommand());
			Commands.Add(new LangCommand());
			Commands.Add(new SaveCommand());
		}

		public static void Update()
		{
			if (Input.IsPressed(Keys.F1))
			{
				Open = !Open;
				Input.EnableImGuiFocus = Open;
			}
		}

		public static void Draw()
		{
			if (!DebugManager.Console)
				return;

			if (!Open)
				return;

			if (forceFocus)
				ImGui.SetNextWindowCollapsed(false);

			ImGui.SetNextWindowSize(size, ImGuiCond.Once);
			ImGui.SetNextWindowPos(new System.Numerics.Vector2(10, Game1.WINDOW_HEIGHT - size.Y - 10), ImGuiCond.Once);
			
			ImGui.Begin("Console");
			{
				filter.Draw("##console");

				//ImGui.SameLine();
				if (ImGui.Button("Clear"))
					Lines.Clear();

				if (ImGui.BeginPopup("Options"))
				{
					bool thing = false;
					ImGui.Checkbox("Auto-scroll", ref thing);
					ImGui.EndPopup();
				}

				ImGui.SameLine();
				if (ImGui.Button("Options"))
					ImGui.OpenPopup("Options");

				ImGui.SameLine();
				if (ImGui.Button("Variables"))
					showVariables = !showVariables;

				if (showVariables)
				{
					ImGui.Begin("Console Variables");
					{
						foreach (ConsoleVariableOf<bool> variable in variables.Where(x => x is ConsoleVariableOf<bool>))
						{
							string name = variable.Name;
							bool value = variable.Value;

							ImGui.TextUnformatted($"{name} :");

							ImGui.SameLine();

							ImGui.PushStyleColor(ImGuiCol.Text, (value)
								? new System.Numerics.Vector4(0.63f, 0.9f, 0.47f, 1f)
								: new System.Numerics.Vector4(0.94f, 0.44f, 0.36f, 1f));
							ImGui.TextUnformatted($"{value.ToString().ToLower()}");
							ImGui.PopStyleColor();
						}

						foreach (ConsoleVariableOf<string> variable in variables.Where(x => x is ConsoleVariableOf<string>))
						{
							string name = variable.Name;
							string value = variable.Value;

							ImGui.TextUnformatted($"{name} :");

							ImGui.SameLine();

							ImGui.PushStyleColor(ImGuiCol.Text, new System.Numerics.Vector4(0.95f, 0.86f, 0.42f, 1f));
							ImGui.TextUnformatted($"\"{value.ToString()}\"");
							ImGui.PopStyleColor();
						}

						foreach (ConsoleVariableOf<float> variable in variables.Where(x => x is ConsoleVariableOf<float>))
						{
							string name = variable.Name;
							float value = variable.Value;

							ImGui.TextUnformatted($"{name} :");

							ImGui.SameLine();

							ImGui.PushStyleColor(ImGuiCol.Text, new System.Numerics.Vector4(0.41f, 0.68f, 0.89f, 1f));
							ImGui.TextUnformatted($"{value.ToString()}");
							ImGui.PopStyleColor();
						}
					}
					ImGui.End();
				}

				ImGui.Separator();
				var height = ImGui.GetStyle().ItemSpacing.Y + ImGui.GetFrameHeightWithSpacing();
				ImGui.BeginChild("ScrollingRegionConsole", new System.Numerics.Vector2(0, -height), false, ImGuiWindowFlags.HorizontalScrollbar);
				ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, spacer);

				foreach (var t in Lines)
				{
					if (filter.PassFilter(t.Text))
					{
						if (t.Text[0] == '>')
							ImGui.PushStyleColor(ImGuiCol.Text, ConsoleLine.COL_CMD);
						else
							ImGui.PushStyleColor(ImGuiCol.Text, t.Colour);

						ImGui.TextUnformatted(t.Text);
						ImGui.PopStyleColor();
					}
				}

				ImGui.PopStyleVar();
				ImGui.EndChild();
				ImGui.Separator();

				if (ImGui.InputText("##Input", ref input, MAX_COMMAND_LENGTH, ImGuiInputTextFlags.EnterReturnsTrue))
				{
					RunCommand(input);
					input = "";
				}

				ImGui.SetItemDefaultFocus();

				if (forceFocus)
					ImGui.SetKeyboardFocusHere(-1);

				forceFocus = false;
			}
			ImGui.End();
		}

		public static void AddCommand(ConsoleCommand command)
		{
			Commands.Add(command);
		}

		private static void RunCommand(string cmd)
		{
			input = input.TrimEnd();

			Lines.Add(new ConsoleLine($"> {input}", ConsoleLine.COL_LOG));

			var parts = input.Split(null);
			var name = parts[0];

			foreach (var command in Commands)
			{
				if (command.Name.Equals(name) || command.ShortName.Equals(name))
				{
					var args = new string[parts.Length - 1];

					for (int ii = 0; ii < args.Length; ii++)
						args[ii] = parts[ii + 1];

					command.Run(args);

					return;
				}
			}

			Error("Unknown Command");
		}

		public static void SetVariable<T>(string var, T val)
		{
			foreach (ConsoleVariableOf<T> variable in variables.Where(x => x is ConsoleVariableOf<T>))
			{
				if (variable.Name == var)
				{
					variable.Value = val;

					if (variable.OnChanged != null)
						variable.OnChanged(val);
				}
			}
		}

		public static T GetVariable<T>(string var)
		{
			foreach (ConsoleVariableOf<T> variable in variables.Where(x => x is ConsoleVariableOf<T>))
			{
				if (variable.Name == var)
				{
					return variable.Value;
				}
			}

			return default(T);
		}

		public static bool HasVariable<T>(string var)
		{
			foreach (ConsoleVariableOf<T> variable in variables.Where(x => x is ConsoleVariableOf<T>))
			{
				if (variable.Name == var)
				{
					return true;
				}
			}

			return false;
		}

		public static void Log(string str)
		{
			Lines.Add(new ConsoleLine("[LOG] " + str, ConsoleLine.COL_LOG));
		}

		public static void Engine(string str)
		{
			Lines.Add(new ConsoleLine("[ENG] " + str, ConsoleLine.COL_ENG));
		}

		public static void Error(string str)
		{
			Lines.Add(new ConsoleLine("[ERR] " + str, ConsoleLine.COL_ERR));
		}
		
		public static void Error(Exception e)
		{
			Lines.Add(new ConsoleLine("[ERR] " + e.Message, ConsoleLine.COL_ERR));
		}

		public class ConsoleLine
		{
			public static readonly System.Numerics.Vector4 COL_CMD = new System.Numerics.Vector4(1.0f, 0.8f, 0.6f, 1.0f);
			public static readonly System.Numerics.Vector4 COL_LOG = new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f);
			public static readonly System.Numerics.Vector4 COL_ERR = new System.Numerics.Vector4(1.0f, 0.4f, 0.4f, 1.0f);
			public static readonly System.Numerics.Vector4 COL_ENG = new System.Numerics.Vector4(0.3f, 0.8f, 0.8f, 1.0f);

			public string Text;
			public Vector4 Colour;

			public ConsoleLine(string text, Vector4 col)
			{
				this.Text = text;
				this.Colour = col;
			}
		}

		public abstract class ConsoleVariable
		{
			public string Name;
		}

		public class ConsoleVariableOf<T> : ConsoleVariable
		{
			public T Value;
			public Action<T> OnChanged;

			public ConsoleVariableOf(string name, T value, Action<T> onChanged = null)
			{
				this.Name = name;
				this.Value = value;
				this.OnChanged = onChanged;
			}
		}
	}
}
