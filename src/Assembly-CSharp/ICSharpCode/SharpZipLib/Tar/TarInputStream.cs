using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200080D RID: 2061
	public class TarInputStream : Stream
	{
		// Token: 0x060035F8 RID: 13816 RVA: 0x000276EC File Offset: 0x000258EC
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarInputStream(Stream inputStream) : this(inputStream, 20, null)
		{
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000276F8 File Offset: 0x000258F8
		public TarInputStream(Stream inputStream, Encoding nameEncoding) : this(inputStream, 20, nameEncoding)
		{
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x00027704 File Offset: 0x00025904
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarInputStream(Stream inputStream, int blockFactor)
		{
			this.inputStream = inputStream;
			this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
			this.encoding = null;
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x00027727 File Offset: 0x00025927
		public TarInputStream(Stream inputStream, int blockFactor, Encoding nameEncoding)
		{
			this.inputStream = inputStream;
			this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
			this.encoding = nameEncoding;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060035FC RID: 13820 RVA: 0x0002774A File Offset: 0x0002594A
		// (set) Token: 0x060035FD RID: 13821 RVA: 0x00027757 File Offset: 0x00025957
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x00027765 File Offset: 0x00025965
		public override bool CanRead
		{
			get
			{
				return this.inputStream.CanRead;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x00004050 File Offset: 0x00002250
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x00027772 File Offset: 0x00025972
		public override long Length
		{
			get
			{
				return this.inputStream.Length;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x0002777F File Offset: 0x0002597F
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x0002778C File Offset: 0x0002598C
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

		// Token: 0x06003604 RID: 13828 RVA: 0x00027798 File Offset: 0x00025998
		public override void Flush()
		{
			this.inputStream.Flush();
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0002778C File Offset: 0x0002598C
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("TarInputStream Seek not supported");
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000277A5 File Offset: 0x000259A5
		public override void SetLength(long value)
		{
			throw new NotSupportedException("TarInputStream SetLength not supported");
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000277B1 File Offset: 0x000259B1
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("TarInputStream Write not supported");
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000277BD File Offset: 0x000259BD
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("TarInputStream WriteByte not supported");
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x0019A144 File Offset: 0x00198344
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x0019A16C File Offset: 0x0019836C
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

		// Token: 0x0600360B RID: 13835 RVA: 0x000277C9 File Offset: 0x000259C9
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.tarBuffer.Close();
			}
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000277D9 File Offset: 0x000259D9
		public void SetEntryFactory(TarInputStream.IEntryFactory factory)
		{
			this.entryFactory = factory;
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000277E2 File Offset: 0x000259E2
		public int RecordSize
		{
			get
			{
				return this.tarBuffer.RecordSize;
			}
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000277E2 File Offset: 0x000259E2
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.tarBuffer.RecordSize;
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x000277EF File Offset: 0x000259EF
		public long Available
		{
			get
			{
				return this.entrySize - this.entryOffset;
			}
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x0019A2E8 File Offset: 0x001984E8
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

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x00004050 File Offset: 0x00002250
		public bool IsMarkSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000042DD File Offset: 0x000024DD
		public void Mark(int markLimit)
		{
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000042DD File Offset: 0x000024DD
		public void Reset()
		{
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x0019A32C File Offset: 0x0019852C
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

		// Token: 0x06003615 RID: 13845 RVA: 0x0019A664 File Offset: 0x00198864
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

		// Token: 0x06003616 RID: 13846 RVA: 0x0019A698 File Offset: 0x00198898
		private void SkipToNextEntry()
		{
			long num = this.entrySize - this.entryOffset;
			if (num > 0L)
			{
				this.Skip(num);
			}
			this.readBuffer = null;
		}

		// Token: 0x040030AE RID: 12462
		protected bool hasHitEOF;

		// Token: 0x040030AF RID: 12463
		protected long entrySize;

		// Token: 0x040030B0 RID: 12464
		protected long entryOffset;

		// Token: 0x040030B1 RID: 12465
		protected byte[] readBuffer;

		// Token: 0x040030B2 RID: 12466
		protected TarBuffer tarBuffer;

		// Token: 0x040030B3 RID: 12467
		private TarEntry currentEntry;

		// Token: 0x040030B4 RID: 12468
		protected TarInputStream.IEntryFactory entryFactory;

		// Token: 0x040030B5 RID: 12469
		private readonly Stream inputStream;

		// Token: 0x040030B6 RID: 12470
		private readonly Encoding encoding;

		// Token: 0x0200080E RID: 2062
		public interface IEntryFactory
		{
			// Token: 0x06003617 RID: 13847
			TarEntry CreateEntry(string name);

			// Token: 0x06003618 RID: 13848
			TarEntry CreateEntryFromFile(string fileName);

			// Token: 0x06003619 RID: 13849
			TarEntry CreateEntry(byte[] headerBuffer);
		}

		// Token: 0x0200080F RID: 2063
		public class EntryFactoryAdapter : TarInputStream.IEntryFactory
		{
			// Token: 0x0600361A RID: 13850 RVA: 0x0000403D File Offset: 0x0000223D
			[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
			public EntryFactoryAdapter()
			{
			}

			// Token: 0x0600361B RID: 13851 RVA: 0x000277FE File Offset: 0x000259FE
			public EntryFactoryAdapter(Encoding nameEncoding)
			{
				this.nameEncoding = nameEncoding;
			}

			// Token: 0x0600361C RID: 13852 RVA: 0x0002780D File Offset: 0x00025A0D
			public TarEntry CreateEntry(string name)
			{
				return TarEntry.CreateTarEntry(name);
			}

			// Token: 0x0600361D RID: 13853 RVA: 0x00027815 File Offset: 0x00025A15
			public TarEntry CreateEntryFromFile(string fileName)
			{
				return TarEntry.CreateEntryFromFile(fileName);
			}

			// Token: 0x0600361E RID: 13854 RVA: 0x0002781D File Offset: 0x00025A1D
			public TarEntry CreateEntry(byte[] headerBuffer)
			{
				return new TarEntry(headerBuffer, this.nameEncoding);
			}

			// Token: 0x040030B7 RID: 12471
			private Encoding nameEncoding;
		}
	}
}
