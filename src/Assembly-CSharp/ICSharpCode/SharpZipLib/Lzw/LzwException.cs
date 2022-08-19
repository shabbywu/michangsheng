using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x0200056A RID: 1386
	[Serializable]
	public class LzwException : SharpZipBaseException
	{
		// Token: 0x06002DC5 RID: 11717 RVA: 0x001421C7 File Offset: 0x001403C7
		public LzwException()
		{
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x001402D3 File Offset: 0x0013E4D3
		public LzwException(string message) : base(message)
		{
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x001402DC File Offset: 0x0013E4DC
		public LzwException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x001402E6 File Offset: 0x0013E4E6
		protected LzwException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
