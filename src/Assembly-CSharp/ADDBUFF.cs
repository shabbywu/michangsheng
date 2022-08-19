using System;
using System.Collections.Generic;
using System.Diagnostics;
using KBEngine;
using Newtonsoft.Json.Linq;
using UnityEngine;

// Token: 0x0200016F RID: 367
public class ADDBUFF : MonoBehaviour
{
	// Token: 0x06000F95 RID: 3989 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x0005D688 File Offset: 0x0005B888
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

	// Token: 0x06000F97 RID: 3991 RVA: 0x0005D700 File Offset: 0x0005B900
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

	// Token: 0x06000F98 RID: 3992 RVA: 0x0005D748 File Offset: 0x0005B948
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

	// Token: 0x06000F99 RID: 3993 RVA: 0x0005D7B8 File Offset: 0x0005B9B8
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

	// Token: 0x06000F9A RID: 3994 RVA: 0x0005D828 File Offset: 0x0005BA28
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

	// Token: 0x06000F9B RID: 3995 RVA: 0x00004095 File Offset: 0x00002295
	private void OnGUI()
	{
	}

	// Token: 0x04000BB0 RID: 2992
	public int buff;

	// Token: 0x04000BB1 RID: 2993
	public int BuffNum = 1;

	// Token: 0x04000BB2 RID: 2994
	public int skillID = 1;
}
