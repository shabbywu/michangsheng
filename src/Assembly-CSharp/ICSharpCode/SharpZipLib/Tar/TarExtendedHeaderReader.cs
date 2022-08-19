using System;
using System.Collections.Generic;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000565 RID: 1381
	public class TarExtendedHeaderReader
	{
		// Token: 0x06002D46 RID: 11590 RVA: 0x0014DDF9 File Offset: 0x0014BFF9
		public TarExtendedHeaderReader()
		{
			this.ResetBuffers();
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x0014DE3C File Offset: 0x0014C03C
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

		// Token: 0x06002D48 RID: 11592 RVA: 0x0014DF04 File Offset: 0x0014C104
		private void Flush()
		{
			int num;
			int charCount;
			bool flag;
			this.decoder.Convert(this.byteBuffer, 0, this.bbIndex, this.charBuffer, 0, 4, false, out num, out charCount, out flag);
			this.sb.Append(this.charBuffer, 0, charCount);
			this.ResetBuffers();
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0014DF52 File Offset: 0x0014C152
		private void ResetBuffers()
		{
			this.charBuffer = new char[4];
			this.byteBuffer = new byte[4];
			this.bbIndex = 0;
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06002D4A RID: 11594 RVA: 0x0014DF73 File Offset: 0x0014C173
		public Dictionary<string, string> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x04002829 RID: 10281
		private const byte LENGTH = 0;

		// Token: 0x0400282A RID: 10282
		private const byte KEY = 1;

		// Token: 0x0400282B RID: 10283
		private const byte VALUE = 2;

		// Token: 0x0400282C RID: 10284
		private const byte END = 3;

		// Token: 0x0400282D RID: 10285
		private readonly Dictionary<string, string> headers = new Dictionary<string, string>();

		// Token: 0x0400282E RID: 10286
		private string[] headerParts = new string[3];

		// Token: 0x0400282F RID: 10287
		private int bbIndex;

		// Token: 0x04002830 RID: 10288
		private byte[] byteBuffer;

		// Token: 0x04002831 RID: 10289
		private char[] charBuffer;

		// Token: 0x04002832 RID: 10290
		private readonly StringBuilder sb = new StringBuilder();

		// Token: 0x04002833 RID: 10291
		private readonly Decoder decoder = Encoding.UTF8.GetDecoder();

		// Token: 0x04002834 RID: 10292
		private int state;

		// Token: 0x04002835 RID: 10293
		private static readonly byte[] StateNext = new byte[]
		{
			32,
			61,
			10
		};
	}
}
