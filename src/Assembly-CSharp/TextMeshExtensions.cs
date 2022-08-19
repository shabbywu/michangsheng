using System;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020004F9 RID: 1273
internal static class TextMeshExtensions
{
	// Token: 0x0600293D RID: 10557 RVA: 0x0013A4B8 File Offset: 0x001386B8
	public static void AdjustFontSize(this TextMesh toAdjust, bool rowSplit, bool hasWhiteSpaces, bool increaseFont)
	{
		if (toAdjust.GetComponent<Collider>() != null)
		{
			if (toAdjust.GetComponent<Collider>().bounds.size.x == 0f || toAdjust.GetComponent<Collider>().bounds.size.y == 0f)
			{
				return;
			}
			string text = toAdjust.text;
			if (increaseFont)
			{
				while (toAdjust.GetComponent<Renderer>().bounds.size.x * 1.1f < toAdjust.GetComponent<Collider>().bounds.size.x)
				{
					if (toAdjust.GetComponent<Renderer>().bounds.size.y * 1.1f >= toAdjust.GetComponent<Collider>().bounds.size.y)
					{
						break;
					}
					toAdjust.characterSize *= 1.1f;
				}
			}
			else if (toAdjust.GetComponent<Renderer>().bounds.size.x < toAdjust.GetComponent<Collider>().bounds.size.x && toAdjust.GetComponent<Renderer>().bounds.size.y < toAdjust.GetComponent<Collider>().bounds.size.y)
			{
				return;
			}
			if (!rowSplit)
			{
				while (toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x || toAdjust.GetComponent<Renderer>().bounds.size.y > toAdjust.GetComponent<Collider>().bounds.size.y)
				{
					toAdjust.characterSize *= 0.9f;
				}
				return;
			}
			string[] array = text.Split(new char[]
			{
				' '
			});
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
				while (toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x || toAdjust.GetComponent<Renderer>().bounds.size.y > toAdjust.GetComponent<Collider>().bounds.size.y)
				{
					toAdjust.characterSize *= 0.9f;
				}
				return;
			}
			if (array.Length == 1)
			{
				while (toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x)
				{
					if (toAdjust.GetComponent<Renderer>().bounds.size.y <= toAdjust.GetComponent<Collider>().bounds.size.y)
					{
						return;
					}
					toAdjust.characterSize *= 0.9f;
				}
			}
			else
			{
				bool flag = false;
				toAdjust.text = array[0];
				int j = 1;
				while (toAdjust.GetComponent<Renderer>().bounds.size.x * 1.1f < toAdjust.GetComponent<Collider>().bounds.size.x && toAdjust.GetComponent<Renderer>().bounds.size.y * 1.1f < toAdjust.GetComponent<Collider>().bounds.size.y)
				{
					toAdjust.characterSize *= 1.1f;
				}
				int count = Regex.Matches(toAdjust.text, "\n").Count;
				while (j < array.Length)
				{
					count = Regex.Matches(toAdjust.text, "\n").Count;
					if (!flag)
					{
						toAdjust.text = toAdjust.text + "\n" + array[j++];
					}
					else
					{
						string text2 = toAdjust.text;
						toAdjust.text = toAdjust.text + (hasWhiteSpaces ? " " : "") + array[j++];
						if (toAdjust.GetComponent<Renderer>().bounds.size.y > toAdjust.GetComponent<Collider>().bounds.size.y || toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x)
						{
							toAdjust.text = text2 + "\n" + array[j - 1];
						}
					}
					while (toAdjust.GetComponent<Renderer>().bounds.size.y > toAdjust.GetComponent<Collider>().bounds.size.y || toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x)
					{
						string text3 = toAdjust.text;
						toAdjust.text = TextMeshExtensions.ReplaceNextOccurence(toAdjust.text, "\n", hasWhiteSpaces ? " " : "", count++);
						if (toAdjust.GetComponent<Renderer>().bounds.size.x < toAdjust.GetComponent<Collider>().bounds.size.x && toAdjust.GetComponent<Renderer>().bounds.size.y < toAdjust.GetComponent<Collider>().bounds.size.y)
						{
							flag = true;
							break;
						}
						count = Regex.Matches(toAdjust.text, "\n").Count;
						toAdjust.characterSize *= 0.96f;
						toAdjust.text = text3;
					}
				}
			}
		}
	}

	// Token: 0x0600293E RID: 10558 RVA: 0x0013AAC4 File Offset: 0x00138CC4
	public static string ReplaceLastOccurrence(string Source, string Find, string Replace)
	{
		int startIndex = Source.LastIndexOf(Find);
		return Source.Remove(startIndex, Find.Length).Insert(startIndex, Replace);
	}

	// Token: 0x0600293F RID: 10559 RVA: 0x0013AAF0 File Offset: 0x00138CF0
	public static int IndexOfOccurence(this string s, string match, int occurence)
	{
		int num = 1;
		int num2 = 0;
		while (num <= occurence && (num2 = s.IndexOf(match, num2 + 1)) != -1)
		{
			if (num == occurence)
			{
				return num2;
			}
			num++;
		}
		return -1;
	}

	// Token: 0x06002940 RID: 10560 RVA: 0x0013AB24 File Offset: 0x00138D24
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

	// Token: 0x06002941 RID: 10561 RVA: 0x0013AB58 File Offset: 0x00138D58
	public static void SetEffectsTexts(this TextMesh toAdjust, bool otherChildren)
	{
		if (TextMeshExtensions.outLine)
		{
			if (toAdjust != null && toAdjust.transform.childCount == 8)
			{
				if (otherChildren)
				{
					toAdjust.transform.Find(toAdjust.name + "RightOutline").GetComponent<TextMesh>().text = toAdjust.text;
					toAdjust.transform.Find(toAdjust.name + "LeftOutline").GetComponent<TextMesh>().text = toAdjust.text;
					toAdjust.transform.Find(toAdjust.name + "UpOutline").GetComponent<TextMesh>().text = toAdjust.text;
					toAdjust.transform.Find(toAdjust.name + "DownOutline").GetComponent<TextMesh>().text = toAdjust.text;
					toAdjust.transform.Find(toAdjust.name + "LeftUpOutline").GetComponent<TextMesh>().text = toAdjust.text;
					toAdjust.transform.Find(toAdjust.name + "RightUpOutline").GetComponent<TextMesh>().text = toAdjust.text;
					toAdjust.transform.Find(toAdjust.name + "LeftDownOutline").GetComponent<TextMesh>().text = toAdjust.text;
					toAdjust.transform.Find(toAdjust.name + "RightDownOutline").GetComponent<TextMesh>().text = toAdjust.text;
					return;
				}
				toAdjust.transform.GetChild(0).GetComponent<TextMesh>().text = toAdjust.text;
				toAdjust.transform.GetChild(1).GetComponent<TextMesh>().text = toAdjust.text;
				toAdjust.transform.GetChild(2).GetComponent<TextMesh>().text = toAdjust.text;
				toAdjust.transform.GetChild(3).GetComponent<TextMesh>().text = toAdjust.text;
				toAdjust.transform.GetChild(4).GetComponent<TextMesh>().text = toAdjust.text;
				toAdjust.transform.GetChild(5).GetComponent<TextMesh>().text = toAdjust.text;
				toAdjust.transform.GetChild(6).GetComponent<TextMesh>().text = toAdjust.text;
				toAdjust.transform.GetChild(7).GetComponent<TextMesh>().text = toAdjust.text;
				return;
			}
		}
		else if (!TextMeshExtensions.shadow && TextMeshExtensions.singleOutline)
		{
			if (otherChildren)
			{
				toAdjust.transform.Find(toAdjust.name + "Outline").GetComponent<TextMesh>().text = toAdjust.text;
				return;
			}
			toAdjust.transform.GetChild(0).GetComponent<TextMesh>().text = toAdjust.text;
		}
	}

	// Token: 0x06002942 RID: 10562 RVA: 0x0013AE28 File Offset: 0x00139028
	public static void AddSingleOutline(this TextMesh toAdjust, Color shadowColor, float offset)
	{
		if (toAdjust.transform.childCount == 0)
		{
			Object.DestroyImmediate(toAdjust.GetComponent<TextMeshPlugin>());
			GameObject gameObject = Object.Instantiate<GameObject>(toAdjust.gameObject);
			GameObject gameObject2 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			GameObject gameObject3 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			GameObject gameObject4 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			gameObject.name = toAdjust.name + "Outline";
			TextMesh component = gameObject.GetComponent<TextMesh>();
			component.color = shadowColor;
			component.characterSize *= offset;
			component.offsetZ = toAdjust.offsetZ + 0.1f;
			gameObject.GetComponent<Renderer>().enabled = true;
			gameObject.transform.parent = toAdjust.transform;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject.transform.localScale = Vector3.one;
			gameObject2.name = toAdjust.name + "Outline2";
			TextMesh component2 = gameObject2.GetComponent<TextMesh>();
			component2.color = shadowColor;
			component2.characterSize /= offset;
			component2.offsetZ = toAdjust.offsetZ + 0.1f;
			gameObject2.GetComponent<Renderer>().enabled = true;
			gameObject2.transform.parent = toAdjust.transform;
			gameObject2.transform.localRotation = Quaternion.identity;
			gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
			gameObject2.transform.localScale = Vector3.one;
			gameObject3.name = toAdjust.name + "Outline3";
			TextMesh component3 = gameObject3.GetComponent<TextMesh>();
			component3.color = shadowColor;
			component3.characterSize /= offset + 0.01f;
			component3.offsetZ = toAdjust.offsetZ + 0.1f;
			gameObject3.GetComponent<Renderer>().enabled = true;
			gameObject3.transform.parent = toAdjust.transform;
			gameObject3.transform.localRotation = Quaternion.identity;
			gameObject3.transform.localPosition = new Vector3(-0.017f, 0f, 0f);
			gameObject3.transform.localScale = Vector3.one;
			gameObject4.name = toAdjust.name + "Outline4";
			TextMesh component4 = gameObject4.GetComponent<TextMesh>();
			component4.color = shadowColor;
			component4.offsetZ = toAdjust.offsetZ + 0.1f;
			gameObject4.GetComponent<Renderer>().enabled = true;
			gameObject4.transform.parent = toAdjust.transform;
			gameObject4.transform.localRotation = Quaternion.identity;
			gameObject4.transform.localPosition = new Vector3(0.04f, 0f, 0f);
			gameObject4.transform.localScale = Vector3.one;
			TextMeshExtensions.singleOutline = true;
		}
	}

	// Token: 0x06002943 RID: 10563 RVA: 0x0013B0F4 File Offset: 0x001392F4
	public static void AddShadow(this TextMesh toAdjust, Color shadowColor, Vector2 offset)
	{
		if (toAdjust.transform.childCount == 0)
		{
			Object.DestroyImmediate(toAdjust.GetComponent<TextMeshPlugin>());
			GameObject gameObject = Object.Instantiate<GameObject>(toAdjust.gameObject);
			gameObject.name = toAdjust.name + "Shadow";
			TextMesh component = gameObject.GetComponent<TextMesh>();
			component.color = shadowColor;
			component.offsetZ = toAdjust.offsetZ + 0.1f;
			gameObject.GetComponent<Renderer>().enabled = true;
			gameObject.transform.parent = toAdjust.transform;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localPosition = new Vector3(offset.x, offset.y, 0f);
			gameObject.transform.localScale = Vector3.one;
			TextMeshExtensions.shadow = true;
		}
	}

	// Token: 0x06002944 RID: 10564 RVA: 0x0013B1C0 File Offset: 0x001393C0
	public static void AddOutline(this TextMesh toAdjust, Color outlineColor, float offset)
	{
		if (toAdjust.transform.childCount != 8)
		{
			Object.DestroyImmediate(toAdjust.GetComponent<TextMeshPlugin>());
			GameObject gameObject = Object.Instantiate<GameObject>(toAdjust.gameObject);
			if (gameObject.GetComponent<Collider>() != null)
			{
				Object.Destroy(gameObject.GetComponent<Collider>());
			}
			gameObject.name = toAdjust.name + "RightOutline";
			TextMesh component = gameObject.GetComponent<TextMesh>();
			component.color = outlineColor;
			component.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject gameObject2 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			if (gameObject2.GetComponent<Collider>() != null)
			{
				Object.Destroy(gameObject2.GetComponent<Collider>());
			}
			gameObject2.name = toAdjust.name + "LeftOutline";
			TextMesh component2 = gameObject2.GetComponent<TextMesh>();
			component2.color = outlineColor;
			component2.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject gameObject3 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			if (gameObject3.GetComponent<Collider>() != null)
			{
				Object.Destroy(gameObject3.GetComponent<Collider>());
			}
			gameObject3.name = toAdjust.name + "UpOutline";
			TextMesh component3 = gameObject3.GetComponent<TextMesh>();
			component3.color = outlineColor;
			component3.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject gameObject4 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			if (gameObject4.GetComponent<Collider>() != null)
			{
				Object.Destroy(gameObject4.GetComponent<Collider>());
			}
			gameObject4.name = toAdjust.name + "DownOutline";
			TextMesh component4 = gameObject4.GetComponent<TextMesh>();
			component4.color = outlineColor;
			component4.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject gameObject5 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			if (gameObject5.GetComponent<Collider>() != null)
			{
				Object.Destroy(gameObject5.GetComponent<Collider>());
			}
			gameObject5.name = toAdjust.name + "LeftUpOutline";
			TextMesh component5 = gameObject5.GetComponent<TextMesh>();
			component5.color = outlineColor;
			component5.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject gameObject6 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			if (gameObject6.GetComponent<Collider>() != null)
			{
				Object.Destroy(gameObject6.GetComponent<Collider>());
			}
			gameObject6.name = toAdjust.name + "RightUpOutline";
			TextMesh component6 = gameObject6.GetComponent<TextMesh>();
			component6.color = outlineColor;
			component6.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject gameObject7 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			if (gameObject7.GetComponent<Collider>() != null)
			{
				Object.Destroy(gameObject7.GetComponent<Collider>());
			}
			gameObject7.name = toAdjust.name + "LeftDownOutline";
			TextMesh component7 = gameObject7.GetComponent<TextMesh>();
			component7.color = outlineColor;
			component7.offsetZ = toAdjust.offsetZ + 0.1f;
			GameObject gameObject8 = Object.Instantiate<GameObject>(toAdjust.gameObject);
			if (gameObject8.GetComponent<Collider>() != null)
			{
				Object.Destroy(gameObject8.GetComponent<Collider>());
			}
			gameObject8.name = toAdjust.name + "RightDownOutline";
			TextMesh component8 = gameObject8.GetComponent<TextMesh>();
			component8.color = outlineColor;
			component8.offsetZ = toAdjust.offsetZ + 0.1f;
			gameObject.GetComponent<Renderer>().enabled = true;
			gameObject.transform.parent = toAdjust.transform;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localPosition = new Vector3(offset, 0f, 0f);
			gameObject.transform.localScale = Vector3.one;
			gameObject2.GetComponent<Renderer>().enabled = true;
			gameObject2.transform.parent = toAdjust.transform;
			gameObject2.transform.localRotation = Quaternion.identity;
			gameObject2.transform.localPosition = new Vector3(-offset, 0f, 0f);
			gameObject2.transform.localScale = Vector3.one;
			gameObject3.GetComponent<Renderer>().enabled = true;
			gameObject3.transform.parent = toAdjust.transform;
			gameObject3.transform.localRotation = Quaternion.identity;
			gameObject3.transform.localPosition = new Vector3(0f, offset, 0f);
			gameObject3.transform.localScale = Vector3.one;
			gameObject4.GetComponent<Renderer>().enabled = true;
			gameObject4.transform.parent = toAdjust.transform;
			gameObject4.transform.localRotation = Quaternion.identity;
			gameObject4.transform.localPosition = new Vector3(0f, -offset, 0f);
			gameObject4.transform.localScale = Vector3.one;
			gameObject5.GetComponent<Renderer>().enabled = true;
			gameObject5.transform.parent = toAdjust.transform;
			gameObject5.transform.localRotation = Quaternion.identity;
			gameObject5.transform.localPosition = new Vector3(-offset, 3f * offset / 3f, 0f);
			gameObject5.transform.localScale = Vector3.one;
			gameObject6.GetComponent<Renderer>().enabled = true;
			gameObject6.transform.parent = toAdjust.transform;
			gameObject6.transform.localRotation = Quaternion.identity;
			gameObject6.transform.localPosition = new Vector3(offset, 3f * offset / 3f, 0f);
			gameObject6.transform.localScale = Vector3.one;
			gameObject7.GetComponent<Renderer>().enabled = true;
			gameObject7.transform.parent = toAdjust.transform;
			gameObject7.transform.localRotation = Quaternion.identity;
			gameObject7.transform.localPosition = new Vector3(-offset, -offset, 0f);
			gameObject7.transform.localScale = Vector3.one;
			gameObject8.GetComponent<Renderer>().enabled = true;
			gameObject8.transform.parent = toAdjust.transform;
			gameObject8.transform.localRotation = Quaternion.identity;
			gameObject8.transform.localPosition = new Vector3(offset, -offset, 0f);
			gameObject8.transform.localScale = Vector3.one;
			TextMeshExtensions.outLine = true;
		}
	}

	// Token: 0x06002945 RID: 10565 RVA: 0x0013B7B0 File Offset: 0x001399B0
	public static void AdjustFontSplitRows(this TextMesh toAdjust, bool hasWhiteSpaces)
	{
		string text = toAdjust.text;
		string[] array = text.Split(new char[]
		{
			' '
		});
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
		while (j < array.Length)
		{
			text2 = toAdjust.text;
			toAdjust.text = toAdjust.text + ((!hasWhiteSpaces) ? "" : " ") + array[j];
			if (toAdjust.GetComponent<Renderer>().bounds.size.x > toAdjust.GetComponent<Collider>().bounds.size.x)
			{
				toAdjust.text = text2 + "\n" + array[j];
			}
			j++;
		}
	}

	// Token: 0x0400256E RID: 9582
	public static bool outLine;

	// Token: 0x0400256F RID: 9583
	public static bool shadow;

	// Token: 0x04002570 RID: 9584
	public static bool singleOutline;
}
