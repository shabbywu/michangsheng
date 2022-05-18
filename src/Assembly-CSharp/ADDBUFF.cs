using System;
using System.Collections.Generic;
using System.Diagnostics;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class ADDBUFF : MonoBehaviour
{
	// Token: 0x060011F3 RID: 4595 RVA: 0x000042DD File Offset: 0x000024DD
	private void Start()
	{
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x000AD160 File Offset: 0x000AB360
	public void Test()
	{
		Avatar player = Tools.instance.getPlayer();
		int itemID = 2;
		JSONObject jsonobject = Tools.CreateItemSeid(itemID);
		jsonobject.AddField("SkillSeids", new JSONObject(JSONObject.Type.ARRAY));
		jsonobject["SkillSeids"].Add(this.AddItemSeid(29, 1, -9999));
		jsonobject["SkillSeids"].Add(this.AddItemSeid(29, 1, -9999));
		player.addItem(itemID, 1, jsonobject, false);
	}

	// Token: 0x060011F5 RID: 4597 RVA: 0x000AD1D8 File Offset: 0x000AB3D8
	private JSONObject AddItemSeid(int seid, int value1 = -9999, int value2 = -9999)
	{
		JSONObject jsonobject = new JSONObject();
		jsonobject.SetField("id", seid);
		if (value1 != -9999)
		{
			jsonobject.SetField("value1", value1);
		}
		if (value2 != -9999)
		{
			jsonobject.SetField("value2", value2);
		}
		return jsonobject;
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x000AD220 File Offset: 0x000AB420
	public void SSS(JObject aa, string ID)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		int num = (int)aa[ID];
		stopwatch.Stop();
		TimeSpan elapsed = stopwatch.Elapsed;
		Debug.Log(string.Concat(new object[]
		{
			num,
			"--SSS",
			ID,
			"--",
			elapsed.TotalMilliseconds
		}));
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x000AD290 File Offset: 0x000AB490
	public void SSSL(List<int> aa, int ID)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		int num = aa[ID];
		stopwatch.Stop();
		TimeSpan elapsed = stopwatch.Elapsed;
		Debug.Log(string.Concat(new object[]
		{
			num,
			"--SSSL",
			ID,
			"--",
			elapsed.TotalMilliseconds
		}));
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x000AD300 File Offset: 0x000AB500
	public void SSSA(Dictionary<string, int> aa, string ID)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		int num = aa[ID];
		stopwatch.Stop();
		TimeSpan elapsed = stopwatch.Elapsed;
		Debug.Log(string.Concat(new object[]
		{
			num,
			"--SSSA",
			ID,
			"--",
			elapsed.TotalMilliseconds
		}));
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x000042DD File Offset: 0x000024DD
	private void OnGUI()
	{
	}

	// Token: 0x04000E80 RID: 3712
	public int buff;

	// Token: 0x04000E81 RID: 3713
	public int BuffNum = 1;

	// Token: 0x04000E82 RID: 3714
	public int skillID = 1;
}
