using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Constraints
{
	public class MouseConstraint : UIConstraint
	{
		private int offset;

		public MouseConstraint(int offset = 0)
		{
			this.offset = offset;
		}

		public override int ConstrainX(UIComponent component) => Input.MouseScreenPosition().X + offset;
		public override int ConstrainY(UIComponent component) => Input.MouseScreenPosition().Y + offset;
		public override int ConstrainWidth(UIComponent component) => throw new NotImplementedException();
		public override int ConstrainHeight(UIComponent component) => throw new NotImplementedException();
	}
}
