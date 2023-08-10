using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

[CommandInfo("YSNPCJiaoHu", "获取当前地图类型", "获取当前地图类型，赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetMapType : Command
{
	private static bool isInited;

	private static Dictionary<string, int> MapTypeDict = new Dictionary<string, int>();

	private static void Init()
	{
		if (isInited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.SceneNameJsonData.list)
		{
			MapTypeDict.Add(item["id"].str, item["MapType"].I);
		}
		isInited = true;
	}

	public override void OnEnter()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		Init();
		Scene activeScene = SceneManager.GetActiveScene();
		string name = ((Scene)(ref activeScene)).name;
		if (MapTypeDict.ContainsKey(name))
		{
			GetFlowchart().SetIntegerVariable("TmpValue", MapTypeDict[name]);
		}
		else
		{
			GetFlowchart().SetIntegerVariable("TmpValue", -1);
			Debug.LogError((object)("fungus尝试获取没有配置的地图类型，当前场景名" + name + "，请检查配表"));
		}
		Continue();
	}
}
