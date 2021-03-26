using Anchored.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI.Constraints
{
	public class TextureConstraint : UIConstraint
	{
		private TextureRegion texture;
		private float scale;

		public TextureConstraint(TextureRegion texture, float scale = 4f)
		{
			this.texture = texture;
			this.scale = scale;
		}

		public override int ConstrainX(UIComponent component) => throw new NotImplementedException();
		public override int ConstrainY(UIComponent component) => throw new NotImplementedException();
		public override int ConstrainWidth(UIComponent component) => (int)(texture.Width * scale);
		public override int ConstrainHeight(UIComponent component) => (int)(texture.Height * scale);
	}
}
