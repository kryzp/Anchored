using Arch;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug.Info
{
	public static class RunInfo
	{
		public static void Draw()
		{
			if (!DebugManager.RunInfo)
				return;

			if (!ImGui.Begin("Run Info"))
			{
				ImGui.End();
				return;
			}

			float fps = 1f / Time.Delta;
			ImGui.TextUnformatted($"FPS: {fps}");

			ImGui.End();
		}
	}
}
