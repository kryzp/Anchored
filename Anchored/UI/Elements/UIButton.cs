using Anchored.UI.Constraints;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Elements
{
	public class UIButton : UIComponent
	{
		private TextureRegion texture;
		private UITexture uiTexture;

		public Action OnPressed = null;

		public bool Pressed;
		public bool Hovering;

		public UIButton(TextureRegion tex)
		{
			this.texture = tex;
		}

		public UIButton(TextureRegion tex, float width, float height)
			: this(tex)
		{
			this.Width = width;
			this.Height = height;
		}

		public override void Init()
		{
			// Texture
			{
				uiTexture = new UITexture(texture);

				UIConstraints uiTextureConstraints = new UIConstraints();
				uiTextureConstraints.X = new CenterConstraint();
				uiTextureConstraints.Y = new PixelConstraint((int)Y);
				uiTextureConstraints.Width = new PixelConstraint((int)Width);
				uiTextureConstraints.Height = new PixelConstraint((int)Height);
				
				base.Add(uiTexture, uiTextureConstraints);
			}
		}

		public override void Update()
		{
			base.Update();

			Vector2 mousePos = Input.MouseScreenPosition();
			Rectangle mouseRect = new Rectangle((int)mousePos.X, (int)mousePos.Y, 1, 1);
			Rectangle boundingBox = new Rectangle((int)X, (int)Y, (int)Width, (int)Height);

			bool mousePressed = Input.IsPressed(MouseButton.Left);

			Hovering = false;
			if (mouseRect.Intersects(boundingBox))
				Hovering = true;

			Pressed = false;
			if (mousePressed && Hovering)
			{
				Pressed = true;
				if (OnPressed != null)
					OnPressed();
			}
		}
	}
}
