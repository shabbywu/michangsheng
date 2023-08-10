using System;
using System.Collections.Generic;
using System.Text;

namespace WXB;

internal class DefaultES : ElementSegment
{
	private enum CharType
	{
		Null,
		English,
		Chinese,
		Punctuation
	}

	private static CharType GetCharType(char c)
	{
		switch (c)
		{
		case ' ':
		case '"':
		case ',':
		case '.':
		case ':':
		case ';':
		case '?':
		case '“':
		case '”':
		case '。':
		case '，':
		case '；':
			return CharType.Punctuation;
		default:
			if (c > '\u0080')
			{
				return CharType.Chinese;
			}
			return CharType.English;
		}
	}

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

	public void Segment(string text, List<NodeBase.Element> widths, Func<char, float> fontwidth)
	{
		PD<StringBuilder> sB = Pool.GetSB();
		try
		{
			StringBuilder value = sB.value;
			CharType charType = CharType.Null;
			foreach (char c in text)
			{
				CharType charType2 = GetCharType(c);
				switch (charType2)
				{
				case CharType.English:
					switch (charType)
					{
					case CharType.Chinese:
						widths.Add(Create(value, fontwidth));
						break;
					case CharType.Punctuation:
						widths.Add(Create(value, fontwidth));
						break;
					}
					value.Append(c);
					break;
				case CharType.Chinese:
					switch (charType)
					{
					case CharType.Chinese:
						widths.Add(Create(value, fontwidth));
						break;
					}
					value.Append(c);
					break;
				case CharType.Punctuation:
					value.Append(c);
					widths.Add(Create(value, fontwidth));
					break;
				}
				charType = charType2;
			}
			if (value.Length != 0)
			{
				widths.Add(Create(value, fontwidth));
			}
		}
		finally
		{
			((IDisposable)sB).Dispose();
		}
	}
}
