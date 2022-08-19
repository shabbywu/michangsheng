using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02000D3D RID: 3389
	internal static class StringConversions
	{
		// Token: 0x06005F72 RID: 24434 RVA: 0x0026B6D6 File Offset: 0x002698D6
		internal static StringConversions.StringSubtype GetStringSubtype(Type desiredType)
		{
			if (desiredType == typeof(string))
			{
				return StringConversions.StringSubtype.String;
			}
			if (desiredType == typeof(StringBuilder))
			{
				return StringConversions.StringSubtype.StringBuilder;
			}
			if (desiredType == typeof(char))
			{
				return StringConversions.StringSubtype.Char;
			}
			return StringConversions.StringSubtype.None;
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x0026B715 File Offset: 0x00269915
		internal static object ConvertString(StringConversions.StringSubtype stringSubType, string str, Type desiredType, DataType dataType)
		{
			switch (stringSubType)
			{
			case StringConversions.StringSubtype.String:
				return str;
			case StringConversions.StringSubtype.StringBuilder:
				return new StringBuilder(str);
			case StringConversions.StringSubtype.Char:
				if (!string.IsNullOrEmpty(str))
				{
					return str[0];
				}
				break;
			}
			throw ScriptRuntimeException.ConvertObjectFailed(dataType, desiredType);
		}

		// Token: 0x0200167D RID: 5757
		internal enum StringSubtype
		{
			// Token: 0x040072C0 RID: 29376
			None,
			// Token: 0x040072C1 RID: 29377
			String,
			// Token: 0x040072C2 RID: 29378
			StringBuilder,
			// Token: 0x040072C3 RID: 29379
			Char
		}
	}
}
