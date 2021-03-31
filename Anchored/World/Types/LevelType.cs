using Anchored.Assets;
using Anchored.World.Components;
using Arch.Assets.Maps;
using Arch.World.Components;
using Arch.World;
using Arch.Streams;
using Arch.Assets;

namespace Anchored.World.Types
{
	public class LevelType : EntityType
	{
		private Map map;
		private bool loadColliders;
		private bool loadEntities;

		public LevelType(Map map, bool loadColliders = true, bool loadEntities = true)
		{
			Serializable = true;
			
			this.map = map;
			this.loadColliders = loadColliders;
			this.loadEntities = loadEntities;
		}

		public override void Create(Entity entity)
		{
			base.Create(entity);

			var tileMap = entity.AddComponent(new TileMapRenderer(map, Camera.Main));

			if (loadColliders)
				tileMap.LoadColliders();

			if (loadEntities)
				LoadEntitiesFromTileMap(entity.World, map);
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
			map = MapManager.Get(mapName);
		}
		
		protected void LoadEntitiesFromTileMap(EntityWorld world, Map map)
		{
			/*
			foreach (var obj in map.GetLayer<TiledMapObjectLayer>("Entities").Objects)
			{
				TiledMapTileObject entityObj = (TiledMapTileObject)obj;
				string entityName = entityObj.Name;
				Vector2 entityPosition = entityObj.Position;
				float entityRotation = entityObj.Rotation;

				var entity = world.AddEntity(entityName);

				if (entityObj.Tile.Type == "Destructible")
				{
					string entityTypeName = entityObj.Tile.Properties["Entity"];
					var tileRegion = entityObj.Tileset.GetTileRegion(entityObj.Tile.LocalTileIdentifier);
					var sprite = new TextureRegion(entityObj.Tileset.Texture, tileRegion);

					EntityType entityType = (EntityType)Activator.CreateInstance(
						Type.GetType(
							$"Anchored.World.Types.{entityTypeName}Type",
							true,
							false
						),
						sprite
					);

					entityType.Create(entity);
				}

				entity.Transform.Position = entityPosition;
				entity.Transform.RotationDegrees = entityRotation;
			}
			*/
		}
	}
}
