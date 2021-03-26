using Anchored.Assets;
using Anchored.Assets.Textures;
using Anchored.UI.Constraints;
using Anchored.UI.Elements;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public class UITestArea : UIMenu
	{
		public override void Create()
		{
			UIComponent display = UIManager.Container;

			var nullTex = TextureManager.Get("null");
			var checkBoxOffTex = TextureBoundManager.Get("ui\\checkbox", "unchecked");
			var checkBoxOnTex = TextureBoundManager.Get("ui\\checkbox", "checked");
			var textboxTex = TextureBoundManager.Get("ui\\textbox", "nsg");
			var textboxFont = FontManager.Get("small_font");
			var dropdownButton = TextureBoundManager.Get("ui\\dropdown_list", "button");
			var dropdownListTex = TextureBoundManager.Get("ui\\dropdown_list", "list");
			var dropdownArrowTex = TextureBoundManager.Get("ui\\dropdown_list", "arrow");

			// Drop Down List
			{
				UIDropDownList uiDropDownList = new UIDropDownList(dropdownButton, dropdownListTex, dropdownArrowTex, textboxFont, new List<string>()
				{
					"Option A",
					"Option B",
					"Option C",
					"Option D"
				});

				uiDropDownList.OnSelectedItem = (item) =>
				{
					System.Diagnostics.Debug.WriteLine(item);
				};

				UIConstraints constraints = new UIConstraints();

				constraints.X = new CenterConstraint();
				constraints.Y = new PixelConstraint(16);

				display.Add(uiDropDownList, constraints);
			}
		}
	}
}
