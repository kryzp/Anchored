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

			// todo: like move the camera around freely and stuff

			ImGui.End();
		}
	}
}
