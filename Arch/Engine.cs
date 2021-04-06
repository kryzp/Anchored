using Arch.Assets;
using Arch.Debug.DearImGui;
using Arch.Math.Tween;
using Arch.State;
using Arch.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Arch
{
	public class Engine : Game
	{
		private static int initialWindowWidth;
		private static int initialWindowHeight;

		private static Shader gameShader;
		private static Shader appShader;

		private static ImGuiRenderer imGuiRenderer;

		public static GraphicsDeviceManager Graphics;
		public static SpriteBatch SpriteBatch;
		public static new GraphicsDevice GraphicsDevice;

		public static RenderTarget2D GameRenderTarget; // only the game stuff in a rendertarget
		public static RenderTarget2D AppRenderTarget; // the entire window surface in a rendertarget

		public static GameState CurrentState;
		public static GameState NextState;

		public static int WindowWidth
		{
			get => Graphics.PreferredBackBufferWidth;
			set
			{
				Graphics.PreferredBackBufferWidth = value;
				Graphics.ApplyChanges();
			}
		}

		public static int WindowHeight
		{
			get => Graphics.PreferredBackBufferHeight;
			set
			{
				Graphics.PreferredBackBufferHeight = value;
				Graphics.ApplyChanges();
			}
		}

		public static Color BackgroundColor;
		public const Int32 SEED = 745321968;

		public object DebugConsole { get; private set; }

		public Engine(string windowName, int windowWidth, int windowHeight)
		{
			Graphics = new GraphicsDeviceManager(this);

			AssetManager.Content = new ContentManager(base.Services, "Content");

			this.Window.AllowAltF4 = true;
			this.Window.Title = windowName;

			initialWindowWidth = windowWidth;
			initialWindowHeight = windowHeight;

			IsMouseVisible = true;
			IsFixedTimeStep = true;
			Window.AllowUserResizing = true;
		}

		protected override void Initialize()
		{
			GraphicsDevice = base.GraphicsDevice;

			GameRenderTarget = new RenderTarget2D(GraphicsDevice, initialWindowWidth, initialWindowHeight);
			AppRenderTarget = new RenderTarget2D(GraphicsDevice, initialWindowWidth, initialWindowHeight);

			Graphics.PreferredBackBufferWidth = initialWindowWidth;
			Graphics.PreferredBackBufferHeight = initialWindowHeight;
			Graphics.ApplyChanges();

			Window.TextInput += TextInputHandler;

			imGuiRenderer = new ImGuiRenderer(this);
			imGuiRenderer.RebuildFontAtlas();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			SpriteBatch = new SpriteBatch(GraphicsDevice);
			BackgroundColor = new Color(22, 18, 27);
		}

		protected override void Update(GameTime gt)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			Tween.Update();
			Input.Update();

			Time.RawDelta = (float)gt.ElapsedGameTime.TotalSeconds;

			if (Time.PauseTimer > 0f)
			{
				Time.PauseTimer -= Time.Delta;
				if (Time.PauseTimer <= -0.0001f)
					Time.RawDelta = -Time.PauseTimer;
				else
					return;
			}

			Time.PrevTotalSeconds = Time.TotalSeconds;
			Time.TotalSeconds += Time.Delta;
			Time.GameTime = gt;

			if (NextState != null)
			{
				CurrentState?.Unload();
				CurrentState = NextState;
				Input.EnableGuiFocus = false;
				CurrentState.Load(SpriteBatch);
				NextState = null;
			}

			gameShader?.Update();

			UIManager.Update();
			CurrentState?.Update();

			base.Update(gt);
		}

		protected override void Draw(GameTime gt)
		{
			GraphicsDevice.SetRenderTarget(GameRenderTarget);
			GraphicsDevice.Clear(BackgroundColor);
			CurrentState?.Draw(SpriteBatch);
			GraphicsDevice.SetRenderTarget(AppRenderTarget);

			SpriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
			gameShader?.Effect.CurrentTechnique.Passes[0].Apply();
			SpriteBatch.Draw(GameRenderTarget, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
			SpriteBatch.End();
			CurrentState?.DrawUI(SpriteBatch);
			UIManager.Draw(SpriteBatch);
			GraphicsDevice.SetRenderTarget(null);

			SpriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
			appShader?.Effect.CurrentTechnique.Passes[0].Apply();
			SpriteBatch.Draw(AppRenderTarget, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
			SpriteBatch.End();

			imGuiRenderer.BeforeLayout(gt);
			CurrentState?.DrawDebug();
			imGuiRenderer.AfterLayout();

			base.Draw(gt);
		}

		public static GameState ChangeState(GameState state)
		{
			UIManager.Container.Clear();
			NextState = state;
			return state;
		}

		public static void SetGameShader(Shader shader)
		{
			gameShader = shader;
		}

		public static void SetAppShader(Shader shader)
		{
			appShader = shader;
		}

		private void TextInputHandler(object sender, TextInputEventArgs args)
		{
			Input.HandleTextInput(args.Key, args.Character);
		}
	}
}
