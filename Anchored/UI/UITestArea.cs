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

			var checkBoxOffTex = TileSheetBounds.Get("ui\\checkbox", "unchecked");
			var checkBoxOnTex = TileSheetBounds.Get("ui\\checkbox", "checked");

			// Checkbox
			{
				UIToggleButton uiToggleButton = new UIToggleButton(checkBoxOffTex, checkBoxOnTex);
				UIConstraints constraints = new UIConstraints();

				constraints.X = new CenterConstraint();
				constraints.Y = new PixelConstraint(20);
				constraints.Width = new PixelConstraint(36);
				constraints.Height = new PixelConstraint(36);

				display.Add(uiToggleButton, constraints);
			}
		}
	}
}
