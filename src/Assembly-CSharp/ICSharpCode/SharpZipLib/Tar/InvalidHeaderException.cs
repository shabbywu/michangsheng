using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200055F RID: 1375
	[Serializable]
	public class InvalidHeaderException : TarException
	{
		// Token: 0x06002CCF RID: 11471 RVA: 0x0014C89A File Offset: 0x0014AA9A
		public InvalidHeaderException()
		{
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0014C8A2 File Offset: 0x0014AAA2
		public InvalidHeaderException(string message) : base(message)
		{
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x0014C8AB File Offset: 0x0014AAAB
		public InvalidHeaderException(string message, Exception exception) : base(message, exception)
		{
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x0014C8B5 File Offset: 0x0014AAB5
		protected InvalidHeaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
