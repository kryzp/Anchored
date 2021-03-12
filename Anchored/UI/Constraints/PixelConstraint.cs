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

		public override void ConstrainX(ref int value, UIComponent component) => value = Pixels;
		public override void ConstrainY(ref int value, UIComponent component) => value = Pixels;
		public override void ConstrainWidth(ref int value, UIComponent component) => value = Pixels;
		public override void ConstrainHeight(ref int value, UIComponent component) => value = Pixels;
	}
}
