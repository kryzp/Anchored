using Anchored.UI.Constraints;
using Anchored.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Elements
{
	public class UIToggleButton : UIComponent
	{
		private TextureRegion textureOn;
		private TextureRegion textureOff;
		private TextureRegion textureHov;
		
		private UITexture uiTexture;

		public Action OnEnabled = null;
		public Action WhileEnabled = null;
		public Action OnHovering = null;

		public bool Enabled;
		public bool Hovering;

		/// <summary>
		/// When set to true, the button will activate upon being pressed, otherwise it will activate upon being released.
		/// </summary>
		public bool Instant = true;

		public UIToggleButton(TextureRegion texOff, TextureRegion texOn)
		{
			this.textureOn = texOn;
			this.textureOff = texOff;
		}
		
		public UIToggleButton(TextureRegion texOff, TextureRegion texOn, TextureRegion texHov)
		{
			this.textureOn = texOn;
			this.textureOff = texOff;
			this.textureHov = texHov;
		}

		public override void Init()
		{
			// Texture
			{
				uiTexture = new UITexture(textureOff);
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

				if (textureHov != null && uiTexture.Texture != textureHov)
					uiTexture.Texture = textureHov;
			}

			bool mouse = (Instant) ? mousePressed : mouseReleased;

			if (mouse && Hovering)
			{
				Enabled = !Enabled;

				if (Enabled)
				{
					uiTexture.Texture = textureOn;

					if (OnEnabled != null)
						OnEnabled();
				}
				else
				{
					uiTexture.Texture = textureOff;
				}
			}

			if (Enabled)
			{
				if (WhileEnabled != null)
					WhileEnabled();
			}
		}
	}
}
