﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Constraints
{
	public class CenterConstraint : UIConstraint
	{
		public CenterConstraint()
		{
		}

		public override void ConstrainX(ref float value, UIComponent component)
		{
			value = (Game1.WINDOW_WIDTH / 2) - (component.Width / 2);
		}

		public override void ConstrainY(ref float value, UIComponent component)
		{
			value = (Game1.WINDOW_HEIGHT / 2) - (component.Height / 2);
		}

		public override void ConstrainWidth(ref float value, UIComponent component)
		{
			throw new NotImplementedException();
		}

		public override void ConstrainHeight(ref float value, UIComponent component)
		{
			throw new NotImplementedException();
		}
	}
}
