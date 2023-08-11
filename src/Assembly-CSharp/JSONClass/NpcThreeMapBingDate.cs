using System;
using System.Collections.Generic;

namespace JSONClass;

public class NpcThreeMapBingDate : IJSONClass
{
	public static Dictionary<int, NpcThreeMapBingDate> DataDict = new Dictionary<int, NpcThreeMapBingDate>();

	public static List<NpcThreeMapBingDate> DataList = new List<NpcThreeMapBingDate>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public List<int> LianDan = new List<int>();

	public List<int> LianQi = new List<int>();

	public List<int> CaiJi = new List<int>();

	public List<int> CaiKuang = new List<int>();

	public List<int> MiJi = new List<int>();

	public List<int> FaBao = new List<int>();

	public List<int> GuangChang = new List<int>();

	public List<int> DaDian = new List<int>();

	public List<int> DongShiGuFangShi = new List<int>();

	public List<int> TianXingChengFangShi = new List<int>();

	public List<int> HaiShangFangShi = new List<int>();

	public List<int> DongShiGuPaiMai = new List<int>();

	public List<int> TianJiGePaiMai = new List<int>();

	public List<int> HaiShangPaiMai = new List<int>();

	public List<int> NanYaChengPaiMai = new List<int>();

	public List<int> YaoDian = new List<int>();

	public List<int> GangKou = new List<int>();

	public List<int> DongFu = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NpcThreeMapBingDate.list)
		{
			try
			{
				NpcThreeMapBingDate npcThreeMapBingDate = new NpcThreeMapBingDate();
				npcThreeMapBingDate.id = item["id"].I;
				npcThreeMapBingDate.LianDan = item["LianDan"].ToList();
				npcThreeMapBingDate.LianQi = item["LianQi"].ToList();
				npcThreeMapBingDate.CaiJi = item["CaiJi"].ToList();
				npcThreeMapBingDate.CaiKuang = item["CaiKuang"].ToList();
				npcThreeMapBingDate.MiJi = item["MiJi"].ToList();
				npcThreeMapBingDate.FaBao = item["FaBao"].ToList();
				npcThreeMapBingDate.GuangChang = item["GuangChang"].ToList();
				npcThreeMapBingDate.DaDian = item["DaDian"].ToList();
				npcThreeMapBingDate.DongShiGuFangShi = item["DongShiGuFangShi"].ToList();
				npcThreeMapBingDate.TianXingChengFangShi = item["TianXingChengFangShi"].ToList();
				npcThreeMapBingDate.HaiShangFangShi = item["HaiShangFangShi"].ToList();
				npcThreeMapBingDate.DongShiGuPaiMai = item["DongShiGuPaiMai"].ToList();
				npcThreeMapBingDate.TianJiGePaiMai = item["TianJiGePaiMai"].ToList();
				npcThreeMapBingDate.HaiShangPaiMai = item["HaiShangPaiMai"].ToList();
				npcThreeMapBingDate.NanYaChengPaiMai = item["NanYaChengPaiMai"].ToList();
				npcThreeMapBingDate.YaoDian = item["YaoDian"].ToList();
				npcThreeMapBingDate.GangKou = item["GangKou"].ToList();
				npcThreeMapBingDate.DongFu = item["DongFu"].ToList();
				if (DataDict.ContainsKey(npcThreeMapBingDate.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NpcThreeMapBingDate.DataDict添加数据时出现重复的键，Key:{npcThreeMapBingDate.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(npcThreeMapBingDate.id, npcThreeMapBingDate);
				DataList.Add(npcThreeMapBingDate);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NpcThreeMapBingDate.DataDict添加数据时出现异常，已跳过，请检查配表");
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
