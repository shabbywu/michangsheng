using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fungus
{
	// Token: 0x02000ED1 RID: 3793
	public static class TextVariationHandler
	{
		// Token: 0x06006B0D RID: 27405 RVA: 0x00295674 File Offset: 0x00293874
		public static void ClearHistory()
		{
			TextVariationHandler.hashedSections.Clear();
		}

		// Token: 0x06006B0E RID: 27406 RVA: 0x00295680 File Offset: 0x00293880
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

		// Token: 0x06006B0F RID: 27407 RVA: 0x002957B8 File Offset: 0x002939B8
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

		// Token: 0x04005A62 RID: 23138
		private static Dictionary<int, int> hashedSections = new Dictionary<int, int>();

		// Token: 0x02001713 RID: 5907
		public class Section
		{
			// Token: 0x06008907 RID: 35079 RVA: 0x002E9F4C File Offset: 0x002E814C
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

			// Token: 0x040074BC RID: 29884
			public TextVariationHandler.Section.VaryType type;

			// Token: 0x040074BD RID: 29885
			public string entire = string.Empty;

			// Token: 0x040074BE RID: 29886
			public List<string> elements = new List<string>();

			// Token: 0x02001760 RID: 5984
			public enum VaryType
			{
				// Token: 0x040075AF RID: 30127
				Sequence,
				// Token: 0x040075B0 RID: 30128
				Cycle,
				// Token: 0x040075B1 RID: 30129
				Once,
				// Token: 0x040075B2 RID: 30130
				Random
			}
		}
	}
}
