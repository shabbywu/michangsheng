using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x020007CA RID: 1994
	[Serializable]
	public class ZipException : SharpZipBaseException
	{
		// Token: 0x060032D1 RID: 13009 RVA: 0x0002505B File Offset: 0x0002325B
		public ZipException()
		{
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x0002488E File Offset: 0x00022A8E
		public ZipException(string message) : base(message)
		{
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x00024897 File Offset: 0x00022A97
		public ZipException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000248A1 File Offset: 0x00022AA1
		protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
