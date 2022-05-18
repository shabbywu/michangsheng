using System;
using System.Collections.Generic;
using System.Text;

namespace WXB
{
	// Token: 0x020009A1 RID: 2465
	internal class DefaultES : ElementSegment
	{
		// Token: 0x06003EE0 RID: 16096 RVA: 0x001B83E8 File Offset: 0x001B65E8
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

		// Token: 0x06003EE1 RID: 16097 RVA: 0x001B8468 File Offset: 0x001B6668
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

		// Token: 0x06003EE2 RID: 16098 RVA: 0x001B84D0 File Offset: 0x001B66D0
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

		// Token: 0x020009A2 RID: 2466
		private enum CharType
		{
			// Token: 0x0400389F RID: 14495
			Null,
			// Token: 0x040038A0 RID: 14496
			English,
			// Token: 0x040038A1 RID: 14497
			Chinese,
			// Token: 0x040038A2 RID: 14498
			Punctuation
		}
	}
}
