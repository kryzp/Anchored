using Anchored.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Save
{
	public static class SaveGame
	{
		public static string GetSaveFilePath(string user)
		{
			string saveFilePath = System.IO.Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"Anchored",
				"Saves",
				user
			);
			return saveFilePath;
		}
	}
}
