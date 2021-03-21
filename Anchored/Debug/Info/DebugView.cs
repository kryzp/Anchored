using Anchored.Debug.Console;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug.Info
{
	public static class DebugView
	{
		public static void Draw()
		{
			if (!DebugManager.DebugView)
				return;

			if (!ImGui.Begin("Debug View"))
			{
				ImGui.End();
				return;
			}

			bool var = DebugConsole.GetVariable<bool>("showcolliders");
			if (ImGui.Checkbox("Show Colliders", ref var))
			{
				DebugConsole.SetVariable<bool>("showcolliders", var);
			}

			// todo: like move the camera around freely and stuff

			ImGui.End();
		}
	}
}
