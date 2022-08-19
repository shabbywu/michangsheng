using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000521 RID: 1313
	[Serializable]
	public class StreamDecodingException : SharpZipBaseException
	{
		// Token: 0x06002A05 RID: 10757 RVA: 0x001402C6 File Offset: 0x0013E4C6
		public StreamDecodingException() : base("Input stream could not be decoded")
		{
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x001402D3 File Offset: 0x0013E4D3
		public StreamDecodingException(string message) : base(message)
		{
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x001402DC File Offset: 0x0013E4DC
		public StreamDecodingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x001402E6 File Offset: 0x0013E4E6
		protected StreamDecodingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04002649 RID: 9801
		private const string GenericMessage = "Input stream could not be decoded";
	}
}
