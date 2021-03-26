using Anchored.UI.Constraints;
using Anchored.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchored.UI.Elements
{
	public class UIDropDownList : UIComponent
	{
		private TextureRegion dropDownButtonArrowTexture;
		private TextureRegion dropDownButtonTexture;
		private TextureRegion dropDownListTexture;

		private UIButton uiDropDownArrow;
		private UIButton uiDropDownButton;
		private List<UIButton> uiDropDownButtonList = new List<UIButton>();

		public List<string> Items = new List<string>();
		public Action<string> OnSelectedItem;

		public SpriteFont TextFont;

		private bool open;
		public bool Open
		{
			get => open;
			set
			{
				open = value;

				if (Open)
					EnableDropDownButtons();
				else
					DisableDropDownButtons();
			}
		}

		public UIDropDownList(TextureRegion button, TextureRegion listTexture, TextureRegion arrowTexture, SpriteFont font)
		{
			dropDownButtonTexture = button;
			dropDownListTexture = listTexture;
			dropDownButtonArrowTexture = arrowTexture;
			TextFont = font;
			Open = false;
		}

		public UIDropDownList(TextureRegion button, TextureRegion listTexture, TextureRegion arrowTexture, SpriteFont font, List<string> items)
			: this(button, listTexture, arrowTexture, font)
		{
			this.Items = items;
		}

		public override void Init()
		{
			CreateDropDownArrowButton();
			CreateDropDownButton();
			CreateDropDownButtons();
			DisableDropDownButtons();
		}

		private void CreateDropDownArrowButton()
		{
			uiDropDownArrow = new UIButton(dropDownButtonArrowTexture);
			UIConstraints uiDropDownArrowConstraints = new UIConstraints();

			uiDropDownArrow.OnClicked = () =>
			{
				Open = !Open;
			};

			uiDropDownArrowConstraints.X = new CenterConstraint((int)(dropDownButtonArrowTexture.Width * 4f * -1.5f));
			uiDropDownArrowConstraints.Y = new PixelConstraint();
			uiDropDownArrowConstraints.Width = new TextureConstraint(dropDownButtonArrowTexture);
			uiDropDownArrowConstraints.Height = new TextureConstraint(dropDownButtonArrowTexture);

			base.Add(uiDropDownArrow, uiDropDownArrowConstraints);
		}

		private void CreateDropDownButton()
		{
			uiDropDownButton = new UIButton(dropDownButtonTexture, Items[0], TextFont);
			UIConstraints uiDropDownButtonConstraints = new UIConstraints();

			uiDropDownButton.OnClicked = () =>
			{
				if (OnSelectedItem != null && Open)
					OnSelectedItem(uiDropDownButton.Text);

				PushItemToFront(Items.FirstOrDefault());

				Open = !Open;
			};

			uiDropDownButtonConstraints.X = new CenterConstraint((int)(dropDownButtonArrowTexture.Width * 4f * 0.5f));
			uiDropDownButtonConstraints.Y = new PixelConstraint();
			uiDropDownButtonConstraints.Width = new TextureConstraint(dropDownButtonTexture);
			uiDropDownButtonConstraints.Height = new TextureConstraint(dropDownButtonTexture);

			base.Add(uiDropDownButton, uiDropDownButtonConstraints);
		}

		private void DisableDropDownButtons()
		{
			for (int ii = 0; ii < uiDropDownButtonList.Count; ii++)
			{
				uiDropDownButtonList[ii].Enabled = false;
			}
		}

		private void EnableDropDownButtons()
		{
			for (int ii = 0; ii < uiDropDownButtonList.Count; ii++)
			{
				uiDropDownButtonList[ii].Enabled = true;
			}
		}

		private void CreateDropDownButtons()
		{
			for (int yy = 1; yy < Items.Count; yy++)
			{
				string name = Items[yy];
				var btn = CreateDropDownButton(yy, name);
				uiDropDownButtonList.Add(btn);
			}
		}

		private UIButton CreateDropDownButton(int ii, string name)
		{
			int yOffset = (int)(ii * dropDownListTexture.Height * 4);

			UIButton btn = new UIButton(dropDownListTexture, name, TextFont);
			UIConstraints btnConstraints = new UIConstraints();

			btn.OnClicked = () =>
			{
				if (OnSelectedItem != null)
					OnSelectedItem(btn.Text);

				PushItemToFront(Items[ii]);
				
				Open = false;
			};

			btnConstraints.X = new CenterConstraint(4 * 4 + 2);
			btnConstraints.Y = new PixelConstraint(yOffset);
			btnConstraints.Width = new TextureConstraint(dropDownListTexture);
			btnConstraints.Height = new TextureConstraint(dropDownListTexture);

			base.Add(btn, btnConstraints);
			return btn;
		}

		private void PushItemToFront(string item)
		{
			string temp = item;
			Items.Remove(item);
			Items.Insert(0, temp);
			UpdateButtonData();
		}

		private void UpdateButtonData()
		{
			uiDropDownButton.Text = Items.FirstOrDefault();
			for (int ii = 1; ii < Items.Count; ii++)
				uiDropDownButtonList[ii-1].Text = Items[ii];
		}
	}
}
