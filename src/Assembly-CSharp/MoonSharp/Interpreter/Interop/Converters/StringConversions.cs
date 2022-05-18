using System;
using System.Text;

namespace MoonSharp.Interpreter.Interop.Converters
{
	// Token: 0x02001145 RID: 4421
	internal static class StringConversions
	{
		// Token: 0x06006B44 RID: 27460 RVA: 0x000491B8 File Offset: 0x000473B8
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

		// Token: 0x06006B45 RID: 27461 RVA: 0x000491F7 File Offset: 0x000473F7
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

		// Token: 0x02001146 RID: 4422
		internal enum StringSubtype
		{
			// Token: 0x040060F6 RID: 24822
			None,
			// Token: 0x040060F7 RID: 24823
			String,
			// Token: 0x040060F8 RID: 24824
			StringBuilder,
			// Token: 0x040060F9 RID: 24825
			Char
		}
	}
}
