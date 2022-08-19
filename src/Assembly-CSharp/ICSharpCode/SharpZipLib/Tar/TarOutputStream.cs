using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000568 RID: 1384
	public class TarOutputStream : Stream
	{
		// Token: 0x06002DA9 RID: 11689 RVA: 0x0014F276 File Offset: 0x0014D476
		[Obsolete("No Encoding for Name field is specified, any non-ASCII bytes will be discarded")]
		public TarOutputStream(Stream outputStream) : this(outputStream, 20)
		{
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x0014F281 File Offset: 0x0014D481
		public TarOutputStream(Stream outputStream, Encoding nameEncoding) : this(outputStream, 20, nameEncoding)
		{
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x0014F290 File Offset: 0x0014D490
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

		// Token: 0x06002DAC RID: 11692 RVA: 0x0014F2E8 File Offset: 0x0014D4E8
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

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x0014F344 File Offset: 0x0014D544
		// (set) Token: 0x06002DAE RID: 11694 RVA: 0x0014F351 File Offset: 0x0014D551
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x0014F35F File Offset: 0x0014D55F
		public override bool CanRead
		{
			get
			{
				return this.outputStream.CanRead;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x0014F36C File Offset: 0x0014D56C
		public override bool CanSeek
		{
			get
			{
				return this.outputStream.CanSeek;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x0014F379 File Offset: 0x0014D579
		public override bool CanWrite
		{
			get
			{
				return this.outputStream.CanWrite;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x0014F386 File Offset: 0x0014D586
		public override long Length
		{
			get
			{
				return this.outputStream.Length;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x0014F393 File Offset: 0x0014D593
		// (set) Token: 0x06002DB4 RID: 11700 RVA: 0x0014F3A0 File Offset: 0x0014D5A0
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

		// Token: 0x06002DB5 RID: 11701 RVA: 0x0014F3AE File Offset: 0x0014D5AE
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.outputStream.Seek(offset, origin);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x0014F3BD File Offset: 0x0014D5BD
		public override void SetLength(long value)
		{
			this.outputStream.SetLength(value);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x0014F3CB File Offset: 0x0014D5CB
		public override int ReadByte()
		{
			return this.outputStream.ReadByte();
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x0014F3D8 File Offset: 0x0014D5D8
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.outputStream.Read(buffer, offset, count);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x0014F3E8 File Offset: 0x0014D5E8
		public override void Flush()
		{
			this.outputStream.Flush();
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x0014F3F5 File Offset: 0x0014D5F5
		public void Finish()
		{
			if (this.IsEntryOpen)
			{
				this.CloseEntry();
			}
			this.WriteEofBlock();
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x0014F40B File Offset: 0x0014D60B
		protected override void Dispose(bool disposing)
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				this.Finish();
				this.buffer.Close();
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x0014F42D File Offset: 0x0014D62D
		public int RecordSize
		{
			get
			{
				return this.buffer.RecordSize;
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x0014F42D File Offset: 0x0014D62D
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.buffer.RecordSize;
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x0014F43A File Offset: 0x0014D63A
		private bool IsEntryOpen
		{
			get
			{
				return this.currBytes < this.currSize;
			}
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x0014F44C File Offset: 0x0014D64C
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

		// Token: 0x06002DC0 RID: 11712 RVA: 0x0014F5D4 File Offset: 0x0014D7D4
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

		// Token: 0x06002DC1 RID: 11713 RVA: 0x0014F66A File Offset: 0x0014D86A
		public override void WriteByte(byte value)
		{
			this.Write(new byte[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x0014F680 File Offset: 0x0014D880
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

		// Token: 0x06002DC3 RID: 11715 RVA: 0x0014F836 File Offset: 0x0014DA36
		private void WriteEofBlock()
		{
			Array.Clear(this.blockBuffer, 0, this.blockBuffer.Length);
			this.buffer.WriteBlock(this.blockBuffer);
			this.buffer.WriteBlock(this.blockBuffer);
		}

		// Token: 0x0400287E RID: 10366
		private long currBytes;

		// Token: 0x0400287F RID: 10367
		private int assemblyBufferLength;

		// Token: 0x04002880 RID: 10368
		private bool isClosed;

		// Token: 0x04002881 RID: 10369
		protected long currSize;

		// Token: 0x04002882 RID: 10370
		protected byte[] blockBuffer;

		// Token: 0x04002883 RID: 10371
		protected byte[] assemblyBuffer;

		// Token: 0x04002884 RID: 10372
		protected TarBuffer buffer;

		// Token: 0x04002885 RID: 10373
		protected Stream outputStream;

		// Token: 0x04002886 RID: 10374
		protected Encoding nameEncoding;
	}
}
