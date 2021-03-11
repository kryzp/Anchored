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

			// Button
			{
				UIButton uiButton = new UIButton(Textures.Get("null"));
				UIConstraints constraints = new UIConstraints();

				constraints.X = new CenterConstraint();
				constraints.Y = new PixelConstraint(20);
				constraints.Width = new PixelConstraint(256);
				constraints.Height = new PixelConstraint(64);

				uiButton.OnPressed = () =>
				{
					System.Diagnostics.Debug.WriteLine("btn prsd");
				};

				display.Add(uiButton, constraints);
			}
		}
	}
}
