using Anchored.Assets;
using Anchored.UI.Constraints;
using Anchored.UI.Elements;
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
			UIComponent uiTexture = new UITexture(Textures.Get("null"));
			UIConstraints constraints = new UIConstraints();

			constraints.X = new CenterConstraint();
			constraints.Y = new PixelConstraint(20);
			constraints.Width = new PixelConstraint(64);
			constraints.Height = new PixelConstraint(64);

			display.Add(uiTexture, constraints);
		}
	}
}
