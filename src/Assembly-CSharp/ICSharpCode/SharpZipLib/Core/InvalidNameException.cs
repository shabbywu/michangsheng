using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x02000583 RID: 1411
	[Serializable]
	public class InvalidNameException : SharpZipBaseException
	{
		// Token: 0x06002E5A RID: 11866 RVA: 0x00151578 File Offset: 0x0014F778
		public InvalidNameException() : base("An invalid name was specified")
		{
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x001402D3 File Offset: 0x0013E4D3
		public InvalidNameException(string message) : base(message)
		{
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x001402DC File Offset: 0x0013E4DC
		public InvalidNameException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x001402E6 File Offset: 0x0013E4E6
		protected InvalidNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
