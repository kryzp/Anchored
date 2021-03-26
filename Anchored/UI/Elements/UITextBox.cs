using Anchored.UI.Constraints;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Elements
{
	public class UITextBox : UIComponent
	{
		private static Point padding = new Point(16, 16);

		private UINineSliceGrid uiNsg;
		private UIText uiText;

		public SpriteFont Font => uiText.Font;
		public string Text
		{
			get => uiText.Text;
			set => uiText.Text = Utility.WordWrap(uiText.Font, value, GetMaxLineWidth());
		}

		public float LayerDepth
		{
			get => uiNsg.LayerDepth;

			set
			{
				uiNsg.LayerDepth = value - 0.01f;
				uiText.LayerDepth = value;
			}
		}

		public UITextBox(string text, SpriteFont font, TextureRegion texture, int gridW, int gridH, int width, int height, float textureScale = 4)
		{
			uiNsg = new UINineSliceGrid(texture, gridW, gridH, width, height, textureScale);
			
			string wrappedText = Utility.WordWrap(font, text, GetMaxLineWidth());
			uiText = new UIText(wrappedText, font);
			uiText.Colour = Color.Black;

			LayerDepth = 0.95f;
		}

		public override void Init()
		{
			// Nine Slice Grid
			{
				UIConstraints uiNsgConstraints = new UIConstraints();

				uiNsgConstraints.X = new CenterConstraint();
				uiNsgConstraints.Y = new PixelConstraint();

				base.Add(uiNsg, uiNsgConstraints);
			}

			// Text
			{
				UIConstraints uiTextConstraints = new UIConstraints();

				uiTextConstraints.X = new PixelConstraint((-(uiNsg.Width/2))+padding.X);
				uiTextConstraints.Y = new PixelConstraint(padding.Y);

				base.Add(uiText, uiTextConstraints);
			}
		}

		private float GetMaxLineWidth()
		{
			return (uiNsg.PixelWidth) - (padding.X * 2);
		}
	}
}
