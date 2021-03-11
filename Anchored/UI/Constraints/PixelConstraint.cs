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

		public override void ConstrainX(ref float value, UIComponent component) => value = Pixels;
		public override void ConstrainY(ref float value, UIComponent component) => value = Pixels;
		public override void ConstrainWidth(ref float value, UIComponent component) => value = Pixels;
		public override void ConstrainHeight(ref float value, UIComponent component) => value = Pixels;
	}
}
