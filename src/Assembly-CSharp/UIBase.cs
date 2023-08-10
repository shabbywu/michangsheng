using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIBase
{
	protected GameObject _go;

	private Dictionary<string, object> _objDict = new Dictionary<string, object>();

	protected T Get<T>(string path)
	{
		if ((Object)(object)_go == (Object)null)
		{
			Debug.LogError((object)"_go对象为空，无法获取组件");
			return default(T);
		}
		string key = path + "_" + typeof(T).Name;
		if (!_objDict.ContainsKey(key))
		{
			Transform val = _go.transform.Find(path);
			if ((Object)(object)val == (Object)null)
			{
				Debug.LogError((object)("不存在该对象,路径:" + path));
				return default(T);
			}
			T component = ((Component)val).GetComponent<T>();
			if (component == null)
			{
				Debug.LogError((object)("不存在该组件" + typeof(T).Name + ",路径:" + path));
				return default(T);
			}
			_objDict.Add(key, component);
		}
		return (T)_objDict[key];
	}

	protected GameObject Get(string path, bool showError = true)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		if ((Object)(object)_go == (Object)null)
		{
			if (showError)
			{
				Debug.LogError((object)"_go对象为空，无法获取组件");
			}
			return null;
		}
		string key = path + "_GameObject";
		if (!_objDict.ContainsKey(key))
		{
			Transform val = _go.transform.Find(path);
			if ((Object)(object)val == (Object)null)
			{
				if (showError)
				{
					Debug.LogError((object)("不存在该对象,路径:" + path));
				}
				return null;
			}
			_objDict.Add(key, ((Component)val).gameObject);
		}
		return (GameObject)_objDict[key];
	}

	public Transform GetTransform()
	{
		return _go.transform;
	}

	public bool IsNull()
	{
		return (Object)(object)_go == (Object)null;
	}
}
