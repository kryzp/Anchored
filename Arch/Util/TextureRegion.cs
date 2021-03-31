using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.Util
{
	public class TextureRegion
	{
		public Texture2D Texture;
		public Rectangle Source;

		public float X => Source.X;
		public float Y => Source.Y;
		public float Width => Source.Width;
		public float Height => Source.Height;

		public TextureRegion()
		{
		}

		public TextureRegion(Texture2D texture)
		{
			Texture = texture;
			Source = texture.Bounds;
		}

		public TextureRegion(TextureRegion texture, int width, int height)
		{
			Texture = texture.Texture;
			Source = new Rectangle()
			{
				X = texture.Source.X,
				Y = texture.Source.Y,
				Width = width,
				Height = height
			};
		}

		public TextureRegion(Texture2D texture, int x, int y, int width, int height)
		{
			Texture = texture;
			Source = new Rectangle()
			{
				X = x,
				Y = y,
				Width = width,
				Height = height
			};
		}

		public TextureRegion(Texture2D texture, Rectangle source)
		{
			Texture = texture;
			Source = source;
		}

		public void Set(TextureRegion region)
		{
			if (region == null)
				return;

			Texture = region.Texture;
			Source = new Rectangle(region.Source.X, region.Source.Y, region.Source.Width, region.Source.Height);
		}

		public void Draw(Vector2 position, Vector2 origin, Color color, float rotation, Vector2 scale, float layer = 0f, SpriteBatch sb = null)
		{
			if (sb == null)
				sb = Engine.SpriteBatch;

			sb.Draw(
				Texture,
				position,
				Source,
				color,
				rotation,
				origin,
				scale,
				SpriteEffects.None,
				layer
			);
		}

		public void Draw(Rectangle dest, Vector2 origin, Color color, float rotation, float layer = 0f, SpriteBatch sb = null)
		{
			if (sb == null)
				sb = Engine.SpriteBatch;

			sb.Draw(Texture, dest, Source, Color.White, rotation, origin, SpriteEffects.None, layer);
		}
	}
}
