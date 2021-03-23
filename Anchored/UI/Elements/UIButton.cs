using Anchored.UI.Constraints;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Anchored.UI.Elements
{
	public class UIButton : UIComponent
	{
		private TextureRegion texture;
		private TextureRegion textureHov;
		private TextureRegion texturePrs;

		private UITexture uiTexture;

		private bool hasPressedAndHoldingDown = false;
		
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
		
		public UIButton(TextureRegion tex, TextureRegion prs, TextureRegion hov)
		{
			this.texture = tex;
			this.texturePrs = prs;
			this.textureHov = hov;
		}

		public override void Init()
		{
			// Texture
			{
				uiTexture = new UITexture(texture);
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

			if (!hasPressedAndHoldingDown)
			{
				if (texture != null && uiTexture.Texture != texture)
					uiTexture.Texture = texture;
			}

			Hovering = false;
			if (mouseRect.Intersects(boundingBox))
			{
				Hovering = true;
				if (OnHovering != null)
					OnHovering();
				
				if (textureHov != null && uiTexture.Texture != textureHov)
					uiTexture.Texture = textureHov;
			}

			Pressed = false;
			if ((mousePressed && Hovering) || hasPressedAndHoldingDown)
			{
				Pressed = true;
				if (OnPressed != null)
					OnPressed();
				
				if (texturePrs != null && uiTexture.Texture != texturePrs)
					uiTexture.Texture = texturePrs;

				hasPressedAndHoldingDown = true;
			}

			Released = false;
			if (mouseReleased)
			{
				Released = true;
				if (OnReleased != null)
					OnReleased();

				hasPressedAndHoldingDown = false;
			}
		}
	}
}
