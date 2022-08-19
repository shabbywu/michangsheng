using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x0200056E RID: 1390
	[Serializable]
	public class GZipException : SharpZipBaseException
	{
		// Token: 0x06002DE0 RID: 11744 RVA: 0x001421C7 File Offset: 0x001403C7
		public GZipException()
		{
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x001402D3 File Offset: 0x0013E4D3
		public GZipException(string message) : base(message)
		{
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x001402DC File Offset: 0x0013E4DC
		public GZipException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x001402E6 File Offset: 0x0013E4E6
		protected GZipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
