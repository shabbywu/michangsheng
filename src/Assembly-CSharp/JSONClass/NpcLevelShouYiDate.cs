using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcLevelShouYiDate : IJSONClass
{
	public static Dictionary<int, NpcLevelShouYiDate> DataDict = new Dictionary<int, NpcLevelShouYiDate>();

	public static List<NpcLevelShouYiDate> DataList = new List<NpcLevelShouYiDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int money;

	public int gongxian;

	public int fabao;

	public int wudaoexp;

	public int ZengLi;

	public int jieshapanduan;

	public int siwangjilv;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcLevelShouYiDate.list)
		{
			try
			{
				NpcLevelShouYiDate npcLevelShouYiDate = new NpcLevelShouYiDate();
				npcLevelShouYiDate.id = item["id"].I;
				npcLevelShouYiDate.money = item["money"].I;
				npcLevelShouYiDate.gongxian = item["gongxian"].I;
				npcLevelShouYiDate.fabao = item["fabao"].I;
				npcLevelShouYiDate.wudaoexp = item["wudaoexp"].I;
				npcLevelShouYiDate.ZengLi = item["ZengLi"].I;
				npcLevelShouYiDate.jieshapanduan = item["jieshapanduan"].I;
				npcLevelShouYiDate.siwangjilv = item["siwangjilv"].I;
				if (DataDict.ContainsKey(npcLevelShouYiDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcLevelShouYiDate.DataDict添加数据时出现重复的键，Key:{npcLevelShouYiDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcLevelShouYiDate.id, npcLevelShouYiDate);
				DataList.Add(npcLevelShouYiDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcLevelShouYiDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
