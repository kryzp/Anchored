using Anchored.State;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Debug.Editors
{
	public class EditorWindow
	{
		public Editor Editor;

		public EditorWindow(Editor e)
		{
			Editor = e;
			EntityEditor.Editor = e;
		}

		public void Draw()
		{
			if (!DebugManager.LevelEditor)
				return;

			if (Game1.CurrentState is PlayingState)
			{
				EntityEditor.Draw();
				return;
			}
		}
	}
}
