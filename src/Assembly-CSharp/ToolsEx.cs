using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public static class ToolsEx
{
	// Token: 0x060016A4 RID: 5796 RVA: 0x00014142 File Offset: 0x00012342
	public static string ToCN(this string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return "";
		}
		return Regex.Unescape(str);
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x00014158 File Offset: 0x00012358
	public static JSONObject ItemJson(this int id)
	{
		if (jsonData.instance.ItemJsonData.ContainsKey(id.ToString()))
		{
			return jsonData.instance.ItemJsonData[id.ToString()];
		}
		return null;
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x0001418A File Offset: 0x0001238A
	public static JSONObject NPCJson(this int id)
	{
		if (jsonData.instance.AvatarJsonData.HasField(id.ToString()))
		{
			return jsonData.instance.AvatarJsonData[id.ToString()];
		}
		return null;
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000CB194 File Offset: 0x000C9394
	public static bool StringListContains(this JSONObject json, string str)
	{
		if (json.type != JSONObject.Type.ARRAY)
		{
			return false;
		}
		using (List<JSONObject>.Enumerator enumerator = json.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.str == str)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x000141BC File Offset: 0x000123BC
	public static int ItemType(this int id)
	{
		return id.ItemJson()["type"].I;
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x000CB200 File Offset: 0x000C9400
	private static string OneBitNumberToChinese(string num)
	{
		string text = "123456789";
		string text2 = "一二三四五六七八九";
		string result = "";
		int num2 = text.IndexOf(num);
		if (num2 > -1)
		{
			result = text2.Substring(num2, 1);
		}
		return result;
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x000141D3 File Offset: 0x000123D3
	private static string OneBitNumberToChinese(int num)
	{
		return ToolsEx.OneBitNumberToChinese(num.ToString());
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x000CB234 File Offset: 0x000C9434
	public static string ToCNNumber(this int num)
	{
		string text = num.ToString();
		string text2 = "";
		if (text.Length == 1)
		{
			return text2 + ToolsEx.OneBitNumberToChinese(num);
		}
		if (text.Length == 2)
		{
			if (text.Substring(0, 1) == "1")
			{
				text2 += "十";
			}
			else
			{
				text2 = text2 + ToolsEx.OneBitNumberToChinese(num / 10) + "十";
			}
			text2 += (num % 10).ToCNNumber();
		}
		else if (text.Length == 3)
		{
			text2 = text2 + ToolsEx.OneBitNumberToChinese(num / 100) + "百";
			if ((num % 100).ToString().Length < 2 && num % 100 != 0)
			{
				text2 += "零";
			}
			text2 += (num % 100).ToCNNumber();
		}
		else if (text.Length == 4)
		{
			text2 = text2 + ToolsEx.OneBitNumberToChinese(num / 1000) + "千";
			if ((num % 1000).ToString().Length < 3 && num % 1000 != 0)
			{
				text2 += "零";
			}
			text2 += (num % 1000).ToCNNumber();
		}
		else if (text.Length == 5)
		{
			text2 = text2 + ToolsEx.OneBitNumberToChinese(num / 10000) + "万";
			if ((num % 10000).ToString().Length < 4 && num % 10000 != 0)
			{
				text2 += "零";
			}
			text2 += (num % 10000).ToCNNumber();
		}
		return text2;
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000CB3D8 File Offset: 0x000C95D8
	public static string MonthToDesc(this int number)
	{
		int num = number / 12;
		int num2 = number % 12;
		if (num > 0)
		{
			return string.Format("{0}年", num);
		}
		return string.Format("{0}月", num2);
	}

	// Token: 0x060016AD RID: 5805 RVA: 0x000CB414 File Offset: 0x000C9614
	public static string DayToDesc(this int number)
	{
		int num = number / 365;
		int num2 = number % 365 / 30;
		int num3 = number % 365 % 30;
		if (num > 0)
		{
			return string.Format("{0}年{1}月", num, num2);
		}
		if (num2 > 0)
		{
			return string.Format("{0}月", num2);
		}
		return string.Format("{0}日", num3);
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x000CB480 File Offset: 0x000C9680
	public static string ToCNNumberWithUnit(this ulong number)
	{
		string str = "";
		int num = 1;
		if (number >= 1000000UL)
		{
			num = 1000000;
			str = "百万";
		}
		else if (number >= 10000UL)
		{
			num = 10000;
			str = "万";
		}
		else if (number >= 1000UL)
		{
			num = 1000;
			str = "千";
		}
		if (num > 1)
		{
			return (number / (float)num).ToString("f1") + str;
		}
		return number.ToString();
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000141E1 File Offset: 0x000123E1
	public static string ToBigLevelName(this int number)
	{
		if (number >= 1 && number <= 5)
		{
			return ToolsEx.BigLevelNames[number - 1];
		}
		return "";
	}

	// Token: 0x060016B0 RID: 5808 RVA: 0x000141FA File Offset: 0x000123FA
	public static string ToLevelName(this int number)
	{
		if (number >= 1 && number <= 15)
		{
			return ToolsEx.LevelNames[number - 1];
		}
		return "";
	}

	// Token: 0x060016B1 RID: 5809 RVA: 0x00014214 File Offset: 0x00012414
	public static string ToLingQiName(this int number)
	{
		if (number >= 0 && number <= 5)
		{
			return ToolsEx.LingQiNames[number];
		}
		return "";
	}

	// Token: 0x060016B2 RID: 5810 RVA: 0x0001422B File Offset: 0x0001242B
	public static string ToShengWangName(this int number)
	{
		if (number >= 1 && number <= 7)
		{
			return ToolsEx.ShengWangNames[number - 1];
		}
		return "";
	}

	// Token: 0x060016B3 RID: 5811 RVA: 0x000CB500 File Offset: 0x000C9700
	public static string ToItemFlagName(this int number)
	{
		if (jsonData.instance.AllItemLeiXin.ContainsKey(number.ToString()))
		{
			return (string)jsonData.instance.AllItemLeiXin[number.ToString()]["name"];
		}
		return "未找到name";
	}

	// Token: 0x060016B4 RID: 5812 RVA: 0x00014244 File Offset: 0x00012444
	public static string RemoveNumber(this string str)
	{
		return Regex.Replace(str, "\\d", "");
	}

	// Token: 0x060016B5 RID: 5813 RVA: 0x000CB550 File Offset: 0x000C9750
	public static string STVarReplace(this string str)
	{
		Regex regex = new Regex("\\{STVar=\\d*\\}");
		MatchCollection matchCollection = Regex.Matches(str, "\\{STVar=\\d*\\}");
		string text = str;
		using (IEnumerator enumerator = matchCollection.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int id;
				if (int.TryParse(((Match)enumerator.Current).Value.Replace("{STVar=", "").Replace("}", ""), out id))
				{
					text = regex.Replace(text, GlobalValue.Get(id, "STVarReplace").ToString());
				}
			}
		}
		return text;
	}

	// Token: 0x060016B6 RID: 5814 RVA: 0x000CB600 File Offset: 0x000C9800
	public static void DestoryAllChild(this Transform t)
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < t.childCount; i++)
		{
			list.Add(t.GetChild(i));
		}
		for (int j = 0; j < list.Count; j++)
		{
			Object.Destroy(list[j].gameObject);
		}
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x000CB654 File Offset: 0x000C9854
	public static string GetPath(this Transform t)
	{
		string text = t.name;
		Transform transform = t;
		while (transform.parent != null)
		{
			text = transform.parent.name + "/" + text;
			transform = transform.parent;
		}
		return text;
	}

	// Token: 0x060016B8 RID: 5816 RVA: 0x000CB69C File Offset: 0x000C989C
	public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> Dict, TKey key, TValue value, string errorFormat = "")
	{
		if (Dict.ContainsKey(key))
		{
			if (string.IsNullOrWhiteSpace(errorFormat))
			{
				Debug.LogError(string.Format("对字典进行Add时出错，字典中已经存在相同的Key，Key:{0} 已有值:{1} 新加值:{2}", key, Dict[key], value));
			}
			else
			{
				Debug.LogError(string.Format(errorFormat, key, Dict[key], value));
			}
			Dict[key] = value;
			return false;
		}
		Dict.Add(key, value);
		return true;
	}

	// Token: 0x060016B9 RID: 5817 RVA: 0x000CB71C File Offset: 0x000C991C
	public static List<T> RandomSort<T>(this List<T> list)
	{
		Random random = new Random();
		List<T> list2 = new List<T>();
		foreach (T item in list)
		{
			list2.Insert(random.Next(list2.Count), item);
		}
		return list2;
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x000CB784 File Offset: 0x000C9984
	public static T GetRandomOne<T>(this List<T> list)
	{
		int index = ToolsEx.random.Next(0, list.Count);
		return list[index];
	}

	// Token: 0x04001219 RID: 4633
	private static string[] BigLevelNames = new string[]
	{
		"炼气期",
		"筑基期",
		"金丹期",
		"元婴期",
		"化神期"
	};

	// Token: 0x0400121A RID: 4634
	private static string[] LevelNames = new string[]
	{
		"炼气初期",
		"炼气中期",
		"炼气后期",
		"筑基初期",
		"筑基中期",
		"筑基后期",
		"金丹初期",
		"金丹中期",
		"金丹后期",
		"元婴初期",
		"元婴中期",
		"元婴后期",
		"化神初期",
		"化神中期",
		"化神后期"
	};

	// Token: 0x0400121B RID: 4635
	private static string[] LingQiNames = new string[]
	{
		"金",
		"木",
		"水",
		"火",
		"土",
		"魔"
	};

	// Token: 0x0400121C RID: 4636
	private static string[] ShengWangNames = new string[]
	{
		"臭名昭著",
		"声名狼藉",
		"名声败坏",
		"默默无闻",
		"略有薄名",
		"声名远扬",
		"誉满天下"
	};

	// Token: 0x0400121D RID: 4637
	private static Random random = new Random();
}
