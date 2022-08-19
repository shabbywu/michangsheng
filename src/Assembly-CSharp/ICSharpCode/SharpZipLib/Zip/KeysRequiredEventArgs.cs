using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000539 RID: 1337
	public class KeysRequiredEventArgs : EventArgs
	{
		// Token: 0x06002AFF RID: 11007 RVA: 0x00142DC6 File Offset: 0x00140FC6
		public KeysRequiredEventArgs(string name)
		{
			this.fileName = name;
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x00142DD5 File Offset: 0x00140FD5
		public KeysRequiredEventArgs(string name, byte[] keyValue)
		{
			this.fileName = name;
			this.key = keyValue;
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x00142DEB File Offset: 0x00140FEB
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x00142DF3 File Offset: 0x00140FF3
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x00142DFB File Offset: 0x00140FFB
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

		// Token: 0x040026FE RID: 9982
		private readonly string fileName;

		// Token: 0x040026FF RID: 9983
		private byte[] key;
	}
}
