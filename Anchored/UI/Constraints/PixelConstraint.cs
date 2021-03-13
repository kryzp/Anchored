using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Constraints
{
	public class PixelConstraint : UIConstraint
	{
		public int Pixels;

		public PixelConstraint(int px)
		{
			this.Pixels = px;
		}

		public override int ConstrainX(UIComponent component) => Pixels;
		public override int ConstrainY(UIComponent component) => Pixels;
		public override int ConstrainWidth(UIComponent component) => Pixels;
		public override int ConstrainHeight(UIComponent component) => Pixels;
	}
}
