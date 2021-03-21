using Anchored.UI.Constraints;
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
	public class UIButton : UIComponent
	{
		private TextureRegion texture;

		public Action OnPressed = null;
		public Action OnReleased = null;
		public Action OnHovering = null;

		public bool Pressed;
		public bool Released;
		public bool Hovering;

		public UIButton(TextureRegion tex)
		{
			this.texture = tex;
		}

		public UIButton(TextureRegion tex, int width, int height)
			: this(tex)
		{
			this.Width = width;
			this.Height = height;
		}

		public override void Init()
		{
			// Texture
			{
				// todo: maybe also like a hover texture?

				UITexture uiTexture = new UITexture(texture);
				UIConstraints uiTextureConstraints = new UIConstraints();

				uiTextureConstraints.X = new CenterConstraint();
				uiTextureConstraints.Y = new PixelConstraint(Y);
				uiTextureConstraints.Width = new PixelConstraint(Width);
				uiTextureConstraints.Height = new PixelConstraint(Height);
				
				base.Add(uiTexture, uiTextureConstraints);
			}
		}

		public override void Update()
		{
			base.Update();

			Point mousePos = Input.MouseScreenPosition();
			Rectangle mouseRect = new Rectangle(mousePos.X, mousePos.Y, 1, 1);
			Rectangle boundingBox = new Rectangle(X, Y, Width, Height);

			bool mousePressed = Input.IsPressed(MouseButton.Left);
			bool mouseReleased = Input.IsReleased(MouseButton.Left);

			Hovering = false;
			if (mouseRect.Intersects(boundingBox))
			{
				Hovering = true;
				if (OnHovering != null)
					OnHovering();
			}

			Pressed = false;
			if (mousePressed && Hovering)
			{
				Pressed = true;
				if (OnPressed != null)
					OnPressed();
			}

			Released = false;
			if (mouseReleased && Hovering)
			{
				Released = true;
				if (OnReleased != null)
					OnReleased();
			}
		}
	}
}
