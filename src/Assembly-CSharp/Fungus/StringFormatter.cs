using System;
using System.Text;

namespace Fungus;

public static class StringFormatter
{
	public static string[] FormatEnumNames(Enum e, string firstLabel)
	{
		string[] names = Enum.GetNames(e.GetType());
		names[0] = firstLabel;
		for (int i = 0; i < names.Length; i++)
		{
			names[i] = SplitCamelCase(names[i]);
		}
		return names;
	}

	public static string SplitCamelCase(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(text.Length * 2);
		stringBuilder.Append(text[0]);
		for (int i = 1; i < text.Length; i++)
		{
			if (char.IsUpper(text[i]) && text[i - 1] != ' ')
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.Append(text[i]);
		}
		return stringBuilder.ToString();
	}

	public static bool IsNullOrWhiteSpace(string value)
	{
		if (value != null)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsWhiteSpace(value[i]))
				{
					return false;
				}
			}
		}
		return true;
	}
}
