using System;
using System.Text;

namespace Fungus
{
	// Token: 0x02000ECC RID: 3788
	public static class StringFormatter
	{
		// Token: 0x06006AFA RID: 27386 RVA: 0x00294B38 File Offset: 0x00292D38
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

		// Token: 0x06006AFB RID: 27387 RVA: 0x00294B70 File Offset: 0x00292D70
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

		// Token: 0x06006AFC RID: 27388 RVA: 0x00294BF0 File Offset: 0x00292DF0
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
