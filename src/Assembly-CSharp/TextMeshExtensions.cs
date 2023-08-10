using System.Text.RegularExpressions;
using UnityEngine;

internal static class TextMeshExtensions
{
	public static bool outLine;

	public static bool shadow;

	public static bool singleOutline;

	public static void AdjustFontSize(this TextMesh toAdjust, bool rowSplit, bool hasWhiteSpaces, bool increaseFont)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		if (!((Object)(object)((Component)toAdjust).GetComponent<Collider>() != (Object)null))
		{
			return;
		}
		Bounds bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
		if (((Bounds)(ref bounds)).size.x == 0f)
		{
			return;
		}
		bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
		if (((Bounds)(ref bounds)).size.y == 0f)
		{
			return;
		}
		string text = toAdjust.text;
		if (increaseFont)
		{
			while (true)
			{
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float num = ((Bounds)(ref bounds)).size.x * 1.1f;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (!(num < ((Bounds)(ref bounds)).size.x))
				{
					break;
				}
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float num2 = ((Bounds)(ref bounds)).size.y * 1.1f;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (num2 < ((Bounds)(ref bounds)).size.y)
				{
					toAdjust.characterSize *= 1.1f;
					continue;
				}
				break;
			}
		}
		else
		{
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float x = ((Bounds)(ref bounds)).size.x;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (x < ((Bounds)(ref bounds)).size.x)
			{
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float y = ((Bounds)(ref bounds)).size.y;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (y < ((Bounds)(ref bounds)).size.y)
				{
					return;
				}
			}
		}
		if (!rowSplit)
		{
			while (true)
			{
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float x2 = ((Bounds)(ref bounds)).size.x;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (!(x2 > ((Bounds)(ref bounds)).size.x))
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float y2 = ((Bounds)(ref bounds)).size.y;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if (!(y2 > ((Bounds)(ref bounds)).size.y))
					{
						break;
					}
				}
				toAdjust.characterSize *= 0.9f;
			}
			return;
		}
		string[] array = text.Split(new char[1] { ' ' });
		if (!hasWhiteSpaces)
		{
			char[] array2 = text.ToCharArray();
			array = new string[array2.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array2[i].ToString();
			}
		}
		if (array.Length == 1)
		{
			while (true)
			{
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float x3 = ((Bounds)(ref bounds)).size.x;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (!(x3 > ((Bounds)(ref bounds)).size.x))
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float y3 = ((Bounds)(ref bounds)).size.y;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if (!(y3 > ((Bounds)(ref bounds)).size.y))
					{
						break;
					}
				}
				toAdjust.characterSize *= 0.9f;
			}
			return;
		}
		if (array.Length == 1)
		{
			while (true)
			{
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float x4 = ((Bounds)(ref bounds)).size.x;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (x4 > ((Bounds)(ref bounds)).size.x)
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float y4 = ((Bounds)(ref bounds)).size.y;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if (y4 > ((Bounds)(ref bounds)).size.y)
					{
						toAdjust.characterSize *= 0.9f;
						continue;
					}
					break;
				}
				break;
			}
			return;
		}
		bool flag = false;
		toAdjust.text = array[0];
		int num3 = 1;
		while (true)
		{
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float num4 = ((Bounds)(ref bounds)).size.x * 1.1f;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (!(num4 < ((Bounds)(ref bounds)).size.x))
			{
				break;
			}
			bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float num5 = ((Bounds)(ref bounds)).size.y * 1.1f;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (!(num5 < ((Bounds)(ref bounds)).size.y))
			{
				break;
			}
			toAdjust.characterSize *= 1.1f;
		}
		int count = Regex.Matches(toAdjust.text, "\n").Count;
		while (num3 < array.Length)
		{
			count = Regex.Matches(toAdjust.text, "\n").Count;
			if (!flag)
			{
				toAdjust.text = toAdjust.text + "\n" + array[num3++];
			}
			else
			{
				string text2 = toAdjust.text;
				toAdjust.text = toAdjust.text + (hasWhiteSpaces ? " " : "") + array[num3++];
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float y5 = ((Bounds)(ref bounds)).size.y;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (!(y5 > ((Bounds)(ref bounds)).size.y))
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float x5 = ((Bounds)(ref bounds)).size.x;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if (!(x5 > ((Bounds)(ref bounds)).size.x))
					{
						goto IL_058a;
					}
				}
				toAdjust.text = text2 + "\n" + array[num3 - 1];
			}
			goto IL_058a;
			IL_058a:
			while (true)
			{
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float y6 = ((Bounds)(ref bounds)).size.y;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (!(y6 > ((Bounds)(ref bounds)).size.y))
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float x6 = ((Bounds)(ref bounds)).size.x;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if (!(x6 > ((Bounds)(ref bounds)).size.x))
					{
						break;
					}
				}
				string text3 = toAdjust.text;
				toAdjust.text = ReplaceNextOccurence(toAdjust.text, "\n", hasWhiteSpaces ? " " : "", count++);
				bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
				float x7 = ((Bounds)(ref bounds)).size.x;
				bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
				if (x7 < ((Bounds)(ref bounds)).size.x)
				{
					bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
					float y7 = ((Bounds)(ref bounds)).size.y;
					bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
					if (y7 < ((Bounds)(ref bounds)).size.y)
					{
						flag = true;
						break;
					}
				}
				count = Regex.Matches(toAdjust.text, "\n").Count;
				toAdjust.characterSize *= 0.96f;
				toAdjust.text = text3;
			}
		}
	}

	public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
	{
		int startIndex = Source.LastIndexOf(Find);
		return Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace);
	}

	public static int IndexOfOccurence(this string s, string match, int occurence)
	{
		int i = 1;
		int num = 0;
		for (; i <= occurence; i++)
		{
			if ((num = s.IndexOf(match, num + 1)) == -1)
			{
				break;
			}
			if (i == occurence)
			{
				return num;
			}
		}
		return -1;
	}

	public static string ReplaceNextOccurence(string Source, string Find, string Replace, int n)
	{
		int num = Source.IndexOfOccurence(Find, n);
		string result = Source;
		if (num != -1)
		{
			result = Source.Remove(num, Find.Length).Insert(num, Replace);
		}
		return result;
	}

	public static void SetEffectsTexts(this TextMesh toAdjust, bool otherChildren)
	{
		if (outLine)
		{
			if ((Object)(object)toAdjust != (Object)null && ((Component)toAdjust).transform.childCount == 8)
			{
				if (otherChildren)
				{
					((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "RightOutline")).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "LeftOutline")).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "UpOutline")).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "DownOutline")).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "LeftUpOutline")).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "RightUpOutline")).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "LeftDownOutline")).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "RightDownOutline")).GetComponent<TextMesh>().text = toAdjust.text;
				}
				else
				{
					((Component)((Component)toAdjust).transform.GetChild(0)).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.GetChild(1)).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.GetChild(2)).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.GetChild(3)).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.GetChild(4)).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.GetChild(5)).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.GetChild(6)).GetComponent<TextMesh>().text = toAdjust.text;
					((Component)((Component)toAdjust).transform.GetChild(7)).GetComponent<TextMesh>().text = toAdjust.text;
				}
			}
		}
		else if (!shadow && singleOutline)
		{
			if (otherChildren)
			{
				((Component)((Component)toAdjust).transform.Find(((Object)toAdjust).name + "Outline")).GetComponent<TextMesh>().text = toAdjust.text;
			}
			else
			{
				((Component)((Component)toAdjust).transform.GetChild(0)).GetComponent<TextMesh>().text = toAdjust.text;
			}
		}
	}

	public static void AddSingleOutline(this TextMesh toAdjust, Color shadowColor, float offset)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)toAdjust).transform.childCount == 0)
		{
			Object.DestroyImmediate((Object)(object)((Component)toAdjust).GetComponent<TextMeshPlugin>());
			GameObject obj = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			GameObject val = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			GameObject val2 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			GameObject val3 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			((Object)obj).name = ((Object)toAdjust).name + "Outline";
			TextMesh component = obj.GetComponent<TextMesh>();
			component.color = shadowColor;
			component.characterSize *= offset;
			component.offsetZ = toAdjust.offsetZ + 0.1f;
			obj.GetComponent<Renderer>().enabled = true;
			obj.transform.parent = ((Component)toAdjust).transform;
			obj.transform.localRotation = Quaternion.identity;
			obj.transform.localPosition = new Vector3(0f, 0f, 0f);
			obj.transform.localScale = Vector3.one;
			((Object)val).name = ((Object)toAdjust).name + "Outline2";
			TextMesh component2 = val.GetComponent<TextMesh>();
			component2.color = shadowColor;
			component2.characterSize /= offset;
			component2.offsetZ = toAdjust.offsetZ + 0.1f;
			val.GetComponent<Renderer>().enabled = true;
			val.transform.parent = ((Component)toAdjust).transform;
			val.transform.localRotation = Quaternion.identity;
			val.transform.localPosition = new Vector3(0f, 0f, 0f);
			val.transform.localScale = Vector3.one;
			((Object)val2).name = ((Object)toAdjust).name + "Outline3";
			TextMesh component3 = val2.GetComponent<TextMesh>();
			component3.color = shadowColor;
			component3.characterSize /= offset + 0.01f;
			component3.offsetZ = toAdjust.offsetZ + 0.1f;
			val2.GetComponent<Renderer>().enabled = true;
			val2.transform.parent = ((Component)toAdjust).transform;
			val2.transform.localRotation = Quaternion.identity;
			val2.transform.localPosition = new Vector3(-0.017f, 0f, 0f);
			val2.transform.localScale = Vector3.one;
			((Object)val3).name = ((Object)toAdjust).name + "Outline4";
			TextMesh component4 = val3.GetComponent<TextMesh>();
			component4.color = shadowColor;
			component4.offsetZ = toAdjust.offsetZ + 0.1f;
			val3.GetComponent<Renderer>().enabled = true;
			val3.transform.parent = ((Component)toAdjust).transform;
			val3.transform.localRotation = Quaternion.identity;
			val3.transform.localPosition = new Vector3(0.04f, 0f, 0f);
			val3.transform.localScale = Vector3.one;
			singleOutline = true;
		}
	}

	public static void AddShadow(this TextMesh toAdjust, Color shadowColor, Vector2 offset)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)toAdjust).transform.childCount == 0)
		{
			Object.DestroyImmediate((Object)(object)((Component)toAdjust).GetComponent<TextMeshPlugin>());
			GameObject obj = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			((Object)obj).name = ((Object)toAdjust).name + "Shadow";
			TextMesh component = obj.GetComponent<TextMesh>();
			component.color = shadowColor;
			component.offsetZ = toAdjust.offsetZ + 0.1f;
			obj.GetComponent<Renderer>().enabled = true;
			obj.transform.parent = ((Component)toAdjust).transform;
			obj.transform.localRotation = Quaternion.identity;
			obj.transform.localPosition = new Vector3(offset.x, offset.y, 0f);
			obj.transform.localScale = Vector3.one;
			shadow = true;
		}
	}

	public static void AddOutline(this TextMesh toAdjust, Color outlineColor, float offset)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		if (((Component)toAdjust).transform.childCount != 8)
		{
			Object.DestroyImmediate((Object)(object)((Component)toAdjust).GetComponent<TextMeshPlugin>());
			GameObject val = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			if ((Object)(object)val.GetComponent<Collider>() != (Object)null)
			{
				Object.Destroy((Object)(object)val.GetComponent<Collider>());
			}
			((Object)val).name = ((Object)toAdjust).name + "RightOutline";
			TextMesh component = val.GetComponent<TextMesh>();
			component.color = outlineColor;
			component.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject val2 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			if ((Object)(object)val2.GetComponent<Collider>() != (Object)null)
			{
				Object.Destroy((Object)(object)val2.GetComponent<Collider>());
			}
			((Object)val2).name = ((Object)toAdjust).name + "LeftOutline";
			TextMesh component2 = val2.GetComponent<TextMesh>();
			component2.color = outlineColor;
			component2.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject val3 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			if ((Object)(object)val3.GetComponent<Collider>() != (Object)null)
			{
				Object.Destroy((Object)(object)val3.GetComponent<Collider>());
			}
			((Object)val3).name = ((Object)toAdjust).name + "UpOutline";
			TextMesh component3 = val3.GetComponent<TextMesh>();
			component3.color = outlineColor;
			component3.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject val4 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			if ((Object)(object)val4.GetComponent<Collider>() != (Object)null)
			{
				Object.Destroy((Object)(object)val4.GetComponent<Collider>());
			}
			((Object)val4).name = ((Object)toAdjust).name + "DownOutline";
			TextMesh component4 = val4.GetComponent<TextMesh>();
			component4.color = outlineColor;
			component4.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject val5 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			if ((Object)(object)val5.GetComponent<Collider>() != (Object)null)
			{
				Object.Destroy((Object)(object)val5.GetComponent<Collider>());
			}
			((Object)val5).name = ((Object)toAdjust).name + "LeftUpOutline";
			TextMesh component5 = val5.GetComponent<TextMesh>();
			component5.color = outlineColor;
			component5.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject val6 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			if ((Object)(object)val6.GetComponent<Collider>() != (Object)null)
			{
				Object.Destroy((Object)(object)val6.GetComponent<Collider>());
			}
			((Object)val6).name = ((Object)toAdjust).name + "RightUpOutline";
			TextMesh component6 = val6.GetComponent<TextMesh>();
			component6.color = outlineColor;
			component6.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject val7 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			if ((Object)(object)val7.GetComponent<Collider>() != (Object)null)
			{
				Object.Destroy((Object)(object)val7.GetComponent<Collider>());
			}
			((Object)val7).name = ((Object)toAdjust).name + "LeftDownOutline";
			TextMesh component7 = val7.GetComponent<TextMesh>();
			component7.color = outlineColor;
			component7.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject val8 = Object.Instantiate<GameObject>(((Component)toAdjust).gameObject);
			if ((Object)(object)val8.GetComponent<Collider>() != (Object)null)
			{
				Object.Destroy((Object)(object)val8.GetComponent<Collider>());
			}
			((Object)val8).name = ((Object)toAdjust).name + "RightDownOutline";
			TextMesh component8 = val8.GetComponent<TextMesh>();
			component8.color = outlineColor;
			component8.offsetZ = toAdjust.offsetZ + 0.1f;
			val.GetComponent<Renderer>().enabled = true;
			val.transform.parent = ((Component)toAdjust).transform;
			val.transform.localRotation = Quaternion.identity;
			val.transform.localPosition = new Vector3(offset, 0f, 0f);
			val.transform.localScale = Vector3.one;
			val2.GetComponent<Renderer>().enabled = true;
			val2.transform.parent = ((Component)toAdjust).transform;
			val2.transform.localRotation = Quaternion.identity;
			val2.transform.localPosition = new Vector3(0f - offset, 0f, 0f);
			val2.transform.localScale = Vector3.one;
			val3.GetComponent<Renderer>().enabled = true;
			val3.transform.parent = ((Component)toAdjust).transform;
			val3.transform.localRotation = Quaternion.identity;
			val3.transform.localPosition = new Vector3(0f, offset, 0f);
			val3.transform.localScale = Vector3.one;
			val4.GetComponent<Renderer>().enabled = true;
			val4.transform.parent = ((Component)toAdjust).transform;
			val4.transform.localRotation = Quaternion.identity;
			val4.transform.localPosition = new Vector3(0f, 0f - offset, 0f);
			val4.transform.localScale = Vector3.one;
			val5.GetComponent<Renderer>().enabled = true;
			val5.transform.parent = ((Component)toAdjust).transform;
			val5.transform.localRotation = Quaternion.identity;
			val5.transform.localPosition = new Vector3(0f - offset, 3f * offset / 3f, 0f);
			val5.transform.localScale = Vector3.one;
			val6.GetComponent<Renderer>().enabled = true;
			val6.transform.parent = ((Component)toAdjust).transform;
			val6.transform.localRotation = Quaternion.identity;
			val6.transform.localPosition = new Vector3(offset, 3f * offset / 3f, 0f);
			val6.transform.localScale = Vector3.one;
			val7.GetComponent<Renderer>().enabled = true;
			val7.transform.parent = ((Component)toAdjust).transform;
			val7.transform.localRotation = Quaternion.identity;
			val7.transform.localPosition = new Vector3(0f - offset, 0f - offset, 0f);
			val7.transform.localScale = Vector3.one;
			val8.GetComponent<Renderer>().enabled = true;
			val8.transform.parent = ((Component)toAdjust).transform;
			val8.transform.localRotation = Quaternion.identity;
			val8.transform.localPosition = new Vector3(offset, 0f - offset, 0f);
			val8.transform.localScale = Vector3.one;
			outLine = true;
		}
	}

	public static void AdjustFontSplitRows(this TextMesh toAdjust, bool hasWhiteSpaces)
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		string text = toAdjust.text;
		string[] array = text.Split(new char[1] { ' ' });
		if (!hasWhiteSpaces)
		{
			char[] array2 = text.ToCharArray();
			array = new string[array2.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array2[i].ToString();
			}
		}
		if (array.Length == 1)
		{
			return;
		}
		int j = 1;
		toAdjust.text = array[0];
		string text2 = toAdjust.text;
		for (; j < array.Length; j++)
		{
			text2 = toAdjust.text;
			toAdjust.text = toAdjust.text + ((!hasWhiteSpaces) ? "" : " ") + array[j];
			Bounds bounds = ((Component)toAdjust).GetComponent<Renderer>().bounds;
			float x = ((Bounds)(ref bounds)).size.x;
			bounds = ((Component)toAdjust).GetComponent<Collider>().bounds;
			if (x > ((Bounds)(ref bounds)).size.x)
			{
				toAdjust.text = text2 + "\n" + array[j];
			}
		}
	}
}
