using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public class UIConstraints
	{
		public UIConstraint X { get; set; } = null;
		public UIConstraint Y { get; set; } = null;
		public UIConstraint Width { get; set; } = null;
		public UIConstraint Height { get; set; } = null;

		public void Constrain(UIComponent component)
		{
			X?.ConstrainX(ref component.X, component);
			Y?.ConstrainY(ref component.Y, component);
			Width?.ConstrainWidth(ref component.Width, component);
			Height?.ConstrainHeight(ref component.Height, component);
		}
	}
}
