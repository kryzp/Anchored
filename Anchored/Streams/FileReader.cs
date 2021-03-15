using Microsoft.Xna.Framework;
using System;
using System.IO;
using System.Text;

namespace Anchored.Streams
{
	public class FileReader
	{
		protected Byte[] read;
		public Byte[] Data => read;

		private Int32 position;

		public Int32 Position
		{
			get => position;

			set
			{
				position = (int)MathF.Max(0, MathF.Min(read.Length-1, value));
			}
		}

		public FileReader(string path)
		{
			if (path == null)
			{
				read = new Byte[1];
				return;
			}

			ReadData(path);
		}

		protected virtual void ReadData(string path)
		{
			var file = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			var stream = new BinaryReader(file);

			read = new Byte[file.Length];

			for (var ii = 0; ii < file.Length; ii++)
				read[ii] = (Byte)file.ReadByte();

			stream.Close();
		}

		public byte ReadByte()
		{
			if (read.Length == Position)
				return 0;

			return read[Position++];
		}

		public sbyte ReadSbyte()
		{
			return (sbyte)ReadByte();
		}

		public bool ReadBoolean()
		{
			return ReadByte() == 1;
		}

		public Int16 ReadInt16()
		{
			return (short)((ReadByte() << 8) | ReadByte());
		}

		public UInt16 ReadUInt16()
		{
			return (ushort)((ReadByte() << 8) | ReadByte());
		}

		public Int32 ReadInt32()
		{
			return (ReadByte() << 24) | (ReadByte() << 16) | (ReadByte() << 8) | ReadByte();
		}

		public UInt32 ReadUInt32()
		{
			return (uint)((ReadByte() << 24) | (ReadByte() << 16) | (ReadByte() << 8) | ReadByte());
		}

		public string ReadString()
		{
			byte length = ReadByte();

			if (length == 0)
				return null;

			var result = new StringBuilder();

			for (int ii = 0; ii < length; ii++)
				result.Append((char)ReadByte());

			return result.ToString();
		}

		public Single ReadFloat()
		{
			return BitConverter.ToSingle(new[] { ReadByte(), ReadByte(), ReadByte(), ReadByte() }, 0);
		}

		public Vector2 ReadVector2()
		{
			float x = ReadFloat();
			float y = ReadFloat();
			return new Vector2(x, y);
		}

		public void SetData(Byte[] data)
		{
			read = data;
			position = 0;
		}
	}
}
