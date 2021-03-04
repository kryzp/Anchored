using Anchored.Debug.DearImGui;
using Anchored.GameStates;
using Anchored.World;
using Anchored.World.Components;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

// Project-Wide TODO List:
// [ ] * Debugging Tools
//    [ ] * ImGui
//    [ ] * Entity Editor (Components)
//       [ ] * Debug window (Opens when you select a component on an entity)
//    [ ] * Animation Editor
//    [ ] * Dialog Editor
//    [ ] * Particle Editor
//    [ ] * Locale Editor (Language/Localization)
//    [ ] * Area Debug View
//       [ ] * Move the camera around and stuff
//    [ ] * Console
//    [ ] * Save File Explorer/Editor
//    [ ] * Item Editor
// [ ] * Basic UI (ThinMatrix did some cool stuff copy that lol)
// [ ] * Serialization System
// [ ] * Localization System
// [ ] * Cutscenes
//    [ ] * Custom scripting language thingy
// [ ] * Better Player
//    [ ] * Art
// [ ] * Improve UI Elements
// [ ] * Inventory
//    [ ] * Item System
//    [ ] * Art
// [ ] * World Stuff
//    [ ] * Improve Tiles
//    [ ] * Do some more work on parts of the world
// [ ] * SFX / Music
// [ ] * m o d   s u p p o r t

namespace Anchored
{
	public class Game1 : Game
	{
		private static ImGuiRenderer imGuiRenderer;

		public static GraphicsDeviceManager Graphics;
		public static SpriteBatch SpriteBatch;
		public static new GraphicsDevice GraphicsDevice;

		public static new ContentManager Content;
		public static ContentManager MapContent;

		private static GameState currentState;
		private static GameState nextState;

		public const int WINDOW_WIDTH = 1280;
		public const int WINDOW_HEIGHT = 720;
		public static Color BackgroundColor;

		public static RenderTarget2D GameRenderTarget; // Only the game stuff in a rendertarget
		public static RenderTarget2D AppRenderTarget; // the entire window surface in a rendertarget

		public static Entity Player;
		public static Camera Camera;

		public static Int32 Seed = 492982532;

		public Game1()
		{
			Graphics = new GraphicsDeviceManager(this);
			Content = new ContentManager(base.Services, "Content");
			MapContent = new ContentManager(base.Services, "Content");
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
			BackgroundColor = Color.CornflowerBlue;

			currentState = new PlayingState();
			currentState.Load(SpriteBatch);
		}
		
		protected override void Update(GameTime gt)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			Input.Update();

			Time.Delta = (float)gt.ElapsedGameTime.TotalSeconds;

			if (Time.PauseTimer > 0f)
			{
				Time.PauseTimer -= Time.Delta;
				if (Time.PauseTimer <= -0.0001f)
					Time.Delta = -Time.PauseTimer;
				else
					return;
			}

			Time.PrevSeconds = Time.Seconds;
			Time.Seconds += Time.Delta;
			Time.GameTime = gt;

			if (nextState != null)
			{
				currentState.Unload();
				currentState = nextState;
				currentState.Load(SpriteBatch);
				nextState = null;
			}

			currentState.Update();

			base.Update(gt);
		}

		protected override void Draw(GameTime gt)
		{
			GraphicsDevice.SetRenderTarget(GameRenderTarget);
			GraphicsDevice.Clear(BackgroundColor);

			currentState.Draw(SpriteBatch);
			
			GraphicsDevice.SetRenderTarget(AppRenderTarget);
			
			SpriteBatch.Begin(SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);
			SpriteBatch.Draw(GameRenderTarget, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
			SpriteBatch.End();
			currentState.DrawUI(SpriteBatch);
			
			GraphicsDevice.SetRenderTarget(null);
			
			SpriteBatch.Begin(SpriteSortMode.Immediate, samplerState: SamplerState.PointClamp);
			SpriteBatch.Draw(AppRenderTarget, new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT), Color.White);
			SpriteBatch.End();

			imGuiRenderer.BeforeLayout(gt);
			{
				ImGui.Begin("Test");
				ImGui.Text("Hello, World!");
				ImGui.End();
			}
			imGuiRenderer.AfterLayout();
			
			base.Draw(gt);
		}

		public static void ChangeState(GameState state)
		{
			nextState = state;
		}

		private void TextInputHandler(object sender, TextInputEventArgs args)
		{
			Input.HandleTextInput(args.Key, args.Character);
		}
	}
}
