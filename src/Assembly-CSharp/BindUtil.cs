using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class BindUtil
{
	// Token: 0x0600229A RID: 8858 RVA: 0x000ED6AF File Offset: 0x000EB8AF
	public static void Bind(string path, Type type)
	{
		if (!BindUtil._prefabAndScriptMap.ContainsKey(path))
		{
			BindUtil._prefabAndScriptMap.Add(path, type);
			return;
		}
		Debug.LogError("当前数据已存在路径：" + path);
	}

	// Token: 0x0600229B RID: 8859 RVA: 0x000ED6DB File Offset: 0x000EB8DB
	public static Type Get(string path)
	{
		if (!BindUtil._prefabAndScriptMap.ContainsKey(path))
		{
			Debug.LogError("当前路径未绑定类：" + path);
			return null;
		}
		return BindUtil._prefabAndScriptMap[path];
	}

	// Token: 0x04001BF0 RID: 7152
	private static Dictionary<string, Type> _prefabAndScriptMap = new Dictionary<string, Type>();
}
