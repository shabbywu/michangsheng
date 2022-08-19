using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x02000564 RID: 1380
	[Serializable]
	public class TarException : SharpZipBaseException
	{
		// Token: 0x06002D42 RID: 11586 RVA: 0x001421C7 File Offset: 0x001403C7
		public TarException()
		{
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x001402D3 File Offset: 0x0013E4D3
		public TarException(string message) : base(message)
		{
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x001402DC File Offset: 0x0013E4DC
		public TarException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x001402E6 File Offset: 0x0013E4E6
		protected TarException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
