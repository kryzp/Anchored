using Arch.Assets.Maps;
using Arch.Assets.Maps.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Assets.Maps
{
	public class AnchoredMap : Map
	{
		public List<MapEntity> Entities = new List<MapEntity>();

		public AnchoredMap(Map map)
			: base(map.Data)
		{
			foreach (var d in map.Data.Entities)
				Entities.Add(new AnchoredMapEntity(d));
		}

		public AnchoredMap(MapJson data)
			: base(data)
		{
		}
	}
}
