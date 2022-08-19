using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000562 RID: 1378
	public class TarBuffer
	{
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x0014D394 File Offset: 0x0014B594
		public int RecordSize
		{
			get
			{
				return this.recordSize;
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0014D394 File Offset: 0x0014B594
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.recordSize;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x0014D39C File Offset: 0x0014B59C
		public int BlockFactor
		{
			get
			{
				return this.blockFactor;
			}
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0014D39C File Offset: 0x0014B59C
		[Obsolete("Use BlockFactor property instead")]
		public int GetBlockFactor()
		{
			return this.blockFactor;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x0014D3A4 File Offset: 0x0014B5A4
		protected TarBuffer()
		{
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0014D3C6 File Offset: 0x0014B5C6
		public static TarBuffer CreateInputTarBuffer(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			return TarBuffer.CreateInputTarBuffer(inputStream, 20);
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x0014D3DE File Offset: 0x0014B5DE
		public static TarBuffer CreateInputTarBuffer(Stream inputStream, int blockFactor)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (blockFactor <= 0)
			{
				throw new ArgumentOutOfRangeException("blockFactor", "Factor cannot be negative");
			}
			TarBuffer tarBuffer = new TarBuffer();
			tarBuffer.inputStream = inputStream;
			tarBuffer.outputStream = null;
			tarBuffer.Initialize(blockFactor);
			return tarBuffer;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0014D41C File Offset: 0x0014B61C
		public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			return TarBuffer.CreateOutputTarBuffer(outputStream, 20);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0014D434 File Offset: 0x0014B634
		public static TarBuffer CreateOutputTarBuffer(Stream outputStream, int blockFactor)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (blockFactor <= 0)
			{
				throw new ArgumentOutOfRangeException("blockFactor", "Factor cannot be negative");
			}
			TarBuffer tarBuffer = new TarBuffer();
			tarBuffer.inputStream = null;
			tarBuffer.outputStream = outputStream;
			tarBuffer.Initialize(blockFactor);
			return tarBuffer;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0014D474 File Offset: 0x0014B674
		private void Initialize(int archiveBlockFactor)
		{
			this.blockFactor = archiveBlockFactor;
			this.recordSize = archiveBlockFactor * 512;
			this.recordBuffer = new byte[this.RecordSize];
			if (this.inputStream != null)
			{
				this.currentRecordIndex = -1;
				this.currentBlockIndex = this.BlockFactor;
				return;
			}
			this.currentRecordIndex = 0;
			this.currentBlockIndex = 0;
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0014D4D0 File Offset: 0x0014B6D0
		[Obsolete("Use IsEndOfArchiveBlock instead")]
		public bool IsEOFBlock(byte[] block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			if (block.Length != 512)
			{
				throw new ArgumentException("block length is invalid");
			}
			for (int i = 0; i < 512; i++)
			{
				if (block[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0014D518 File Offset: 0x0014B718
		public static bool IsEndOfArchiveBlock(byte[] block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			if (block.Length != 512)
			{
				throw new ArgumentException("block length is invalid");
			}
			for (int i = 0; i < 512; i++)
			{
				if (block[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0014D560 File Offset: 0x0014B760
		public void SkipBlock()
		{
			if (this.inputStream == null)
			{
				throw new TarException("no input stream defined");
			}
			if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecord())
			{
				throw new TarException("Failed to read a record");
			}
			this.currentBlockIndex++;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x0014D5B0 File Offset: 0x0014B7B0
		public byte[] ReadBlock()
		{
			if (this.inputStream == null)
			{
				throw new TarException("TarBuffer.ReadBlock - no input stream defined");
			}
			if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecord())
			{
				throw new TarException("Failed to read a record");
			}
			byte[] array = new byte[512];
			Array.Copy(this.recordBuffer, this.currentBlockIndex * 512, array, 0, 512);
			this.currentBlockIndex++;
			return array;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x0014D62C File Offset: 0x0014B82C
		private bool ReadRecord()
		{
			if (this.inputStream == null)
			{
				throw new TarException("no input stream defined");
			}
			this.currentBlockIndex = 0;
			int num = 0;
			long num2;
			for (int i = this.RecordSize; i > 0; i -= (int)num2)
			{
				num2 = (long)this.inputStream.Read(this.recordBuffer, num, i);
				if (num2 <= 0L)
				{
					break;
				}
				num += (int)num2;
			}
			this.currentRecordIndex++;
			return true;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x0014D695 File Offset: 0x0014B895
		public int CurrentBlock
		{
			get
			{
				return this.currentBlockIndex;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x0014D69D File Offset: 0x0014B89D
		// (set) Token: 0x06002D15 RID: 11541 RVA: 0x0014D6A5 File Offset: 0x0014B8A5
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x06002D16 RID: 11542 RVA: 0x0014D695 File Offset: 0x0014B895
		[Obsolete("Use CurrentBlock property instead")]
		public int GetCurrentBlockNum()
		{
			return this.currentBlockIndex;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06002D17 RID: 11543 RVA: 0x0014D6AE File Offset: 0x0014B8AE
		public int CurrentRecord
		{
			get
			{
				return this.currentRecordIndex;
			}
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x0014D6AE File Offset: 0x0014B8AE
		[Obsolete("Use CurrentRecord property instead")]
		public int GetCurrentRecordNum()
		{
			return this.currentRecordIndex;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x0014D6B8 File Offset: 0x0014B8B8
		public void WriteBlock(byte[] block)
		{
			if (block == null)
			{
				throw new ArgumentNullException("block");
			}
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteBlock - no output stream defined");
			}
			if (block.Length != 512)
			{
				throw new TarException(string.Format("TarBuffer.WriteBlock - block to write has length '{0}' which is not the block size of '{1}'", block.Length, 512));
			}
			if (this.currentBlockIndex >= this.BlockFactor)
			{
				this.WriteRecord();
			}
			Array.Copy(block, 0, this.recordBuffer, this.currentBlockIndex * 512, 512);
			this.currentBlockIndex++;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x0014D754 File Offset: 0x0014B954
		public void WriteBlock(byte[] buffer, int offset)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteBlock - no output stream defined");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + 512 > buffer.Length)
			{
				throw new TarException(string.Format("TarBuffer.WriteBlock - record has length '{0}' with offset '{1}' which is less than the record size of '{2}'", buffer.Length, offset, this.recordSize));
			}
			if (this.currentBlockIndex >= this.BlockFactor)
			{
				this.WriteRecord();
			}
			Array.Copy(buffer, offset, this.recordBuffer, this.currentBlockIndex * 512, 512);
			this.currentBlockIndex++;
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x0014D80C File Offset: 0x0014BA0C
		private void WriteRecord()
		{
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteRecord no output stream defined");
			}
			this.outputStream.Write(this.recordBuffer, 0, this.RecordSize);
			this.outputStream.Flush();
			this.currentBlockIndex = 0;
			this.currentRecordIndex++;
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x0014D864 File Offset: 0x0014BA64
		private void WriteFinalRecord()
		{
			if (this.outputStream == null)
			{
				throw new TarException("TarBuffer.WriteFinalRecord no output stream defined");
			}
			if (this.currentBlockIndex > 0)
			{
				int num = this.currentBlockIndex * 512;
				Array.Clear(this.recordBuffer, num, this.RecordSize - num);
				this.WriteRecord();
			}
			this.outputStream.Flush();
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0014D8C0 File Offset: 0x0014BAC0
		public void Close()
		{
			if (this.outputStream != null)
			{
				this.WriteFinalRecord();
				if (this.IsStreamOwner)
				{
					this.outputStream.Dispose();
				}
				this.outputStream = null;
				return;
			}
			if (this.inputStream != null)
			{
				if (this.IsStreamOwner)
				{
					this.inputStream.Dispose();
				}
				this.inputStream = null;
			}
		}

		// Token: 0x0400281C RID: 10268
		public const int BlockSize = 512;

		// Token: 0x0400281D RID: 10269
		public const int DefaultBlockFactor = 20;

		// Token: 0x0400281E RID: 10270
		public const int DefaultRecordSize = 10240;

		// Token: 0x04002820 RID: 10272
		private Stream inputStream;

		// Token: 0x04002821 RID: 10273
		private Stream outputStream;

		// Token: 0x04002822 RID: 10274
		private byte[] recordBuffer;

		// Token: 0x04002823 RID: 10275
		private int currentBlockIndex;

		// Token: 0x04002824 RID: 10276
		private int currentRecordIndex;

		// Token: 0x04002825 RID: 10277
		private int recordSize = 10240;

		// Token: 0x04002826 RID: 10278
		private int blockFactor = 20;
	}
}
