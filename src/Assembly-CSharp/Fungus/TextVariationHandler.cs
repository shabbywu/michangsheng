using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fungus;

public static class TextVariationHandler
{
	public class Section
	{
		public enum VaryType
		{
			Sequence,
			Cycle,
			Once,
			Random
		}

		public VaryType type;

		public string entire = string.Empty;

		public List<string> elements = new List<string>();

		public string Select(ref int index)
		{
			switch (type)
			{
			case VaryType.Sequence:
				index = Mathf.Min(index, elements.Count - 1);
				break;
			case VaryType.Cycle:
				index %= elements.Count;
				break;
			case VaryType.Once:
				index = Mathf.Min(index, elements.Count);
				break;
			case VaryType.Random:
				index = Random.Range(0, elements.Count);
				break;
			}
			if (index >= 0 && index < elements.Count)
			{
				return elements[index];
			}
			return string.Empty;
		}
	}

	private static Dictionary<int, int> hashedSections = new Dictionary<int, int>();

	public static void ClearHistory()
	{
		hashedSections.Clear();
	}

	public static bool TokenizeVarySections(string input, List<Section> varyingSections)
	{
		varyingSections.Clear();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		Section section = null;
		for (int i = 0; i < input.Length; i++)
		{
			switch (input[i])
			{
			case '[':
				if (num == 0)
				{
					section = new Section();
					varyingSections.Add(section);
					switch (input[i + 1])
					{
					case '~':
						section.type = Section.VaryType.Random;
						break;
					case '&':
						section.type = Section.VaryType.Cycle;
						break;
					case '!':
						section.type = Section.VaryType.Once;
						break;
					}
					num2 = i;
					num3 = i + 1;
				}
				num++;
				break;
			case ']':
				if (num == 1)
				{
					section.entire = input.Substring(num2, i - num2 + 1);
					if (num2 != num3 - 1)
					{
						section.elements.Add(input.Substring(num3, i - num3));
					}
					if (section.type != 0)
					{
						section.elements[0] = section.elements[0].Substring(1);
					}
				}
				num--;
				break;
			case '|':
				if (num == 1)
				{
					section.elements.Add(input.Substring(num3, i - num3));
					num3 = i + 1;
				}
				break;
			}
		}
		return varyingSections.Count > 0;
	}

	public static string SelectVariations(string input, int parentHash = 0)
	{
		List<Section> list = new List<Section>();
		if (!TokenizeVarySections(input, list))
		{
			return input;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Length = 0;
		stringBuilder.Append(input);
		for (int i = 0; i < list.Count; i++)
		{
			Section section = list[i];
			string empty = string.Empty;
			int num = -1;
			int hashCode = input.GetHashCode();
			int num2 = hashCode ^ (hashCode << 13);
			int hashCode2 = section.entire.GetHashCode();
			hashCode2 ^= hashCode2 >> 17;
			int num3 = num2 ^ hashCode2 ^ parentHash;
			int value = 0;
			if (hashedSections.TryGetValue(num3, out value))
			{
				num = value;
			}
			num++;
			empty = section.Select(ref num);
			hashedSections[num3] = num;
			empty = SelectVariations(empty, num3);
			stringBuilder.Replace(section.entire, empty);
		}
		return stringBuilder.ToString();
	}
}
