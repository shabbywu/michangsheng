using System;
using System.IO;
using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000817 RID: 2071
	public class GZipInputStream : InflaterInputStream
	{
		// Token: 0x0600365A RID: 13914 RVA: 0x00027A1C File Offset: 0x00025C1C
		public GZipInputStream(Stream baseInputStream) : this(baseInputStream, 4096)
		{
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x00027A2A File Offset: 0x00025C2A
		public GZipInputStream(Stream baseInputStream, int size) : base(baseInputStream, new Inflater(true), size)
		{
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x0019B374 File Offset: 0x00199574
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num;
			do
			{
				if (!this.readGZIPHeader)
				{
					try
					{
						if (!this.ReadHeader())
						{
							return 0;
						}
					}
					catch (Exception ex) when (this.completedLastBlock && (ex is GZipException || ex is EndOfStreamException))
					{
						return 0;
					}
				}
				num = base.Read(buffer, offset, count);
				if (num > 0)
				{
					this.crc.Update(new ArraySegment<byte>(buffer, offset, num));
				}
				if (this.inf.IsFinished)
				{
					this.ReadFooter();
				}
			}
			while (num <= 0 && count != 0);
			return num;
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x0019B420 File Offset: 0x00199620
		private bool ReadHeader()
		{
			this.crc = new Crc32();
			if (this.inputBuffer.Available <= 0)
			{
				this.inputBuffer.Fill();
				if (this.inputBuffer.Available <= 0)
				{
					return false;
				}
			}
			Crc32 crc = new Crc32();
			int num = this.inputBuffer.ReadLeByte();
			if (num < 0)
			{
				throw new EndOfStreamException("EOS reading GZIP header");
			}
			crc.Update(num);
			if (num != 31)
			{
				throw new GZipException("Error GZIP header, first magic byte doesn't match");
			}
			num = this.inputBuffer.ReadLeByte();
			if (num < 0)
			{
				throw new EndOfStreamException("EOS reading GZIP header");
			}
			if (num != 139)
			{
				throw new GZipException("Error GZIP header,  second magic byte doesn't match");
			}
			crc.Update(num);
			int num2 = this.inputBuffer.ReadLeByte();
			if (num2 < 0)
			{
				throw new EndOfStreamException("EOS reading GZIP header");
			}
			if (num2 != 8)
			{
				throw new GZipException("Error GZIP header, data not in deflate format");
			}
			crc.Update(num2);
			int num3 = this.inputBuffer.ReadLeByte();
			if (num3 < 0)
			{
				throw new EndOfStreamException("EOS reading GZIP header");
			}
			crc.Update(num3);
			if ((num3 & 224) != 0)
			{
				throw new GZipException("Reserved flag bits in GZIP header != 0");
			}
			for (int i = 0; i < 6; i++)
			{
				int num4 = this.inputBuffer.ReadLeByte();
				if (num4 < 0)
				{
					throw new EndOfStreamException("EOS reading GZIP header");
				}
				crc.Update(num4);
			}
			if ((num3 & 4) != 0)
			{
				int num5 = this.inputBuffer.ReadLeByte();
				int num6 = this.inputBuffer.ReadLeByte();
				if (num5 < 0 || num6 < 0)
				{
					throw new EndOfStreamException("EOS reading GZIP header");
				}
				crc.Update(num5);
				crc.Update(num6);
				int num7 = num6 << 8 | num5;
				for (int j = 0; j < num7; j++)
				{
					int num8 = this.inputBuffer.ReadLeByte();
					if (num8 < 0)
					{
						throw new EndOfStreamException("EOS reading GZIP header");
					}
					crc.Update(num8);
				}
			}
			if ((num3 & 8) != 0)
			{
				int num9;
				while ((num9 = this.inputBuffer.ReadLeByte()) > 0)
				{
					crc.Update(num9);
				}
				if (num9 < 0)
				{
					throw new EndOfStreamException("EOS reading GZIP header");
				}
				crc.Update(num9);
			}
			if ((num3 & 16) != 0)
			{
				int num10;
				while ((num10 = this.inputBuffer.ReadLeByte()) > 0)
				{
					crc.Update(num10);
				}
				if (num10 < 0)
				{
					throw new EndOfStreamException("EOS reading GZIP header");
				}
				crc.Update(num10);
			}
			if ((num3 & 2) != 0)
			{
				int num11 = this.inputBuffer.ReadLeByte();
				if (num11 < 0)
				{
					throw new EndOfStreamException("EOS reading GZIP header");
				}
				int num12 = this.inputBuffer.ReadLeByte();
				if (num12 < 0)
				{
					throw new EndOfStreamException("EOS reading GZIP header");
				}
				if ((num11 << 8 | num12) != ((int)crc.Value & 65535))
				{
					throw new GZipException("Header CRC value mismatch");
				}
			}
			this.readGZIPHeader = true;
			return true;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x0019B6C4 File Offset: 0x001998C4
		private void ReadFooter()
		{
			byte[] array = new byte[8];
			long num = this.inf.TotalOut & (long)((ulong)-1);
			this.inputBuffer.Available += this.inf.RemainingInput;
			this.inf.Reset();
			int num2;
			for (int i = 8; i > 0; i -= num2)
			{
				num2 = this.inputBuffer.ReadClearTextBuffer(array, 8 - i, i);
				if (num2 <= 0)
				{
					throw new EndOfStreamException("EOS reading GZIP footer");
				}
			}
			int num3 = (int)(array[0] & byte.MaxValue) | (int)(array[1] & byte.MaxValue) << 8 | (int)(array[2] & byte.MaxValue) << 16 | (int)array[3] << 24;
			if (num3 != (int)this.crc.Value)
			{
				throw new GZipException(string.Concat(new object[]
				{
					"GZIP crc sum mismatch, theirs \"",
					num3,
					"\" and ours \"",
					(int)this.crc.Value
				}));
			}
			uint num4 = (uint)((int)(array[4] & byte.MaxValue) | (int)(array[5] & byte.MaxValue) << 8 | (int)(array[6] & byte.MaxValue) << 16 | (int)array[7] << 24);
			if (num != (long)((ulong)num4))
			{
				throw new GZipException("Number of bytes mismatch in footer");
			}
			this.readGZIPHeader = false;
			this.completedLastBlock = true;
		}

		// Token: 0x040030EA RID: 12522
		protected Crc32 crc;

		// Token: 0x040030EB RID: 12523
		private bool readGZIPHeader;

		// Token: 0x040030EC RID: 12524
		private bool completedLastBlock;
	}
}
