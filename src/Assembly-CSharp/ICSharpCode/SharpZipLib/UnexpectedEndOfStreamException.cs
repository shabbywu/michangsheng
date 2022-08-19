using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000523 RID: 1315
	[Serializable]
	public class UnexpectedEndOfStreamException : StreamDecodingException
	{
		// Token: 0x06002A0D RID: 10765 RVA: 0x0014031A File Offset: 0x0013E51A
		public UnexpectedEndOfStreamException() : base("Input stream ended unexpectedly")
		{
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x001402FD File Offset: 0x0013E4FD
		public UnexpectedEndOfStreamException(string message) : base(message)
		{
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00140306 File Offset: 0x0013E506
		public UnexpectedEndOfStreamException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00140310 File Offset: 0x0013E510
		protected UnexpectedEndOfStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400264B RID: 9803
		private const string GenericMessage = "Input stream ended unexpectedly";
	}
}
