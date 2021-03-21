using Anchored.Util;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.World.Components
{
	public class Sprite : GraphicsComponent
	{
		public Sprite()
		{
		}

		public Sprite(TextureRegion tex)
			: this()
		{
			Texture = tex;
		}

		public override void DrawBegin(SpriteBatch sb)
		{
		}

		public override void Draw(SpriteBatch sb)
		{
			Shader?.Begin(sb);

			Texture.Draw(
				Entity.Transform.Position,
				Origin + Entity.Transform.Origin,
				Colour,
				Entity.Transform.RotationDegrees,
				Entity.Transform.Scale,
				sb,
				LayerDepth
			);

			Shader?.End(sb);
		}

		public override void DrawEnd(SpriteBatch sb)
		{
		}
	}
}
