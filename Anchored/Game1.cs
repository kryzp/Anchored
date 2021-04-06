using Anchored.Assets;
using Anchored.State;
using Anchored.World;
using Anchored.World.Components;
using Anchored.Save;
using Anchored.Debug;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using Arch.Math.Tween;
using Arch;
using Arch.Assets;
using Arch.World;
using Arch.UI;
using Arch.Assets.Maps.Serialization;
using Newtonsoft.Json;
using Anchored.Assets.Maps;
using Anchored.Assets.Textures;
using Anchored.UI.Elements;
using Arch.Assets.Textures;
using Arch.Util;
using Anchored.UI.Constraints;
using Arch.World.Components;
using Arch.Graphics;

namespace Anchored
{
	public class Game1 : Engine
	{
		public static Entity Player;

		public Game1()
			: base("Anchored", 1920, 1080)
		{
		}

        protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			Options.Load(SaveManager.GetOptionsFilePath());

			Camera.Main = new Camera(WindowWidth, WindowHeight);
			Camera.Main.Origin = Vector2.Zero;

			ChangeState(new AssetLoadState());
			SaveManager.Init();
		}

		protected override void UnloadContent()
		{
			base.UnloadContent();

			MapManager.Destroy();
			AssetManager.Destroy();
			TextureBoundManager.Destroy();
		}

		protected override void Update(GameTime gt)
		{
			base.Update(gt);
			Camera.Main?.Update();
		}

		protected override void Draw(GameTime gt)
		{
			base.Draw(gt);
		}
	}
}
