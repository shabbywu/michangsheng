using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x020007B7 RID: 1975
	[Serializable]
	public class UnexpectedEndOfStreamException : StreamDecodingException
	{
		// Token: 0x06003220 RID: 12832 RVA: 0x000248D5 File Offset: 0x00022AD5
		public UnexpectedEndOfStreamException() : base("Input stream ended unexpectedly")
		{
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x000248B8 File Offset: 0x00022AB8
		public UnexpectedEndOfStreamException(string message) : base(message)
		{
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x000248C1 File Offset: 0x00022AC1
		public UnexpectedEndOfStreamException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000248CB File Offset: 0x00022ACB
		protected UnexpectedEndOfStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04002E3B RID: 11835
		private const string GenericMessage = "Input stream ended unexpectedly";
	}
}
