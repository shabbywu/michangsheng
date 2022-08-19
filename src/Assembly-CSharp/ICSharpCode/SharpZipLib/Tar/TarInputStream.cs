using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000567 RID: 1383
	public class TarInputStream : Stream
	{
		// Token: 0x06002D8A RID: 11658 RVA: 0x0014EBE3 File Offset: 0x0014CDE3
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarInputStream(Stream inputStream) : this(inputStream, 20, null)
		{
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x0014EBEF File Offset: 0x0014CDEF
		public TarInputStream(Stream inputStream, Encoding nameEncoding) : this(inputStream, 20, nameEncoding)
		{
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x0014EBFB File Offset: 0x0014CDFB
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarInputStream(Stream inputStream, int blockFactor)
		{
			this.inputStream = inputStream;
			this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
			this.encoding = null;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x0014EC1E File Offset: 0x0014CE1E
		public TarInputStream(Stream inputStream, int blockFactor, Encoding nameEncoding)
		{
			this.inputStream = inputStream;
			this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
			this.encoding = nameEncoding;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x0014EC41 File Offset: 0x0014CE41
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x0014EC4E File Offset: 0x0014CE4E
		public bool IsStreamOwner
		{
			get
			{
				return this.tarBuffer.IsStreamOwner;
			}
			set
			{
				this.tarBuffer.IsStreamOwner = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x0014EC5C File Offset: 0x0014CE5C
		public override bool CanRead
		{
			get
			{
				return this.inputStream.CanRead;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06002D91 RID: 11665 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x0000280F File Offset: 0x00000A0F
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06002D93 RID: 11667 RVA: 0x0014EC69 File Offset: 0x0014CE69
		public override long Length
		{
			get
			{
				return this.inputStream.Length;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x0014EC76 File Offset: 0x0014CE76
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x0014EC83 File Offset: 0x0014CE83
		public override long Position
		{
			get
			{
				return this.inputStream.Position;
			}
			set
			{
				throw new NotSupportedException("TarInputStream Seek not supported");
			}
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x0014EC8F File Offset: 0x0014CE8F
		public override void Flush()
		{
			this.inputStream.Flush();
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x0014EC83 File Offset: 0x0014CE83
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("TarInputStream Seek not supported");
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x0014EC9C File Offset: 0x0014CE9C
		public override void SetLength(long value)
		{
			throw new NotSupportedException("TarInputStream SetLength not supported");
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x0014ECA8 File Offset: 0x0014CEA8
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("TarInputStream Write not supported");
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x0014ECB4 File Offset: 0x0014CEB4
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("TarInputStream WriteByte not supported");
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x0014ECC0 File Offset: 0x0014CEC0
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x0014ECE8 File Offset: 0x0014CEE8
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			int num = 0;
			if (this.entryOffset >= this.entrySize)
			{
				return 0;
			}
			long num2 = (long)count;
			if (num2 + this.entryOffset > this.entrySize)
			{
				num2 = this.entrySize - this.entryOffset;
			}
			if (this.readBuffer != null)
			{
				int num3 = (num2 > (long)this.readBuffer.Length) ? this.readBuffer.Length : ((int)num2);
				Array.Copy(this.readBuffer, 0, buffer, offset, num3);
				if (num3 >= this.readBuffer.Length)
				{
					this.readBuffer = null;
				}
				else
				{
					int num4 = this.readBuffer.Length - num3;
					byte[] destinationArray = new byte[num4];
					Array.Copy(this.readBuffer, num3, destinationArray, 0, num4);
					this.readBuffer = destinationArray;
				}
				num += num3;
				num2 -= (long)num3;
				offset += num3;
			}
			while (num2 > 0L)
			{
				byte[] array = this.tarBuffer.ReadBlock();
				if (array == null)
				{
					throw new TarException("unexpected EOF with " + num2 + " bytes unread");
				}
				int num5 = (int)num2;
				int num6 = array.Length;
				if (num6 > num5)
				{
					Array.Copy(array, 0, buffer, offset, num5);
					this.readBuffer = new byte[num6 - num5];
					Array.Copy(array, num5, this.readBuffer, 0, num6 - num5);
				}
				else
				{
					num5 = num6;
					Array.Copy(array, 0, buffer, offset, num6);
				}
				num += num5;
				num2 -= (long)num5;
				offset += num5;
			}
			this.entryOffset += (long)num;
			return num;
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x0014EE63 File Offset: 0x0014D063
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.tarBuffer.Close();
			}
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x0014EE73 File Offset: 0x0014D073
		public void SetEntryFactory(TarInputStream.IEntryFactory factory)
		{
			this.entryFactory = factory;
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x0014EE7C File Offset: 0x0014D07C
		public int RecordSize
		{
			get
			{
				return this.tarBuffer.RecordSize;
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x0014EE7C File Offset: 0x0014D07C
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.tarBuffer.RecordSize;
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x0014EE89 File Offset: 0x0014D089
		public long Available
		{
			get
			{
				return this.entrySize - this.entryOffset;
			}
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x0014EE98 File Offset: 0x0014D098
		public void Skip(long skipCount)
		{
			byte[] array = new byte[8192];
			int num2;
			for (long num = skipCount; num > 0L; num -= (long)num2)
			{
				int count = (num > (long)array.Length) ? array.Length : ((int)num);
				num2 = this.Read(array, 0, count);
				if (num2 == -1)
				{
					break;
				}
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x0000280F File Offset: 0x00000A0F
		public bool IsMarkSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x00004095 File Offset: 0x00002295
		public void Mark(int markLimit)
		{
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x00004095 File Offset: 0x00002295
		public void Reset()
		{
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x0014EEDC File Offset: 0x0014D0DC
		public TarEntry GetNextEntry()
		{
			if (this.hasHitEOF)
			{
				return null;
			}
			if (this.currentEntry != null)
			{
				this.SkipToNextEntry();
			}
			byte[] array = this.tarBuffer.ReadBlock();
			if (array == null)
			{
				this.hasHitEOF = true;
			}
			else if (TarBuffer.IsEndOfArchiveBlock(array))
			{
				this.hasHitEOF = true;
				this.tarBuffer.ReadBlock();
			}
			else
			{
				this.hasHitEOF = false;
			}
			if (this.hasHitEOF)
			{
				this.currentEntry = null;
			}
			else
			{
				try
				{
					TarHeader tarHeader = new TarHeader();
					tarHeader.ParseBuffer(array, this.encoding);
					if (!tarHeader.IsChecksumValid)
					{
						throw new TarException("Header checksum is invalid");
					}
					this.entryOffset = 0L;
					this.entrySize = tarHeader.Size;
					StringBuilder stringBuilder = null;
					if (tarHeader.TypeFlag == 76)
					{
						byte[] array2 = new byte[512];
						long num = this.entrySize;
						stringBuilder = new StringBuilder();
						while (num > 0L)
						{
							int num2 = this.Read(array2, 0, (num > (long)array2.Length) ? array2.Length : ((int)num));
							if (num2 == -1)
							{
								throw new InvalidHeaderException("Failed to read long name entry");
							}
							stringBuilder.Append(TarHeader.ParseName(array2, 0, num2, this.encoding).ToString());
							num -= (long)num2;
						}
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					else if (tarHeader.TypeFlag == 103)
					{
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					else if (tarHeader.TypeFlag == 120)
					{
						byte[] array3 = new byte[512];
						long num3 = this.entrySize;
						TarExtendedHeaderReader tarExtendedHeaderReader = new TarExtendedHeaderReader();
						while (num3 > 0L)
						{
							int num4 = this.Read(array3, 0, (num3 > (long)array3.Length) ? array3.Length : ((int)num3));
							if (num4 == -1)
							{
								throw new InvalidHeaderException("Failed to read long name entry");
							}
							tarExtendedHeaderReader.Read(array3, num4);
							num3 -= (long)num4;
						}
						string value;
						if (tarExtendedHeaderReader.Headers.TryGetValue("path", out value))
						{
							stringBuilder = new StringBuilder(value);
						}
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					else if (tarHeader.TypeFlag == 86)
					{
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					else if (tarHeader.TypeFlag != 48 && tarHeader.TypeFlag != 0 && tarHeader.TypeFlag != 49 && tarHeader.TypeFlag != 50 && tarHeader.TypeFlag != 53)
					{
						this.SkipToNextEntry();
						array = this.tarBuffer.ReadBlock();
					}
					if (this.entryFactory == null)
					{
						this.currentEntry = new TarEntry(array, this.encoding);
						if (stringBuilder != null)
						{
							this.currentEntry.Name = stringBuilder.ToString();
						}
					}
					else
					{
						this.currentEntry = this.entryFactory.CreateEntry(array);
					}
					this.entryOffset = 0L;
					this.entrySize = this.currentEntry.Size;
				}
				catch (InvalidHeaderException ex)
				{
					this.entrySize = 0L;
					this.entryOffset = 0L;
					this.currentEntry = null;
					throw new InvalidHeaderException(string.Format("Bad header in record {0} block {1} {2}", this.tarBuffer.CurrentRecord, this.tarBuffer.CurrentBlock, ex.Message));
				}
			}
			return this.currentEntry;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x0014F214 File Offset: 0x0014D414
		public void CopyEntryContents(Stream outputStream)
		{
			byte[] array = new byte[32768];
			for (;;)
			{
				int num = this.Read(array, 0, array.Length);
				if (num <= 0)
				{
					break;
				}
				outputStream.Write(array, 0, num);
			}
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x0014F248 File Offset: 0x0014D448
		private void SkipToNextEntry()
		{
			long num = this.entrySize - this.entryOffset;
			if (num > 0L)
			{
				this.Skip(num);
			}
			this.readBuffer = null;
		}

		// Token: 0x04002875 RID: 10357
		protected bool hasHitEOF;

		// Token: 0x04002876 RID: 10358
		protected long entrySize;

		// Token: 0x04002877 RID: 10359
		protected long entryOffset;

		// Token: 0x04002878 RID: 10360
		protected byte[] readBuffer;

		// Token: 0x04002879 RID: 10361
		protected TarBuffer tarBuffer;

		// Token: 0x0400287A RID: 10362
		private TarEntry currentEntry;

		// Token: 0x0400287B RID: 10363
		protected TarInputStream.IEntryFactory entryFactory;

		// Token: 0x0400287C RID: 10364
		private readonly Stream inputStream;

		// Token: 0x0400287D RID: 10365
		private readonly Encoding encoding;

		// Token: 0x02001490 RID: 5264
		public interface IEntryFactory
		{
			// Token: 0x0600814A RID: 33098
			TarEntry CreateEntry(string name);

			// Token: 0x0600814B RID: 33099
			TarEntry CreateEntryFromFile(string fileName);

			// Token: 0x0600814C RID: 33100
			TarEntry CreateEntry(byte[] headerBuffer);
		}

		// Token: 0x02001491 RID: 5265
		public class EntryFactoryAdapter : TarInputStream.IEntryFactory
		{
			// Token: 0x0600814D RID: 33101 RVA: 0x000027FC File Offset: 0x000009FC
			[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
			public EntryFactoryAdapter()
			{
			}

			// Token: 0x0600814E RID: 33102 RVA: 0x002D8BCB File Offset: 0x002D6DCB
			public EntryFactoryAdapter(Encoding nameEncoding)
			{
				this.nameEncoding = nameEncoding;
			}

			// Token: 0x0600814F RID: 33103 RVA: 0x002D8BDA File Offset: 0x002D6DDA
			public TarEntry CreateEntry(string name)
			{
				return TarEntry.CreateTarEntry(name);
			}

			// Token: 0x06008150 RID: 33104 RVA: 0x002D8BE2 File Offset: 0x002D6DE2
			public TarEntry CreateEntryFromFile(string fileName)
			{
				return TarEntry.CreateEntryFromFile(fileName);
			}

			// Token: 0x06008151 RID: 33105 RVA: 0x002D8BEA File Offset: 0x002D6DEA
			public TarEntry CreateEntry(byte[] headerBuffer)
			{
				return new TarEntry(headerBuffer, this.nameEncoding);
			}

			// Token: 0x04006C7B RID: 27771
			private Encoding nameEncoding;
		}
	}
}
