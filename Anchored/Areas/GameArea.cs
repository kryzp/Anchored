using Anchored.World;
using Anchored.World.Components;
using Anchored.World.Types;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace Anchored.Areas
{
	public class GameArea
	{
		protected EntityWorld world;
		protected Entity cameraEntity;
		protected Camera camera => (Camera)cameraEntity.GetComponent<Camera>();

		public Camera Camera => camera;

		public GameArea(EntityWorld world)
		{
			this.world = world;

			cameraEntity = world.AddEntity("Camera");
			new CameraType().Create(cameraEntity, 0);
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
			world.Draw(sb);
		}

		public virtual void DrawUI(SpriteBatch sb)
		{
		}

		protected Entity SetupTileMap(TiledMap map, bool loadColliders = true)
		{
			var tileMapEntity = world.AddEntity("Tile Map");
			var tileMap = tileMapEntity.AddComponent(new TileMap(map, camera));
			if (loadColliders) tileMap.LoadColliders();
			return tileMapEntity;
		}
	}
}
