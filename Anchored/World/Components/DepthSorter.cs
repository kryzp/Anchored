using Arch;
using Arch.World;

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
            float layer = bottom / Constants.LAYER_DEPTH_DIVIDER;
            graphicsComponent.LayerDepth = layer;
        }
    }
}