using System;
using System.Collections.Generic;
using Anchored.Assets.Prefabs;
using Anchored.Streams;
using Anchored.World;

namespace Anchored.Save
{
    public class PrefabSaver : LevelSave
    {
        public List<PrefabData> Datas = new List<PrefabData>();

        public override void Load(EntityWorld world, FileReader reader)
        {
            Datas.Clear();
            base.Load(world, reader);
        }

        protected override void ReadEntity(EntityWorld world, FileReader reader)
        {
            var type = reader.ReadString();
            var name = reader.ReadString();
            
            var size = reader.ReadUInt16();
            var position = reader.Position;

            var t = Type.GetType($"Anchored.{type}", true, false);

            var prefab = new PrefabData();
            prefab.Type = t;
            prefab.Data = new byte[size];

            for (int ii = 0; ii < size; ii++)
                prefab.Data[ii] = reader.ReadByte();
            
            Datas.Add(prefab);
        }
    }
}
