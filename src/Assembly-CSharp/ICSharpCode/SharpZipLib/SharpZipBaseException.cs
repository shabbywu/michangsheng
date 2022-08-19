using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000520 RID: 1312
	[Serializable]
	public class SharpZipBaseException : Exception
	{
		// Token: 0x06002A01 RID: 10753 RVA: 0x001402A1 File Offset: 0x0013E4A1
		public SharpZipBaseException()
		{
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x001402A9 File Offset: 0x0013E4A9
		public SharpZipBaseException(string message) : base(message)
		{
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x001402B2 File Offset: 0x0013E4B2
		public SharpZipBaseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x001402BC File Offset: 0x0013E4BC
		protected SharpZipBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
