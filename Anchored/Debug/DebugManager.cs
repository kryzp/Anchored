using Arch;
using Arch.World;
using ImGuiNET;
using Microsoft.Xna.Framework;

namespace Anchored.Debug
{
	public static class DebugManager
	{
		private static System.Numerics.Vector2 size;
		private static System.Numerics.Vector3 bgcol;

		public static bool Entities = false; // not done
		public static bool RunInfo = false;
		public static bool Console = true;
		public static bool LayerDebug = false; // not done
		public static bool Areas = false; // not done
		public static bool Settings = false;
		public static bool DebugView = false;

		static DebugManager()
		{
			var vec3 = Engine.BackgroundColor.ToVector3();
			bgcol = new System.Numerics.Vector3(vec3.X, vec3.Y, vec3.Z);
		}

		public static void Draw(EntityWorld world)
		{
			DrawSettings();
			//RunStatistics.DrawDebug();
			Anchored.Debug.Info.DebugView.Draw();
			Anchored.Debug.Info.RunInfo.Draw();

			ImGui.SetNextWindowPos(
				new System.Numerics.Vector2(
					Engine.WindowWidth - 10 - size.X,
					Engine.WindowHeight - 10 - size.Y
				)
			);

			ImGui.Begin(
				"Windows",
				ImGuiWindowFlags.NoCollapse |
				ImGuiWindowFlags.AlwaysAutoResize |
				ImGuiWindowFlags.NoTitleBar
			);

			if (ImGui.Button("Hide All"))
			{
				DebugView = Entities = RunInfo = LayerDebug = Areas = Settings = false;
			}

			ImGui.SameLine();

			if (ImGui.Button("Show All"))
			{
				DebugView = Entities = RunInfo =  LayerDebug = Areas = Settings = true;
			}

			ImGui.BeginMainMenuBar();
			{
				if (ImGui.BeginMenu("General"))
				{
					ImGui.MenuItem("Settings", "", ref Settings);
					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Tools"))
				{
					ImGui.MenuItem("Console", "", ref Console);
					ImGui.MenuItem("Areas", "", ref Areas);
					ImGui.MenuItem("Layer Debug", "", ref LayerDebug);
					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Info"))
				{
					ImGui.MenuItem("Entities", "", ref Entities);
					ImGui.MenuItem("Run Info", "", ref RunInfo);
					ImGui.MenuItem("Debug View", "", ref DebugView);
					ImGui.EndMenu();
				}
			}
			ImGui.EndMainMenuBar();

			size = ImGui.GetWindowSize();
			ImGui.End();
		}
		
		private static void DrawSettings()
		{
			if (!Settings)
				return;

			if (!ImGui.Begin("Settings"))
			{
				ImGui.End();
				return;
			}

			if (ImGui.ColorPicker3("Background Colour", ref bgcol))
			{
				Engine.BackgroundColor = new Color(bgcol.X, bgcol.Y, bgcol.Z, 255);
			}

			ImGui.End();
		}
	}
}
