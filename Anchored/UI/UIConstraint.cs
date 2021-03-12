using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public abstract class UIConstraint
	{
		public abstract void ConstrainX(ref int value, UIComponent component);
		public abstract void ConstrainY(ref int value, UIComponent component);
		public abstract void ConstrainWidth(ref int value, UIComponent component);
		public abstract void ConstrainHeight(ref int value, UIComponent component);
	}
}
