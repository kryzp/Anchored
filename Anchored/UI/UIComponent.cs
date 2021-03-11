using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.UI
{
	public class UIComponent
	{
		private Dictionary<UIComponent, UIConstraints> components = new Dictionary<UIComponent, UIConstraints>();

		public float X;
		public float Y;
		public float Width;
		public float Height;
		public float Alpha;

		public Vector2 Position
		{
			get => new Vector2(X, Y);
			set
			{
				if (value != null)
				{
					this.X = value.X;
					this.Y = value.Y;
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
					this.Width = value.X;
					this.Height = value.Y;
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
			constraints?.X?.ConstrainX(ref component.X, component);
			constraints?.Y?.ConstrainY(ref component.Y, component);
			constraints?.Width?.ConstrainWidth(ref component.Width, component);
			constraints?.Height?.ConstrainHeight(ref component.Height, component);

			component.Init();
			components.Add(component, constraints);
		}
	}
}
