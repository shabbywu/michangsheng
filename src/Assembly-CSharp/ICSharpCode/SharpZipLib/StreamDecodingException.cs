using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x020007B5 RID: 1973
	[Serializable]
	public class StreamDecodingException : SharpZipBaseException
	{
		// Token: 0x06003218 RID: 12824 RVA: 0x00024881 File Offset: 0x00022A81
		public StreamDecodingException() : base("Input stream could not be decoded")
		{
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x0002488E File Offset: 0x00022A8E
		public StreamDecodingException(string message) : base(message)
		{
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x00024897 File Offset: 0x00022A97
		public StreamDecodingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000248A1 File Offset: 0x00022AA1
		protected StreamDecodingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04002E39 RID: 11833
		private const string GenericMessage = "Input stream could not be decoded";
	}
}
