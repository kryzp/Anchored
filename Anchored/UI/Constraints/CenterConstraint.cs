using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Constraints
{
	public class CenterConstraint : UIConstraint
	{
		private int offset;

		public CenterConstraint(int offset = 0)
		{
			this.offset = offset;
		}

		public override int ConstrainX(UIComponent component)
		{
			if (component.Parent != null)
			{
				return ((component.Parent.Width / 2) - (component.Width / 2)) + offset;
			}
			else
			{
				return ((Game1.WINDOW_WIDTH / 2) - (component.Width / 2)) + offset;
			}
		}

		public override int ConstrainY(UIComponent component)
		{
			if (component.Parent != null)
			{
				return ((component.Parent.Height / 2) - (component.Height / 2)) + offset;
			}
			else
			{
				return ((Game1.WINDOW_HEIGHT / 2) - (component.Height / 2)) + offset;
			}
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
