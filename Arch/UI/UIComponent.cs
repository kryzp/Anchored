using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arch.UI
{
	public class UIComponent
	{
		private Dictionary<UIComponent, UIConstraints> components = new Dictionary<UIComponent, UIConstraints>();

		private int x;
		private int y;
		private int width;
		private int height;

		public int X
		{
			get => x;
			set => x = value + ((Parent != null) ? Parent.X : 0);
		}

		public int Y
		{
			get => y;
			set => y = value + ((Parent != null) ? Parent.Y : 0);
		}

		public int Width
		{
			get => width;
			set => width = value;
		}

		public int Height
		{
			get => height;
			set => height = value;
		}

		public float Alpha;

		public UIComponent Parent = null;

		public bool Enabled = true;

		public Vector2 Position
		{
			get => new Vector2(X, Y);
			set
			{
				if (value != null)
				{
					this.X = (int)value.X;
					this.Y = (int)value.Y;
				}
			}
		}

		public Vector2 Size
		{
			get => new Vector2(Width, Height);
			set
			{
				if (value != null)
				{
					this.Width = (int)value.X;
					this.Height = (int)value.Y;
				}
			}
		}

		public UIComponent()
		{
		}

		public virtual void Init()
		{
		}

		public virtual void Update()
		{
			foreach (var pair in components)
			{
				var component = pair.Key;
				var constraints = pair.Value;

				if (!component.Enabled)
					continue;

				constraints?.Constrain(component);
				component.Update();
			}
		}

		public virtual void Draw(SpriteBatch sb)
		{
			foreach (var component in components.Keys)
			{
				if (!component.Enabled)
					continue;

				component.Draw(sb);
			}
		}

		public void Add(UIComponent component, UIConstraints constraints)
		{
			component.Parent = this;
			constraints?.Constrain(component);
			component.Init();
			components.Add(component, constraints);
		}

		public void Clear()
		{
			components.Clear();
		}

		public virtual bool MouseHoveringOver(bool includeChildren = true)
		{
			bool hovering = false;

			Point mousePos = Input.MouseScreenPosition();
			Rectangle mouseRect = new Rectangle(mousePos.X, mousePos.Y, 1, 1);

			Rectangle parentBoundingBox = new Rectangle(X, Y, Width, Height);
			hovering = mouseRect.Intersects(parentBoundingBox);

			if (!hovering && includeChildren)
			{
				foreach (var child in components.Keys)
				{
					Rectangle boundingBox = new Rectangle(child.X, child.Y, child.Width, child.Height);
					hovering = mouseRect.Intersects(boundingBox);

					if (hovering)
						break;
				}
			}

			return hovering;
		}

		public virtual bool MouseClickedOnMe(bool includeChildren = true)
		{
			return MouseHoveringOver(includeChildren) && MouseClicked();
		}

		public bool MouseClicked()
		{
			return Input.IsPressed("select", true);
		}
	}
}
