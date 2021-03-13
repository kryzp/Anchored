using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Constraints
{
	public class CenterConstraint : UIConstraint
	{
		public CenterConstraint()
		{
		}

		public override int ConstrainX(UIComponent component)
		{
			return (Game1.WINDOW_WIDTH / 2) - (component.Width / 2);
		}

		public override int ConstrainY(UIComponent component)
		{
			return (Game1.WINDOW_HEIGHT / 2) - (component.Height / 2);
		}

		public override int ConstrainWidth(UIComponent component)
		{
			throw new NotImplementedException();
		}

		public override int ConstrainHeight(UIComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
