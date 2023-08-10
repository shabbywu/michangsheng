using System;
using System.Collections.Generic;
using UnityEngine;

public class BindUtil
{
	private static Dictionary<string, Type> _prefabAndScriptMap = new Dictionary<string, Type>();

	public static void Bind(string path, Type type)
	{
		if (!_prefabAndScriptMap.ContainsKey(path))
		{
			_prefabAndScriptMap.Add(path, type);
		}
		else
		{
			Debug.LogError((object)("当前数据已存在路径：" + path));
		}
	}

	public static Type Get(string path)
	{
		if (!_prefabAndScriptMap.ContainsKey(path))
		{
			Debug.LogError((object)("当前路径未绑定类：" + path));
			return null;
		}
		return _prefabAndScriptMap[path];
	}
}
