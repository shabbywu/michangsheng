using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x0200082C RID: 2092
	[Serializable]
	public class InvalidNameException : SharpZipBaseException
	{
		// Token: 0x060036D0 RID: 14032 RVA: 0x00027DD7 File Offset: 0x00025FD7
		public InvalidNameException() : base("An invalid name was specified")
		{
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x0002488E File Offset: 0x00022A8E
		public InvalidNameException(string message) : base(message)
		{
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x00024897 File Offset: 0x00022A97
		public InvalidNameException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000248A1 File Offset: 0x00022AA1
		protected InvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
