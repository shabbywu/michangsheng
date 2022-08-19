using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020001CD RID: 461
public static class SceneEx
{
	// Token: 0x06001337 RID: 4919 RVA: 0x00078C60 File Offset: 0x00076E60
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

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06001338 RID: 4920 RVA: 0x00078E58 File Offset: 0x00077058
	public static string NowSceneName
	{
		get
		{
			return SceneManager.GetActiveScene().name;
		}
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x00078E74 File Offset: 0x00077074
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

	// Token: 0x0600133A RID: 4922 RVA: 0x00078F3C File Offset: 0x0007713C
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

	// Token: 0x0600133B RID: 4923 RVA: 0x0007900C File Offset: 0x0007720C
	public static void LoadFuBen(string fubenName, int pos)
	{
		Fungus.LoadFuBen.loadfuben(fubenName, pos);
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x00079018 File Offset: 0x00077218
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

	// Token: 0x0600133D RID: 4925 RVA: 0x00079098 File Offset: 0x00077298
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

	// Token: 0x04000E88 RID: 3720
	private static bool isInited;

	// Token: 0x04000E89 RID: 3721
	private static Dictionary<string, int> _SceneTypeDict = new Dictionary<string, int>();

	// Token: 0x04000E8A RID: 3722
	private static Dictionary<int, List<int>> _SceneItemFlagType = new Dictionary<int, List<int>>();

	// Token: 0x04000E8B RID: 3723
	private static Dictionary<int, List<int>> _SceneItemPercent = new Dictionary<int, List<int>>();
}
