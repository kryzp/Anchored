using System;

namespace Arch
{
	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var engine = new Engine("Arch Application", 720, 1280))
				engine.Run();
		}
	}
}
