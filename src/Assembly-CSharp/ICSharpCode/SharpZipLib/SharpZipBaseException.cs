using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x020007B4 RID: 1972
	[Serializable]
	public class SharpZipBaseException : Exception
	{
		// Token: 0x06003214 RID: 12820 RVA: 0x0002485C File Offset: 0x00022A5C
		public SharpZipBaseException()
		{
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x00024864 File Offset: 0x00022A64
		public SharpZipBaseException(string message) : base(message)
		{
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x0002486D File Offset: 0x00022A6D
		public SharpZipBaseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x00024877 File Offset: 0x00022A77
		protected SharpZipBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
