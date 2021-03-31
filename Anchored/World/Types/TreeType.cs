

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
        private TextureRegion texture;
        
        public TreeType(TextureRegion tex)
        {
            this.texture = tex;
        }
        
        public override void Create(Entity entity)
        {
            base.Create(entity);

            entity.Transform.Origin = OriginFactory.GetOrigin(OriginPosition.BottomCenter, texture);

            var sprite = entity.AddComponent(new Sprite(texture));

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
