using System;
using System.Collections.Generic;
using System.Diagnostics;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ADDBUFF : MonoBehaviour
{
	public int buff;

	public int BuffNum = 1;

	public int skillID = 1;

	private void Start()
	{
	}

	public void Test()
	{
		Avatar player = Tools.instance.getPlayer();
		int itemID = 2;
		JSONObject jSONObject = Tools.CreateItemSeid(itemID);
		jSONObject.AddField("SkillSeids", new JSONObject(JSONObject.Type.ARRAY));
		jSONObject["SkillSeids"].Add(AddItemSeid(29, 1));
		jSONObject["SkillSeids"].Add(AddItemSeid(29, 1));
		player.addItem(itemID, 1, jSONObject);
	}

	private JSONObject AddItemSeid(int seid, int value1 = -9999, int value2 = -9999)
	{
		JSONObject jSONObject = new JSONObject();
		jSONObject.SetField("id", seid);
		if (value1 != -9999)
		{
			jSONObject.SetField("value1", value1);
		}
		if (value2 != -9999)
		{
			jSONObject.SetField("value2", value2);
		}
		return jSONObject;
	}

	public void SSS(JObject aa, string ID)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		int num = (int)aa[ID];
		stopwatch.Stop();
		TimeSpan elapsed = stopwatch.Elapsed;
		Debug.Log((object)(num + "--SSS" + ID + "--" + elapsed.TotalMilliseconds));
	}

	public void SSSL(List<int> aa, int ID)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		int num = aa[ID];
		stopwatch.Stop();
		TimeSpan elapsed = stopwatch.Elapsed;
		Debug.Log((object)(num + "--SSSL" + ID + "--" + elapsed.TotalMilliseconds));
	}

	public void SSSA(Dictionary<string, int> aa, string ID)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		int num = aa[ID];
		stopwatch.Stop();
		TimeSpan elapsed = stopwatch.Elapsed;
		Debug.Log((object)(num + "--SSSA" + ID + "--" + elapsed.TotalMilliseconds));
	}

	private void OnGUI()
	{
	}
}
