using System;
using System.Collections.Generic;
using UltimateSurvival.AI;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000620 RID: 1568
	public static class Extensions
	{
		// Token: 0x060031E7 RID: 12775 RVA: 0x00161B18 File Offset: 0x0015FD18
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

		// Token: 0x060031E8 RID: 12776 RVA: 0x00161B61 File Offset: 0x0015FD61
		public static bool IndexIsValid<T>(this List<T> list, int index)
		{
			return index >= 0 && index < list.Count;
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00161B74 File Offset: 0x0015FD74
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

		// Token: 0x060031EA RID: 12778 RVA: 0x00161BB4 File Offset: 0x0015FDB4
		public static bool IsInRangeLimitsExcluded(this float f, float l1, float l2)
		{
			return f > l1 && f < l2;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x00161BC0 File Offset: 0x0015FDC0
		public static bool IsInRangeLimitsIncluded(this float f, float l1, float l2)
		{
			return f >= l1 && f <= l2;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x00161BD0 File Offset: 0x0015FDD0
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

		// Token: 0x060031ED RID: 12781 RVA: 0x00161C2C File Offset: 0x0015FE2C
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
