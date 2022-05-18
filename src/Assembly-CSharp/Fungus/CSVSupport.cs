using System;

namespace Fungus
{
	// Token: 0x02001395 RID: 5013
	public static class CSVSupport
	{
		// Token: 0x06007948 RID: 31048 RVA: 0x002B84F0 File Offset: 0x002B66F0
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

		// Token: 0x06007949 RID: 31049 RVA: 0x002B8550 File Offset: 0x002B6750
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

		// Token: 0x0400690F RID: 26895
		private const string QUOTE = "\"";

		// Token: 0x04006910 RID: 26896
		private const string ESCAPED_QUOTE = "\"\"";

		// Token: 0x04006911 RID: 26897
		private static char[] CHARACTERS_THAT_MUST_BE_QUOTED = new char[]
		{
			',',
			'"',
			'\n'
		};
	}
}
