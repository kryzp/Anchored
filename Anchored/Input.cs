using Anchored.Assets;
using Anchored.World.Components;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Anchored
{
	public enum MouseButton
	{
		None = 0,
		Left,
		Middle,
		Right,
	}

	public enum GamePadButton
	{
		None = 0,
		X,
		Y,
		A,
		B,
		DPadUp,
		DPadDown,
		DPadLeft,
		DPadRight,
		LeftShoulder,
		RightShoulder,
		LeftStick,
		RightStick,
		Back,
		BigButton,
		Start,
	}

	public class VirtualButton
	{
		public List<Keys> Keys = new List<Keys>();
		public List<MouseButton> MouseButtons = new List<MouseButton>();
		public List<GamePadButton> GamePadButtons = new List<GamePadButton>();

		public bool IsDown() => Input.IsDown(this);
		public bool IsPressed() => Input.IsPressed(this);
		public bool IsReleased() => Input.IsReleased(this);
	}

	public static class Input
	{
		private static KeyboardState prevKeyboardState;
		private static KeyboardState keyboardState;
		private static bool guiBlocksKeyboard => Input.EnableGuiFocus || (AssetManager.ImGuiEnabled && ImGui.GetIO().WantCaptureKeyboard);

		private static MouseState prevMouseState;
		private static MouseState mouseState;
		private static bool guiBlocksMouse =>  Input.EnableGuiFocus || (AssetManager.ImGuiEnabled && ImGui.GetIO().WantCaptureMouse);
		
		private static GamePadState[] prevGamePadState;
		private static GamePadState[] gamePadState;

		private static string textInput;
		public static string TextInput => textInput;

		public static bool EnableGuiFocus;

		public const int TabSize = 4;

		static Input()
		{
			prevGamePadState = new GamePadState[4];
			gamePadState = new GamePadState[4];
		}

		public static void Update()
		{
			prevKeyboardState = keyboardState;
			keyboardState = Keyboard.GetState();

			prevMouseState = mouseState;
			mouseState = Mouse.GetState();

			prevGamePadState[0] = gamePadState[0];
			prevGamePadState[1] = gamePadState[1];
			prevGamePadState[2] = gamePadState[2];
			prevGamePadState[3] = gamePadState[3];

			gamePadState[0] = GamePad.GetState(PlayerIndex.One);
			gamePadState[1] = GamePad.GetState(PlayerIndex.Two);
			gamePadState[2] = GamePad.GetState(PlayerIndex.Three);
			gamePadState[3] = GamePad.GetState(PlayerIndex.Four);
		}

		public static Point MouseScreenPosition()
		{
			return mouseState.Position;
		}

		public static Vector2 MouseWorldPosition(Matrix transform)
		{
			Vector2 mousePos = MouseScreenPosition().ToVector2();
			Matrix inverse = Matrix.Invert(transform);
			return Vector2.Transform(mousePos, inverse);
		}

		public static Vector2 MouseWorldPosition(Camera camera)
		{
			return MouseWorldPosition(camera.GetViewMatrix());
		}

		public static int MouseScroll()
		{
			return mouseState.ScrollWheelValue / 120;
		}

		public static int MouseScrollChange()
		{
			return (mouseState.ScrollWheelValue - prevMouseState.ScrollWheelValue) / 120;
		}

		public static bool IsDown(VirtualButton button, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			foreach (var key in button.Keys)
			{
				if (IsDown(key, ignoreGui))
					return true;
			}

			foreach (var mb in button.MouseButtons)
			{
				if (IsDown(mb, ignoreGui))
					return true;
			}

			foreach (var btn in button.GamePadButtons)
			{
				if (IsDown(btn, index))
					return true;
			}

			return false;
		}

		public static bool IsPressed(VirtualButton button, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			foreach (var key in button.Keys)
			{
				if (IsPressed(key, ignoreGui))
					return true;
			}

			foreach (var mb in button.MouseButtons)
			{
				if (IsPressed(mb, ignoreGui))
					return true;
			}

			foreach (var btn in button.GamePadButtons)
			{
				if (IsPressed(btn, index))
					return true;
			}

			return false;
		}

		public static bool IsReleased(VirtualButton button, bool ignoreGui = false, PlayerIndex index = PlayerIndex.One)
		{
			foreach (var key in button.Keys)
			{
				if (IsReleased(key, ignoreGui))
					return true;
			}

			foreach (var mb in button.MouseButtons)
			{
				if (IsReleased(mb, ignoreGui))
					return true;
			}

			foreach (var btn in button.GamePadButtons)
			{
				if (IsReleased(btn, index))
					return true;
			}

			return false;
		}

		public static bool IsDown(MouseButton button, bool ignoreGui = false)
		{
			if (button == MouseButton.Left)
			{
				return (
					mouseState.LeftButton == ButtonState.Pressed
				) && (ignoreGui || !guiBlocksMouse);
			}
			else if (button == MouseButton.Middle)
			{
				return (
					mouseState.LeftButton == ButtonState.Pressed
				) && (ignoreGui || !guiBlocksMouse);
			}
			else if (button == MouseButton.Right)
			{
				return (
					mouseState.LeftButton == ButtonState.Pressed
				) && (ignoreGui || !guiBlocksMouse);
			}

			return false;
		}

		public static bool IsPressed(MouseButton button, bool ignoreGui = false)
		{
			if (button == MouseButton.Left)
			{
				return (
					mouseState.LeftButton == ButtonState.Pressed &&
					prevMouseState.LeftButton == ButtonState.Released
				) && (ignoreGui || !guiBlocksMouse);
			}
			else if (button == MouseButton.Middle)
			{
				return (
					mouseState.MiddleButton == ButtonState.Pressed &&
					prevMouseState.MiddleButton == ButtonState.Released
				) && (ignoreGui || !guiBlocksMouse);
			}
			else if (button == MouseButton.Right)
			{
				return (
					mouseState.RightButton == ButtonState.Pressed &&
					prevMouseState.RightButton == ButtonState.Released
				) && (ignoreGui || !guiBlocksMouse);
			}

			return false;
		}

		public static bool IsReleased(MouseButton button, bool ignoreGui = false)
		{
			if (button == MouseButton.Left)
			{
				return (
					mouseState.LeftButton == ButtonState.Released &&
					prevMouseState.LeftButton == ButtonState.Pressed
				) && (ignoreGui || !guiBlocksMouse);
			}
			else if (button == MouseButton.Middle)
			{
				return (
					mouseState.MiddleButton == ButtonState.Released &&
					prevMouseState.MiddleButton == ButtonState.Pressed
				) && (ignoreGui || !guiBlocksMouse);
			}
			else if (button == MouseButton.Right)
			{
				return (
					mouseState.RightButton == ButtonState.Released &&
					prevMouseState.RightButton == ButtonState.Pressed
				) && (ignoreGui || !guiBlocksMouse);
			}

			return false;
		}
		
		public static bool IsDown(Keys key, bool ignoreGui = false)
		{
			return keyboardState.IsKeyDown(key) && (ignoreGui || !guiBlocksKeyboard);
		}

		public static bool IsPressed(Keys key, bool ignoreGui = false)
		{
			return (
				keyboardState.IsKeyDown(key) &&
				prevKeyboardState.IsKeyUp(key)
			) && (ignoreGui || !guiBlocksKeyboard);
		}

		public static bool IsReleased(Keys key, bool ignoreGui = false)
		{
			return (
				keyboardState.IsKeyUp(key) &&
				prevKeyboardState.IsKeyDown(key)
			) && (ignoreGui || !guiBlocksKeyboard);
		}

		public static bool Ctrl(bool ignoreGui = false)
		{
			return IsDown(Keys.LeftControl, ignoreGui) || Input.IsDown(Keys.RightControl, ignoreGui);
		}

		public static bool Shift(bool ignoreGui = false)
		{
			return IsDown(Keys.LeftShift, ignoreGui) || Input.IsDown(Keys.RightShift, ignoreGui);
		}

		public static bool Alt(bool ignoreGui = false)
		{
			return IsDown(Keys.LeftAlt, ignoreGui) || Input.IsDown(Keys.RightAlt, ignoreGui);
		}

		public static bool IsDown(GamePadButton button, PlayerIndex player = PlayerIndex.One)
		{
			var state = gamePadState[(int)player];

			switch (button)
			{
				case GamePadButton.X:
					return (state.Buttons.X == ButtonState.Pressed);

				case GamePadButton.Y:
					return (state.Buttons.Y == ButtonState.Pressed);

				case GamePadButton.A:
					return (state.Buttons.A == ButtonState.Pressed);

				case GamePadButton.B:
					return (state.Buttons.B == ButtonState.Pressed);

				case GamePadButton.DPadUp:
					return (state.DPad.Up == ButtonState.Pressed);

				case GamePadButton.DPadDown:
					return (state.DPad.Down == ButtonState.Pressed);

				case GamePadButton.DPadLeft:
					return (state.DPad.Left == ButtonState.Pressed);

				case GamePadButton.DPadRight:
					return (state.DPad.Right == ButtonState.Pressed);

				case GamePadButton.LeftShoulder:
					return (state.Buttons.LeftShoulder == ButtonState.Pressed);

				case GamePadButton.RightShoulder:
					return (state.Buttons.RightShoulder == ButtonState.Pressed);

				case GamePadButton.LeftStick:
					return (state.Buttons.LeftStick == ButtonState.Pressed);

				case GamePadButton.RightStick:
					return (state.Buttons.RightStick == ButtonState.Pressed);

				case GamePadButton.Back:
					return (state.Buttons.Back == ButtonState.Pressed);

				case GamePadButton.BigButton:
					return (state.Buttons.BigButton == ButtonState.Pressed);

				case GamePadButton.Start:
					return (state.Buttons.Start == ButtonState.Pressed);
			}

			return false;
		}

		public static bool IsPressed(GamePadButton button, PlayerIndex player = PlayerIndex.One)
		{
			var state = gamePadState[(int)player];
			var prevState = prevGamePadState[(int)player];

			switch (button)
			{
				case GamePadButton.X:
					return (
						state.Buttons.X == ButtonState.Pressed &&
						prevState.Buttons.X == ButtonState.Released
					);

				case GamePadButton.Y:
					return (
						state.Buttons.Y == ButtonState.Pressed &&
						prevState.Buttons.Y == ButtonState.Released
					);

				case GamePadButton.A:
					return (
						state.Buttons.A == ButtonState.Pressed &&
						prevState.Buttons.A == ButtonState.Released
					);

				case GamePadButton.B:
					return (
						state.Buttons.B == ButtonState.Pressed &&
						prevState.Buttons.B == ButtonState.Released
					);

				case GamePadButton.DPadUp:
					return (
						state.DPad.Up == ButtonState.Pressed &&
						prevState.DPad.Up == ButtonState.Released
					);

				case GamePadButton.DPadDown:
					return (
						state.DPad.Down == ButtonState.Pressed &&
						prevState.DPad.Down == ButtonState.Released
					);

				case GamePadButton.DPadLeft:
					return (
						state.DPad.Left == ButtonState.Pressed &&
						prevState.DPad.Left == ButtonState.Released
					);

				case GamePadButton.DPadRight:
					return (
						state.DPad.Right == ButtonState.Pressed &&
						prevState.DPad.Right == ButtonState.Released
					);

				case GamePadButton.LeftShoulder:
					return (
						state.Buttons.LeftShoulder == ButtonState.Pressed &&
						prevState.Buttons.LeftShoulder == ButtonState.Released
					);

				case GamePadButton.RightShoulder:
					return (
						state.Buttons.RightShoulder == ButtonState.Pressed &&
						prevState.Buttons.RightShoulder == ButtonState.Released
					);

				case GamePadButton.LeftStick:
					return (
						state.Buttons.LeftStick == ButtonState.Pressed &&
						prevState.Buttons.LeftStick == ButtonState.Released
					);

				case GamePadButton.RightStick:
					return (
						state.Buttons.RightStick == ButtonState.Pressed &&
						prevState.Buttons.RightStick == ButtonState.Released
					);

				case GamePadButton.Back:
					return (
						state.Buttons.Back == ButtonState.Pressed &&
						prevState.Buttons.Back == ButtonState.Released
					);

				case GamePadButton.BigButton:
					return (
						state.Buttons.BigButton == ButtonState.Pressed &&
						prevState.Buttons.BigButton == ButtonState.Released
					);

				case GamePadButton.Start:
					return (
						state.Buttons.Start == ButtonState.Pressed &&
						prevState.Buttons.Start == ButtonState.Released
					);
			}

			return false;
		}

		public static bool IsReleased(GamePadButton button, PlayerIndex player = PlayerIndex.One)
		{
			var state = gamePadState[(int)player];
			var prevState = prevGamePadState[(int)player];

			switch (button)
			{
				case GamePadButton.X:
					return (
						state.Buttons.X == ButtonState.Released &&
						prevState.Buttons.X == ButtonState.Pressed
					);

				case GamePadButton.Y:
					return (
						state.Buttons.Y == ButtonState.Released &&
						prevState.Buttons.Y == ButtonState.Pressed
					);

				case GamePadButton.A:
					return (
						state.Buttons.A == ButtonState.Released &&
						prevState.Buttons.A == ButtonState.Pressed
					);

				case GamePadButton.B:
					return (
						state.Buttons.B == ButtonState.Released &&
						prevState.Buttons.B == ButtonState.Pressed
					);

				case GamePadButton.DPadUp:
					return (
						state.DPad.Up == ButtonState.Released &&
						prevState.DPad.Up == ButtonState.Pressed
					);

				case GamePadButton.DPadDown:
					return (
						state.DPad.Down == ButtonState.Released &&
						prevState.DPad.Down == ButtonState.Pressed
					);

				case GamePadButton.DPadLeft:
					return (
						state.DPad.Left == ButtonState.Released &&
						prevState.DPad.Left == ButtonState.Pressed
					);

				case GamePadButton.DPadRight:
					return (
						state.DPad.Right == ButtonState.Released &&
						prevState.DPad.Right == ButtonState.Pressed
					);

				case GamePadButton.LeftShoulder:
					return (
						state.Buttons.LeftShoulder == ButtonState.Released &&
						prevState.Buttons.LeftShoulder == ButtonState.Pressed
					);

				case GamePadButton.RightShoulder:
					return (
						state.Buttons.RightShoulder == ButtonState.Released &&
						prevState.Buttons.RightShoulder == ButtonState.Pressed
					);

				case GamePadButton.LeftStick:
					return (
						state.Buttons.LeftStick == ButtonState.Released &&
						prevState.Buttons.LeftStick == ButtonState.Pressed
					);

				case GamePadButton.RightStick:
					return (
						state.Buttons.RightStick == ButtonState.Released &&
						prevState.Buttons.RightStick == ButtonState.Pressed
					);

				case GamePadButton.Back:
					return (
						state.Buttons.Back == ButtonState.Released &&
						prevState.Buttons.Back == ButtonState.Pressed
					);

				case GamePadButton.BigButton:
					return (
						state.Buttons.BigButton == ButtonState.Released &&
						prevState.Buttons.BigButton == ButtonState.Pressed
					);

				case GamePadButton.Start:
					return (
						state.Buttons.Start == ButtonState.Released &&
						prevState.Buttons.Start == ButtonState.Pressed
					);
			}

			return false;
		}

		public static Vector2 ThumbStickLeft(PlayerIndex player = PlayerIndex.One)
		{
			return gamePadState[(int)player].ThumbSticks.Left;
		}

		public static Vector2 ThumbStickRight(PlayerIndex player = PlayerIndex.One)
		{
			return gamePadState[(int)player].ThumbSticks.Right;
		}

		public static float TriggerLeft(PlayerIndex player = PlayerIndex.One)
		{
			return gamePadState[(int)player].Triggers.Left;
		}

		public static float TriggerRight(PlayerIndex player = PlayerIndex.One)
		{
			return gamePadState[(int)player].Triggers.Right;
		}

		public static void HandleTextInput(Keys key, char ch)
		{
			if (key == Keys.Enter)
			{
				textInput += "\n";
			}
			else if (key == Keys.Tab)
			{
				for (int ii = 0; ii < TabSize; ii++)
					textInput += " ";
			}
			else if (key == Keys.Back)
			{
				if (textInput.Length > 0)
				{
					textInput = textInput[0..(textInput.Length - 1)];
				}
			}
			else
			{
				textInput += ch;
			}
		}
	}
}
