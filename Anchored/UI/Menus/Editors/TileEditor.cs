using Anchored.State;
using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Arch;
using Arch.Math;

namespace Anchored.UI.Menus.Editors
{
	public static class TileEditor
	{
		private static System.Numerics.Vector2 position = new System.Numerics.Vector2(10, 10);
		private static System.Numerics.Vector2 size = new System.Numerics.Vector2(250, 350);

		public static Editor Editor;

		public static void DrawInGame(SpriteBatch sb)
		{
			if (Editor.Level != null)
			{
				sb.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp, transformMatrix: Editor.Camera.GetViewMatrix());
				DrawTiles(sb);
				sb.End();

				sb.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp, transformMatrix: Editor.Camera.GetViewMatrix());
				DrawEntityPreviews(sb);
				sb.End();
			}
		}

		public static void Draw()
		{
			ImGui.SetNextWindowPos(position, ImGuiCond.Once);
			ImGui.SetNextWindowSize(size, ImGuiCond.Once);
			ImGui.Begin("Tile Editor", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize);
			{
			}
			ImGui.End();
		}

		private static void DrawEntityPreviews(SpriteBatch sb)
		{
			// todo
		}

		private static void DrawTiles(SpriteBatch sb)
		{
			Editor.Level.Draw(sb, 4);
		}
	}
}
