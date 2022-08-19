using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000538 RID: 1336
	public sealed class ZipExtraData : IDisposable
	{
		// Token: 0x06002AE1 RID: 10977 RVA: 0x001427AF File Offset: 0x001409AF
		public ZipExtraData()
		{
			this.Clear();
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x001427BD File Offset: 0x001409BD
		public ZipExtraData(byte[] data)
		{
			if (data == null)
			{
				this._data = new byte[0];
				return;
			}
			this._data = data;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x001427DC File Offset: 0x001409DC
		public byte[] GetEntryData()
		{
			if (this.Length > 65535)
			{
				throw new ZipException("Data exceeds maximum length");
			}
			return (byte[])this._data.Clone();
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x00142806 File Offset: 0x00140A06
		public void Clear()
		{
			if (this._data == null || this._data.Length != 0)
			{
				this._data = new byte[0];
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x00142825 File Offset: 0x00140A25
		public int Length
		{
			get
			{
				return this._data.Length;
			}
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00142830 File Offset: 0x00140A30
		public Stream GetStreamForTag(int tag)
		{
			Stream result = null;
			if (this.Find(tag))
			{
				result = new MemoryStream(this._data, this._index, this._readValueLength, false);
			}
			return result;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x00142864 File Offset: 0x00140A64
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

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x001428B2 File Offset: 0x00140AB2
		public int ValueLength
		{
			get
			{
				return this._readValueLength;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x001428BA File Offset: 0x00140ABA
		public int CurrentReadIndex
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x001428C2 File Offset: 0x00140AC2
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

		// Token: 0x06002AEB RID: 10987 RVA: 0x001428FC File Offset: 0x00140AFC
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

		// Token: 0x06002AEC RID: 10988 RVA: 0x0014299A File Offset: 0x00140B9A
		public void AddEntry(ITaggedData taggedData)
		{
			if (taggedData == null)
			{
				throw new ArgumentNullException("taggedData");
			}
			this.AddEntry((int)taggedData.TagID, taggedData.GetData());
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x001429BC File Offset: 0x00140BBC
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

		// Token: 0x06002AEE RID: 10990 RVA: 0x00142A7F File Offset: 0x00140C7F
		public void StartNewEntry()
		{
			this._newEntry = new MemoryStream();
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x00142A8C File Offset: 0x00140C8C
		public void AddNewEntry(int headerID)
		{
			byte[] fieldData = this._newEntry.ToArray();
			this._newEntry = null;
			this.AddEntry(headerID, fieldData);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00142AB4 File Offset: 0x00140CB4
		public void AddData(byte data)
		{
			this._newEntry.WriteByte(data);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x00142AC2 File Offset: 0x00140CC2
		public void AddData(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._newEntry.Write(data, 0, data.Length);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x00142AE2 File Offset: 0x00140CE2
		public void AddLeShort(int toAdd)
		{
			this._newEntry.WriteByte((byte)toAdd);
			this._newEntry.WriteByte((byte)(toAdd >> 8));
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x00142B00 File Offset: 0x00140D00
		public void AddLeInt(int toAdd)
		{
			this.AddLeShort((int)((short)toAdd));
			this.AddLeShort((int)((short)(toAdd >> 16)));
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x00142B15 File Offset: 0x00140D15
		public void AddLeLong(long toAdd)
		{
			this.AddLeInt((int)(toAdd & (long)((ulong)-1)));
			this.AddLeInt((int)(toAdd >> 32));
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x00142B30 File Offset: 0x00140D30
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

		// Token: 0x06002AF6 RID: 10998 RVA: 0x00142BA4 File Offset: 0x00140DA4
		public long ReadLong()
		{
			this.ReadCheck(8);
			return ((long)this.ReadInt() & (long)((ulong)-1)) | (long)this.ReadInt() << 32;
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x00142BC4 File Offset: 0x00140DC4
		public int ReadInt()
		{
			this.ReadCheck(4);
			int result = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8) + ((int)this._data[this._index + 2] << 16) + ((int)this._data[this._index + 3] << 24);
			this._index += 4;
			return result;
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x00142C2B File Offset: 0x00140E2B
		public int ReadShort()
		{
			this.ReadCheck(2);
			int result = (int)this._data[this._index] + ((int)this._data[this._index + 1] << 8);
			this._index += 2;
			return result;
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x00142C64 File Offset: 0x00140E64
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

		// Token: 0x06002AFA RID: 11002 RVA: 0x00142CB5 File Offset: 0x00140EB5
		public void Skip(int amount)
		{
			this.ReadCheck(amount);
			this._index += amount;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x00142CCC File Offset: 0x00140ECC
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

		// Token: 0x06002AFC RID: 11004 RVA: 0x00142D38 File Offset: 0x00140F38
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

		// Token: 0x06002AFD RID: 11005 RVA: 0x00142D8F File Offset: 0x00140F8F
		private void SetShort(ref int index, int source)
		{
			this._data[index] = (byte)source;
			this._data[index + 1] = (byte)(source >> 8);
			index += 2;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x00142DB1 File Offset: 0x00140FB1
		public void Dispose()
		{
			if (this._newEntry != null)
			{
				this._newEntry.Dispose();
			}
		}

		// Token: 0x040026F9 RID: 9977
		private int _index;

		// Token: 0x040026FA RID: 9978
		private int _readValueStart;

		// Token: 0x040026FB RID: 9979
		private int _readValueLength;

		// Token: 0x040026FC RID: 9980
		private MemoryStream _newEntry;

		// Token: 0x040026FD RID: 9981
		private byte[] _data;
	}
}
