using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x020007B8 RID: 1976
	[Serializable]
	public class ValueOutOfRangeException : StreamDecodingException
	{
		// Token: 0x06003224 RID: 12836 RVA: 0x000248E2 File Offset: 0x00022AE2
		public ValueOutOfRangeException(string nameOfValue) : base(nameOfValue + " out of range")
		{
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x000248F5 File Offset: 0x00022AF5
		public ValueOutOfRangeException(string nameOfValue, long value, long maxValue, long minValue = 0L) : this(nameOfValue, value.ToString(), maxValue.ToString(), minValue.ToString())
		{
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x00024913 File Offset: 0x00022B13
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

		// Token: 0x06003227 RID: 12839 RVA: 0x0002494F File Offset: 0x00022B4F
		private ValueOutOfRangeException()
		{
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x000248C1 File Offset: 0x00022AC1
		private ValueOutOfRangeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000248CB File Offset: 0x00022ACB
		protected ValueOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
