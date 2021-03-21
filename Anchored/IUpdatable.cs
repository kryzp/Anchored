using Microsoft.Xna.Framework;

namespace Anchored
{
	public interface IUpdatable
	{
		// note: the higher the Order, the earlier the component is executed
		int Order { get; set; }
		void Update();
	}
}
