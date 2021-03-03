using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Anchored.Util
{
	public class DrawUtil
	{
		public static Texture2D GetWhitePixel(SpriteBatch spriteBatch = null)
		{
			if (spriteBatch == null)
				spriteBatch = Game1.SpriteBatch;
			Texture2D texture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			texture.SetData(new[] { Color.White });
			return texture;
		}

		public static void DrawRectangle(RectangleF rectangle, Color color, float layer = 0.95f, SpriteBatch spriteBatch = null)
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
			var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
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
	}
}
