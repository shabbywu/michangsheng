using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Tar
{
	// Token: 0x0200080A RID: 2058
	[Serializable]
	public class TarException : SharpZipBaseException
	{
		// Token: 0x060035B0 RID: 13744 RVA: 0x0002505B File Offset: 0x0002325B
		public TarException()
		{
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0002488E File Offset: 0x00022A8E
		public TarException(string message) : base(message)
		{
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x00024897 File Offset: 0x00022A97
		public TarException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000248A1 File Offset: 0x00022AA1
		protected TarException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
