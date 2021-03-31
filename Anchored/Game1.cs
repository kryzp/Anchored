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

namespace Anchored
{
	public class Game1 : Engine
	{
		public static Entity Player;

		public Game1()
			: base("Anchored", 1280, 720)
		{
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			SaveManager.Init();
			ChangeState(new AssetLoadState());
		}

		protected override void UnloadContent()
		{
			AssetManager.Destroy();
		}

		protected override void Update(GameTime gt)
		{
			base.Update(gt);
		}

		protected override void Draw(GameTime gt)
		{
			base.Draw(gt);
		}
	}
}
