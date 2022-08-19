using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public static class ToolsEx
{
	// Token: 0x060013D3 RID: 5075 RVA: 0x0007E420 File Offset: 0x0007C620
	public static string ToCN(this string str)
	{
		string result;
		try
		{
			if (string.IsNullOrEmpty(str))
			{
				result = "";
			}
			else
			{
				result = Regex.Unescape(str);
			}
		}
		catch (Exception)
		{
			result = str;
		}
		return result;
	}

	// Token: 0x060013D4 RID: 5076 RVA: 0x0007E45C File Offset: 0x0007C65C
	public static JSONObject ItemJson(this int id)
	{
		if (jsonData.instance.ItemJsonData.ContainsKey(id.ToString()))
		{
			return jsonData.instance.ItemJsonData[id.ToString()];
		}
		return null;
	}

	// Token: 0x060013D5 RID: 5077 RVA: 0x0007E48E File Offset: 0x0007C68E
	public static JSONObject NPCJson(this int id)
	{
		if (jsonData.instance.AvatarJsonData.HasField(id.ToString()))
		{
			return jsonData.instance.AvatarJsonData[id.ToString()];
		}
		return null;
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x0007E4C0 File Offset: 0x0007C6C0
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

	// Token: 0x060013D7 RID: 5079 RVA: 0x0007E52C File Offset: 0x0007C72C
	public static int ItemType(this int id)
	{
		return id.ItemJson()["type"].I;
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x0007E544 File Offset: 0x0007C744
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

	// Token: 0x060013D9 RID: 5081 RVA: 0x0007E577 File Offset: 0x0007C777
	private static string OneBitNumberToChinese(int num)
	{
		return ToolsEx.OneBitNumberToChinese(num.ToString());
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x0007E588 File Offset: 0x0007C788
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

	// Token: 0x060013DB RID: 5083 RVA: 0x0007E72C File Offset: 0x0007C92C
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

	// Token: 0x060013DC RID: 5084 RVA: 0x0007E768 File Offset: 0x0007C968
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

	// Token: 0x060013DD RID: 5085 RVA: 0x0007E7D4 File Offset: 0x0007C9D4
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

	// Token: 0x060013DE RID: 5086 RVA: 0x0007E851 File Offset: 0x0007CA51
	public static string ToBigLevelName(this int number)
	{
		if (number >= 1 && number <= 5)
		{
			return ToolsEx.BigLevelNames[number - 1];
		}
		return "";
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x0007E86A File Offset: 0x0007CA6A
	public static string ToLevelName(this int number)
	{
		if (number >= 1 && number <= 15)
		{
			return ToolsEx.LevelNames[number - 1];
		}
		return "";
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x0007E884 File Offset: 0x0007CA84
	public static bool IsMatching(int value1, int value2, string fuhao)
	{
		if (fuhao == "=")
		{
			return value1 == value2;
		}
		if (!(fuhao == ">"))
		{
			return fuhao == "<" && value1 < value2;
		}
		return value1 > value2;
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x0007E8BF File Offset: 0x0007CABF
	public static string ToLingQiName(this int number)
	{
		if (number >= 0 && number <= 5)
		{
			return ToolsEx.LingQiNames[number];
		}
		return "";
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x0007E8D6 File Offset: 0x0007CAD6
	public static string ToShengWangName(this int number)
	{
		if (number >= 1 && number <= 7)
		{
			return ToolsEx.ShengWangNames[number - 1];
		}
		return "";
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x0007E8F0 File Offset: 0x0007CAF0
	public static string ToItemFlagName(this int number)
	{
		if (jsonData.instance.AllItemLeiXin.ContainsKey(number.ToString()))
		{
			return (string)jsonData.instance.AllItemLeiXin[number.ToString()]["name"];
		}
		return "未找到name";
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x0007E940 File Offset: 0x0007CB40
	public static string RemoveNumber(this string str)
	{
		return Regex.Replace(str, "\\d", "");
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x0007E954 File Offset: 0x0007CB54
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

	// Token: 0x060013E6 RID: 5094 RVA: 0x0007EA04 File Offset: 0x0007CC04
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

	// Token: 0x060013E7 RID: 5095 RVA: 0x0007EA58 File Offset: 0x0007CC58
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

	// Token: 0x060013E8 RID: 5096 RVA: 0x0007EAD8 File Offset: 0x0007CCD8
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

	// Token: 0x060013E9 RID: 5097 RVA: 0x0007EB40 File Offset: 0x0007CD40
	public static T GetRandomOne<T>(this List<T> list)
	{
		int index = ToolsEx.random.Next(0, list.Count);
		return list[index];
	}

	// Token: 0x060013EA RID: 5098 RVA: 0x0007EB66 File Offset: 0x0007CD66
	public static bool CheckPath(string path)
	{
		return new Regex("^[a-zA-Z]:(((\\\\(?! )[^/:*?<>\\\"|\\\\]+)+\\\\?)|(\\\\)?)\\s*$").IsMatch(path);
	}

	// Token: 0x060013EB RID: 5099 RVA: 0x0007EB78 File Offset: 0x0007CD78
	public static string GetPath(this Transform t)
	{
		if (t.parent == null)
		{
			return t.name;
		}
		StringBuilder stringBuilder = new StringBuilder();
		List<string> list = new List<string>();
		list.Add(t.name);
		Transform parent = t.parent;
		while (parent != null)
		{
			list.Add(parent.name);
			parent = parent.parent;
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			stringBuilder.Append(list[i]);
			if (i != 0)
			{
				stringBuilder.Append("/");
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x0007EC0D File Offset: 0x0007CE0D
	public static string GetPath(this GameObject obj)
	{
		return obj.transform.GetPath();
	}

	// Token: 0x04000EC6 RID: 3782
	private static string[] BigLevelNames = new string[]
	{
		"炼气期",
		"筑基期",
		"金丹期",
		"元婴期",
		"化神期"
	};

	// Token: 0x04000EC7 RID: 3783
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

	// Token: 0x04000EC8 RID: 3784
	private static string[] LingQiNames = new string[]
	{
		"金",
		"木",
		"水",
		"火",
		"土",
		"魔"
	};

	// Token: 0x04000EC9 RID: 3785
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

	// Token: 0x04000ECA RID: 3786
	private static Random random = new Random();
}
