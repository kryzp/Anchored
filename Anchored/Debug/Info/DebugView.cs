using Anchored.Debug;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Text;
using Anchored.World.Components;
using Microsoft.Xna.Framework.Input;
using Arch;
using Arch.World.Components;

namespace Anchored.Debug.Info
{
	public static class DebugView
	{
		private static bool debugCameraMove;
		private static float debugCameraMoveSpeed = 200f;
		private static System.Numerics.Vector2 debugCameraPos;

		public const float MAX_CAMERA_SPEED = 500f;

		public static bool DebugCameraMove => debugCameraMove;
		
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
				DebugConsole.SetVariable("showcolliders", var);
			}
			
			ImGui.Checkbox("Move Camera Freely", ref debugCameraMove);
			var camera = Camera.Main;

			ImGui.SliderFloat("Move Camera Speed", ref debugCameraMoveSpeed, 0f, MAX_CAMERA_SPEED);

			ImGui.InputFloat2("Camera Position", ref debugCameraPos);
			if (ImGui.Button("Update Camera Position"))
			{
				camera.Position = new Microsoft.Xna.Framework.Vector2(debugCameraPos.X, debugCameraPos.Y);
			}
			
			if (debugCameraMove)
			{
				camera.DoFollow = false;
				
				int mx = 0;
				int my = 0;

				float mspd = debugCameraMoveSpeed * Time.RawDelta;
				
				Input.EnableGuiFocus = true;

				if (Input.IsDown("player_move_up", true))
					my -= 1;

				if (Input.IsDown("player_move_down", true))
					my += 1;

				if (Input.IsDown("player_move_left", true))
					mx -= 1;
				
				if (Input.IsDown("player_move_right", true))
					mx += 1;

				camera.Position.X += mspd * mx;
				camera.Position.Y += mspd * my;
			}
			else
			{
				camera.DoFollow = true;
			}

			ImGui.End();
		}
	}
}
