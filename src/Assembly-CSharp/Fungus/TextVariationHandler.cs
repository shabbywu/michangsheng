using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02001370 RID: 4976
	public static class TextVariationHandler
	{
		// Token: 0x060078AA RID: 30890 RVA: 0x00051ED3 File Offset: 0x000500D3
		public static void ClearHistory()
		{
			TextVariationHandler.hashedSections.Clear();
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x002B7920 File Offset: 0x002B5B20
		public static bool TokenizeVarySections(string input, List<TextVariationHandler.Section> varyingSections)
		{
			varyingSections.Clear();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			TextVariationHandler.Section section = null;
			for (int i = 0; i < input.Length; i++)
			{
				char c = input[i];
				if (c != '[')
				{
					if (c != ']')
					{
						if (c == '|')
						{
							if (num == 1)
							{
								section.elements.Add(input.Substring(num3, i - num3));
								num3 = i + 1;
							}
						}
					}
					else
					{
						if (num == 1)
						{
							section.entire = input.Substring(num2, i - num2 + 1);
							if (num2 != num3 - 1)
							{
								section.elements.Add(input.Substring(num3, i - num3));
							}
							if (section.type != TextVariationHandler.Section.VaryType.Sequence)
							{
								section.elements[0] = section.elements[0].Substring(1);
							}
						}
						num--;
					}
				}
				else
				{
					if (num == 0)
					{
						section = new TextVariationHandler.Section();
						varyingSections.Add(section);
						char c2 = input[i + 1];
						if (c2 != '!')
						{
							if (c2 != '&')
							{
								if (c2 == '~')
								{
									section.type = TextVariationHandler.Section.VaryType.Random;
								}
							}
							else
							{
								section.type = TextVariationHandler.Section.VaryType.Cycle;
							}
						}
						else
						{
							section.type = TextVariationHandler.Section.VaryType.Once;
						}
						num2 = i;
						num3 = i + 1;
					}
					num++;
				}
			}
			return varyingSections.Count > 0;
		}

		// Token: 0x060078AC RID: 30892 RVA: 0x002B7A58 File Offset: 0x002B5C58
		public static string SelectVariations(string input, int parentHash = 0)
		{
			List<TextVariationHandler.Section> list = new List<TextVariationHandler.Section>();
			if (!TextVariationHandler.TokenizeVarySections(input, list))
			{
				return input;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Length = 0;
			stringBuilder.Append(input);
			for (int i = 0; i < list.Count; i++)
			{
				TextVariationHandler.Section section = list[i];
				string text = string.Empty;
				int num = -1;
				int hashCode = input.GetHashCode();
				int num2 = hashCode ^ hashCode << 13;
				int num3 = section.entire.GetHashCode();
				num3 ^= num3 >> 17;
				int num4 = num2 ^ num3 ^ parentHash;
				int num5 = 0;
				if (TextVariationHandler.hashedSections.TryGetValue(num4, out num5))
				{
					num = num5;
				}
				num++;
				text = section.Select(ref num);
				TextVariationHandler.hashedSections[num4] = num;
				text = TextVariationHandler.SelectVariations(text, num4);
				stringBuilder.Replace(section.entire, text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040068C3 RID: 26819
		private static Dictionary<int, int> hashedSections = new Dictionary<int, int>();

		// Token: 0x02001371 RID: 4977
		public class Section
		{
			// Token: 0x060078AE RID: 30894 RVA: 0x002B7B34 File Offset: 0x002B5D34
			public string Select(ref int index)
			{
				switch (this.type)
				{
				case TextVariationHandler.Section.VaryType.Sequence:
					index = Mathf.Min(index, this.elements.Count - 1);
					break;
				case TextVariationHandler.Section.VaryType.Cycle:
					index %= this.elements.Count;
					break;
				case TextVariationHandler.Section.VaryType.Once:
					index = Mathf.Min(index, this.elements.Count);
					break;
				case TextVariationHandler.Section.VaryType.Random:
					index = Random.Range(0, this.elements.Count);
					break;
				}
				if (index >= 0 && index < this.elements.Count)
				{
					return this.elements[index];
				}
				return string.Empty;
			}

			// Token: 0x040068C4 RID: 26820
			public TextVariationHandler.Section.VaryType type;

			// Token: 0x040068C5 RID: 26821
			public string entire = string.Empty;

			// Token: 0x040068C6 RID: 26822
			public List<string> elements = new List<string>();

			// Token: 0x02001372 RID: 4978
			public enum VaryType
			{
				// Token: 0x040068C8 RID: 26824
				Sequence,
				// Token: 0x040068C9 RID: 26825
				Cycle,
				// Token: 0x040068CA RID: 26826
				Once,
				// Token: 0x040068CB RID: 26827
				Random
			}
		}
	}
}
