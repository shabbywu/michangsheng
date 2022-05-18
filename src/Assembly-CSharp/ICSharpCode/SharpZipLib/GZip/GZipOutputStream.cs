using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000818 RID: 2072
	public class GZipOutputStream : DeflaterOutputStream
	{
		// Token: 0x0600365F RID: 13919 RVA: 0x00027A3A File Offset: 0x00025C3A
		public GZipOutputStream(Stream baseOutputStream) : this(baseOutputStream, 4096)
		{
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x00027A48 File Offset: 0x00025C48
		public GZipOutputStream(Stream baseOutputStream, int size) : base(baseOutputStream, new Deflater(-1, true), size)
		{
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x00027A64 File Offset: 0x00025C64
		public void SetLevel(int level)
		{
			if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level", "Compression level must be 0-9");
			}
			this.deflater_.SetLevel(level);
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x00026237 File Offset: 0x00024437
		public int GetLevel()
		{
			return this.deflater_.GetLevel();
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x00027A8B File Offset: 0x00025C8B
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.WriteHeader();
			}
			if (this.state_ != GZipOutputStream.OutputState.Footer)
			{
				throw new InvalidOperationException("Write not permitted in current state");
			}
			this.crc.Update(new ArraySegment<byte>(buffer, offset, count));
			base.Write(buffer, offset, count);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x0019B800 File Offset: 0x00199A00
		protected override void Dispose(bool disposing)
		{
			try
			{
				this.Finish();
			}
			finally
			{
				if (this.state_ != GZipOutputStream.OutputState.Closed)
				{
					this.state_ = GZipOutputStream.OutputState.Closed;
					if (base.IsStreamOwner)
					{
						this.baseOutputStream_.Dispose();
					}
				}
			}
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x00027ACB File Offset: 0x00025CCB
		public override void Flush()
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.WriteHeader();
			}
			base.Flush();
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x0019B84C File Offset: 0x00199A4C
		public override void Finish()
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.WriteHeader();
			}
			if (this.state_ == GZipOutputStream.OutputState.Footer)
			{
				this.state_ = GZipOutputStream.OutputState.Finished;
				base.Finish();
				uint num = (uint)(this.deflater_.TotalIn & (long)((ulong)-1));
				uint num2 = (uint)(this.crc.Value & (long)((ulong)-1));
				byte[] array = new byte[]
				{
					(byte)num2,
					(byte)(num2 >> 8),
					(byte)(num2 >> 16),
					(byte)(num2 >> 24),
					(byte)num,
					(byte)(num >> 8),
					(byte)(num >> 16),
					(byte)(num >> 24)
				};
				this.baseOutputStream_.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x0019B8EC File Offset: 0x00199AEC
		private void WriteHeader()
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.state_ = GZipOutputStream.OutputState.Footer;
				int num = (int)((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000L);
				byte[] array = new byte[]
				{
					31,
					139,
					8,
					0,
					0,
					0,
					0,
					0,
					0,
					byte.MaxValue
				};
				array[4] = (byte)num;
				array[5] = (byte)(num >> 8);
				array[6] = (byte)(num >> 16);
				array[7] = (byte)(num >> 24);
				byte[] array2 = array;
				this.baseOutputStream_.Write(array2, 0, array2.Length);
			}
		}

		// Token: 0x040030ED RID: 12525
		protected Crc32 crc = new Crc32();

		// Token: 0x040030EE RID: 12526
		private GZipOutputStream.OutputState state_;

		// Token: 0x02000819 RID: 2073
		private enum OutputState
		{
			// Token: 0x040030F0 RID: 12528
			Header,
			// Token: 0x040030F1 RID: 12529
			Footer,
			// Token: 0x040030F2 RID: 12530
			Finished,
			// Token: 0x040030F3 RID: 12531
			Closed
		}
	}
}
