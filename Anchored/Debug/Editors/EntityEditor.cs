using Anchored.State;
using Anchored.Util;
using Anchored.World;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Anchored.Debug.Editors
{
	public static class EntityEditor
	{
		private enum EditorMode
		{
			Place,
			Edit
		}

		private static unsafe ImGuiTextFilterPtr filter = new ImGuiTextFilterPtr(ImGuiNative.ImGuiTextFilter_ImGuiTextFilter(null));

		private static System.Numerics.Vector2 size = new System.Numerics.Vector2(400, 240);
		private static System.Numerics.Vector2 pos = new System.Numerics.Vector2(345, 24);

		private static Vector2 offset;
		private static Entity entity;
		private static int selected;

		private static Color gridBoxColorA = new Color(32, 245, 209, 255);
		private static Color gridBoxColorB = new Color(128, 153, 255, 255);
		private static Color gridBoxColor = gridBoxColorA;

		private static EditorMode mode = EditorMode.Place;
		private static string currentMode = "Place";

		public static Editor Editor;

		static EntityEditor()
		{
		}

		public static void Draw()
		{
			ImGui.SetNextWindowSize(size, ImGuiCond.Once);
			ImGui.SetNextWindowPos(pos, ImGuiCond.Once);

			if (!ImGui.Begin("Entity Editor"))
			{
				ImGui.End();
				return;
			}

			if (ImGui.BeginCombo("##modecombo", currentMode))
			{
				string[] modeList = Enum.GetNames(typeof(EntityEditor.EditorMode))
					.Select(x => x.ToString())
					.ToArray();

				for (int ii = 0; ii < modeList.Length; ii += 1)
				{
					bool isSelected = currentMode == modeList[ii];
					
					if (ImGui.Selectable(modeList[ii], isSelected))
					{
						currentMode = modeList[ii];
						mode = (EntityEditor.EditorMode)Enum.Parse(typeof(EntityEditor.EditorMode), currentMode);
					}

					if (isSelected)
						ImGui.SetItemDefaultFocus();
				}

				ImGui.EndCombo();
			}

			if (mode == EditorMode.Place)
			{
				bool down = !ImGui.GetIO().WantCaptureMouse && Input.IsDown(MouseButton.Left);
				bool clicked = !ImGui.GetIO().WantCaptureMouse && Input.IsPressed(MouseButton.Left);

				if (Input.Ctrl(true))
				{
					if (entity != null)
					{
					}
				}

				float amountA = (MathF.Sin((float)(Time.Seconds * 4f)) + 1f) / 2f;
				float amountB = 1f - amountA;

				gridBoxColor = DrawUtil.BlendColours(
					gridBoxColorA, gridBoxColorB,
					amountA, amountB
				);

				int mouseX = (int)(MathF.Round((Input.MouseWorldPosition(Editor.Camera).X - 8) / 16) * 16);
				int mouseY = (int)(MathF.Round((Input.MouseWorldPosition(Editor.Camera).Y - 8) / 16) * 16);

				RectangleF rect = new Rectangle(mouseX, mouseY, 16, 16);

				Game1.SpriteBatch.Begin(SpriteSortMode.FrontToBack, transformMatrix: Editor.Camera.GetViewMatrix());
				ShapeExtensions.DrawRectangle(Game1.SpriteBatch, rect, gridBoxColor, 1f, 0.95f);
				Game1.SpriteBatch.End();
			}
			else if (mode == EditorMode.Edit)
			{
				if (entity == null)
				{
					ImGui.Text("Select an entity from the list to edit it!");

					// todo: draw list
				}
				else
				{
					// todo: do the editing
				}
			}

			ImGui.End();
		}
	}
}
