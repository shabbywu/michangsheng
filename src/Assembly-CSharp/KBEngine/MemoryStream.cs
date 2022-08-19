using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

namespace KBEngine
{
	// Token: 0x02000BDA RID: 3034
	public class MemoryStream : ObjectPool<MemoryStream>
	{
		// Token: 0x0600543C RID: 21564 RVA: 0x002349F2 File Offset: 0x00232BF2
		public byte[] setBuffer(byte[] buffer)
		{
			byte[] result = this.datas_;
			this.datas_ = buffer;
			return result;
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00234A04 File Offset: 0x00232C04
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

		// Token: 0x0600543E RID: 21566 RVA: 0x00234A57 File Offset: 0x00232C57
		public void reclaimObject()
		{
			this.clear();
			ObjectPool<MemoryStream>.reclaimObject(this);
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x00234A65 File Offset: 0x00232C65
		public byte[] data()
		{
			return this.datas_;
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x00234A6D File Offset: 0x00232C6D
		public void setData(byte[] data)
		{
			this.datas_ = data;
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00234A78 File Offset: 0x00232C78
		public sbyte readInt8()
		{
			byte[] array = this.datas_;
			int num = this.rpos;
			this.rpos = num + 1;
			return array[num];
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x00234A9E File Offset: 0x00232C9E
		public short readInt16()
		{
			this.rpos += 2;
			return BitConverter.ToInt16(this.datas_, this.rpos - 2);
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x00234AC1 File Offset: 0x00232CC1
		public int readInt32()
		{
			this.rpos += 4;
			return BitConverter.ToInt32(this.datas_, this.rpos - 4);
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00234AE4 File Offset: 0x00232CE4
		public long readInt64()
		{
			this.rpos += 8;
			return BitConverter.ToInt64(this.datas_, this.rpos - 8);
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x00234B08 File Offset: 0x00232D08
		public byte readUint8()
		{
			byte[] array = this.datas_;
			int num = this.rpos;
			this.rpos = num + 1;
			return array[num];
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x00234B2D File Offset: 0x00232D2D
		public ushort readUint16()
		{
			this.rpos += 2;
			return BitConverter.ToUInt16(this.datas_, this.rpos - 2);
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x00234B50 File Offset: 0x00232D50
		public uint readUint32()
		{
			this.rpos += 4;
			return BitConverter.ToUInt32(this.datas_, this.rpos - 4);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x00234B73 File Offset: 0x00232D73
		public ulong readUint64()
		{
			this.rpos += 8;
			return BitConverter.ToUInt64(this.datas_, this.rpos - 8);
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x00234B96 File Offset: 0x00232D96
		public float readFloat()
		{
			this.rpos += 4;
			return BitConverter.ToSingle(this.datas_, this.rpos - 4);
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x00234BB9 File Offset: 0x00232DB9
		public double readDouble()
		{
			this.rpos += 8;
			return BitConverter.ToDouble(this.datas_, this.rpos - 8);
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x00234BDC File Offset: 0x00232DDC
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

		// Token: 0x0600544C RID: 21580 RVA: 0x00234C25 File Offset: 0x00232E25
		public string readUnicode()
		{
			return Encoding.UTF8.GetString(this.readBlob());
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x00234C38 File Offset: 0x00232E38
		public byte[] readBlob()
		{
			uint num = this.readUint32();
			byte[] array = new byte[num];
			Array.Copy(this.datas_, (long)this.rpos, array, 0L, (long)((ulong)num));
			this.rpos += (int)num;
			return array;
		}

		// Token: 0x0600544E RID: 21582 RVA: 0x00234C79 File Offset: 0x00232E79
		public byte[] readEntitycall()
		{
			this.readUint64();
			this.readInt32();
			this.readUint16();
			this.readUint16();
			return new byte[0];
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x00234CA0 File Offset: 0x00232EA0
		public Vector2 readVector2()
		{
			float num = this.readFloat();
			float num2 = this.readFloat();
			return new Vector2(num, num2);
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x00234CC0 File Offset: 0x00232EC0
		public Vector3 readVector3()
		{
			float num = this.readFloat();
			float num2 = this.readFloat();
			float num3 = this.readFloat();
			return new Vector3(num, num2, num3);
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x00234CE8 File Offset: 0x00232EE8
		public Vector4 readVector4()
		{
			float num = this.readFloat();
			float num2 = this.readFloat();
			float num3 = this.readFloat();
			float num4 = this.readFloat();
			return new Vector4(num, num2, num3, num4);
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x00234D18 File Offset: 0x00232F18
		public byte[] readPython()
		{
			return this.readBlob();
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x00234D20 File Offset: 0x00232F20
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

		// Token: 0x06005454 RID: 21588 RVA: 0x00234E18 File Offset: 0x00233018
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

		// Token: 0x06005455 RID: 21589 RVA: 0x00234E84 File Offset: 0x00233084
		public void writeInt8(sbyte v)
		{
			byte[] array = this.datas_;
			int num = this.wpos;
			this.wpos = num + 1;
			array[num] = (byte)v;
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x00234EAB File Offset: 0x002330AB
		public void writeInt16(short v)
		{
			this.writeInt8((sbyte)(v & 255));
			this.writeInt8((sbyte)(v >> 8 & 255));
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x00234ECC File Offset: 0x002330CC
		public void writeInt32(int v)
		{
			for (int i = 0; i < 4; i++)
			{
				this.writeInt8((sbyte)(v >> i * 8 & 255));
			}
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00234EFC File Offset: 0x002330FC
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

		// Token: 0x06005459 RID: 21593 RVA: 0x00234F3C File Offset: 0x0023313C
		public void writeUint8(byte v)
		{
			byte[] array = this.datas_;
			int num = this.wpos;
			this.wpos = num + 1;
			array[num] = v;
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x00234F62 File Offset: 0x00233162
		public void writeUint16(ushort v)
		{
			this.writeUint8((byte)(v & 255));
			this.writeUint8((byte)(v >> 8 & 255));
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x00234F84 File Offset: 0x00233184
		public void writeUint32(uint v)
		{
			for (int i = 0; i < 4; i++)
			{
				this.writeUint8((byte)(v >> i * 8 & 255U));
			}
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x00234FB4 File Offset: 0x002331B4
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

		// Token: 0x0600545D RID: 21597 RVA: 0x00234FF4 File Offset: 0x002331F4
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

		// Token: 0x0600545E RID: 21598 RVA: 0x00235034 File Offset: 0x00233234
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

		// Token: 0x0600545F RID: 21599 RVA: 0x00235074 File Offset: 0x00233274
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

		// Token: 0x06005460 RID: 21600 RVA: 0x002350CC File Offset: 0x002332CC
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

		// Token: 0x06005461 RID: 21601 RVA: 0x00235142 File Offset: 0x00233342
		public void writeVector2(Vector2 v)
		{
			this.writeFloat(v.x);
			this.writeFloat(v.y);
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x0023515C File Offset: 0x0023335C
		public void writeVector3(Vector3 v)
		{
			this.writeFloat(v.x);
			this.writeFloat(v.y);
			this.writeFloat(v.z);
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x00235182 File Offset: 0x00233382
		public void writeVector4(Vector4 v)
		{
			this.writeFloat(v.x);
			this.writeFloat(v.y);
			this.writeFloat(v.z);
			this.writeFloat(v.w);
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x002351B4 File Offset: 0x002333B4
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

		// Token: 0x06005465 RID: 21605 RVA: 0x002351E8 File Offset: 0x002333E8
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

		// Token: 0x06005466 RID: 21606 RVA: 0x00235253 File Offset: 0x00233453
		public void readSkip(uint v)
		{
			this.rpos += (int)v;
		}

		// Token: 0x06005467 RID: 21607 RVA: 0x00235263 File Offset: 0x00233463
		public uint space()
		{
			return (uint)(this.data().Length - this.wpos);
		}

		// Token: 0x06005468 RID: 21608 RVA: 0x00235274 File Offset: 0x00233474
		public uint length()
		{
			return (uint)(this.wpos - this.rpos);
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x00235283 File Offset: 0x00233483
		public bool readEOF()
		{
			return 5840 - this.rpos <= 0;
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x00235297 File Offset: 0x00233497
		public void done()
		{
			this.rpos = this.wpos;
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x002352A8 File Offset: 0x002334A8
		public void clear()
		{
			this.rpos = (this.wpos = 0);
			if (this.datas_.Length > 5840)
			{
				this.datas_ = new byte[5840];
			}
		}

		// Token: 0x0600546C RID: 21612 RVA: 0x002352E4 File Offset: 0x002334E4
		public byte[] getbuffer()
		{
			byte[] array = new byte[this.length()];
			Array.Copy(this.data(), (long)this.rpos, array, 0L, (long)((ulong)this.length()));
			return array;
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x0023531C File Offset: 0x0023351C
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

		// Token: 0x0400507D RID: 20605
		public const int BUFFER_MAX = 5840;

		// Token: 0x0400507E RID: 20606
		public int rpos;

		// Token: 0x0400507F RID: 20607
		public int wpos;

		// Token: 0x04005080 RID: 20608
		private byte[] datas_ = new byte[5840];

		// Token: 0x04005081 RID: 20609
		private static ASCIIEncoding _converter = new ASCIIEncoding();

		// Token: 0x020015F9 RID: 5625
		[StructLayout(LayoutKind.Explicit, Size = 4)]
		private struct PackFloatXType
		{
			// Token: 0x040070F8 RID: 28920
			[FieldOffset(0)]
			public float fv;

			// Token: 0x040070F9 RID: 28921
			[FieldOffset(0)]
			public uint uv;

			// Token: 0x040070FA RID: 28922
			[FieldOffset(0)]
			public int iv;
		}
	}
}
