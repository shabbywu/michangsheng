using System;
using System.Collections.Generic;

namespace JSONClass;

public class NPCImportantDate : IJSONClass
{
	public static Dictionary<int, NPCImportantDate> DataDict = new Dictionary<int, NPCImportantDate>();

	public static List<NPCImportantDate> DataList = new List<NPCImportantDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int LiuPai;

	public int level;

	public int sex;

	public int zizhi;

	public int wuxing;

	public int nianling;

	public int XingGe;

	public int ChengHao;

	public int NPCTag;

	public int DaShiXiong;

	public int ZhangMeng;

	public int ZhangLao;

	public string ZhuJiTime;

	public string JinDanTime;

	public string YuanYingTime;

	public string HuaShengTime;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NPCImportantDate.list)
		{
			try
			{
				NPCImportantDate nPCImportantDate = new NPCImportantDate();
				nPCImportantDate.id = item["id"].I;
				nPCImportantDate.LiuPai = item["LiuPai"].I;
				nPCImportantDate.level = item["level"].I;
				nPCImportantDate.sex = item["sex"].I;
				nPCImportantDate.zizhi = item["zizhi"].I;
				nPCImportantDate.wuxing = item["wuxing"].I;
				nPCImportantDate.nianling = item["nianling"].I;
				nPCImportantDate.XingGe = item["XingGe"].I;
				nPCImportantDate.ChengHao = item["ChengHao"].I;
				nPCImportantDate.NPCTag = item["NPCTag"].I;
				nPCImportantDate.DaShiXiong = item["DaShiXiong"].I;
				nPCImportantDate.ZhangMeng = item["ZhangMeng"].I;
				nPCImportantDate.ZhangLao = item["ZhangLao"].I;
				nPCImportantDate.ZhuJiTime = item["ZhuJiTime"].Str;
				nPCImportantDate.JinDanTime = item["JinDanTime"].Str;
				nPCImportantDate.YuanYingTime = item["YuanYingTime"].Str;
				nPCImportantDate.HuaShengTime = item["HuaShengTime"].Str;
				if (DataDict.ContainsKey(nPCImportantDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NPCImportantDate.DataDict添加数据时出现重复的键，Key:{nPCImportantDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nPCImportantDate.id, nPCImportantDate);
				DataList.Add(nPCImportantDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NPCImportantDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
