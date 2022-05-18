using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.BZip2
{
	// Token: 0x0200083B RID: 2107
	[Serializable]
	public class BZip2Exception : SharpZipBaseException
	{
		// Token: 0x06003722 RID: 14114 RVA: 0x0002505B File Offset: 0x0002325B
		public BZip2Exception()
		{
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x0002488E File Offset: 0x00022A8E
		public BZip2Exception(string message) : base(message)
		{
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x00024897 File Offset: 0x00022A97
		public BZip2Exception(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000248A1 File Offset: 0x00022AA1
		protected BZip2Exception(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
