using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x02000592 RID: 1426
	[Serializable]
	public class BZip2Exception : SharpZipBaseException
	{
		// Token: 0x06002EAC RID: 11948 RVA: 0x001421C7 File Offset: 0x001403C7
		public BZip2Exception()
		{
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x001402D3 File Offset: 0x0013E4D3
		public BZip2Exception(string message) : base(message)
		{
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x001402DC File Offset: 0x0013E4DC
		public BZip2Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x001402E6 File Offset: 0x0013E4E6
		protected BZip2Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
