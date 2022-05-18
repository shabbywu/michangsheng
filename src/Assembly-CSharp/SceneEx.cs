using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002D8 RID: 728
public static class SceneEx
{
	// Token: 0x060015F3 RID: 5619 RVA: 0x000C5A04 File Offset: 0x000C3C04
	private static void Init()
	{
		if (!SceneEx.isInited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.SceneNameJsonData.list)
			{
				SceneEx._SceneTypeDict.TryAdd(jsonobject["id"].str, jsonobject["MoneyType"].I, "");
			}
			SceneEx._SceneItemFlagType.Add(0, new List<int>());
			SceneEx._SceneItemPercent.Add(0, new List<int>());
			foreach (JSONObject jsonobject2 in jsonData.instance.ScenePriceData.list)
			{
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				foreach (JSONObject jsonobject3 in jsonobject2["ItemFlag"].list)
				{
					list.Add(jsonobject3.I);
				}
				foreach (JSONObject jsonobject4 in jsonobject2["percent"].list)
				{
					list2.Add(jsonobject4.I);
				}
				SceneEx._SceneItemFlagType.TryAdd(jsonobject2["id"].I, list, "");
				SceneEx._SceneItemPercent.TryAdd(jsonobject2["id"].I, list2, "");
			}
			SceneEx.isInited = true;
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000C5BFC File Offset: 0x000C3DFC
	public static string NowSceneName
	{
		get
		{
			return SceneManager.GetActiveScene().name;
		}
	}

	// Token: 0x060015F5 RID: 5621 RVA: 0x000C5C18 File Offset: 0x000C3E18
	public static int GetNowSceneType()
	{
		SceneEx.Init();
		if (SceneEx._SceneTypeDict.ContainsKey(SceneEx.NowSceneName))
		{
			return SceneEx._SceneTypeDict[SceneEx.NowSceneName];
		}
		if (!(SceneEx.NowSceneName == "FRandomBase"))
		{
			Debug.LogError("获取场景类型出错，此场景" + SceneEx.NowSceneName + "没有在配表数据中，需要反馈");
			return 0;
		}
		if (SceneEx._SceneTypeDict.ContainsKey(PlayerEx.Player.lastFuBenScence))
		{
			return SceneEx._SceneTypeDict[PlayerEx.Player.lastFuBenScence];
		}
		Debug.LogError(string.Concat(new string[]
		{
			"获取场景类型出错，此随机场景",
			SceneEx.NowSceneName,
			"的出口",
			PlayerEx.Player.lastFuBenScence,
			"没有在配表数据中，需要反馈"
		}));
		return 0;
	}

	// Token: 0x060015F6 RID: 5622 RVA: 0x000C5CE0 File Offset: 0x000C3EE0
	public static int ItemNowSceneJiaCheng(int itemid)
	{
		int result = 0;
		int nowSceneType = SceneEx.GetNowSceneType();
		JSONObject jsonobject = itemid.ItemJson();
		if (!jsonobject["ItemFlag"].IsNull)
		{
			foreach (JSONObject jsonobject2 in jsonobject["ItemFlag"].list)
			{
				for (int i = 0; i < SceneEx._SceneItemFlagType[nowSceneType].Count; i++)
				{
					if (SceneEx._SceneItemFlagType[nowSceneType][i] == jsonobject2.I)
					{
						return SceneEx._SceneItemPercent[nowSceneType][i];
					}
				}
			}
			return result;
		}
		return result;
	}

	// Token: 0x060015F7 RID: 5623 RVA: 0x00013B76 File Offset: 0x00011D76
	public static void LoadFuBen(string fubenName, int pos)
	{
		Fungus.LoadFuBen.loadfuben(fubenName, pos);
	}

	// Token: 0x060015F8 RID: 5624 RVA: 0x000C5DB0 File Offset: 0x000C3FB0
	public static void CloseYSFight()
	{
		foreach (GameObject gameObject in SceneManager.GetActiveScene().GetRootGameObjects())
		{
			if (gameObject.name != "Main Camera" && gameObject.name != "EventSystem" && gameObject != null)
			{
				try
				{
					gameObject.SetActive(false);
				}
				catch
				{
					Debug.Log("异常");
				}
			}
		}
	}

	// Token: 0x060015F9 RID: 5625 RVA: 0x000C5E30 File Offset: 0x000C4030
	public static MapArea GetNowMapArea()
	{
		SceneEx.Init();
		int nowSceneType = SceneEx.GetNowSceneType();
		if (nowSceneType == 1)
		{
			return MapArea.NingZhou;
		}
		if (nowSceneType == 2)
		{
			return MapArea.Sea;
		}
		if (nowSceneType == 3)
		{
			return MapArea.Sea;
		}
		return MapArea.Unknow;
	}

	// Token: 0x040011C7 RID: 4551
	private static bool isInited;

	// Token: 0x040011C8 RID: 4552
	private static Dictionary<string, int> _SceneTypeDict = new Dictionary<string, int>();

	// Token: 0x040011C9 RID: 4553
	private static Dictionary<int, List<int>> _SceneItemFlagType = new Dictionary<int, List<int>>();

	// Token: 0x040011CA RID: 4554
	private static Dictionary<int, List<int>> _SceneItemPercent = new Dictionary<int, List<int>>();
}
