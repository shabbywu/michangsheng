using System;
using System.Collections.Generic;

namespace JSONClass;

public class LianQiEquipIconBiao : IJSONClass
{
	public static Dictionary<int, LianQiEquipIconBiao> DataDict = new Dictionary<int, LianQiEquipIconBiao>();

	public static List<LianQiEquipIconBiao> DataList = new List<LianQiEquipIconBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int zhonglei;

	public int quality;

	public int pingjie;

	public string desc;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.LianQiEquipIconBiao.list)
		{
			try
			{
				LianQiEquipIconBiao lianQiEquipIconBiao = new LianQiEquipIconBiao();
				lianQiEquipIconBiao.id = item["id"].I;
				lianQiEquipIconBiao.zhonglei = item["zhonglei"].I;
				lianQiEquipIconBiao.quality = item["quality"].I;
				lianQiEquipIconBiao.pingjie = item["pingjie"].I;
				lianQiEquipIconBiao.desc = item["desc"].Str;
				if (DataDict.ContainsKey(lianQiEquipIconBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典LianQiEquipIconBiao.DataDict添加数据时出现重复的键，Key:{lianQiEquipIconBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(lianQiEquipIconBiao.id, lianQiEquipIconBiao);
				DataList.Add(lianQiEquipIconBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典LianQiEquipIconBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
