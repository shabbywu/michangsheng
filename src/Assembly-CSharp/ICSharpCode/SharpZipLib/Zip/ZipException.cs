using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000532 RID: 1330
	[Serializable]
	public class ZipException : SharpZipBaseException
	{
		// Token: 0x06002ABA RID: 10938 RVA: 0x001421C7 File Offset: 0x001403C7
		public ZipException()
		{
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x001402D3 File Offset: 0x0013E4D3
		public ZipException(string message) : base(message)
		{
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x001402DC File Offset: 0x0013E4DC
		public ZipException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x001402E6 File Offset: 0x0013E4E6
		protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
