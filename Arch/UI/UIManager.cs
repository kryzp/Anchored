using Microsoft.Xna.Framework.Graphics;

namespace Arch.UI
{
	public static class UIManager
	{
		public static UIComponent Container { get; private set; }

		static UIManager()
		{
			Container = new UIComponent();
			Container.Width = Engine.WindowWidth;
			Container.Height = Engine.WindowHeight;
		}

		public static void Update()
		{
			Input.EnableGuiFocus = false;
			Container.Update();
		}

		public static void Draw(SpriteBatch sb)
		{
			sb.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
			Container.Draw(sb);
			sb.End();
		}
	}
}
