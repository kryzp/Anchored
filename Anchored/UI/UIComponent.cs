using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public class UIComponent
	{
		private Dictionary<UIComponent, UIConstraints> components = new Dictionary<UIComponent, UIConstraints>();

		public int X;
		public int Y;
		public int Width;
		public int Height;
		public int Alpha;

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

				constraints?.Constrain(component);
				component.Update();
			}
		}

		public virtual void Draw(SpriteBatch sb)
		{
			foreach (var component in components.Keys)
			{
				component.Draw(sb);
			}
		}

		public void Add(UIComponent component, UIConstraints constraints)
		{
			constraints?.Constrain(component);
			component.Init();
			components.Add(component, constraints);
		}
	}
}
