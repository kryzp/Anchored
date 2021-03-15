using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Anchored.Streams
{
	public class FileWriter
	{
		private BinaryWriter stream;
		private List<byte> cache = new List<byte>();

		public int CacheSize => cache.Count;
		public bool Cache;

		public FileWriter(string path, bool append = false)
		{
			OpenStream(path, append);
		}

		protected virtual void OpenStream(string path, bool append)
		{
			stream = new BinaryWriter(
				File.Open(
					path,
					(append)
						? FileMode.Append
						: FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite
				)
			);
		}

		public void Flush()
		{
			foreach (var b in cache)
				WriteByte(b);

			cache.Clear();
		}

		protected virtual void Write(byte value)
		{
			stream.Write(value);
		}

		public void WriteByte(byte value)
		{
			if (Cache)
			{
				cache.Add(value);
			}
			else
			{
				Write(value);
			}
		}

		public unsafe void WriteSbyte(sbyte value)
		{
			WriteByte(*((byte*)&value));
		}

		public void WriteBoolean(bool value)
		{
			WriteByte((byte)(value ? 1 : 0));
		}

		public void WriteInt16(Int16 value)
		{
			WriteByte((byte)(value >> 8));
			WriteByte((byte)value);
		}

		public void WriteUInt16(UInt16 value)
		{
			WriteByte((byte)(value >> 8));
			WriteByte((byte)value);
		}

		public void WriteInt32(Int32 value)
		{
			WriteByte((byte)((value >> 24) & 0xFF));
			WriteByte((byte)((value >> 16) & 0xFF));
			WriteByte((byte)((value >> 8) & 0xFF));
			WriteByte((byte)(value & 0xFF));
		}

		public void WriteUInt32(UInt32 value)
		{
			WriteByte((byte)((value >> 24) & 0xFF));
			WriteByte((byte)((value >> 16) & 0xFF));
			WriteByte((byte)((value >> 8) & 0xFF));
			WriteByte((byte)(value & 0xFF));
		}

		public void WriteString(string str)
		{
			if (str == null)
			{
				WriteByte(0);
			}
			else
			{
				// note: any string 256 or bigger in length will be trimmed to fit!

				WriteByte((byte)str.Length);

				for (var ii = 0; ii < MathF.Min(255, str.Length); ii++)
				{
					WriteByte((byte)str[ii]);
				}
			}
		}

		public unsafe void WriteFloat(Single value)
		{
			uint val = *((uint*)&value);

			WriteByte((byte)(val & 0xFF));
			WriteByte((byte)((val >> 8) & 0xFF));
			WriteByte((byte)((val >> 16) & 0xFF));
			WriteByte((byte)((val >> 24) & 0xFF));
		}

		public void WriteVector2(Vector2 value)
		{
			WriteFloat(value.X);
			WriteFloat(value.Y);
		}

		public virtual void Close()
		{
			stream.Close();
		}
	}
}
