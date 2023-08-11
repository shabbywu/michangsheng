using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace KBEngine;

public class MemoryStream : ObjectPool<MemoryStream>
{
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	private struct PackFloatXType
	{
		[FieldOffset(0)]
		public float fv;

		[FieldOffset(0)]
		public uint uv;

		[FieldOffset(0)]
		public int iv;
	}

	public const int BUFFER_MAX = 5840;

	public int rpos;

	public int wpos;

	private byte[] datas_ = new byte[5840];

	private static ASCIIEncoding _converter = new ASCIIEncoding();

	public byte[] setBuffer(byte[] buffer)
	{
		byte[] result = datas_;
		datas_ = buffer;
		return result;
	}

	public void swap(MemoryStream stream)
	{
		int num = rpos;
		int num2 = wpos;
		rpos = stream.rpos;
		wpos = stream.wpos;
		stream.rpos = num;
		stream.wpos = num2;
		datas_ = stream.setBuffer(datas_);
	}

	public void reclaimObject()
	{
		clear();
		ObjectPool<MemoryStream>.reclaimObject(this);
	}

	public byte[] data()
	{
		return datas_;
	}

	public void setData(byte[] data)
	{
		datas_ = data;
	}

	public sbyte readInt8()
	{
		return (sbyte)datas_[rpos++];
	}

	public short readInt16()
	{
		rpos += 2;
		return BitConverter.ToInt16(datas_, rpos - 2);
	}

	public int readInt32()
	{
		rpos += 4;
		return BitConverter.ToInt32(datas_, rpos - 4);
	}

	public long readInt64()
	{
		rpos += 8;
		return BitConverter.ToInt64(datas_, rpos - 8);
	}

	public byte readUint8()
	{
		return datas_[rpos++];
	}

	public ushort readUint16()
	{
		rpos += 2;
		return BitConverter.ToUInt16(datas_, rpos - 2);
	}

	public uint readUint32()
	{
		rpos += 4;
		return BitConverter.ToUInt32(datas_, rpos - 4);
	}

	public ulong readUint64()
	{
		rpos += 8;
		return BitConverter.ToUInt64(datas_, rpos - 8);
	}

	public float readFloat()
	{
		rpos += 4;
		return BitConverter.ToSingle(datas_, rpos - 4);
	}

	public double readDouble()
	{
		rpos += 8;
		return BitConverter.ToDouble(datas_, rpos - 8);
	}

	public string readString()
	{
		int num = rpos;
		while (datas_[rpos++] != 0)
		{
		}
		return _converter.GetString(datas_, num, rpos - num - 1);
	}

	public string readUnicode()
	{
		return Encoding.UTF8.GetString(readBlob());
	}

	public byte[] readBlob()
	{
		uint num = readUint32();
		byte[] array = new byte[num];
		Array.Copy(datas_, rpos, array, 0L, num);
		rpos += (int)num;
		return array;
	}

	public byte[] readEntitycall()
	{
		readUint64();
		readInt32();
		readUint16();
		readUint16();
		return new byte[0];
	}

	public Vector2 readVector2()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		float num = readFloat();
		float num2 = readFloat();
		return new Vector2(num, num2);
	}

	public Vector3 readVector3()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		float num = readFloat();
		float num2 = readFloat();
		float num3 = readFloat();
		return new Vector3(num, num2, num3);
	}

	public Vector4 readVector4()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		float num = readFloat();
		float num2 = readFloat();
		float num3 = readFloat();
		float num4 = readFloat();
		return new Vector4(num, num2, num3, num4);
	}

	public byte[] readPython()
	{
		return readBlob();
	}

	public Vector2 readPackXZ()
	{
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		PackFloatXType packFloatXType = default(PackFloatXType);
		packFloatXType.fv = 0f;
		PackFloatXType packFloatXType2 = default(PackFloatXType);
		packFloatXType2.fv = 0f;
		packFloatXType.uv = 1073741824u;
		packFloatXType2.uv = 1073741824u;
		byte b = readUint8();
		byte b2 = readUint8();
		byte b3 = readUint8();
		uint num = 0u;
		num |= (uint)(b << 16);
		num |= (uint)(b2 << 8);
		num |= b3;
		packFloatXType.uv |= (num & 0x7FF000) << 3;
		packFloatXType2.uv |= (num & 0x7FF) << 15;
		packFloatXType.fv -= 2f;
		packFloatXType2.fv -= 2f;
		packFloatXType.uv |= (num & 0x800000) << 8;
		packFloatXType2.uv |= (num & 0x800) << 20;
		return new Vector2(packFloatXType.fv, packFloatXType2.fv);
	}

	public float readPackY()
	{
		PackFloatXType packFloatXType = default(PackFloatXType);
		packFloatXType.fv = 0f;
		packFloatXType.uv = 1073741824u;
		ushort num = readUint16();
		packFloatXType.uv |= (uint)((num & 0x7FFF) << 12);
		packFloatXType.fv -= 2f;
		packFloatXType.uv |= (uint)((num & 0x8000) << 16);
		return packFloatXType.fv;
	}

	public void writeInt8(sbyte v)
	{
		datas_[wpos++] = (byte)v;
	}

	public void writeInt16(short v)
	{
		writeInt8((sbyte)(v & 0xFF));
		writeInt8((sbyte)((v >> 8) & 0xFF));
	}

	public void writeInt32(int v)
	{
		for (int i = 0; i < 4; i++)
		{
			writeInt8((sbyte)((v >> i * 8) & 0xFF));
		}
	}

	public void writeInt64(long v)
	{
		byte[] bytes = BitConverter.GetBytes(v);
		for (int i = 0; i < bytes.Length; i++)
		{
			datas_[wpos++] = bytes[i];
		}
	}

	public void writeUint8(byte v)
	{
		datas_[wpos++] = v;
	}

	public void writeUint16(ushort v)
	{
		writeUint8((byte)(v & 0xFFu));
		writeUint8((byte)((uint)(v >> 8) & 0xFFu));
	}

	public void writeUint32(uint v)
	{
		for (int i = 0; i < 4; i++)
		{
			writeUint8((byte)((v >> i * 8) & 0xFFu));
		}
	}

	public void writeUint64(ulong v)
	{
		byte[] bytes = BitConverter.GetBytes(v);
		for (int i = 0; i < bytes.Length; i++)
		{
			datas_[wpos++] = bytes[i];
		}
	}

	public void writeFloat(float v)
	{
		byte[] bytes = BitConverter.GetBytes(v);
		for (int i = 0; i < bytes.Length; i++)
		{
			datas_[wpos++] = bytes[i];
		}
	}

	public void writeDouble(double v)
	{
		byte[] bytes = BitConverter.GetBytes(v);
		for (int i = 0; i < bytes.Length; i++)
		{
			datas_[wpos++] = bytes[i];
		}
	}

	public void writeBlob(byte[] v)
	{
		uint num = (uint)v.Length;
		if (num + 4 > space())
		{
			Dbg.ERROR_MSG("memorystream::writeBlob: no free!");
			return;
		}
		writeUint32(num);
		for (uint num2 = 0u; num2 < num; num2++)
		{
			datas_[wpos++] = v[num2];
		}
	}

	public void writeString(string v)
	{
		if (v.Length > space())
		{
			Dbg.ERROR_MSG("memorystream::writeString: no free!");
			return;
		}
		byte[] bytes = Encoding.ASCII.GetBytes(v);
		for (int i = 0; i < bytes.Length; i++)
		{
			datas_[wpos++] = bytes[i];
		}
		datas_[wpos++] = 0;
	}

	public void writeVector2(Vector2 v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		writeFloat(v.x);
		writeFloat(v.y);
	}

	public void writeVector3(Vector3 v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		writeFloat(v.x);
		writeFloat(v.y);
		writeFloat(v.z);
	}

	public void writeVector4(Vector4 v)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		writeFloat(v.x);
		writeFloat(v.y);
		writeFloat(v.z);
		writeFloat(v.w);
	}

	public void writeEntitycall(byte[] v)
	{
		ulong num = 0uL;
		int num2 = 0;
		ushort num3 = 0;
		ushort num4 = 0;
		writeUint64(num);
		writeInt32(num2);
		writeUint16(num3);
		writeUint16(num4);
	}

	public void append(byte[] datas, uint offset, uint size)
	{
		if (space() < size)
		{
			byte[] destinationArray = new byte[datas_.Length + size * 2];
			Array.Copy(datas_, 0, destinationArray, 0, wpos);
			datas_ = destinationArray;
		}
		Array.Copy(datas, offset, datas_, wpos, size);
		wpos += (int)size;
	}

	public void readSkip(uint v)
	{
		rpos += (int)v;
	}

	public uint space()
	{
		return (uint)(data().Length - wpos);
	}

	public uint length()
	{
		return (uint)(wpos - rpos);
	}

	public bool readEOF()
	{
		return 5840 - rpos <= 0;
	}

	public void done()
	{
		rpos = wpos;
	}

	public void clear()
	{
		rpos = (wpos = 0);
		if (datas_.Length > 5840)
		{
			datas_ = new byte[5840];
		}
	}

	public byte[] getbuffer()
	{
		byte[] array = new byte[length()];
		Array.Copy(data(), rpos, array, 0L, length());
		return array;
	}

	public string toString()
	{
		string text = "";
		int num = 0;
		byte[] array = getbuffer();
		for (int i = 0; i < array.Length; i++)
		{
			num++;
			if (num >= 200)
			{
				text = "";
				num = 0;
			}
			text += array[i];
			text += " ";
		}
		return text;
	}
}
