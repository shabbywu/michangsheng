using System;

namespace Fungus
{
	// Token: 0x02000EF4 RID: 3828
	public static class CSVSupport
	{
		// Token: 0x06006BA9 RID: 27561 RVA: 0x00296F6C File Offset: 0x0029516C
		public static string Escape(string s)
		{
			s = s.Replace("\n", "\\n");
			if (s.Contains("\""))
			{
				s = s.Replace("\"", "\"\"");
			}
			if (s.IndexOfAny(CSVSupport.CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
			{
				s = "\"" + s + "\"";
			}
			return s;
		}

		// Token: 0x06006BAA RID: 27562 RVA: 0x00296FCC File Offset: 0x002951CC
		public static string Unescape(string s)
		{
			s = s.Replace("\\n", "\n");
			if (s.StartsWith("\"") && s.EndsWith("\""))
			{
				s = s.Substring(1, s.Length - 2);
				if (s.Contains("\"\""))
				{
					s = s.Replace("\"\"", "\"");
				}
			}
			return s;
		}

		// Token: 0x04005AA6 RID: 23206
		private const string QUOTE = "\"";

		// Token: 0x04005AA7 RID: 23207
		private const string ESCAPED_QUOTE = "\"\"";

		// Token: 0x04005AA8 RID: 23208
		private static char[] CHARACTERS_THAT_MUST_BE_QUOTED = new char[]
		{
			',',
			'"',
			'\n'
		};
	}
}
