using Arch.Assets.Maps.Serialization;

namespace Arch.Assets.Maps
{
    public class Level
    {
        public int Height;

        public Level()
		{
		}

        public Level(LevelJson data)
		{
            this.Height = data.Height;
		}
    }
}
