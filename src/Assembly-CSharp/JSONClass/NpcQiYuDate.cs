using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcQiYuDate : IJSONClass
{
	public static Dictionary<int, NpcQiYuDate> DataDict = new Dictionary<int, NpcQiYuDate>();

	public static List<NpcQiYuDate> DataList = new List<NpcQiYuDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int quanzhong;

	public int ZhuangTai;

	public int Itemnum;

	public int XiuWei;

	public int XueLiang;

	public string QiYuInfo;

	public List<int> JingJie = new List<int>();

	public List<int> Item = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcQiYuDate.list)
		{
			try
			{
				NpcQiYuDate npcQiYuDate = new NpcQiYuDate();
				npcQiYuDate.id = item["id"].I;
				npcQiYuDate.quanzhong = item["quanzhong"].I;
				npcQiYuDate.ZhuangTai = item["ZhuangTai"].I;
				npcQiYuDate.Itemnum = item["Itemnum"].I;
				npcQiYuDate.XiuWei = item["XiuWei"].I;
				npcQiYuDate.XueLiang = item["XueLiang"].I;
				npcQiYuDate.QiYuInfo = item["QiYuInfo"].Str;
				npcQiYuDate.JingJie = item["JingJie"].ToList();
				npcQiYuDate.Item = item["Item"].ToList();
				if (DataDict.ContainsKey(npcQiYuDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcQiYuDate.DataDict添加数据时出现重复的键，Key:{npcQiYuDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcQiYuDate.id, npcQiYuDate);
				DataList.Add(npcQiYuDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcQiYuDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
