using System;
using System.Collections.Generic;

namespace script.YarnEditor.Manager;

[Serializable]
public class SaveValueManager
{
	public Dictionary<string, string> ValueDict = new Dictionary<string, string>();

	public void Init()
	{
		if (ValueDict == null)
		{
			ValueDict = new Dictionary<string, string>();
		}
	}

	public void SetValue(string key, string value)
	{
		ValueDict[key] = value;
	}

	public string GetValue(string key)
	{
		if (ValueDict.ContainsKey(key))
		{
			return ValueDict[key];
		}
		return "-1";
	}
}
