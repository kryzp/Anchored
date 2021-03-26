using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Elements
{
	public class UIText : UIComponent
	{
		public SpriteFont Font = null;
		public string Text = "";
		public float LayerDepth = 0.95f;
		public Color Colour = Color.White;
		public float Scale = 1f;
		public TextAlign TextAlign = TextAlign.Left;

		public UIText(string text, SpriteFont font, TextAlign align = TextAlign.Left)
		{
			this.Font = font;
			this.Text = text;
			this.TextAlign = align;

			base.Width = (int)font.MeasureString(text).X;
			base.Height = (int)font.MeasureString(text).Y;
		}

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);

			sb.DrawString(
				this.Font,
				this.Text,
				Position,
				Colour,
				0f,
				Vector2.Zero,
				Scale,
				SpriteEffects.None,
				LayerDepth
			);
		}
	}
}
