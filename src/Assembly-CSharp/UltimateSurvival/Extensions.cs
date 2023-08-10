using System.Collections.Generic;
using UltimateSurvival.AI;
using UnityEngine;

namespace UltimateSurvival;

public static class Extensions
{
	public static Transform FindDeepChild(this Transform parent, string childName)
	{
		Transform val = parent.Find(childName);
		if (Object.op_Implicit((Object)(object)val))
		{
			return val;
		}
		for (int i = 0; i < parent.childCount; i++)
		{
			val = parent.GetChild(i).FindDeepChild(childName);
			if (Object.op_Implicit((Object)(object)val))
			{
				return val;
			}
		}
		return null;
	}

	public static bool IndexIsValid<T>(this List<T> list, int index)
	{
		if (index >= 0)
		{
			return index < list.Count;
		}
		return false;
	}

	public static List<T> CopyOther<T>(this List<T> list, List<T> toCopy)
	{
		if (toCopy == null || toCopy.Count == 0)
		{
			return null;
		}
		list = new List<T>();
		for (int i = 0; i < toCopy.Count; i++)
		{
			list.Add(toCopy[i]);
		}
		return list;
	}

	public static bool IsInRangeLimitsExcluded(this float f, float l1, float l2)
	{
		if (f > l1)
		{
			return f < l2;
		}
		return false;
	}

	public static bool IsInRangeLimitsIncluded(this float f, float l1, float l2)
	{
		if (f >= l1)
		{
			return f <= l2;
		}
		return false;
	}

	public static string GetDisplayInfoForElement(this StateData data, string key)
	{
		string result = string.Empty;
		object value = null;
		if (data.m_Dictionary.TryGetValue(key, out value))
		{
			result = "Key: " + key + " -  Value: " + value;
		}
		else
		{
			Debug.LogError((object)("Could not print element with key " + key));
		}
		return result;
	}

	public static string GetDisplayInfoForAll(this StateData data, string sepparator = " - ")
	{
		string text = string.Empty;
		foreach (KeyValuePair<string, object> item in data.m_Dictionary)
		{
			text = text + sepparator + data.GetDisplayInfoForElement(item.Key);
		}
		return text;
	}
}
