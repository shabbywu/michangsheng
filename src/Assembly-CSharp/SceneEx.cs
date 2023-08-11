using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneEx
{
	private static bool isInited;

	private static Dictionary<string, int> _SceneTypeDict = new Dictionary<string, int>();

	private static Dictionary<int, List<int>> _SceneItemFlagType = new Dictionary<int, List<int>>();

	private static Dictionary<int, List<int>> _SceneItemPercent = new Dictionary<int, List<int>>();

	public static string NowSceneName
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			Scene activeScene = SceneManager.GetActiveScene();
			return ((Scene)(ref activeScene)).name;
		}
	}

	private static void Init()
	{
		if (isInited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.SceneNameJsonData.list)
		{
			ToolsEx.TryAdd(_SceneTypeDict, item["id"].str, item["MoneyType"].I);
		}
		_SceneItemFlagType.Add(0, new List<int>());
		_SceneItemPercent.Add(0, new List<int>());
		foreach (JSONObject item2 in jsonData.instance.ScenePriceData.list)
		{
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			foreach (JSONObject item3 in item2["ItemFlag"].list)
			{
				list.Add(item3.I);
			}
			foreach (JSONObject item4 in item2["percent"].list)
			{
				list2.Add(item4.I);
			}
			ToolsEx.TryAdd(_SceneItemFlagType, item2["id"].I, list);
			ToolsEx.TryAdd(_SceneItemPercent, item2["id"].I, list2);
		}
		isInited = true;
	}

	public static int GetNowSceneType()
	{
		Init();
		if (_SceneTypeDict.ContainsKey(NowSceneName))
		{
			return _SceneTypeDict[NowSceneName];
		}
		if (NowSceneName == "FRandomBase")
		{
			if (_SceneTypeDict.ContainsKey(PlayerEx.Player.lastFuBenScence))
			{
				return _SceneTypeDict[PlayerEx.Player.lastFuBenScence];
			}
			Debug.LogError((object)("获取场景类型出错，此随机场景" + NowSceneName + "的出口" + PlayerEx.Player.lastFuBenScence + "没有在配表数据中，需要反馈"));
			return 0;
		}
		Debug.LogError((object)("获取场景类型出错，此场景" + NowSceneName + "没有在配表数据中，需要反馈"));
		return 0;
	}

	public static int ItemNowSceneJiaCheng(int itemid)
	{
		int result = 0;
		int nowSceneType = GetNowSceneType();
		JSONObject jSONObject = itemid.ItemJson();
		if (!jSONObject["ItemFlag"].IsNull)
		{
			foreach (JSONObject item in jSONObject["ItemFlag"].list)
			{
				for (int i = 0; i < _SceneItemFlagType[nowSceneType].Count; i++)
				{
					if (_SceneItemFlagType[nowSceneType][i] == item.I)
					{
						return _SceneItemPercent[nowSceneType][i];
					}
				}
			}
		}
		return result;
	}

	public static void LoadFuBen(string fubenName, int pos)
	{
		Fungus.LoadFuBen.loadfuben(fubenName, pos);
	}

	public static void CloseYSFight()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		Scene activeScene = SceneManager.GetActiveScene();
		GameObject[] rootGameObjects = ((Scene)(ref activeScene)).GetRootGameObjects();
		foreach (GameObject val in rootGameObjects)
		{
			if (((Object)val).name != "Main Camera" && ((Object)val).name != "EventSystem" && (Object)(object)val != (Object)null)
			{
				try
				{
					val.SetActive(false);
				}
				catch
				{
					Debug.Log((object)"异常");
				}
			}
		}
	}

	public static MapArea GetNowMapArea()
	{
		Init();
		return GetNowSceneType() switch
		{
			1 => MapArea.NingZhou, 
			2 => MapArea.Sea, 
			3 => MapArea.Sea, 
			_ => MapArea.Unknow, 
		};
	}
}
