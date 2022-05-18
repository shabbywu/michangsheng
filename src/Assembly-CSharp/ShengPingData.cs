using System;
using System.Collections.Generic;
using JSONClass;

// Token: 0x020002DB RID: 731
public class ShengPingData : IComparable
{
	// Token: 0x06001604 RID: 5636 RVA: 0x0000403D File Offset: 0x0000223D
	public ShengPingData()
	{
	}

	// Token: 0x06001605 RID: 5637 RVA: 0x000C61E0 File Offset: 0x000C43E0
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

	// Token: 0x06001606 RID: 5638 RVA: 0x000C628C File Offset: 0x000C448C
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

	// Token: 0x06001607 RID: 5639 RVA: 0x000C6334 File Offset: 0x000C4534
	public int CompareTo(object obj)
	{
		ShengPingData shengPingData = obj as ShengPingData;
		if (this.time.CompareTo(shengPingData.time) == 0)
		{
			return this.priority.CompareTo(shengPingData.priority);
		}
		return this.time.CompareTo(shengPingData.time);
	}

	// Token: 0x06001608 RID: 5640 RVA: 0x000C6380 File Offset: 0x000C4580
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

	// Token: 0x040011DB RID: 4571
	public string ID;

	// Token: 0x040011DC RID: 4572
	public int priority;

	// Token: 0x040011DD RID: 4573
	public DateTime time;

	// Token: 0x040011DE RID: 4574
	public Dictionary<string, string> args;
}
