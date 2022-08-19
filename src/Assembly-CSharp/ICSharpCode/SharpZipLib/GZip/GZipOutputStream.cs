using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000570 RID: 1392
	public class GZipOutputStream : DeflaterOutputStream
	{
		// Token: 0x06002DE9 RID: 11753 RVA: 0x001505BF File Offset: 0x0014E7BF
		public GZipOutputStream(Stream baseOutputStream) : this(baseOutputStream, 4096)
		{
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x001505CD File Offset: 0x0014E7CD
		public GZipOutputStream(Stream baseOutputStream, int size) : base(baseOutputStream, new Deflater(-1, true), size)
		{
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x001505E9 File Offset: 0x0014E7E9
		public void SetLevel(int level)
		{
			if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level", "Compression level must be 0-9");
			}
			this.deflater_.SetLevel(level);
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x001478FD File Offset: 0x00145AFD
		public int GetLevel()
		{
			return this.deflater_.GetLevel();
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x00150610 File Offset: 0x0014E810
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

		// Token: 0x06002DEE RID: 11758 RVA: 0x00150650 File Offset: 0x0014E850
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

		// Token: 0x06002DEF RID: 11759 RVA: 0x0015069C File Offset: 0x0014E89C
		public override void Flush()
		{
			if (this.state_ == GZipOutputStream.OutputState.Header)
			{
				this.WriteHeader();
			}
			base.Flush();
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x001506B4 File Offset: 0x0014E8B4
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

		// Token: 0x06002DF1 RID: 11761 RVA: 0x00150754 File Offset: 0x0014E954
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

		// Token: 0x040028B3 RID: 10419
		protected Crc32 crc = new Crc32();

		// Token: 0x040028B4 RID: 10420
		private GZipOutputStream.OutputState state_;

		// Token: 0x02001492 RID: 5266
		private enum OutputState
		{
			// Token: 0x04006C7D RID: 27773
			Header,
			// Token: 0x04006C7E RID: 27774
			Footer,
			// Token: 0x04006C7F RID: 27775
			Finished,
			// Token: 0x04006C80 RID: 27776
			Closed
		}
	}
}
