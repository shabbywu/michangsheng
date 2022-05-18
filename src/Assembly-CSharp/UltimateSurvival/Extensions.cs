using System;
using System.Collections.Generic;
using UltimateSurvival.AI;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000907 RID: 2311
	public static class Extensions
	{
		// Token: 0x06003B1B RID: 15131 RVA: 0x001AB3F4 File Offset: 0x001A95F4
		public static Transform FindDeepChild(this Transform parent, string childName)
		{
			Transform transform = parent.Find(childName);
			if (transform)
			{
				return transform;
			}
			for (int i = 0; i < parent.childCount; i++)
			{
				transform = parent.GetChild(i).FindDeepChild(childName);
				if (transform)
				{
					return transform;
				}
			}
			return null;
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x0002AD9D File Offset: 0x00028F9D
		public static bool IndexIsValid<T>(this List<T> list, int index)
		{
			return index >= 0 && index < list.Count;
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x001AB440 File Offset: 0x001A9640
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

		// Token: 0x06003B1E RID: 15134 RVA: 0x0002ADAE File Offset: 0x00028FAE
		public static bool IsInRangeLimitsExcluded(this float f, float l1, float l2)
		{
			return f > l1 && f < l2;
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x0002ADBA File Offset: 0x00028FBA
		public static bool IsInRangeLimitsIncluded(this float f, float l1, float l2)
		{
			return f >= l1 && f <= l2;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x001AB480 File Offset: 0x001A9680
		public static string GetDisplayInfoForElement(this StateData data, string key)
		{
			string result = string.Empty;
			object obj = null;
			if (data.m_Dictionary.TryGetValue(key, out obj))
			{
				result = string.Concat(new object[]
				{
					"Key: ",
					key,
					" -  Value: ",
					obj
				});
			}
			else
			{
				Debug.LogError("Could not print element with key " + key);
			}
			return result;
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x001AB4DC File Offset: 0x001A96DC
		public static string GetDisplayInfoForAll(this StateData data, string sepparator = " - ")
		{
			string text = string.Empty;
			foreach (KeyValuePair<string, object> keyValuePair in data.m_Dictionary)
			{
				text = text + sepparator + data.GetDisplayInfoForElement(keyValuePair.Key);
			}
			return text;
		}
	}
}
