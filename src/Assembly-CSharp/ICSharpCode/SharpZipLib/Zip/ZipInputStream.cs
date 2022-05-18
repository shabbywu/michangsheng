using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007ED RID: 2029
	public class ZipInputStream : InflaterInputStream
	{
		// Token: 0x0600341F RID: 13343 RVA: 0x0002602E File Offset: 0x0002422E
		public ZipInputStream(Stream baseInputStream) : base(baseInputStream, new Inflater(true))
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x0002605A File Offset: 0x0002425A
		public ZipInputStream(Stream baseInputStream, int bufferSize) : base(baseInputStream, new Inflater(true), bufferSize)
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x00026087 File Offset: 0x00024287
		// (set) Token: 0x06003422 RID: 13346 RVA: 0x0002608F File Offset: 0x0002428F
		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				this.password = value;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x00026098 File Offset: 0x00024298
		public bool CanDecompressEntry
		{
			get
			{
				return this.entry != null && ZipInputStream.IsEntryCompressionMethodSupported(this.entry) && this.entry.CanDecompress;
			}
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x001929B8 File Offset: 0x00190BB8
		private static bool IsEntryCompressionMethodSupported(ZipEntry entry)
		{
			CompressionMethod compressionMethodForHeader = entry.CompressionMethodForHeader;
			return compressionMethodForHeader == CompressionMethod.Deflated || compressionMethodForHeader == CompressionMethod.Stored;
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x001929D8 File Offset: 0x00190BD8
		public ZipEntry GetNextEntry()
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed.");
			}
			if (this.entry != null)
			{
				this.CloseEntry();
			}
			int num = this.inputBuffer.ReadLeInt();
			if (num == 33639248 || num == 101010256 || num == 84233040 || num == 117853008 || num == 101075792)
			{
				base.Dispose();
				return null;
			}
			if (num == 808471376 || num == 134695760)
			{
				num = this.inputBuffer.ReadLeInt();
			}
			if (num != 67324752)
			{
				throw new ZipException("Wrong Local header signature: 0x" + string.Format("{0:X}", num));
			}
			short versionRequiredToExtract = (short)this.inputBuffer.ReadLeShort();
			this.flags = this.inputBuffer.ReadLeShort();
			this.method = (CompressionMethod)this.inputBuffer.ReadLeShort();
			uint num2 = (uint)this.inputBuffer.ReadLeInt();
			int num3 = this.inputBuffer.ReadLeInt();
			this.csize = (long)this.inputBuffer.ReadLeInt();
			this.size = (long)this.inputBuffer.ReadLeInt();
			int num4 = this.inputBuffer.ReadLeShort();
			int num5 = this.inputBuffer.ReadLeShort();
			bool flag = (this.flags & 1) == 1;
			byte[] array = new byte[num4];
			this.inputBuffer.ReadRawBuffer(array);
			string name = ZipStrings.ConvertToStringExt(this.flags, array);
			this.entry = new ZipEntry(name, (int)versionRequiredToExtract, 51, this.method)
			{
				Flags = this.flags
			};
			if ((this.flags & 8) == 0)
			{
				this.entry.Crc = ((long)num3 & (long)((ulong)-1));
				this.entry.Size = (this.size & (long)((ulong)-1));
				this.entry.CompressedSize = (this.csize & (long)((ulong)-1));
				this.entry.CryptoCheckValue = (byte)(num3 >> 24 & 255);
			}
			else
			{
				if (num3 != 0)
				{
					this.entry.Crc = ((long)num3 & (long)((ulong)-1));
				}
				if (this.size != 0L)
				{
					this.entry.Size = (this.size & (long)((ulong)-1));
				}
				if (this.csize != 0L)
				{
					this.entry.CompressedSize = (this.csize & (long)((ulong)-1));
				}
				this.entry.CryptoCheckValue = (byte)(num2 >> 8 & 255U);
			}
			this.entry.DosTime = (long)((ulong)num2);
			if (num5 > 0)
			{
				byte[] array2 = new byte[num5];
				this.inputBuffer.ReadRawBuffer(array2);
				this.entry.ExtraData = array2;
			}
			this.entry.ProcessExtraData(true);
			if (this.entry.CompressedSize >= 0L)
			{
				this.csize = this.entry.CompressedSize;
			}
			if (this.entry.Size >= 0L)
			{
				this.size = this.entry.Size;
			}
			if (this.method == CompressionMethod.Stored && ((!flag && this.csize != this.size) || (flag && this.csize - 12L != this.size)))
			{
				throw new ZipException("Stored, but compressed != uncompressed");
			}
			if (ZipInputStream.IsEntryCompressionMethodSupported(this.entry))
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.InitialRead);
			}
			else
			{
				this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotSupported);
			}
			return this.entry;
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x00192D0C File Offset: 0x00190F0C
		private void ReadDataDescriptor()
		{
			if (this.inputBuffer.ReadLeInt() != 134695760)
			{
				throw new ZipException("Data descriptor signature not found");
			}
			this.entry.Crc = ((long)this.inputBuffer.ReadLeInt() & (long)((ulong)-1));
			if (this.entry.LocalHeaderRequiresZip64)
			{
				this.csize = this.inputBuffer.ReadLeLong();
				this.size = this.inputBuffer.ReadLeLong();
			}
			else
			{
				this.csize = (long)this.inputBuffer.ReadLeInt();
				this.size = (long)this.inputBuffer.ReadLeInt();
			}
			this.entry.CompressedSize = this.csize;
			this.entry.Size = this.size;
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x00192DC8 File Offset: 0x00190FC8
		private void CompleteCloseEntry(bool testCrc)
		{
			base.StopDecrypting();
			if ((this.flags & 8) != 0)
			{
				this.ReadDataDescriptor();
			}
			this.size = 0L;
			if (testCrc && (this.crc.Value & (long)((ulong)-1)) != this.entry.Crc && this.entry.Crc != -1L)
			{
				throw new ZipException("CRC mismatch");
			}
			this.crc.Reset();
			if (this.method == CompressionMethod.Deflated)
			{
				this.inf.Reset();
			}
			this.entry = null;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x00192E54 File Offset: 0x00191054
		public void CloseEntry()
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed");
			}
			if (this.entry == null)
			{
				return;
			}
			if (this.method == CompressionMethod.Deflated)
			{
				if ((this.flags & 8) != 0)
				{
					byte[] array = new byte[4096];
					while (this.Read(array, 0, array.Length) > 0)
					{
					}
					return;
				}
				this.csize -= this.inf.TotalIn;
				this.inputBuffer.Available += this.inf.RemainingInput;
			}
			if ((long)this.inputBuffer.Available > this.csize && this.csize >= 0L)
			{
				this.inputBuffer.Available = (int)((long)this.inputBuffer.Available - this.csize);
			}
			else
			{
				this.csize -= (long)this.inputBuffer.Available;
				this.inputBuffer.Available = 0;
				while (this.csize != 0L)
				{
					long num = base.Skip(this.csize);
					if (num <= 0L)
					{
						throw new ZipException("Zip archive ends early.");
					}
					this.csize -= num;
				}
			}
			this.CompleteCloseEntry(false);
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06003429 RID: 13353 RVA: 0x000260BC File Offset: 0x000242BC
		public override int Available
		{
			get
			{
				if (this.entry == null)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000260C9 File Offset: 0x000242C9
		public override long Length
		{
			get
			{
				if (this.entry == null)
				{
					throw new InvalidOperationException("No current entry");
				}
				if (this.entry.Size >= 0L)
				{
					return this.entry.Size;
				}
				throw new ZipException("Length not available for the current entry");
			}
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x00192F80 File Offset: 0x00191180
		public override int ReadByte()
		{
			byte[] array = new byte[1];
			if (this.Read(array, 0, 1) <= 0)
			{
				return -1;
			}
			return (int)(array[0] & byte.MaxValue);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x00026103 File Offset: 0x00024303
		private int ReadingNotAvailable(byte[] destination, int offset, int count)
		{
			throw new InvalidOperationException("Unable to read from this stream");
		}

		// Token: 0x0600342D RID: 13357 RVA: 0x0002610F File Offset: 0x0002430F
		private int ReadingNotSupported(byte[] destination, int offset, int count)
		{
			throw new ZipException("The compression method for this entry is not supported");
		}

		// Token: 0x0600342E RID: 13358 RVA: 0x00192FAC File Offset: 0x001911AC
		private int InitialRead(byte[] destination, int offset, int count)
		{
			if (!this.CanDecompressEntry)
			{
				throw new ZipException("Library cannot extract this entry. Version required is (" + this.entry.Version + ")");
			}
			if (this.entry.IsCrypted)
			{
				if (this.password == null)
				{
					throw new ZipException("No password set.");
				}
				PkzipClassicManaged pkzipClassicManaged = new PkzipClassicManaged();
				byte[] rgbKey = PkzipClassic.GenerateKeys(ZipStrings.ConvertToArray(this.password));
				this.inputBuffer.CryptoTransform = pkzipClassicManaged.CreateDecryptor(rgbKey, null);
				byte[] array = new byte[12];
				this.inputBuffer.ReadClearTextBuffer(array, 0, 12);
				if (array[11] != this.entry.CryptoCheckValue)
				{
					throw new ZipException("Invalid password");
				}
				if (this.csize >= 12L)
				{
					this.csize -= 12L;
				}
				else if ((this.entry.Flags & 8) == 0)
				{
					throw new ZipException(string.Format("Entry compressed size {0} too small for encryption", this.csize));
				}
			}
			else
			{
				this.inputBuffer.CryptoTransform = null;
			}
			if (this.csize > 0L || (this.flags & 8) != 0)
			{
				if (this.method == CompressionMethod.Deflated && this.inputBuffer.Available > 0)
				{
					this.inputBuffer.SetInflaterInput(this.inf);
				}
				this.internalReader = new ZipInputStream.ReadDataHandler(this.BodyRead);
				return this.BodyRead(destination, offset, count);
			}
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
			return 0;
		}

		// Token: 0x0600342F RID: 13359 RVA: 0x00193128 File Offset: 0x00191328
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("Invalid offset/count combination");
			}
			return this.internalReader(buffer, offset, count);
		}

		// Token: 0x06003430 RID: 13360 RVA: 0x0019318C File Offset: 0x0019138C
		private int BodyRead(byte[] buffer, int offset, int count)
		{
			if (this.crc == null)
			{
				throw new InvalidOperationException("Closed");
			}
			if (this.entry == null || count <= 0)
			{
				return 0;
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentException("Offset + count exceeds buffer size");
			}
			bool flag = false;
			CompressionMethod compressionMethod = this.method;
			if (compressionMethod != CompressionMethod.Stored)
			{
				if (compressionMethod == CompressionMethod.Deflated)
				{
					count = base.Read(buffer, offset, count);
					if (count <= 0)
					{
						if (!this.inf.IsFinished)
						{
							throw new ZipException("Inflater not finished!");
						}
						this.inputBuffer.Available = this.inf.RemainingInput;
						if ((this.flags & 8) == 0 && ((this.inf.TotalIn != this.csize && this.csize != (long)((ulong)-1) && this.csize != -1L) || this.inf.TotalOut != this.size))
						{
							throw new ZipException(string.Concat(new object[]
							{
								"Size mismatch: ",
								this.csize,
								";",
								this.size,
								" <-> ",
								this.inf.TotalIn,
								";",
								this.inf.TotalOut
							}));
						}
						this.inf.Reset();
						flag = true;
					}
				}
			}
			else
			{
				if ((long)count > this.csize && this.csize >= 0L)
				{
					count = (int)this.csize;
				}
				if (count > 0)
				{
					count = this.inputBuffer.ReadClearTextBuffer(buffer, offset, count);
					if (count > 0)
					{
						this.csize -= (long)count;
						this.size -= (long)count;
					}
				}
				if (this.csize == 0L)
				{
					flag = true;
				}
				else if (count < 0)
				{
					throw new ZipException("EOF in stored block");
				}
			}
			if (count > 0)
			{
				this.crc.Update(new ArraySegment<byte>(buffer, offset, count));
			}
			if (flag)
			{
				this.CompleteCloseEntry(true);
			}
			return count;
		}

		// Token: 0x06003431 RID: 13361 RVA: 0x0002611B File Offset: 0x0002431B
		protected override void Dispose(bool disposing)
		{
			this.internalReader = new ZipInputStream.ReadDataHandler(this.ReadingNotAvailable);
			this.crc = null;
			this.entry = null;
			base.Dispose(disposing);
		}

		// Token: 0x04002F5B RID: 12123
		private ZipInputStream.ReadDataHandler internalReader;

		// Token: 0x04002F5C RID: 12124
		private Crc32 crc = new Crc32();

		// Token: 0x04002F5D RID: 12125
		private ZipEntry entry;

		// Token: 0x04002F5E RID: 12126
		private long size;

		// Token: 0x04002F5F RID: 12127
		private CompressionMethod method;

		// Token: 0x04002F60 RID: 12128
		private int flags;

		// Token: 0x04002F61 RID: 12129
		private string password;

		// Token: 0x020007EE RID: 2030
		// (Invoke) Token: 0x06003433 RID: 13363
		private delegate int ReadDataHandler(byte[] b, int offset, int length);
	}
}
