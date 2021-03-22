using Anchored.Assets;
using Anchored.World.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using Anchored.Streams;

namespace Anchored.World.Types
{
	public class TileMapType : EntityType
	{
		private TiledMap map;
		private bool loadColliders;
		private bool loadEntities;

		public TileMapType(TiledMap map, bool loadColliders = true, bool loadEntities = true)
		{
			this.map = map;
			this.loadColliders = loadColliders;
			this.loadEntities = loadEntities;
		}

		public override void Create(Entity entity)
		{
			base.Create(entity);

			var tileMap = entity.AddComponent(new TileMap(map, Camera.Main));

			if (loadColliders)
				tileMap.LoadColliders();

			if (loadEntities)
				LoadEntitiesFromTileMap(entity.World, map);
		}

		protected void LoadEntitiesFromTileMap(EntityWorld world, TiledMap map)
		{
			foreach (var obj in map.GetLayer<TiledMapObjectLayer>("Entities").Objects)
			{
				// todo: move into external class that inherits from like "TiledMapEntity" or something like that which has a Create method n' stuff!

				TiledMapRectangleObject entityObj = (TiledMapRectangleObject)obj;
				string entityName = entityObj.Name;
				Vector2 entityPosition = entityObj.Position;
				float entityRotation = entityObj.Rotation;

				/*
				if (entityObj.Type == "Tree")
				{
				}
				*/
			}
		}
		
		public override void Save(FileWriter stream)
		{
			stream.WriteString(map.Name);
			stream.WriteBoolean(loadColliders);
			stream.WriteBoolean(loadEntities);
		}

		public override void Load(FileReader stream)
		{
			string mapName = stream.ReadString();
			loadColliders = stream.ReadBoolean();
			loadEntities = stream.ReadBoolean();
			
			map = TileMaps.Get(mapName);
		}
	}
}
