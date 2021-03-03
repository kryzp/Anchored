using Anchored.World;
using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;

namespace Anchored.Areas
{
	public class GameArea
	{
		protected EntityWorld world;

		public GameArea()
		{
		}

		public virtual void Load(SpriteBatch sb)
		{
			world = new EntityWorld();
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
			var tileMapEntity = world.AddEntity();
			var tileMap = tileMapEntity.AddComponent(new TileMap(map));
			if (loadColliders)
				tileMap.LoadColliders();
			return tileMapEntity;
		}
	}
}
