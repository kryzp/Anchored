using Microsoft.Xna.Framework;

namespace Arch
{
	public interface IUpdatable
	{
		int Order { get; set; }
		void Update();
	}
}
