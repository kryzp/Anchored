using System.Collections.Generic;
using System.Linq;
using Arch.Assets.Maps.Serialization;
using Microsoft.Xna.Framework.Graphics;

namespace Arch.Assets.Maps
{
	public class Map
	{
		public string Name = "";

		public int MapWidth = 0;
		public int MapHeight = 0;
		
		public int MasterLevel = 0;
		public List<Level> Levels = new List<Level>();

		public List<Layer> Layers = new List<Layer>();

		public MapJson Data;

		public Map(MapJson data)
		{
			this.Data = data;

			this.Name = data.Name;

			this.MapWidth = data.MapWidth;
			this.MapHeight = data.MapHeight;

			this.MasterLevel = data.MasterLevel;

			foreach (var d in data.Levels)
				Levels.Add(new Level(d));

			foreach (var d in data.Layers)
				Layers.Add(new Layer(d));
		}

		public void Update()
		{
			foreach (var layer in Layers)
			{
				layer.Update();
			}
		}
		
		public void Draw(SpriteBatch sb)
		{
			foreach (var layer in Layers.OrderBy(x => x.Height))
			{
				layer.Draw(sb);
			}
		}

		public void AddLayer(Layer layer)
		{
			Layers.Add(layer);
		}
	}
}
