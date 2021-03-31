using Arch;
using Arch.Util;
using Microsoft.Xna.Framework.Graphics;

namespace Anchored.UI.Elements
{
	public class UIInputBox : UITextBox
	{
		public bool Selected;

		public UIInputBox(SpriteFont font, TextureRegion texture, int gridW, int gridH, int width, int height, float textureScale = 4)
			: base("", font, texture, gridW, gridH, width, height, textureScale)
		{
		}

		public override void Update()
		{
			base.Update();

			if (MouseClickedOnMe())
				Selected = true;
			else if (MouseClicked())
				Selected = false;

			if (Selected)
			{
				Input.EnableGuiFocus = true;

				if (Input.TextInput != null)
					Text = Input.TextInput;
			}
		}
	}
}
