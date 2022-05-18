using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000352 RID: 850
[CommandInfo("YSNPCJiaoHu", "获取当前地图类型", "获取当前地图类型，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetMapType : Command
{
	// Token: 0x060018C8 RID: 6344 RVA: 0x000DDC9C File Offset: 0x000DBE9C
	private static void Init()
	{
		if (!CmdGetMapType.isInited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.SceneNameJsonData.list)
			{
				CmdGetMapType.MapTypeDict.Add(jsonobject["id"].str, jsonobject["MapType"].I);
			}
			CmdGetMapType.isInited = true;
		}
	}

	// Token: 0x060018C9 RID: 6345 RVA: 0x000DDD28 File Offset: 0x000DBF28
	public override void OnEnter()
	{
		CmdGetMapType.Init();
		string name = SceneManager.GetActiveScene().name;
		if (CmdGetMapType.MapTypeDict.ContainsKey(name))
		{
			this.GetFlowchart().SetIntegerVariable("TmpValue", CmdGetMapType.MapTypeDict[name]);
		}
		else
		{
			this.GetFlowchart().SetIntegerVariable("TmpValue", -1);
			Debug.LogError("fungus尝试获取没有配置的地图类型，当前场景名" + name + "，请检查配表");
		}
		this.Continue();
	}

	// Token: 0x040013BC RID: 5052
	private static bool isInited;

	// Token: 0x040013BD RID: 5053
	private static Dictionary<string, int> MapTypeDict = new Dictionary<string, int>();
}
