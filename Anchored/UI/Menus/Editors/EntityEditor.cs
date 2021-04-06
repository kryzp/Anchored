using Anchored.State;
using ImGuiNET;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Menus.Editors
{
	public static class EntityEditor
	{
		private static System.Numerics.Vector2 position = new System.Numerics.Vector2(10, 10);
		private static System.Numerics.Vector2 size = new System.Numerics.Vector2(250, 350);

		public static Editor Editor;

		public static void DrawInGame(SpriteBatch sb)
		{
		}

		public static void Draw()
		{
			ImGui.SetNextWindowPos(new System.Numerics.Vector2(Game1.WindowWidth - size.X - position.X, position.Y), ImGuiCond.Once);
			ImGui.SetNextWindowSize(size, ImGuiCond.Once);
			ImGui.Begin("Entity Placer", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize);
			{
			}
			ImGui.End();
		}
	}
}
