using Anchored.UI.Constraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public static class ConstraintFactory
	{
		public static UIConstraints GetPosition(int x, int y, int width, int height)
		{
			UIConstraints constraints = new UIConstraints();

			constraints.X = new PixelConstraint(x);
			constraints.Y = new PixelConstraint(y);
			constraints.Width = new PixelConstraint(width);
			constraints.Height = new PixelConstraint(height);

			return constraints;
		}
	}
}
