using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000524 RID: 1316
	[Serializable]
	public class ValueOutOfRangeException : StreamDecodingException
	{
		// Token: 0x06002A11 RID: 10769 RVA: 0x00140327 File Offset: 0x0013E527
		public ValueOutOfRangeException(string nameOfValue) : base(nameOfValue + " out of range")
		{
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x0014033A File Offset: 0x0013E53A
		public ValueOutOfRangeException(string nameOfValue, long value, long maxValue, long minValue = 0L) : this(nameOfValue, value.ToString(), maxValue.ToString(), minValue.ToString())
		{
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00140358 File Offset: 0x0013E558
		public ValueOutOfRangeException(string nameOfValue, string value, string maxValue, string minValue = "0") : base(string.Concat(new string[]
		{
			nameOfValue,
			" out of range: ",
			value,
			", should be ",
			minValue,
			"..",
			maxValue
		}))
		{
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x00140394 File Offset: 0x0013E594
		private ValueOutOfRangeException()
		{
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x00140306 File Offset: 0x0013E506
		private ValueOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x00140310 File Offset: 0x0013E510
		protected ValueOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
