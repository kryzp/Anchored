using System;

namespace Anchored
{
	// note: i put 'engineer gaming' whenever theres code that is spaghetti mama mia

	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new Game1())
				game.Run();
		}
	}
}
