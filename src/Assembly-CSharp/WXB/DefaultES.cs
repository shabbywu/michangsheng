using System;
using System.Collections.Generic;
using System.Text;

namespace WXB
{
	// Token: 0x0200068F RID: 1679
	internal class DefaultES : ElementSegment
	{
		// Token: 0x06003525 RID: 13605 RVA: 0x001700BC File Offset: 0x0016E2BC
		private static DefaultES.CharType GetCharType(char c)
		{
			if (c <= ';')
			{
				if (c <= ',')
				{
					if (c != ' ' && c != '"' && c != ',')
					{
						goto IL_65;
					}
				}
				else if (c != '.' && c != ':' && c != ';')
				{
					goto IL_65;
				}
			}
			else if (c <= '”')
			{
				if (c != '?' && c != '“' && c != '”')
				{
					goto IL_65;
				}
			}
			else if (c != '。' && c != '，' && c != '；')
			{
				goto IL_65;
			}
			return DefaultES.CharType.Punctuation;
			IL_65:
			if (c > '\u0080')
			{
				return DefaultES.CharType.Chinese;
			}
			return DefaultES.CharType.English;
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x0017013C File Offset: 0x0016E33C
		private static NodeBase.Element Create(StringBuilder sb, Func<char, float> fontwidth)
		{
			if (sb.Length == 1)
			{
				NodeBase.Element result = new NodeBase.Element(fontwidth(sb[0]));
				sb.Length = 0;
				return result;
			}
			List<float> list = new List<float>();
			for (int i = 0; i < sb.Length; i++)
			{
				list.Add(fontwidth(sb[i]));
			}
			NodeBase.Element result2 = new NodeBase.Element(list);
			sb.Length = 0;
			return result2;
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x001701A4 File Offset: 0x0016E3A4
		public void Segment(string text, List<NodeBase.Element> widths, Func<char, float> fontwidth)
		{
			using (PD<StringBuilder> sb = Pool.GetSB())
			{
				StringBuilder value = sb.value;
				DefaultES.CharType charType = DefaultES.CharType.Null;
				foreach (char c in text)
				{
					DefaultES.CharType charType2 = DefaultES.GetCharType(c);
					switch (charType2)
					{
					case DefaultES.CharType.English:
						switch (charType)
						{
						case DefaultES.CharType.Chinese:
							widths.Add(DefaultES.Create(value, fontwidth));
							break;
						case DefaultES.CharType.Punctuation:
							widths.Add(DefaultES.Create(value, fontwidth));
							break;
						}
						value.Append(c);
						break;
					case DefaultES.CharType.Chinese:
						switch (charType)
						{
						case DefaultES.CharType.Chinese:
							widths.Add(DefaultES.Create(value, fontwidth));
							break;
						}
						value.Append(c);
						break;
					case DefaultES.CharType.Punctuation:
						value.Append(c);
						widths.Add(DefaultES.Create(value, fontwidth));
						break;
					}
					charType = charType2;
				}
				if (value.Length != 0)
				{
					widths.Add(DefaultES.Create(value, fontwidth));
				}
			}
		}

		// Token: 0x020014FC RID: 5372
		private enum CharType
		{
			// Token: 0x04006DF6 RID: 28150
			Null,
			// Token: 0x04006DF7 RID: 28151
			English,
			// Token: 0x04006DF8 RID: 28152
			Chinese,
			// Token: 0x04006DF9 RID: 28153
			Punctuation
		}
	}
}
