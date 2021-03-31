using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.UI
{
	public class UIConstraints
	{
		public UIConstraint X { get; set; } = null;
		public UIConstraint Y { get; set; } = null;
		public UIConstraint Width { get; set; } = null;
		public UIConstraint Height { get; set; } = null;

		public void Constrain(UIComponent component)
		{
			if (X != null)
				component.X = X.ConstrainX(component);

			if (Y != null)
				component.Y = Y.ConstrainY(component);

			if (Width != null)
				component.Width = Width.ConstrainWidth(component);

			if (Height != null)
				component.Height = Height.ConstrainHeight(component);
		}
	}
}
