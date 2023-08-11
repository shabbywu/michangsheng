namespace Fungus;

public static class CSVSupport
{
	private const string QUOTE = "\"";

	private const string ESCAPED_QUOTE = "\"\"";

	private static char[] CHARACTERS_THAT_MUST_BE_QUOTED = new char[3] { ',', '"', '\n' };

	public static string Escape(string s)
	{
		s = s.Replace("\n", "\\n");
		if (s.Contains("\""))
		{
			s = s.Replace("\"", "\"\"");
		}
		if (s.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
		{
			s = "\"" + s + "\"";
		}
		return s;
	}

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
}
