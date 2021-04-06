using Anchored.Assets;
using Anchored.Assets.Maps;
using Anchored.Util;
using Anchored.World;
using Anchored.World.Components;
using Anchored.World.Types;
using Arch.Assets.Maps;
using Arch.Graphics;
using Arch.Graphics.CameraDrivers;
using Arch.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Anchored.Areas
{
	public class GameArea
	{
		protected EntityWorld world;

		public GameArea(EntityWorld world)
		{
			this.world = world;
		}

		public virtual void Load(SpriteBatch sb)
		{
		}

		public virtual void Unload()
		{
		}

		public virtual void Update()
		{
			world.Update();
		}

		public virtual void Draw(SpriteBatch sb)
		{
			world.Draw(sb, Camera.Main.GetPerfectViewMatrix());
		}

		public virtual void DrawUI(SpriteBatch sb)
		{
		}

		protected Entity SetupLevel(AnchoredMap map, bool loadColliders = true, bool loadEntities = true)
		{
			Entity tileMapEntity = world.AddEntity("TileMap");
			new LevelType(map, loadColliders, loadEntities).Create(tileMapEntity);
			return tileMapEntity;
		}
	}
}
