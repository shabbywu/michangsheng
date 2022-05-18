using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007D2 RID: 2002
	public class KeysRequiredEventArgs : EventArgs
	{
		// Token: 0x06003316 RID: 13078 RVA: 0x00025495 File Offset: 0x00023695
		public KeysRequiredEventArgs(string name)
		{
			this.fileName = name;
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000254A4 File Offset: 0x000236A4
		public KeysRequiredEventArgs(string name, byte[] keyValue)
		{
			this.fileName = name;
			this.key = keyValue;
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000254BA File Offset: 0x000236BA
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06003319 RID: 13081 RVA: 0x000254C2 File Offset: 0x000236C2
		// (set) Token: 0x0600331A RID: 13082 RVA: 0x000254CA File Offset: 0x000236CA
		public byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				this.key = value;
			}
		}

		// Token: 0x04002F05 RID: 12037
		private readonly string fileName;

		// Token: 0x04002F06 RID: 12038
		private byte[] key;
	}
}
