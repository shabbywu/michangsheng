using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000F5D RID: 3933
	public class MemoryStream : ObjectPool<MemoryStream>
	{
		// Token: 0x06005E7A RID: 24186 RVA: 0x000422FE File Offset: 0x000404FE
		public byte[] setBuffer(byte[] buffer)
		{
			byte[] result = this.datas_;
			this.datas_ = buffer;
			return result;
		}

		// Token: 0x06005E7B RID: 24187 RVA: 0x00262280 File Offset: 0x00260480
		public void swap(MemoryStream stream)
		{
			int num = this.rpos;
			int num2 = this.wpos;
			this.rpos = stream.rpos;
			this.wpos = stream.wpos;
			stream.rpos = num;
			stream.wpos = num2;
			this.datas_ = stream.setBuffer(this.datas_);
		}

		// Token: 0x06005E7C RID: 24188 RVA: 0x0004230D File Offset: 0x0004050D
		public void reclaimObject()
		{
			this.clear();
			ObjectPool<MemoryStream>.reclaimObject(this);
		}

		// Token: 0x06005E7D RID: 24189 RVA: 0x0004231B File Offset: 0x0004051B
		public byte[] data()
		{
			return this.datas_;
		}

		// Token: 0x06005E7E RID: 24190 RVA: 0x00042323 File Offset: 0x00040523
		public void setData(byte[] data)
		{
			this.datas_ = data;
		}

		// Token: 0x06005E7F RID: 24191 RVA: 0x002622D4 File Offset: 0x002604D4
		public sbyte readInt8()
		{
			byte[] array = this.datas_;
			int num = this.rpos;
			this.rpos = num + 1;
			return array[num];
		}

		// Token: 0x06005E80 RID: 24192 RVA: 0x0004232C File Offset: 0x0004052C
		public short readInt16()
		{
			this.rpos += 2;
			return BitConverter.ToInt16(this.datas_, this.rpos - 2);
		}

		// Token: 0x06005E81 RID: 24193 RVA: 0x0004234F File Offset: 0x0004054F
		public int readInt32()
		{
			this.rpos += 4;
			return BitConverter.ToInt32(this.datas_, this.rpos - 4);
		}

		// Token: 0x06005E82 RID: 24194 RVA: 0x00042372 File Offset: 0x00040572
		public long readInt64()
		{
			this.rpos += 8;
			return BitConverter.ToInt64(this.datas_, this.rpos - 8);
		}

		// Token: 0x06005E83 RID: 24195 RVA: 0x002622FC File Offset: 0x002604FC
		public byte readUint8()
		{
			byte[] array = this.datas_;
			int num = this.rpos;
			this.rpos = num + 1;
			return array[num];
		}

		// Token: 0x06005E84 RID: 24196 RVA: 0x00042395 File Offset: 0x00040595
		public ushort readUint16()
		{
			this.rpos += 2;
			return BitConverter.ToUInt16(this.datas_, this.rpos - 2);
		}

		// Token: 0x06005E85 RID: 24197 RVA: 0x000423B8 File Offset: 0x000405B8
		public uint readUint32()
		{
			this.rpos += 4;
			return BitConverter.ToUInt32(this.datas_, this.rpos - 4);
		}

		// Token: 0x06005E86 RID: 24198 RVA: 0x000423DB File Offset: 0x000405DB
		public ulong readUint64()
		{
			this.rpos += 8;
			return BitConverter.ToUInt64(this.datas_, this.rpos - 8);
		}

		// Token: 0x06005E87 RID: 24199 RVA: 0x000423FE File Offset: 0x000405FE
		public float readFloat()
		{
			this.rpos += 4;
			return BitConverter.ToSingle(this.datas_, this.rpos - 4);
		}

		// Token: 0x06005E88 RID: 24200 RVA: 0x00042421 File Offset: 0x00040621
		public double readDouble()
		{
			this.rpos += 8;
			return BitConverter.ToDouble(this.datas_, this.rpos - 8);
		}

		// Token: 0x06005E89 RID: 24201 RVA: 0x00262324 File Offset: 0x00260524
		public string readString()
		{
			int num = this.rpos;
			byte[] array;
			int num2;
			do
			{
				array = this.datas_;
				num2 = this.rpos;
				this.rpos = num2 + 1;
			}
			while (array[num2]);
			return MemoryStream._converter.GetString(this.datas_, num, this.rpos - num - 1);
		}

		// Token: 0x06005E8A RID: 24202 RVA: 0x00042444 File Offset: 0x00040644
		public string readUnicode()
		{
			return Encoding.UTF8.GetString(this.readBlob());
		}

		// Token: 0x06005E8B RID: 24203 RVA: 0x00262370 File Offset: 0x00260570
		public byte[] readBlob()
		{
			uint num = this.readUint32();
			byte[] array = new byte[num];
			Array.Copy(this.datas_, (long)this.rpos, array, 0L, (long)((ulong)num));
			this.rpos += (int)num;
			return array;
		}

		// Token: 0x06005E8C RID: 24204 RVA: 0x00042456 File Offset: 0x00040656
		public byte[] readEntitycall()
		{
			this.readUint64();
			this.readInt32();
			this.readUint16();
			this.readUint16();
			return new byte[0];
		}

		// Token: 0x06005E8D RID: 24205 RVA: 0x002623B4 File Offset: 0x002605B4
		public Vector2 readVector2()
		{
			float num = this.readFloat();
			float num2 = this.readFloat();
			return new Vector2(num, num2);
		}

		// Token: 0x06005E8E RID: 24206 RVA: 0x002623D4 File Offset: 0x002605D4
		public Vector3 readVector3()
		{
			float num = this.readFloat();
			float num2 = this.readFloat();
			float num3 = this.readFloat();
			return new Vector3(num, num2, num3);
		}

		// Token: 0x06005E8F RID: 24207 RVA: 0x002623FC File Offset: 0x002605FC
		public Vector4 readVector4()
		{
			float num = this.readFloat();
			float num2 = this.readFloat();
			float num3 = this.readFloat();
			float num4 = this.readFloat();
			return new Vector4(num, num2, num3, num4);
		}

		// Token: 0x06005E90 RID: 24208 RVA: 0x0004247A File Offset: 0x0004067A
		public byte[] readPython()
		{
			return this.readBlob();
		}

		// Token: 0x06005E91 RID: 24209 RVA: 0x0026242C File Offset: 0x0026062C
		public Vector2 readPackXZ()
		{
			MemoryStream.PackFloatXType packFloatXType;
			packFloatXType.fv = 0f;
			MemoryStream.PackFloatXType packFloatXType2;
			packFloatXType2.fv = 0f;
			packFloatXType.uv = 1073741824U;
			packFloatXType2.uv = 1073741824U;
			byte b = this.readUint8();
			byte b2 = this.readUint8();
			byte b3 = this.readUint8();
			uint num = 0U;
			num |= (uint)((uint)b << 16);
			num |= (uint)((uint)b2 << 8);
			num |= (uint)b3;
			packFloatXType.uv |= (num & 8384512U) << 3;
			packFloatXType2.uv |= (num & 2047U) << 15;
			packFloatXType.fv -= 2f;
			packFloatXType2.fv -= 2f;
			packFloatXType.uv |= (num & 8388608U) << 8;
			packFloatXType2.uv |= (num & 2048U) << 20;
			return new Vector2(packFloatXType.fv, packFloatXType2.fv);
		}

		// Token: 0x06005E92 RID: 24210 RVA: 0x00262524 File Offset: 0x00260724
		public float readPackY()
		{
			MemoryStream.PackFloatXType packFloatXType;
			packFloatXType.fv = 0f;
			packFloatXType.uv = 1073741824U;
			ushort num = this.readUint16();
			packFloatXType.uv |= (uint)((uint)(num & 32767) << 12);
			packFloatXType.fv -= 2f;
			packFloatXType.uv |= (uint)((uint)(num & 32768) << 16);
			return packFloatXType.fv;
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x00262590 File Offset: 0x00260790
		public void writeInt8(sbyte v)
		{
			byte[] array = this.datas_;
			int num = this.wpos;
			this.wpos = num + 1;
			array[num] = (byte)v;
		}

		// Token: 0x06005E94 RID: 24212 RVA: 0x00042482 File Offset: 0x00040682
		public void writeInt16(short v)
		{
			this.writeInt8((sbyte)(v & 255));
			this.writeInt8((sbyte)(v >> 8 & 255));
		}

		// Token: 0x06005E95 RID: 24213 RVA: 0x002625B8 File Offset: 0x002607B8
		public void writeInt32(int v)
		{
			for (int i = 0; i < 4; i++)
			{
				this.writeInt8((sbyte)(v >> i * 8 & 255));
			}
		}

		// Token: 0x06005E96 RID: 24214 RVA: 0x002625E8 File Offset: 0x002607E8
		public void writeInt64(long v)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			for (int i = 0; i < bytes.Length; i++)
			{
				byte[] array = this.datas_;
				int num = this.wpos;
				this.wpos = num + 1;
				array[num] = bytes[i];
			}
		}

		// Token: 0x06005E97 RID: 24215 RVA: 0x00262628 File Offset: 0x00260828
		public void writeUint8(byte v)
		{
			byte[] array = this.datas_;
			int num = this.wpos;
			this.wpos = num + 1;
			array[num] = v;
		}

		// Token: 0x06005E98 RID: 24216 RVA: 0x000424A2 File Offset: 0x000406A2
		public void writeUint16(ushort v)
		{
			this.writeUint8((byte)(v & 255));
			this.writeUint8((byte)(v >> 8 & 255));
		}

		// Token: 0x06005E99 RID: 24217 RVA: 0x00262650 File Offset: 0x00260850
		public void writeUint32(uint v)
		{
			for (int i = 0; i < 4; i++)
			{
				this.writeUint8((byte)(v >> i * 8 & 255U));
			}
		}

		// Token: 0x06005E9A RID: 24218 RVA: 0x00262680 File Offset: 0x00260880
		public void writeUint64(ulong v)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			for (int i = 0; i < bytes.Length; i++)
			{
				byte[] array = this.datas_;
				int num = this.wpos;
				this.wpos = num + 1;
				array[num] = bytes[i];
			}
		}

		// Token: 0x06005E9B RID: 24219 RVA: 0x002626C0 File Offset: 0x002608C0
		public void writeFloat(float v)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			for (int i = 0; i < bytes.Length; i++)
			{
				byte[] array = this.datas_;
				int num = this.wpos;
				this.wpos = num + 1;
				array[num] = bytes[i];
			}
		}

		// Token: 0x06005E9C RID: 24220 RVA: 0x00262700 File Offset: 0x00260900
		public void writeDouble(double v)
		{
			byte[] bytes = BitConverter.GetBytes(v);
			for (int i = 0; i < bytes.Length; i++)
			{
				byte[] array = this.datas_;
				int num = this.wpos;
				this.wpos = num + 1;
				array[num] = bytes[i];
			}
		}

		// Token: 0x06005E9D RID: 24221 RVA: 0x00262740 File Offset: 0x00260940
		public void writeBlob(byte[] v)
		{
			uint num = (uint)v.Length;
			if (num + 4U > this.space())
			{
				Dbg.ERROR_MSG("memorystream::writeBlob: no free!");
				return;
			}
			this.writeUint32(num);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				byte[] array = this.datas_;
				int num3 = this.wpos;
				this.wpos = num3 + 1;
				array[num3] = v[(int)num2];
			}
		}

		// Token: 0x06005E9E RID: 24222 RVA: 0x00262798 File Offset: 0x00260998
		public void writeString(string v)
		{
			if ((long)v.Length > (long)((ulong)this.space()))
			{
				Dbg.ERROR_MSG("memorystream::writeString: no free!");
				return;
			}
			byte[] bytes = Encoding.ASCII.GetBytes(v);
			int num;
			for (int i = 0; i < bytes.Length; i++)
			{
				byte[] array = this.datas_;
				num = this.wpos;
				this.wpos = num + 1;
				array[num] = bytes[i];
			}
			byte[] array2 = this.datas_;
			num = this.wpos;
			this.wpos = num + 1;
			array2[num] = 0;
		}

		// Token: 0x06005E9F RID: 24223 RVA: 0x000424C2 File Offset: 0x000406C2
		public void writeVector2(Vector2 v)
		{
			this.writeFloat(v.x);
			this.writeFloat(v.y);
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x000424DC File Offset: 0x000406DC
		public void writeVector3(Vector3 v)
		{
			this.writeFloat(v.x);
			this.writeFloat(v.y);
			this.writeFloat(v.z);
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x00042502 File Offset: 0x00040702
		public void writeVector4(Vector4 v)
		{
			this.writeFloat(v.x);
			this.writeFloat(v.y);
			this.writeFloat(v.z);
			this.writeFloat(v.w);
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x00262810 File Offset: 0x00260A10
		public void writeEntitycall(byte[] v)
		{
			ulong v2 = 0UL;
			int v3 = 0;
			ushort v4 = 0;
			ushort v5 = 0;
			this.writeUint64(v2);
			this.writeInt32(v3);
			this.writeUint16(v4);
			this.writeUint16(v5);
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x00262844 File Offset: 0x00260A44
		public void append(byte[] datas, uint offset, uint size)
		{
			if (this.space() < size)
			{
				byte[] destinationArray = new byte[(long)this.datas_.Length + (long)((ulong)(size * 2U))];
				Array.Copy(this.datas_, 0, destinationArray, 0, this.wpos);
				this.datas_ = destinationArray;
			}
			Array.Copy(datas, (long)((ulong)offset), this.datas_, (long)this.wpos, (long)((ulong)size));
			this.wpos += (int)size;
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x00042534 File Offset: 0x00040734
		public void readSkip(uint v)
		{
			this.rpos += (int)v;
		}

		// Token: 0x06005EA5 RID: 24229 RVA: 0x00042544 File Offset: 0x00040744
		public uint space()
		{
			return (uint)(this.data().Length - this.wpos);
		}

		// Token: 0x06005EA6 RID: 24230 RVA: 0x00042555 File Offset: 0x00040755
		public uint length()
		{
			return (uint)(this.wpos - this.rpos);
		}

		// Token: 0x06005EA7 RID: 24231 RVA: 0x00042564 File Offset: 0x00040764
		public bool readEOF()
		{
			return 5840 - this.rpos <= 0;
		}

		// Token: 0x06005EA8 RID: 24232 RVA: 0x00042578 File Offset: 0x00040778
		public void done()
		{
			this.rpos = this.wpos;
		}

		// Token: 0x06005EA9 RID: 24233 RVA: 0x002628B0 File Offset: 0x00260AB0
		public void clear()
		{
			this.rpos = (this.wpos = 0);
			if (this.datas_.Length > 5840)
			{
				this.datas_ = new byte[5840];
			}
		}

		// Token: 0x06005EAA RID: 24234 RVA: 0x002628EC File Offset: 0x00260AEC
		public byte[] getbuffer()
		{
			byte[] array = new byte[this.length()];
			Array.Copy(this.data(), (long)this.rpos, array, 0L, (long)((ulong)this.length()));
			return array;
		}

		// Token: 0x06005EAB RID: 24235 RVA: 0x00262924 File Offset: 0x00260B24
		public string toString()
		{
			string text = "";
			int num = 0;
			byte[] array = this.getbuffer();
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

		// Token: 0x04005B1E RID: 23326
		public const int BUFFER_MAX = 5840;

		// Token: 0x04005B1F RID: 23327
		public int rpos;

		// Token: 0x04005B20 RID: 23328
		public int wpos;

		// Token: 0x04005B21 RID: 23329
		private byte[] datas_ = new byte[5840];

		// Token: 0x04005B22 RID: 23330
		private static ASCIIEncoding _converter = new ASCIIEncoding();

		// Token: 0x02000F5E RID: 3934
		[StructLayout(LayoutKind.Explicit, Size = 4)]
		private struct PackFloatXType
		{
			// Token: 0x04005B23 RID: 23331
			[FieldOffset(0)]
			public float fv;

			// Token: 0x04005B24 RID: 23332
			[FieldOffset(0)]
			public uint uv;

			// Token: 0x04005B25 RID: 23333
			[FieldOffset(0)]
			public int iv;
		}
	}
}
