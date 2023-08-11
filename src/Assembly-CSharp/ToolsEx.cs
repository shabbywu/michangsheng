using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public static class ToolsEx
{
	private static string[] BigLevelNames = new string[5] { "炼气期", "筑基期", "金丹期", "元婴期", "化神期" };

	private static string[] LevelNames = new string[15]
	{
		"炼气初期", "炼气中期", "炼气后期", "筑基初期", "筑基中期", "筑基后期", "金丹初期", "金丹中期", "金丹后期", "元婴初期",
		"元婴中期", "元婴后期", "化神初期", "化神中期", "化神后期"
	};

	private static string[] LingQiNames = new string[6] { "金", "木", "水", "火", "土", "魔" };

	private static string[] ShengWangNames = new string[7] { "臭名昭著", "声名狼藉", "名声败坏", "默默无闻", "略有薄名", "声名远扬", "誉满天下" };

	private static Random random = new Random();

	public static string ToCN(this string str)
	{
		try
		{
			if (string.IsNullOrEmpty(str))
			{
				return "";
			}
			return Regex.Unescape(str);
		}
		catch (Exception)
		{
			return str;
		}
	}

	public static JSONObject ItemJson(this int id)
	{
		if (jsonData.instance.ItemJsonData.ContainsKey(id.ToString()))
		{
			return jsonData.instance.ItemJsonData[id.ToString()];
		}
		return null;
	}

	public static JSONObject NPCJson(this int id)
	{
		if (jsonData.instance.AvatarJsonData.HasField(id.ToString()))
		{
			return jsonData.instance.AvatarJsonData[id.ToString()];
		}
		return null;
	}

	public static bool StringListContains(this JSONObject json, string str)
	{
		if (json.type != JSONObject.Type.ARRAY)
		{
			return false;
		}
		foreach (JSONObject item in json.list)
		{
			if (item.str == str)
			{
				return true;
			}
		}
		return false;
	}

	public static int ItemType(this int id)
	{
		return id.ItemJson()["type"].I;
	}

	private static string OneBitNumberToChinese(string num)
	{
		string text = "一二三四五六七八九";
		string result = "";
		int num2 = "123456789".IndexOf(num);
		if (num2 > -1)
		{
			result = text.Substring(num2, 1);
		}
		return result;
	}

	private static string OneBitNumberToChinese(int num)
	{
		return OneBitNumberToChinese(num.ToString());
	}

	public static string ToCNNumber(this int num)
	{
		string text = num.ToString();
		string text2 = "";
		if (text.Length == 1)
		{
			return text2 + OneBitNumberToChinese(num);
		}
		if (text.Length == 2)
		{
			text2 = ((!(text.Substring(0, 1) == "1")) ? (text2 + OneBitNumberToChinese(num / 10) + "十") : (text2 + "十"));
			text2 += (num % 10).ToCNNumber();
		}
		else if (text.Length == 3)
		{
			text2 = text2 + OneBitNumberToChinese(num / 100) + "百";
			if ((num % 100).ToString().Length < 2 && num % 100 != 0)
			{
				text2 += "零";
			}
			text2 += (num % 100).ToCNNumber();
		}
		else if (text.Length == 4)
		{
			text2 = text2 + OneBitNumberToChinese(num / 1000) + "千";
			if ((num % 1000).ToString().Length < 3 && num % 1000 != 0)
			{
				text2 += "零";
			}
			text2 += (num % 1000).ToCNNumber();
		}
		else if (text.Length == 5)
		{
			text2 = text2 + OneBitNumberToChinese(num / 10000) + "万";
			if ((num % 10000).ToString().Length < 4 && num % 10000 != 0)
			{
				text2 += "零";
			}
			text2 += (num % 10000).ToCNNumber();
		}
		return text2;
	}

	public static string MonthToDesc(this int number)
	{
		int num = number / 12;
		int num2 = number % 12;
		if (num > 0)
		{
			return $"{num}年";
		}
		return $"{num2}月";
	}

	public static string DayToDesc(this int number)
	{
		int num = number / 365;
		int num2 = number % 365 / 30;
		int num3 = number % 365 % 30;
		if (num > 0)
		{
			return $"{num}年{num2}月";
		}
		if (num2 > 0)
		{
			return $"{num2}月";
		}
		return $"{num3}日";
	}

	public static string ToCNNumberWithUnit(this ulong number)
	{
		string text = "";
		int num = 1;
		if (number >= 1000000)
		{
			num = 1000000;
			text = "百万";
		}
		else if (number >= 10000)
		{
			num = 10000;
			text = "万";
		}
		else if (number >= 1000)
		{
			num = 1000;
			text = "千";
		}
		if (num > 1)
		{
			return ((float)number / (float)num).ToString("f1") + text;
		}
		return number.ToString();
	}

	public static string ToBigLevelName(this int number)
	{
		if (number >= 1 && number <= 5)
		{
			return BigLevelNames[number - 1];
		}
		return "";
	}

	public static string ToLevelName(this int number)
	{
		if (number >= 1 && number <= 15)
		{
			return LevelNames[number - 1];
		}
		return "";
	}

	public static bool IsMatching(int value1, int value2, string fuhao)
	{
		return fuhao switch
		{
			"=" => value1 == value2, 
			">" => value1 > value2, 
			"<" => value1 < value2, 
			_ => false, 
		};
	}

	public static string ToLingQiName(this int number)
	{
		if (number >= 0 && number <= 5)
		{
			return LingQiNames[number];
		}
		return "";
	}

	public static string ToShengWangName(this int number)
	{
		if (number >= 1 && number <= 7)
		{
			return ShengWangNames[number - 1];
		}
		return "";
	}

	public static string ToItemFlagName(this int number)
	{
		if (jsonData.instance.AllItemLeiXin.ContainsKey(number.ToString()))
		{
			return (string)jsonData.instance.AllItemLeiXin[number.ToString()][(object)"name"];
		}
		return "未找到name";
	}

	public static string RemoveNumber(this string str)
	{
		return Regex.Replace(str, "\\d", "");
	}

	public static string STVarReplace(this string str)
	{
		Regex regex = new Regex("\\{STVar=\\d*\\}");
		MatchCollection matchCollection = Regex.Matches(str, "\\{STVar=\\d*\\}");
		string text = str;
		foreach (Match item in matchCollection)
		{
			if (int.TryParse(item.Value.Replace("{STVar=", "").Replace("}", ""), out var result))
			{
				text = regex.Replace(text, GlobalValue.Get(result, "STVarReplace").ToString());
			}
		}
		return text;
	}

	public static void DestoryAllChild(this Transform t)
	{
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < t.childCount; i++)
		{
			list.Add(t.GetChild(i));
		}
		for (int j = 0; j < list.Count; j++)
		{
			Object.Destroy((Object)(object)((Component)list[j]).gameObject);
		}
	}

	public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> Dict, TKey key, TValue value, string errorFormat = "")
	{
		if (Dict.ContainsKey(key))
		{
			if (string.IsNullOrWhiteSpace(errorFormat))
			{
				Debug.LogError((object)$"对字典进行Add时出错，字典中已经存在相同的Key，Key:{key} 已有值:{Dict[key]} 新加值:{value}");
			}
			else
			{
				Debug.LogError((object)string.Format(errorFormat, key, Dict[key], value));
			}
			Dict[key] = value;
			return false;
		}
		Dict.Add(key, value);
		return true;
	}

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

	public static T GetRandomOne<T>(this List<T> list)
	{
		int index = random.Next(0, list.Count);
		return list[index];
	}

	public static bool CheckPath(string path)
	{
		return new Regex("^[a-zA-Z]:(((\\\\(?! )[^/:*?<>\\\"|\\\\]+)+\\\\?)|(\\\\)?)\\s*$").IsMatch(path);
	}

	public static string GetPath(this Transform t)
	{
		if ((Object)(object)t.parent == (Object)null)
		{
			return ((Object)t).name;
		}
		StringBuilder stringBuilder = new StringBuilder();
		List<string> list = new List<string>();
		list.Add(((Object)t).name);
		Transform parent = t.parent;
		while ((Object)(object)parent != (Object)null)
		{
			list.Add(((Object)parent).name);
			parent = parent.parent;
		}
		for (int num = list.Count - 1; num >= 0; num--)
		{
			stringBuilder.Append(list[num]);
			if (num != 0)
			{
				stringBuilder.Append("/");
			}
		}
		return stringBuilder.ToString();
	}

	public static string GetPath(this GameObject obj)
	{
		return obj.transform.GetPath();
	}
}
