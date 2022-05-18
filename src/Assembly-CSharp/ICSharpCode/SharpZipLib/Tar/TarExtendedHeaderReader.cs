using System;
using System.Collections.Generic;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200080B RID: 2059
	public class TarExtendedHeaderReader
	{
		// Token: 0x060035B4 RID: 13748 RVA: 0x000273BA File Offset: 0x000255BA
		public TarExtendedHeaderReader()
		{
			this.ResetBuffers();
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x00199694 File Offset: 0x00197894
		public void Read(byte[] buffer, int length)
		{
			for (int i = 0; i < length; i++)
			{
				byte b = buffer[i];
				if (b == TarExtendedHeaderReader.StateNext[this.state])
				{
					this.Flush();
					this.headerParts[this.state] = this.sb.ToString();
					this.sb.Clear();
					int num = this.state + 1;
					this.state = num;
					if (num == 3)
					{
						this.headers.Add(this.headerParts[1], this.headerParts[2]);
						this.headerParts = new string[3];
						this.state = 0;
					}
				}
				else
				{
					byte[] array = this.byteBuffer;
					int num = this.bbIndex;
					this.bbIndex = num + 1;
					array[num] = b;
					if (this.bbIndex == 4)
					{
						this.Flush();
					}
				}
			}
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x0019975C File Offset: 0x0019795C
		private void Flush()
		{
			int num;
			int charCount;
			bool flag;
			this.decoder.Convert(this.byteBuffer, 0, this.bbIndex, this.charBuffer, 0, 4, false, out num, out charCount, out flag);
			this.sb.Append(this.charBuffer, 0, charCount);
			this.ResetBuffers();
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000273FA File Offset: 0x000255FA
		private void ResetBuffers()
		{
			this.charBuffer = new char[4];
			this.byteBuffer = new byte[4];
			this.bbIndex = 0;
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x0002741B File Offset: 0x0002561B
		public Dictionary<string, string> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x04003062 RID: 12386
		private const byte LENGTH = 0;

		// Token: 0x04003063 RID: 12387
		private const byte KEY = 1;

		// Token: 0x04003064 RID: 12388
		private const byte VALUE = 2;

		// Token: 0x04003065 RID: 12389
		private const byte END = 3;

		// Token: 0x04003066 RID: 12390
		private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

		// Token: 0x04003067 RID: 12391
		private string[] headerParts = new string[3];

		// Token: 0x04003068 RID: 12392
		private int bbIndex;

		// Token: 0x04003069 RID: 12393
		private byte[] byteBuffer;

		// Token: 0x0400306A RID: 12394
		private char[] charBuffer;

		// Token: 0x0400306B RID: 12395
		private readonly StringBuilder sb = new StringBuilder();

		// Token: 0x0400306C RID: 12396
		private readonly Decoder decoder = Encoding.UTF8.GetDecoder();

		// Token: 0x0400306D RID: 12397
		private int state;

		// Token: 0x0400306E RID: 12398
		private static readonly byte[] StateNext = new byte[]
		{
			32,
			61,
			10
		};
	}
}
