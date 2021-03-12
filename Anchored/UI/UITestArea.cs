using Anchored.Assets;
using Anchored.UI.Constraints;
using Anchored.UI.Elements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public class UITestArea
	{
		public static void CreateUI()
		{
			UIComponent display = UIManager.Container;

			var nullTex = Textures.Get("null");

			var checkBoxOffTex = Textures.Get("ui\\checkbox");
			checkBoxOffTex.Source = new Rectangle(0, 0, 9, 9);

			var checkBoxOnTex = Textures.Get("ui\\checkbox");
			checkBoxOnTex.Source = new Rectangle(9, 0, 9, 9);

			// Checkbox
			{
				UIToggleButton uiToggleButton = new UIToggleButton(checkBoxOffTex, checkBoxOnTex);
				UIConstraints constraints = new UIConstraints();

				constraints.X = new CenterConstraint();
				constraints.Y = new PixelConstraint(20);
				constraints.Width = new PixelConstraint(64);
				constraints.Height = new PixelConstraint(64);

				display.Add(uiToggleButton, constraints);
			}
		}
	}
}
