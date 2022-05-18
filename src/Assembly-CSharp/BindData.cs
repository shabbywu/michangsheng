using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Token: 0x020002AD RID: 685
public static class BindData
{
	// Token: 0x060014D6 RID: 5334 RVA: 0x000131EE File Offset: 0x000113EE
	public static void Bind(string key, object obj)
	{
		if (BindData._dict.ContainsKey(key))
		{
			BindData._dict[key] = obj;
			return;
		}
		BindData._dict.Add(key, obj);
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x00013216 File Offset: 0x00011416
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

	// Token: 0x060014D8 RID: 5336 RVA: 0x000BC420 File Offset: 0x000BA620
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

	// Token: 0x060014D9 RID: 5337 RVA: 0x0001324E File Offset: 0x0001144E
	public static bool ContainsKey(string key)
	{
		return BindData._dict.ContainsKey(key);
	}

	// Token: 0x04001007 RID: 4103
	private static Dictionary<string, object> _dict = new Dictionary<string, object>();
}
