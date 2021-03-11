using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public abstract class UIConstraint
	{
		public abstract void ConstrainX(ref float value, UIComponent component);
		public abstract void ConstrainY(ref float value, UIComponent component);
		public abstract void ConstrainWidth(ref float value, UIComponent component);
		public abstract void ConstrainHeight(ref float value, UIComponent component);
	}
}
