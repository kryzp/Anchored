

using Anchored.Graphics;
using Anchored.World.Components;
using Arch;
using Arch.Math;
using Arch.Util;
using Arch.World;
using Microsoft.Xna.Framework;

namespace Anchored.World.Types
{
    public class TreeType : EntityType
    {
        [EntityTypeSetting("texture")]
        public TextureRegion Texture;
        
        public TreeType()
        {
        }
        
        public override void Create(Entity entity)
        {
            base.Create(entity);

            entity.Transform.Origin = OriginFactory.GetOrigin(OriginPosition.BottomCenter, Texture);

            var sprite = entity.AddComponent(new Sprite(Texture));

            var collider = entity.AddComponent(new Collider());
            collider.Mask = Masks.Solid;
            collider.MakeRect(
                new RectangleF(
                    0,
                    0,
                    16,
                    16
                )
            );

            collider.Transform.Position = new Vector2(16, 48);

            var depth = entity.AddComponent(new DepthSorter(sprite));
        }
    }
}
