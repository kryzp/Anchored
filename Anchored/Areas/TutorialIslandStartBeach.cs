using Anchored.Assets;
using Anchored.World;
using Anchored.World.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Anchored.World.Types;
using Anchored.Assets.Textures;

namespace Anchored.Areas
{
	public class TutorialIslandStartBeach : GameArea
	{
		public TutorialIslandStartBeach(EntityWorld world)
			: base(world)
		{
		}

		public override void Load(SpriteBatch sb)
		{
			base.Load(sb);

			SetupLevel(TileMapManager.Get("tutorial_island_start_beach"));

			var playerEntity = world.AddEntity("Player");
			{
				new PlayerType().Create(playerEntity);
				Game1.Player = playerEntity;
			}

			Camera.Follow = playerEntity;
			Camera.FollowLerp = 0.2f;
		}

		public override void Unload()
		{
			base.Unload();
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw(SpriteBatch sb)
		{
			base.Draw(sb);
		}

		public override void DrawUI(SpriteBatch sb)
		{
			base.DrawUI(sb);
		}
	}
}
