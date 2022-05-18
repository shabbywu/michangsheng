using System;
using System.Text;

namespace Fungus
{
	// Token: 0x0200136B RID: 4971
	public static class StringFormatter
	{
		// Token: 0x06007897 RID: 30871 RVA: 0x002B6E04 File Offset: 0x002B5004
		public static string[] FormatEnumNames(Enum e, string firstLabel)
		{
			string[] names = Enum.GetNames(e.GetType());
			names[0] = firstLabel;
			for (int i = 0; i < names.Length; i++)
			{
				names[i] = StringFormatter.SplitCamelCase(names[i]);
			}
			return names;
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x002B6E3C File Offset: 0x002B503C
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

		// Token: 0x06007899 RID: 30873 RVA: 0x002B6EBC File Offset: 0x002B50BC
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
}
