using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Lzw
{
	// Token: 0x02000812 RID: 2066
	[Serializable]
	public class LzwException : SharpZipBaseException
	{
		// Token: 0x0600363B RID: 13883 RVA: 0x0002505B File Offset: 0x0002325B
		public LzwException()
		{
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x0002488E File Offset: 0x00022A8E
		public LzwException(string message) : base(message)
		{
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x00024897 File Offset: 0x00022A97
		public LzwException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000248A1 File Offset: 0x00022AA1
		protected LzwException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
