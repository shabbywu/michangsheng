using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x020001AF RID: 431
public static class BindData
{
	// Token: 0x0600122B RID: 4651 RVA: 0x0006E6B2 File Offset: 0x0006C8B2
	public static void Bind(string key, object obj)
	{
		if (BindData._dict.ContainsKey(key))
		{
			BindData._dict[key] = obj;
			return;
		}
		BindData._dict.Add(key, obj);
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x0006E6DA File Offset: 0x0006C8DA
	public static object Get(string key)
	{
		if (!BindData._dict.ContainsKey(key))
		{
			Debug.LogError("不存在Key值：" + key);
			return null;
		}
		object result = BindData._dict[key];
		BindData._dict.Remove(key);
		return result;
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x0006E714 File Offset: 0x0006C914
	public static T Get<T>(string key)
	{
		Type typeFromHandle = typeof(T);
		TypeConverter converter = TypeDescriptor.GetConverter(typeFromHandle);
		if (!BindData._dict.ContainsKey(key))
		{
			Debug.LogError("不存在Key值：" + key);
			return default(T);
		}
		object value = BindData._dict[key];
		BindData._dict.Remove(key);
		return (T)((object)converter.ConvertTo(value, typeFromHandle));
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x0006E77F File Offset: 0x0006C97F
	public static bool ContainsKey(string key)
	{
		return BindData._dict.ContainsKey(key);
	}

	// Token: 0x04000CDC RID: 3292
	private static Dictionary<string, object> _dict = new Dictionary<string, object>();
}
