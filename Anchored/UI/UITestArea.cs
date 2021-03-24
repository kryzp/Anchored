using Anchored.Assets;
using Anchored.UI.Constraints;
using Anchored.UI.Elements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public class UITestArea : UIMenu
	{
		public override void Create()
		{
			UIComponent display = UIManager.Container;

			var nullTex = Textures.Get("null");
			var checkBoxOffTex = TileSheetBounds.Get("ui\\checkbox", "unchecked");
			var checkBoxOnTex = TileSheetBounds.Get("ui\\checkbox", "checked");
			var textboxTex = TileSheetBounds.Get("ui\\textbox", "nsg");

			// Nine Slice Grid
			{
				UINineSliceGrid uiNineSliceGrid = new UINineSliceGrid(textboxTex, 9, 9, 9, 3);
				UIConstraints constraints = new UIConstraints();

				constraints.X = new CenterConstraint();
				constraints.Y = new PixelConstraint(16);

				display.Add(uiNineSliceGrid, constraints);
			}
		}
	}
}
