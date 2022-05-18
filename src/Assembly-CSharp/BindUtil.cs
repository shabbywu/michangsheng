using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005FA RID: 1530
public class BindUtil
{
	// Token: 0x06002659 RID: 9817 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
	public static void Bind(string path, Type type)
	{
		if (!BindUtil._prefabAndScriptMap.ContainsKey(path))
		{
			BindUtil._prefabAndScriptMap.Add(path, type);
			return;
		}
		Debug.LogError("当前数据已存在路径：" + path);
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x0001E8FC File Offset: 0x0001CAFC
	public static Type Get(string path)
	{
		if (!BindUtil._prefabAndScriptMap.ContainsKey(path))
		{
			Debug.LogError("当前路径未绑定类：" + path);
			return null;
		}
		return BindUtil._prefabAndScriptMap[path];
	}

	// Token: 0x040020BC RID: 8380
	private static Dictionary<string, Type> _prefabAndScriptMap = new Dictionary<string, Type>();
}
