using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000805 RID: 2053
	[Serializable]
	public class InvalidHeaderException : TarException
	{
		// Token: 0x0600353D RID: 13629 RVA: 0x00026D7C File Offset: 0x00024F7C
		public InvalidHeaderException()
		{
		}

		// Token: 0x0600353E RID: 13630 RVA: 0x00026D84 File Offset: 0x00024F84
		public InvalidHeaderException(string message) : base(message)
		{
		}

		// Token: 0x0600353F RID: 13631 RVA: 0x00026D8D File Offset: 0x00024F8D
		public InvalidHeaderException(string message, Exception exception) : base(message, exception)
		{
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x00026D97 File Offset: 0x00024F97
		protected InvalidHeaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
