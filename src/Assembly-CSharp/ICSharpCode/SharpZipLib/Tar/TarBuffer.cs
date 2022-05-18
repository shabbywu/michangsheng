using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000808 RID: 2056
	public class TarBuffer
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x000270AE File Offset: 0x000252AE
		public int RecordSize
		{
			get
			{
				return this.recordSize;
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000270AE File Offset: 0x000252AE
		[Obsolete("Use RecordSize property instead")]
		public int GetRecordSize()
		{
			return this.recordSize;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06003574 RID: 13684 RVA: 0x000270B6 File Offset: 0x000252B6
		public int BlockFactor
		{
			get
			{
				return this.blockFactor;
			}
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000270B6 File Offset: 0x000252B6
		[Obsolete("Use BlockFactor property instead")]
		public int GetBlockFactor()
		{
			return this.blockFactor;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000270BE File Offset: 0x000252BE
		protected TarBuffer()
		{
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000270E0 File Offset: 0x000252E0
		public static TarBuffer CreateInputTarBuffer(Stream inputStream)
		{
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			return TarBuffer.CreateInputTarBuffer(inputStream, 20);
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x000270F8 File Offset: 0x000252F8
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

		// Token: 0x06003579 RID: 13689 RVA: 0x00027136 File Offset: 0x00025336
		public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			return TarBuffer.CreateOutputTarBuffer(outputStream, 20);
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x0002714E File Offset: 0x0002534E
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

		// Token: 0x0600357B RID: 13691 RVA: 0x00198F3C File Offset: 0x0019713C
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

		// Token: 0x0600357C RID: 13692 RVA: 0x00198F98 File Offset: 0x00197198
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

		// Token: 0x0600357D RID: 13693 RVA: 0x00198FE0 File Offset: 0x001971E0
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

		// Token: 0x0600357E RID: 13694 RVA: 0x00199028 File Offset: 0x00197228
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

		// Token: 0x0600357F RID: 13695 RVA: 0x00199078 File Offset: 0x00197278
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

		// Token: 0x06003580 RID: 13696 RVA: 0x001990F4 File Offset: 0x001972F4
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

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x0002718C File Offset: 0x0002538C
		public int CurrentBlock
		{
			get
			{
				return this.currentBlockIndex;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x00027194 File Offset: 0x00025394
		// (set) Token: 0x06003583 RID: 13699 RVA: 0x0002719C File Offset: 0x0002539C
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x06003584 RID: 13700 RVA: 0x0002718C File Offset: 0x0002538C
		[Obsolete("Use CurrentBlock property instead")]
		public int GetCurrentBlockNum()
		{
			return this.currentBlockIndex;
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000271A5 File Offset: 0x000253A5
		public int CurrentRecord
		{
			get
			{
				return this.currentRecordIndex;
			}
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x000271A5 File Offset: 0x000253A5
		[Obsolete("Use CurrentRecord property instead")]
		public int GetCurrentRecordNum()
		{
			return this.currentRecordIndex;
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x00199160 File Offset: 0x00197360
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

		// Token: 0x06003588 RID: 13704 RVA: 0x001991FC File Offset: 0x001973FC
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

		// Token: 0x06003589 RID: 13705 RVA: 0x001992B4 File Offset: 0x001974B4
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

		// Token: 0x0600358A RID: 13706 RVA: 0x0019930C File Offset: 0x0019750C
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

		// Token: 0x0600358B RID: 13707 RVA: 0x00199368 File Offset: 0x00197568
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

		// Token: 0x04003055 RID: 12373
		public const int BlockSize = 512;

		// Token: 0x04003056 RID: 12374
		public const int DefaultBlockFactor = 20;

		// Token: 0x04003057 RID: 12375
		public const int DefaultRecordSize = 10240;

		// Token: 0x04003059 RID: 12377
		private Stream inputStream;

		// Token: 0x0400305A RID: 12378
		private Stream outputStream;

		// Token: 0x0400305B RID: 12379
		private byte[] recordBuffer;

		// Token: 0x0400305C RID: 12380
		private int currentBlockIndex;

		// Token: 0x0400305D RID: 12381
		private int currentRecordIndex;

		// Token: 0x0400305E RID: 12382
		private int recordSize = 10240;

		// Token: 0x0400305F RID: 12383
		private int blockFactor = 20;
	}
}
