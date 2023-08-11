using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public static class BindData
{
	private static Dictionary<string, object> _dict = new Dictionary<string, object>();

	public static void Bind(string key, object obj)
	{
		if (_dict.ContainsKey(key))
		{
			_dict[key] = obj;
		}
		else
		{
			_dict.Add(key, obj);
		}
	}

	public static object Get(string key)
	{
		if (!_dict.ContainsKey(key))
		{
			Debug.LogError((object)("不存在Key值：" + key));
			return null;
		}
		object result = _dict[key];
		_dict.Remove(key);
		return result;
	}

	public static T Get<T>(string key)
	{
		Type typeFromHandle = typeof(T);
		TypeConverter converter = TypeDescriptor.GetConverter(typeFromHandle);
		if (!_dict.ContainsKey(key))
		{
			Debug.LogError((object)("不存在Key值：" + key));
			return default(T);
		}
		object value = _dict[key];
		_dict.Remove(key);
		return (T)converter.ConvertTo(value, typeFromHandle);
	}

	public static bool ContainsKey(string key)
	{
		if (!_dict.ContainsKey(key))
		{
			return false;
		}
		return true;
	}
}
