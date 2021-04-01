using Arch.Assets.Maps;
using Arch.Assets.Maps.Serialization;
using Arch.World;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anchored.Assets.Maps
{
	public class AnchoredMapEntity : MapEntity
	{
		public AnchoredMapEntity()
		{
		}

		public AnchoredMapEntity(MapEntityJson data)
			: base(data)
		{
		}

		protected override EntityType Load(string type)
		{
			return (EntityType)Activator.CreateInstance(
				System.Type.GetType(
					$"Anchored.World.Types.{type}",
					true,
					false
				)
			);
		}
	}
}
