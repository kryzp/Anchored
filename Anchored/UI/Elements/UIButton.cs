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
		private string initText = "";

		protected UITexture uiTexture;
		protected UIText uiText;

		protected bool hasPressedAndHoldingDown = false;
		
		public Action OnClicked = null;
		public Action OnReleased = null;
		public Action OnHovering = null;

		public bool Clicked;
		public bool Released;
		public bool Hovering;

		public bool BlockInputOnHover = false;

		public SpriteFont Font;
		public string Text
		{
			get
			{
				if (uiText != null)
				{
					return uiText.Text;
				}

				return "<UI_BUTTON_TEXT_NOT_SETUP>";
			}

			set
			{
				if (uiText != null)
				{
					uiText.Text = value;
				}
			}
		}

		public TextureRegion Texture = null;
		public TextureRegion TextureHov = null;
		public TextureRegion TexturePrs = null;

		public float LayerDepth = 0.95f;

		public UIButton(TextureRegion tex)
		{
			this.Texture = tex;
		}

		public UIButton(TextureRegion tex, string text, SpriteFont font)
			: this(tex)
		{
			this.initText = text;
			this.Font = font;
		}

		public override void Init()
		{
			// Texture
			if (Texture != null)
			{
				uiTexture = new UITexture(Texture);
				uiTexture.LayerDepth = LayerDepth - 0.01f;

				UIConstraints uiTextureConstraints = new UIConstraints();

				uiTextureConstraints.X = new PixelConstraint();
				uiTextureConstraints.Y = new PixelConstraint();
				uiTextureConstraints.Width = new TextureConstraint(Texture);
				uiTextureConstraints.Height = new TextureConstraint(Texture);
				
				base.Add(uiTexture, uiTextureConstraints);
			}

			// Text
			if (Font != null && initText != "")
			{
				uiText = new UIText(initText, Font);
				uiText.LayerDepth = LayerDepth;

				UIConstraints uiTextConstraints = new UIConstraints();

				uiTextConstraints.X = new CenterConstraint();
				uiTextConstraints.Y = new CenterConstraint();

				base.Add(uiText, uiTextConstraints);
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
				if (Texture != null && uiTexture.Texture != Texture)
					uiTexture.Texture = Texture;
			}

			if (MouseHoveringOver())
			{
				if (BlockInputOnHover)
					Input.EnableGuiFocus = true;

				Hovering = true;
				if (OnHovering != null)
					OnHovering();

				if (TextureHov != null && uiTexture.Texture != TextureHov)
					uiTexture.Texture = TextureHov;
			}

			if ((mousePressed && Hovering))
			{
				Clicked = true;
				if (OnClicked != null)
					OnClicked();

				if (hasPressedAndHoldingDown)
				{
					if (TexturePrs != null && uiTexture.Texture != TexturePrs)
						uiTexture.Texture = TexturePrs;
				}

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
			Clicked = false;
			Released = false;
		}
	}
}
