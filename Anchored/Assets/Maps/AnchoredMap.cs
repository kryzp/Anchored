using Arch.Assets.Maps;
using Arch.Assets.Maps.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Assets.Maps
{
	public class AnchoredMap
	{
		public Map Map;

		public List<MapEntity> Entities = new List<MapEntity>();

		public AnchoredMap(Map map)
		{
			this.Map = map;

			foreach (var d in map.Data.Entities)
				Entities.Add(new AnchoredMapEntity(d));
		}
	}
}
