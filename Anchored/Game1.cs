using Anchored.Assets;
using Anchored.Debug.Console;
using Anchored.Debug.DearImGui;
using Anchored.State;
using Anchored.UI;
using Anchored.World;
using Anchored.World.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using Anchored.Util.Math.Tween;
using Anchored.Save;

namespace Anchored
{
	public class Game1 : Game
	{
		private static Shader gameShader;
		
		private static ImGuiRenderer imGuiRenderer;

		public static GraphicsDeviceManager Graphics;
		public static SpriteBatch SpriteBatch;
		public static new GraphicsDevice GraphicsDevice;

		public static GameState CurrentState;
		public static GameState NextState;

		public const int WINDOW_WIDTH = 1280;
		public const int WINDOW_HEIGHT = 720;
		public static Color BackgroundColor;

		public static RenderTarget2D GameRenderTarget; // only the game stuff in a rendertarget
		public static RenderTarget2D AppRenderTarget; // the entire window surface in a rendertarget

		public static Entity Player;

		public static Int32 Seed = 745321968;

		public Game1()
		{
			Graphics = new GraphicsDeviceManager(this);

			AssetManager.Content = new ContentManager(base.Services, "Content");

			IsMouseVisible = true;
			IsFixedTimeStep = true;
		}

		protected override void Initialize()
		{
			GraphicsDevice = base.GraphicsDevice;

			GameRenderTarget = new RenderTarget2D(GraphicsDevice, WINDOW_WIDTH, WINDOW_HEIGHT);
			AppRenderTarget = new RenderTarget2D(GraphicsDevice, WINDOW_WIDTH, WINDOW_HEIGHT);

			Graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
			Graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
			Graphics.ApplyChanges();

			Window.TextInput += TextInputHandler;

			imGuiRenderer = new ImGuiRenderer(this);
			imGuiRenderer.RebuildFontAtlas();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			BackgroundColor = new Color(22, 18, 27);
			
			Camera.Main = new Camera(320, 180);

			SaveManager.Init();
			ChangeState(new AssetLoadState());
		}

		protected override void UnloadContent()
		{
			AssetManager.Destroy();
		}

		protected override void Update(GameTime gt)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			DebugConsole.Update();
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

			Time.TotalSeconds = Time.TotalSeconds;
			Time.TotalSeconds += Time.Delta;
			Time.GameTime = gt;

			if (NextState != null)
			{
				CurrentState?.Unload();
				CurrentState = NextState;
				Input.EnableImGuiFocus = false;
				CurrentState.Load(SpriteBatch);
				NextState = null;
			}

			gameShader?.Update();
			
			UIManager.Update();
			CurrentState.Update();
			
			base.Update(gt);
		}

		protected override void Draw(GameTime gt)
		{
			GraphicsDevice.SetRenderTarget(GameRenderTarget);
			GraphicsDevice.Clear(BackgroundColor);

			CurrentState.Draw(SpriteBatch);
			
			GraphicsDevice.SetRenderTarget(AppRenderTarget);
			
			SpriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
			gameShader?.Effect.CurrentTechnique.Passes[0].Apply();

			SpriteBatch.Draw(GameRenderTarget, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
			SpriteBatch.End();
			CurrentState.DrawUI(SpriteBatch);
			UIManager.Draw(SpriteBatch);

			GraphicsDevice.SetRenderTarget(null);
			
			SpriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
			SpriteBatch.Draw(AppRenderTarget, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
			SpriteBatch.End();

			imGuiRenderer.BeforeLayout(gt);
			CurrentState.DrawDebug();
			DebugConsole.Draw();
			imGuiRenderer.AfterLayout();
			
			base.Draw(gt);
		}

		public static GameState ChangeState(GameState state)
		{
			NextState = state;
			return state;
		}

		public static void SetGameShader(Shader shader)
		{
			gameShader = shader;
		}
		
		private void TextInputHandler(object sender, TextInputEventArgs args)
		{
			Input.HandleTextInput(args.Key, args.Character);
		}
	}
}
