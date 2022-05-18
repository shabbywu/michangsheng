using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007D1 RID: 2001
	public sealed class ZipExtraData : IDisposable
	{
		// Token: 0x060032F8 RID: 13048 RVA: 0x00025281 File Offset: 0x00023481
		public ZipExtraData()
		{
			this.Clear();
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x0002528F File Offset: 0x0002348F
		public ZipExtraData(byte[] data)
		{
			if (data == null)
			{
				this._data = new byte[0];
				return;
			}
			this._data = data;
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000252AE File Offset: 0x000234AE
		public byte[] GetEntryData()
		{
			if (this.Length > 65535)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			return (byte[])this._data.Clone();
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000252D8 File Offset: 0x000234D8
		public void Clear()
		{
			if (this._data == null || this._data.Length != 0)
			{
				this._data = new byte[0];
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x000252F7 File Offset: 0x000234F7
		public int Length
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x0018EF4C File Offset: 0x0018D14C
		public Stream GetStreamForTag(int tag)
		{
			Stream result = null;
			if (this.Find(tag))
			{
				result = new MemoryStream(this._data, this._index, this._readValueLength, false);
			}
			return result;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x0018EF80 File Offset: 0x0018D180
		public T GetData<T>() where T : class, ITaggedData, new()
		{
			T t = Activator.CreateInstance<T>();
			if (this.Find((int)t.TagID))
			{
				t.SetData(this._data, this._readValueStart, this._readValueLength);
				return t;
			}
			return default(T);
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x00025301 File Offset: 0x00023501
		public int ValueLength
		{
			get
			{
				return this._readValueLength;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x00025309 File Offset: 0x00023509
		public int CurrentReadIndex
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x00025311 File Offset: 0x00023511
		public int UnreadCount
		{
			get
			{
				if (this._readValueStart > this._data.Length || this._readValueStart < 4)
				{
					throw new ZipException("Find must be called before calling a Read method");
				}
				return this._readValueStart + this._readValueLength - this._index;
			}
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x0018EFD0 File Offset: 0x0018D1D0
		public bool Find(int headerID)
		{
			this._readValueStart = this._data.Length;
			this._readValueLength = 0;
			this._index = 0;
			int num = this._readValueStart;
			int num2 = headerID - 1;
			while (num2 != headerID && this._index < this._data.Length - 3)
			{
				num2 = this.ReadShortInternal();
				num = this.ReadShortInternal();
				if (num2 != headerID)
				{
					this._index += num;
				}
			}
			bool flag = num2 == headerID && this._index + num <= this._data.Length;
			if (flag)
			{
				this._readValueStart = this._index;
				this._readValueLength = num;
			}
			return flag;
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x0002534B File Offset: 0x0002354B
		public void AddEntry(ITaggedData taggedData)
		{
			if (taggedData == null)
			{
				throw new ArgumentNullException("taggedData");
			}
			this.AddEntry((int)taggedData.TagID, taggedData.GetData());
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x0018F070 File Offset: 0x0018D270
		public void AddEntry(int headerID, byte[] fieldData)
		{
			if (headerID > 65535 || headerID < 0)
			{
				throw new ArgumentOutOfRangeException("headerID");
			}
			int num = (fieldData == null) ? 0 : fieldData.Length;
			if (num > 65535)
			{
				throw new ArgumentOutOfRangeException("fieldData", "exceeds maximum length");
			}
			int num2 = this._data.Length + num + 4;
			if (this.Find(headerID))
			{
				num2 -= this.ValueLength + 4;
			}
			if (num2 > 65535)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			this.Delete(headerID);
			byte[] array = new byte[num2];
			this._data.CopyTo(array, 0);
			int index = this._data.Length;
			this._data = array;
			this.SetShort(ref index, headerID);
			this.SetShort(ref index, num);
			if (fieldData != null)
			{
				fieldData.CopyTo(array, index);
			}
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x0002536D File Offset: 0x0002356D
		public void StartNewEntry()
		{
			this._newEntry = new MemoryStream();
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x0018F134 File Offset: 0x0018D334
		public void AddNewEntry(int headerID)
		{
			byte[] fieldData = this._newEntry.ToArray();
			this._newEntry = null;
			this.AddEntry(headerID, fieldData);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x0002537A File Offset: 0x0002357A
		public void AddData(byte data)
		{
			this._newEntry.WriteByte(data);
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x00025388 File Offset: 0x00023588
		public void AddData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._newEntry.Write(data, 0, data.Length);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000253A8 File Offset: 0x000235A8
		public void AddLeShort(int toAdd)
		{
			this._newEntry.WriteByte((byte)toAdd);
			this._newEntry.WriteByte((byte)(toAdd >> 8));
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000253C6 File Offset: 0x000235C6
		public void AddLeInt(int toAdd)
		{
			this.AddLeShort((int)((short)toAdd));
			this.AddLeShort((int)((short)(toAdd >> 16)));
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000253DB File Offset: 0x000235DB
		public void AddLeLong(long toAdd)
		{
			this.AddLeInt((int)(toAdd & (long)((ulong)-1)));
			this.AddLeInt((int)(toAdd >> 32));
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x0018F15C File Offset: 0x0018D35C
		public bool Delete(int headerID)
		{
			bool result = false;
			if (this.Find(headerID))
			{
				result = true;
				int num = this._readValueStart - 4;
				byte[] array = new byte[this._data.Length - (this.ValueLength + 4)];
				Array.Copy(this._data, 0, array, 0, num);
				int num2 = num + this.ValueLength + 4;
				Array.Copy(this._data, num2, array, num, this._data.Length - num2);
				this._data = array;
			}
			return result;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000253F3 File Offset: 0x000235F3
		public long ReadLong()
		{
			this.ReadCheck(8);
			return ((long)this.ReadInt() & (long)((ulong)-1)) | (long)this.ReadInt() << 32;
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x0018F1D0 File Offset: 0x0018D3D0
		public int ReadInt()
		{
			this.ReadCheck(4);
			int result = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8) + ((int)this._data[this._index + 2] << 16) + ((int)this._data[this._index + 3] << 24);
			this._index += 4;
			return result;
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x00025411 File Offset: 0x00023611
		public int ReadShort()
		{
			this.ReadCheck(2);
			int result = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return result;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x0018F238 File Offset: 0x0018D438
		public int ReadByte()
		{
			int result = -1;
			if (this._index < this._data.Length && this._readValueStart + this._readValueLength > this._index)
			{
				result = (int)this._data[this._index];
				this._index++;
			}
			return result;
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x00025447 File Offset: 0x00023647
		public void Skip(int amount)
		{
			this.ReadCheck(amount);
			this._index += amount;
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x0018F28C File Offset: 0x0018D48C
		private void ReadCheck(int length)
		{
			if (this._readValueStart > this._data.Length || this._readValueStart < 4)
			{
				throw new ZipException("Find must be called before calling a Read method");
			}
			if (this._index > this._readValueStart + this._readValueLength - length)
			{
				throw new ZipException("End of extra data");
			}
			if (this._index + length < 4)
			{
				throw new ZipException("Cannot read before start of tag");
			}
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x0018F2F8 File Offset: 0x0018D4F8
		private int ReadShortInternal()
		{
			if (this._index > this._data.Length - 2)
			{
				throw new ZipException("End of extra data");
			}
			int result = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return result;
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x0002545E File Offset: 0x0002365E
		private void SetShort(ref int index, int source)
		{
			this._data[index] = (byte)source;
			this._data[index + 1] = (byte)(source >> 8);
			index += 2;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x00025480 File Offset: 0x00023680
		public void Dispose()
		{
			if (this._newEntry != null)
			{
				this._newEntry.Dispose();
			}
		}

		// Token: 0x04002F00 RID: 12032
		private int _index;

		// Token: 0x04002F01 RID: 12033
		private int _readValueStart;

		// Token: 0x04002F02 RID: 12034
		private int _readValueLength;

		// Token: 0x04002F03 RID: 12035
		private MemoryStream _newEntry;

		// Token: 0x04002F04 RID: 12036
		private byte[] _data;
	}
}
