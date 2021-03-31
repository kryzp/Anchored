using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.UI
{
	public abstract class UIConstraint
	{
		public abstract int ConstrainX(UIComponent component);
		public abstract int ConstrainY(UIComponent component);
		public abstract int ConstrainWidth(UIComponent component);
		public abstract int ConstrainHeight(UIComponent component);
	}
}
