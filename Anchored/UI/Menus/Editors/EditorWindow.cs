using Anchored.State;
using Arch;
using Arch.Assets.Textures;
using Arch.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Menus.Editors
{
	public class EditorWindow
	{
		private static float painterPositionX;
		private static float painterPositionY;
		private static float painterColorBlend = 0f;

		public static readonly Color PainterColorA = new Color(69, 247, 238);
		public static readonly Color PainterColorB = new Color(87, 144, 242);

		public static readonly Color BackgroundBoxColorA = new Color(192, 192, 192);
		public static readonly Color BackgroundBoxColorB = new Color(128, 128, 128);
		public const int GRID_SIZE = 64;

		public Editor Editor;

		public EditorWindow(Editor editor)
		{
			this.Editor = editor;

			EntityEditor.Editor = editor;
			TileEditor.Editor = editor;
		}

		public void DrawInGame(SpriteBatch sb)
		{
			sb.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
			DrawBackground(sb);
			sb.End();

			EntityEditor.DrawInGame(sb);
			TileEditor.DrawInGame(sb);

			sb.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp, transformMatrix: Editor.Camera.GetPerfectViewMatrix());
			DrawGrid(sb);
			DrawPainterBox(sb);
			sb.End();
		}

		public void Draw()
		{
			EntityEditor.Draw();
			TileEditor.Draw();
		}

		private void DrawBackground(SpriteBatch sb)
		{
			Color cc = BackgroundBoxColorA;
			for (int y = 0; y < (Game1.WindowHeight / GRID_SIZE) + 1; y++)
			{
				for (int x = 0; x < (Game1.WindowWidth / GRID_SIZE) + 1; x++)
				{
					sb.Draw(TextureManager.Get("pixel").Texture, new Rectangle(x * GRID_SIZE, y * GRID_SIZE, GRID_SIZE, GRID_SIZE), cc);
					cc=(((x+y)%2)==0)?BackgroundBoxColorB:BackgroundBoxColorA;
				}
			}
		}

		private void DrawGrid(SpriteBatch sb)
		{
			var col = Color.Black * 0.2f;

			for (int x = 0; x < Editor.Level.MapWidth + 1; x++)
			{
				Utility.DrawLine(
					new Line(
						x * GRID_SIZE,
						0,
						x * GRID_SIZE,
						Editor.Level.MapHeight * GRID_SIZE
					),
					col,
					1f,
					0.95f,
					sb
				);
			}

			for (int y = 0; y < Editor.Level.MapHeight + 1; y++)
			{
				Utility.DrawLine(
					new Line(
						0,
						y * GRID_SIZE,
						Editor.Level.MapWidth * GRID_SIZE,
						y * GRID_SIZE
					),
					col,
					1f,
					0.95f,
					sb
				);
			}
		}

		private void DrawPainterBox(SpriteBatch sb)
		{
			var mouse = Input.MouseWorldPosition(Editor.Camera.GetViewMatrix());
			int targetMouseX = (int)(MathF.Floor(mouse.X/64)*64);
			int targetMouseY = (int)(MathF.Floor(mouse.Y/64)*64);

			painterPositionX = MathHelper.Lerp(painterPositionX, targetMouseX, 40 * Time.RawDelta);
			painterPositionY = MathHelper.Lerp(painterPositionY, targetMouseY, 40 * Time.RawDelta);

			painterColorBlend = MathF.Sin(Time.TotalSeconds * 4f);
			RectangleF rect = new RectangleF(painterPositionX, painterPositionY, 64, 64);
			Color col = Utility.BlendColours(PainterColorA, PainterColorB, painterColorBlend);
			Utility.DrawRectangleOutline(rect, col, 2, 0.95f, sb);
		}
	}
}
