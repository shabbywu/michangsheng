using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000236 RID: 566
[CommandInfo("YSNPCJiaoHu", "获取当前地图类型", "获取当前地图类型，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetMapType : Command
{
	// Token: 0x06001610 RID: 5648 RVA: 0x00095678 File Offset: 0x00093878
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

	// Token: 0x06001611 RID: 5649 RVA: 0x00095704 File Offset: 0x00093904
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

	// Token: 0x04001064 RID: 4196
	private static bool isInited;

	// Token: 0x04001065 RID: 4197
	private static Dictionary<string, int> MapTypeDict = new Dictionary<string, int>();
}
