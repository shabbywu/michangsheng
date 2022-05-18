using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000810 RID: 2064
	public class TarOutputStream : Stream
	{
		// Token: 0x0600361F RID: 13855 RVA: 0x0002782B File Offset: 0x00025A2B
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarOutputStream(Stream outputStream) : this(outputStream, 20)
		{
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x00027836 File Offset: 0x00025A36
		public TarOutputStream(Stream outputStream, Encoding nameEncoding) : this(outputStream, 20, nameEncoding)
		{
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x0019A6C8 File Offset: 0x001988C8
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarOutputStream(Stream outputStream, int blockFactor)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			this.outputStream = outputStream;
			this.buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
			this.assemblyBuffer = new byte[512];
			this.blockBuffer = new byte[512];
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x0019A720 File Offset: 0x00198920
		public TarOutputStream(Stream outputStream, int blockFactor, Encoding nameEncoding)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			this.outputStream = outputStream;
			this.buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
			this.assemblyBuffer = new byte[512];
			this.blockBuffer = new byte[512];
			this.nameEncoding = nameEncoding;
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x00027842 File Offset: 0x00025A42
		// (set) Token: 0x06003624 RID: 13860 RVA: 0x0002784F File Offset: 0x00025A4F
		public bool IsStreamOwner
		{
			get
			{
				return this.buffer.IsStreamOwner;
			}
			set
			{
				this.buffer.IsStreamOwner = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x0002785D File Offset: 0x00025A5D
		public override bool CanRead
		{
			get
			{
				return this.outputStream.CanRead;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x0002786A File Offset: 0x00025A6A
		public override bool CanSeek
		{
			get
			{
				return this.outputStream.CanSeek;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x00027877 File Offset: 0x00025A77
		public override bool CanWrite
		{
			get
			{
				return this.outputStream.CanWrite;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x00027884 File Offset: 0x00025A84
		public override long Length
		{
			get
			{
				return this.outputStream.Length;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06003629 RID: 13865 RVA: 0x00027891 File Offset: 0x00025A91
		// (set) Token: 0x0600362A RID: 13866 RVA: 0x0002789E File Offset: 0x00025A9E
		public override long Position
		{
			get
			{
				return this.outputStream.Position;
			}
			set
			{
				this.outputStream.Position = value;
			}
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000278AC File Offset: 0x00025AAC
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.outputStream.Seek(offset, origin);
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000278BB File Offset: 0x00025ABB
		public override void SetLength(long value)
		{
			this.outputStream.SetLength(value);
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000278C9 File Offset: 0x00025AC9
		public override int ReadByte()
		{
			return this.outputStream.ReadByte();
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000278D6 File Offset: 0x00025AD6
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.outputStream.Read(buffer, offset, count);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000278E6 File Offset: 0x00025AE6
		public override void Flush()
		{
			this.outputStream.Flush();
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000278F3 File Offset: 0x00025AF3
		public void Finish()
		{
			if (this.IsEntryOpen)
			{
				this.CloseEntry();
			}
			this.WriteEofBlock();
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x00027909 File Offset: 0x00025B09
		protected override void Dispose(bool disposing)
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				this.Finish();
				this.buffer.Close();
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x0002792B File Offset: 0x00025B2B
		public int RecordSize
		{
			get
			{
				return this.buffer.RecordSize;
			}
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x0002792B File Offset: 0x00025B2B
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.buffer.RecordSize;
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06003634 RID: 13876 RVA: 0x00027938 File Offset: 0x00025B38
		private bool IsEntryOpen
		{
			get
			{
				return this.currBytes < this.currSize;
			}
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x0019A77C File Offset: 0x0019897C
		public void PutNextEntry(TarEntry entry)
		{
			if (entry == null)
			{
				throw new ArgumentNullException("entry");
			}
			int num = (this.nameEncoding != null) ? this.nameEncoding.GetByteCount(entry.TarHeader.Name) : entry.TarHeader.Name.Length;
			if (num > 100)
			{
				TarHeader tarHeader = new TarHeader();
				tarHeader.TypeFlag = 76;
				tarHeader.Name += "././@LongLink";
				tarHeader.Mode = 420;
				tarHeader.UserId = entry.UserId;
				tarHeader.GroupId = entry.GroupId;
				tarHeader.GroupName = entry.GroupName;
				tarHeader.UserName = entry.UserName;
				tarHeader.LinkName = "";
				tarHeader.Size = (long)(num + 1);
				tarHeader.WriteHeader(this.blockBuffer, this.nameEncoding);
				this.buffer.WriteBlock(this.blockBuffer);
				int i = 0;
				while (i < num + 1)
				{
					Array.Clear(this.blockBuffer, 0, this.blockBuffer.Length);
					TarHeader.GetAsciiBytes(entry.TarHeader.Name, i, this.blockBuffer, 0, 512, this.nameEncoding);
					i += 512;
					this.buffer.WriteBlock(this.blockBuffer);
				}
			}
			entry.WriteEntryHeader(this.blockBuffer, this.nameEncoding);
			this.buffer.WriteBlock(this.blockBuffer);
			this.currBytes = 0L;
			this.currSize = (entry.IsDirectory ? 0L : entry.Size);
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x0019A904 File Offset: 0x00198B04
		public void CloseEntry()
		{
			if (this.assemblyBufferLength > 0)
			{
				Array.Clear(this.assemblyBuffer, this.assemblyBufferLength, this.assemblyBuffer.Length - this.assemblyBufferLength);
				this.buffer.WriteBlock(this.assemblyBuffer);
				this.currBytes += (long)this.assemblyBufferLength;
				this.assemblyBufferLength = 0;
			}
			if (this.currBytes < this.currSize)
			{
				throw new TarException(string.Format("Entry closed at '{0}' before the '{1}' bytes specified in the header were written", this.currBytes, this.currSize));
			}
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x00027948 File Offset: 0x00025B48
		public override void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x0019A99C File Offset: 0x00198B9C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot be negative");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset and count combination is invalid");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Cannot be negative");
			}
			if (this.currBytes + (long)count > this.currSize)
			{
				string message = string.Format("request to write '{0}' bytes exceeds size in header of '{1}' bytes", count, this.currSize);
				throw new ArgumentOutOfRangeException("count", message);
			}
			if (this.assemblyBufferLength > 0)
			{
				if (this.assemblyBufferLength + count >= this.blockBuffer.Length)
				{
					int num = this.blockBuffer.Length - this.assemblyBufferLength;
					Array.Copy(this.assemblyBuffer, 0, this.blockBuffer, 0, this.assemblyBufferLength);
					Array.Copy(buffer, offset, this.blockBuffer, this.assemblyBufferLength, num);
					this.buffer.WriteBlock(this.blockBuffer);
					this.currBytes += (long)this.blockBuffer.Length;
					offset += num;
					count -= num;
					this.assemblyBufferLength = 0;
				}
				else
				{
					Array.Copy(buffer, offset, this.assemblyBuffer, this.assemblyBufferLength, count);
					offset += count;
					this.assemblyBufferLength += count;
					count -= count;
				}
			}
			while (count > 0)
			{
				if (count < this.blockBuffer.Length)
				{
					Array.Copy(buffer, offset, this.assemblyBuffer, this.assemblyBufferLength, count);
					this.assemblyBufferLength += count;
					return;
				}
				this.buffer.WriteBlock(buffer, offset);
				int num2 = this.blockBuffer.Length;
				this.currBytes += (long)num2;
				count -= num2;
				offset += num2;
			}
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x0002795C File Offset: 0x00025B5C
		private void WriteEofBlock()
		{
			Array.Clear(this.blockBuffer, 0, this.blockBuffer.Length);
			this.buffer.WriteBlock(this.blockBuffer);
			this.buffer.WriteBlock(this.blockBuffer);
		}

		// Token: 0x040030B8 RID: 12472
		private long currBytes;

		// Token: 0x040030B9 RID: 12473
		private int assemblyBufferLength;

		// Token: 0x040030BA RID: 12474
		private bool isClosed;

		// Token: 0x040030BB RID: 12475
		protected long currSize;

		// Token: 0x040030BC RID: 12476
		protected byte[] blockBuffer;

		// Token: 0x040030BD RID: 12477
		protected byte[] assemblyBuffer;

		// Token: 0x040030BE RID: 12478
		protected TarBuffer buffer;

		// Token: 0x040030BF RID: 12479
		protected Stream outputStream;

		// Token: 0x040030C0 RID: 12480
		protected Encoding nameEncoding;
	}
}
