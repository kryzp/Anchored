using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Elements
{
	public class UITexture : UIComponent
	{
		public TextureRegion Texture;
		public float LayerDepth = 0.95f;

		public UITexture(TextureRegion tex)
		{
			this.Texture = tex;
		}

		public override void Init()
		{
		}

		public override void Draw(SpriteBatch sb)
		{
			Texture.Draw(
				Position,
				Vector2.Zero,
				Color.White,
				0f,
				Size / new Vector2(Texture.Width, Texture.Height),
				sb,
				LayerDepth
			);
		}
	}
}
