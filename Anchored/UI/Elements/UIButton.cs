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
		protected TextureRegion texture;
		protected TextureRegion textureHov;
		protected TextureRegion texturePrs;

		protected UITexture uiTexture;

		protected bool hasPressedAndHoldingDown = false;
		
		public Action OnPressed = null;
		public Action OnReleased = null;
		public Action OnHovering = null;

		public bool Pressed;
		public bool Released;
		public bool Hovering;

		public bool BlockInputOnHover = false;

		public UIButton(TextureRegion tex)
		{
			this.texture = tex;
		}

		public UIButton(TextureRegion tex, TextureRegion prs)
		{
			this.texture = tex;
			this.texturePrs = prs;
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

			bool mousePressed = Input.IsPressed(MouseButton.Left, true);
			bool mouseReleased = Input.IsReleased(MouseButton.Left, true);

			UpdateVariables();

			if (!hasPressedAndHoldingDown)
			{
				if (texture != null && uiTexture.Texture != texture)
					uiTexture.Texture = texture;
			}

			if (MouseHovering())
			{
				if (BlockInputOnHover)
					Input.EnableGuiFocus = true;

				Hovering = true;
				if (OnHovering != null)
					OnHovering();

				if (textureHov != null && uiTexture.Texture != textureHov)
					uiTexture.Texture = textureHov;
			}

			if ((mousePressed && Hovering) || hasPressedAndHoldingDown)
			{
				Pressed = true;
				if (OnPressed != null)
					OnPressed();

				if (texturePrs != null && uiTexture.Texture != texturePrs)
					uiTexture.Texture = texturePrs;

				hasPressedAndHoldingDown = true;
			}

			if (mouseReleased)
			{
				Released = true;
				if (OnReleased != null)
					OnReleased();

				hasPressedAndHoldingDown = false;
			}
		}

		private void UpdateVariables()
		{
			Hovering = false;
			Pressed = false;
			Released = false;
		}
	}
}
