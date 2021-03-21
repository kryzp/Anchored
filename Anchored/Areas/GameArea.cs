using Anchored.Assets;
using Anchored.Util;
using Anchored.World;
using Anchored.World.Components;
using Anchored.World.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;

namespace Anchored.Areas
{
	public class GameArea
	{
		protected EntityWorld world;
		protected Entity cameraEntity;

		public Camera Camera => (Camera)cameraEntity.GetComponent<Camera>();

		public GameArea(EntityWorld world)
		{
			this.world = world;

			cameraEntity = world.AddEntity("Camera");
			var camera = cameraEntity.AddComponent(new Camera(320, 180));
			camera.Origin = new Vector2(160, 90);

			Camera.Main = camera;
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
			world.Draw(sb, Camera.Main.GetViewMatrix());
		}

		public virtual void DrawUI(SpriteBatch sb)
		{
		}

		protected Entity SetupTileMap(TiledMap map, bool loadColliders = true, bool loadEntities = true)
		{
			Entity tileMapEntity = world.AddEntity("TileMap");
			new TileMapType(map, loadColliders, loadEntities).Create(tileMapEntity);
			return tileMapEntity;
		}
	}
}
