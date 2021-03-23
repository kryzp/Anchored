using System;
using Anchored.Util.Math;

namespace Anchored.World.Components
{
    public class DepthSorter : Component, IUpdatable
    {
        private GraphicsComponent graphicsComponent;

        public int Order { get; set; } = 0;

        public DepthSorter()
        {
        }
        
        public DepthSorter(GraphicsComponent gc)
        {
            graphicsComponent = gc;
        }
        
        public void Update()
        {
            SortBasedOnY();
        }

        private void SortBasedOnY()
        {
            float position = Entity.Transform.Position.Y;
            float bottom = graphicsComponent.Texture.Texture.Bounds.Bottom + position;

            // if you're wondering why this isn't just position / 100000f then its because then
            // there can't be negative values. though, it might not be worth the performance hit
            // research into this at some point lol
            //  * could be inefficient though...
            
            float min = +Single.MaxValue;
            float max = -Single.MaxValue;
            
            World.ForeachEntity((entity) =>
            {
                if (entity.Transform.Position.Y < min)
                    min = entity.Transform.Position.Y;
                else if (entity.Transform.Position.Y > max)
                    max = entity.Transform.Position.Y;
            });

            float layer = Calc.MapValue(0f, 1f, min, max, position);
            graphicsComponent.LayerDepth = layer;
        }
    }
}