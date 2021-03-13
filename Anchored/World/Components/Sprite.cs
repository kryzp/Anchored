using Anchored.Assets;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.World.Components
{
	public class Sprite : Component, IRenderable
	{
		public TextureRegion Texture;
		public float LayerDepth = 0f;
		public Color Colour = Color.White;
		public Vector2 Origin = Vector2.Zero;

		public Sprite()
		{
			LayerDepth = 0f;
			Colour = Color.White;
			Origin = Vector2.Zero;
		}

		public Sprite(TextureRegion tex)
			: this()
		{
			Texture = tex;
		}

		public Sprite(Texture2D tex)
			: this()
		{
			Texture = new TextureRegion(tex);
		}

		public void Draw(SpriteBatch sb)
		{
			Texture.Draw(
				Entity.Transform.Position,
				Origin + Entity.Transform.Origin,
				Colour,
				Entity.Transform.RotationDegrees,
				Entity.Transform.Scale,
				sb,
				LayerDepth
			);
		}
	}
}
