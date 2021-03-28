using Anchored.Math;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

		public static void DrawRectangle(Rectangle rectangle, Color color, float layer = 0.95f, SpriteBatch sb = null)
		{
			if (sb == null)
				sb = Game1.SpriteBatch;
			Texture2D rect = new Texture2D(
				sb.GraphicsDevice,
				(int)rectangle.Width,
				(int)rectangle.Height
			);
			Color[] data = new Color[(int)rectangle.Width * (int)rectangle.Height];
			for (int ii = 0; ii < data.Length; ii++)
				data[ii] = color;
			rect.SetData(data);
			Vector2 coor = new Vector2(rectangle.X, rectangle.Y);
			sb.Draw(rect, coor, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
		}
		
		public static void DrawRectangleOutline(RectangleF rectangle, Color color, float thickness = 1f, float layer = 0.95f, SpriteBatch sb = null)
		{
			if (sb == null)
				sb = Game1.SpriteBatch;
			DrawLine(rectangle.TopLeft, rectangle.TopRight, color, thickness, layer, sb);
			DrawLine(rectangle.TopRight, rectangle.BottomRight, color, thickness, layer, sb);
			DrawLine(rectangle.BottomRight, rectangle.BottomLeft, color, thickness, layer, sb);
			DrawLine(rectangle.BottomLeft, rectangle.TopLeft, color, thickness, layer, sb);
		}

		public static void DrawLine(LineF line, Color color, float thickness = 1f, float layer = 0.98f, SpriteBatch sb = null)
		{
			if (sb == null)
				sb = Game1.SpriteBatch;
			DrawLine(line.A, line.B, color, thickness, layer, sb);
		}

		public static void DrawLine(Vector2 point1, Vector2 point2, Color color, float thickness = 1f, float Layer = 0.98f, SpriteBatch sb = null)
		{
			if (sb == null)
				sb = Game1.SpriteBatch;
			var distance = Vector2.Distance(point1, point2);
			var angle = MathF.Atan2(point2.Y - point1.Y, point2.X - point1.X);
			DrawLine(point1, distance, angle, color, thickness, Layer, sb);
		}

		public static void DrawLine(Vector2 point, float length, float angle, Color color, float thickness = 1f, float layer = 0.98f, SpriteBatch sb = null)
		{
			if (sb == null)
				sb = Game1.SpriteBatch;
			var origin = new Vector2(0f, 0.5f);
			var scale = new Vector2(length, thickness);
			sb.Draw(GetWhitePixel(sb), point, null, color, angle, origin, scale, SpriteEffects.None, layer);
		}

		public static void DrawCircleOutline(Circle circle, int steps, Color color, float thickness = 1f, float layer = 0.95f, SpriteBatch sb = null)
		{
			Vector2 lastInner = new Vector2(circle.Center.X + circle.Radius - thickness, circle.Center.Y);
			Vector2 lastOuter = new Vector2(circle.Center.X + circle.Radius, circle.Center.Y);

			for (int ii = 1; ii <= steps; ii++)
			{
				float radians = (ii / (float)steps) * MathHelper.Tau;
				Vector2 normal = new Vector2(MathF.Cos(radians), MathF.Sin(radians));

				Vector2 nextInner = new Vector2(
					circle.Center.X + (normal.X * (circle.Radius - thickness)),
					circle.Center.Y + (normal.Y * (circle.Radius - thickness))
				);

				Vector2 nextOuter = new Vector2(
					circle.Center.X + (normal.X * circle.Radius),
					circle.Center.Y + (normal.Y * circle.Radius)
				);

				Polygon polygon = new Polygon(
					new List<Vector2>()
					{
						lastInner,
						lastOuter,
						nextOuter,
						nextInner
					}
				);

				DrawPolygonOutline(polygon, color, thickness, layer, sb);

				lastInner = nextInner;
				lastOuter = nextOuter;
			}
		}

		public static void DrawPolygonOutline(Polygon polygon, Color color, float thickness = 1f, float layer = 0.95f, SpriteBatch sb = null)
		{
			polygon.ForeachPoint((ii, vertex, nextVertex) =>
			{
				LineF line = new LineF(vertex, nextVertex);
				DrawLine(line, color, thickness, layer, sb);
			});
		}

		public static Texture2D CreateTexture(int width, int height, Func<int, Color> paint, SpriteBatch sb = null)
		{
			if (sb == null)
				sb = Game1.SpriteBatch;
			var texture = new Texture2D(sb.GraphicsDevice, width, height);
			Color[] data = new Color[width * height];
			for (int pixel = 0; pixel < data.Length; pixel++)
				data[pixel] = paint(pixel);
			texture.SetData(data);
			return texture;
		}
	}
}
