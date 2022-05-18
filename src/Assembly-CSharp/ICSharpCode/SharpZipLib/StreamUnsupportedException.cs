using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x020007B6 RID: 1974
	[Serializable]
	public class StreamUnsupportedException : StreamDecodingException
	{
		// Token: 0x0600321C RID: 12828 RVA: 0x000248AB File Offset: 0x00022AAB
		public StreamUnsupportedException() : base("Input stream is in a unsupported format")
		{
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000248B8 File Offset: 0x00022AB8
		public StreamUnsupportedException(string message) : base(message)
		{
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000248C1 File Offset: 0x00022AC1
		public StreamUnsupportedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000248CB File Offset: 0x00022ACB
		protected StreamUnsupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04002E3A RID: 11834
		private const string GenericMessage = "Input stream is in a unsupported format";
	}
}
