using System;
using System.Collections.Generic;

namespace JSONClass;

public class LevelUpDataJsonData : IJSONClass
{
	public static Dictionary<int, LevelUpDataJsonData> DataDict = new Dictionary<int, LevelUpDataJsonData>();

	public static List<LevelUpDataJsonData> DataList = new List<LevelUpDataJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int level;

	public int AddHp;

	public int AddShenShi;

	public int AddDunSu;

	public int AddShouYuan;

	public int MaxExp;

	public int wudaodian;

	public string Name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LevelUpDataJsonData.list)
		{
			try
			{
				LevelUpDataJsonData levelUpDataJsonData = new LevelUpDataJsonData();
				levelUpDataJsonData.id = item["id"].I;
				levelUpDataJsonData.level = item["level"].I;
				levelUpDataJsonData.AddHp = item["AddHp"].I;
				levelUpDataJsonData.AddShenShi = item["AddShenShi"].I;
				levelUpDataJsonData.AddDunSu = item["AddDunSu"].I;
				levelUpDataJsonData.AddShouYuan = item["AddShouYuan"].I;
				levelUpDataJsonData.MaxExp = item["MaxExp"].I;
				levelUpDataJsonData.wudaodian = item["wudaodian"].I;
				levelUpDataJsonData.Name = item["Name"].Str;
				if (DataDict.ContainsKey(levelUpDataJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LevelUpDataJsonData.DataDict添加数据时出现重复的键，Key:{levelUpDataJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(levelUpDataJsonData.id, levelUpDataJsonData);
				DataList.Add(levelUpDataJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LevelUpDataJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
