using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000816 RID: 2070
	[Serializable]
	public class GZipException : SharpZipBaseException
	{
		// Token: 0x06003656 RID: 13910 RVA: 0x0002505B File Offset: 0x0002325B
		public GZipException()
		{
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x0002488E File Offset: 0x00022A8E
		public GZipException(string message) : base(message)
		{
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x00024897 File Offset: 0x00022A97
		public GZipException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000248A1 File Offset: 0x00022AA1
		protected GZipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
