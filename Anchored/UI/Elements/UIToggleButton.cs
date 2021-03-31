using Anchored.UI.Constraints;
using Arch;
using Arch.UI;
using Arch.Util;
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

		public bool Toggled;
		public bool Hovering;

		public bool BlockInputOnHover = false;

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
				uiTextureConstraints.Y = new PixelConstraint();
				uiTextureConstraints.Width = new PixelConstraint(Width);
				uiTextureConstraints.Height = new PixelConstraint(Height);

				base.Add(uiTexture, uiTextureConstraints);
			}
		}

		public override void Update()
		{
			base.Update();

			Hovering = false;
			if (MouseHoveringOver())
			{
				if (BlockInputOnHover)
					Input.EnableGuiFocus = true;

				Hovering = true;
				if (OnHovering != null)
					OnHovering();

				if (textureHov != null && uiTexture.Texture != textureHov)
					uiTexture.Texture = textureHov;
			}

			if (MouseClicked() && Hovering)
			{
				Toggled = !Toggled;

				if (Toggled)
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

			if (Toggled)
			{
				if (WhileEnabled != null)
					WhileEnabled();
			}
		}
	}
}
