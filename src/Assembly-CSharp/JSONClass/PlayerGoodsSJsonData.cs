using System;
using System.Collections.Generic;

namespace JSONClass;

public class PlayerGoodsSJsonData : IJSONClass
{
	public static Dictionary<int, PlayerGoodsSJsonData> DataDict = new Dictionary<int, PlayerGoodsSJsonData>();

	public static List<PlayerGoodsSJsonData> DataList = new List<PlayerGoodsSJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int itemStack;

	public int onlyOne;

	public string script;

	public string name;

	public string type;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.PlayerGoodsSJsonData.list)
		{
			try
			{
				PlayerGoodsSJsonData playerGoodsSJsonData = new PlayerGoodsSJsonData();
				playerGoodsSJsonData.id = item["id"].I;
				playerGoodsSJsonData.itemStack = item["itemStack"].I;
				playerGoodsSJsonData.onlyOne = item["onlyOne"].I;
				playerGoodsSJsonData.script = item["script"].Str;
				playerGoodsSJsonData.name = item["name"].Str;
				playerGoodsSJsonData.type = item["type"].Str;
				if (DataDict.ContainsKey(playerGoodsSJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典PlayerGoodsSJsonData.DataDict添加数据时出现重复的键，Key:{playerGoodsSJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(playerGoodsSJsonData.id, playerGoodsSJsonData);
				DataList.Add(playerGoodsSJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典PlayerGoodsSJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
				PreloadManager.LogException($"异常信息:\n{arg}");
				PreloadManager.LogException($"数据序列化:\n{item}");
			}
		}
		if (OnInitFinishAction != null)
		{
			OnInitFinishAction();
		}
	}

	private static void OnInitFinish()
	{
	}
}
