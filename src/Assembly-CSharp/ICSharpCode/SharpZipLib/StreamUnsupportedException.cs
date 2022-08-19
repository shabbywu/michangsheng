using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000522 RID: 1314
	[Serializable]
	public class StreamUnsupportedException : StreamDecodingException
	{
		// Token: 0x06002A09 RID: 10761 RVA: 0x001402F0 File Offset: 0x0013E4F0
		public StreamUnsupportedException() : base("Input stream is in a unsupported format")
		{
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x001402FD File Offset: 0x0013E4FD
		public StreamUnsupportedException(string message) : base(message)
		{
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00140306 File Offset: 0x0013E506
		public StreamUnsupportedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00140310 File Offset: 0x0013E510
		protected StreamUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400264A RID: 9802
		private const string GenericMessage = "Input stream is in a unsupported format";
	}
}
