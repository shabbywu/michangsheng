using System;
using System.Collections.Generic;
using JSONClass;

public class ShengPingData : IComparable
{
	public string ID;

	public int priority;

	public DateTime time;

	public Dictionary<string, string> args;

	public ShengPingData()
	{
	}

	public ShengPingData(JSONObject json)
	{
		time = DateTime.Parse(json["time"].str);
		if (!json.HasField("args"))
		{
			return;
		}
		args = new Dictionary<string, string>();
		JSONObject jSONObject = json["args"];
		foreach (string key in jSONObject.keys)
		{
			args[key] = jSONObject[key].Str;
		}
	}

	public JSONObject ToJson()
	{
		JSONObject jSONObject = new JSONObject(JSONObject.Type.OBJECT);
		jSONObject.SetField("time", time.ToString());
		if (args != null && args.Count > 0)
		{
			JSONObject jSONObject2 = new JSONObject(JSONObject.Type.OBJECT);
			foreach (KeyValuePair<string, string> arg in args)
			{
				jSONObject2.SetField(arg.Key, arg.Value);
			}
			jSONObject.SetField("args", jSONObject2);
		}
		return jSONObject;
	}

	public int CompareTo(object obj)
	{
		ShengPingData shengPingData = obj as ShengPingData;
		if (time.CompareTo(shengPingData.time) == 0)
		{
			return priority.CompareTo(shengPingData.priority);
		}
		return time.CompareTo(shengPingData.time);
	}

	public override string ToString()
	{
		string text = "null";
		if (args != null && args.Count > 0)
		{
			text = "";
			foreach (KeyValuePair<string, string> arg in args)
			{
				text = text + arg.Key + ":" + arg.Value + " ";
			}
		}
		if (string.IsNullOrWhiteSpace(ID))
		{
			return $"时间:{time} 参数:{text}";
		}
		if (ShengPing.DataDict.ContainsKey(ID))
		{
			string text2 = ShengPing.DataDict[ID].descr;
			if (args != null && args.Count > 0)
			{
				foreach (KeyValuePair<string, string> arg2 in args)
				{
					text2 = text2.Replace("{" + arg2.Key + "}", arg2.Value);
				}
			}
			return $"时间:{time} 参数:{text} 描述:{text2}";
		}
		return $"时间:{time} 参数:{text}";
	}
}
