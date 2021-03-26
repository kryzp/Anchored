using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchored
{
	public static class Utility
	{
		public static string WordWrap(SpriteFont font, string text, float maxLineWidth)
		{
			string[] words = text.Split(' ');
			StringBuilder sb = new StringBuilder();

			float lineWidth = 0f;
			float spaceWidth = font.MeasureString(" ").X;

			foreach (string word in words)
			{
				Vector2 size = font.MeasureString(word);

				if (lineWidth + size.X < maxLineWidth)
				{
					sb.Append(word + " ");
					lineWidth += size.X + spaceWidth;
				}
				else
				{
					sb.Append("\n" + word + " ");
					lineWidth = size.X + spaceWidth;
				}
			}

			return sb.ToString();
		}

		public static Rectangle ConvertStringToRectangle(string raw)
		{
			List<string> values = raw.Split(' ').ToList();

			if (values.Count > 4)
				return Rectangle.Empty;
			else if (values.Count < 4)
				return Rectangle.Empty;

			Int32 x = Int32.Parse(values[0]);
			Int32 y = Int32.Parse(values[1]);
			Int32 w = Int32.Parse(values[2]);
			Int32 h = Int32.Parse(values[3]);

			return new Rectangle(x, y, w, h);
		}

		public static string ConvertRectangleToString(Rectangle raw)
		{
			return $"{raw.Width} {raw.Height} {raw.Width} {raw.Height}";
		}
		
		public static Color BlendColours(Color a, Color b, float factor)
		{
			return new Color(
				(byte)MathHelper.Clamp(((a.R * factor) + (b.R * (1f - factor))), 0, 255),
				(byte)MathHelper.Clamp(((a.G * factor) + (b.G * (1f - factor))), 0, 255),
				(byte)MathHelper.Clamp(((a.B * factor) + (b.B * (1f - factor))), 0, 255)
			);
		}

		public static Texture2D GetWhitePixel(SpriteBatch spriteBatch = null)
		{
			if (spriteBatch == null)
				spriteBatch = Game1.SpriteBatch;
			Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			texture.SetData(new[] { Color.White });
			return texture;
		}

		public static void DrawRectangle(Rectangle rectangle, Color color, float layer = 0.95f, SpriteBatch spriteBatch = null)
		{
			if (spriteBatch == null)
				spriteBatch = Game1.SpriteBatch;
			Texture2D rect = new Texture2D(
				spriteBatch.GraphicsDevice,
				(int)rectangle.Width,
				(int)rectangle.Height
			);
			Color[] data = new Color[(int)rectangle.Width * (int)rectangle.Height];
			for (int ii = 0; ii < data.Length; ii++)
				data[ii] = color;
			rect.SetData(data);
			Vector2 coor = new Vector2(rectangle.X, rectangle.Y);
			spriteBatch.Draw(rect, coor, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
		}

		public static void DrawLine(Vector2 point1, Vector2 point2, Color color, float thickness = 1f, float Layer = 0.98f, SpriteBatch spriteBatch = null)
		{
			if (spriteBatch == null)
				spriteBatch = Game1.SpriteBatch;
			var distance = Vector2.Distance(point1, point2);
			var angle = MathF.Atan2(point2.Y - point1.Y, point2.X - point1.X);
			DrawLine(point1, distance, angle, color, thickness, Layer, spriteBatch);
		}

		public static void DrawLine(Vector2 point, float length, float angle, Color color, float thickness = 1f, float layer = 0.98f, SpriteBatch spriteBatch = null)
		{
			if (spriteBatch == null)
				spriteBatch = Game1.SpriteBatch;
			var origin = new Vector2(0f, 0.5f);
			var scale = new Vector2(length, thickness);
			spriteBatch.Draw(GetWhitePixel(spriteBatch), point, null, color, angle, origin, scale, SpriteEffects.None, layer);
		}

		public static Texture2D CreateTexture(int width, int height, Func<int, Color> paint, SpriteBatch spriteBatch = null)
		{
			if (spriteBatch == null)
				spriteBatch = Game1.SpriteBatch;
			var texture = new Texture2D(spriteBatch.GraphicsDevice, width, height);
			Color[] data = new Color[width * height];
			for (int pixel = 0; pixel < data.Length; pixel++)
				data[pixel] = paint(pixel);
			texture.SetData(data);
			return texture;
		}

		/*
		public static void DrawRoundedRectangle(
			RectangleF rectangle,
			float r,
			Color colour,
			float layer = 0.95f,
			int sides = 15,
			SpriteBatch sb = null
		)
		{
			float d = r * 2;

			int x0 = (int)(rectangle.X + r);
			int y0 = (int)(rectangle.Y + r);
			int x1 = (int)(x0 + rectangle.Width);
			int y1 = (int)(y0 + rectangle.Height);

			RectangleF horiRect = new RectangleF(x0-r, y0, rectangle.Width+r, rectangle.Height-r);
			RectangleF vertRect = new RectangleF(x0, y0-r, rectangle.Width-r, rectangle.Height+r);

			// Draw Rectangles
			DrawRectangle(horiRect, colour, layer, sb);
			DrawRectangle(vertRect, colour, layer, sb);

			// Draw Circles
			int variableThatFixesThingsLmao = 1;
			ShapeExtensions.DrawCircle(sb, x0+variableThatFixesThingsLmao, y0+variableThatFixesThingsLmao, r, sides, colour, d, layer);
			ShapeExtensions.DrawCircle(sb, x1-r-variableThatFixesThingsLmao, y0, r, sides, colour, d, layer);
			ShapeExtensions.DrawCircle(sb, x0, y1-r-variableThatFixesThingsLmao, r, sides, colour, d, layer);
			ShapeExtensions.DrawCircle(sb, x1-r-variableThatFixesThingsLmao, y1-r-variableThatFixesThingsLmao, r, sides, colour, d, layer);
		}
		*/
	}
}
