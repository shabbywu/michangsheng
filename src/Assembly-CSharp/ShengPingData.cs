using System;
using System.Collections.Generic;
using JSONClass;

// Token: 0x020001D0 RID: 464
public class ShengPingData : IComparable
{
	// Token: 0x06001348 RID: 4936 RVA: 0x000027FC File Offset: 0x000009FC
	public ShengPingData()
	{
	}

	// Token: 0x06001349 RID: 4937 RVA: 0x0007948C File Offset: 0x0007768C
	public ShengPingData(JSONObject json)
	{
		this.time = DateTime.Parse(json["time"].str);
		if (json.HasField("args"))
		{
			this.args = new Dictionary<string, string>();
			JSONObject jsonobject = json["args"];
			foreach (string text in jsonobject.keys)
			{
				this.args[text] = jsonobject[text].Str;
			}
		}
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x00079538 File Offset: 0x00077738
	public JSONObject ToJson()
	{
		JSONObject jsonobject = new JSONObject(JSONObject.Type.OBJECT);
		jsonobject.SetField("time", this.time.ToString());
		if (this.args != null && this.args.Count > 0)
		{
			JSONObject jsonobject2 = new JSONObject(JSONObject.Type.OBJECT);
			foreach (KeyValuePair<string, string> keyValuePair in this.args)
			{
				jsonobject2.SetField(keyValuePair.Key, keyValuePair.Value);
			}
			jsonobject.SetField("args", jsonobject2);
		}
		return jsonobject;
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x000795E0 File Offset: 0x000777E0
	public int CompareTo(object obj)
	{
		ShengPingData shengPingData = obj as ShengPingData;
		if (this.time.CompareTo(shengPingData.time) == 0)
		{
			return this.priority.CompareTo(shengPingData.priority);
		}
		return this.time.CompareTo(shengPingData.time);
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x0007962C File Offset: 0x0007782C
	public override string ToString()
	{
		string text = "null";
		if (this.args != null && this.args.Count > 0)
		{
			text = "";
			foreach (KeyValuePair<string, string> keyValuePair in this.args)
			{
				text = string.Concat(new string[]
				{
					text,
					keyValuePair.Key,
					":",
					keyValuePair.Value,
					" "
				});
			}
		}
		if (string.IsNullOrWhiteSpace(this.ID))
		{
			return string.Format("时间:{0} 参数:{1}", this.time, text);
		}
		if (ShengPing.DataDict.ContainsKey(this.ID))
		{
			string text2 = ShengPing.DataDict[this.ID].descr;
			if (this.args != null && this.args.Count > 0)
			{
				foreach (KeyValuePair<string, string> keyValuePair2 in this.args)
				{
					text2 = text2.Replace("{" + keyValuePair2.Key + "}", keyValuePair2.Value);
				}
			}
			return string.Format("时间:{0} 参数:{1} 描述:{2}", this.time, text, text2);
		}
		return string.Format("时间:{0} 参数:{1}", this.time, text);
	}

	// Token: 0x04000E9C RID: 3740
	public string ID;

	// Token: 0x04000E9D RID: 3741
	public int priority;

	// Token: 0x04000E9E RID: 3742
	public DateTime time;

	// Token: 0x04000E9F RID: 3743
	public Dictionary<string, string> args;
}
