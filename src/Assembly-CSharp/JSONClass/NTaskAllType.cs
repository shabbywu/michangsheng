using System;
using System.Collections.Generic;

namespace JSONClass;

public class NTaskAllType : IJSONClass
{
	public static Dictionary<int, NTaskAllType> DataDict = new Dictionary<int, NTaskAllType>();

	public static List<NTaskAllType> DataList = new List<NTaskAllType>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int Id;

	public int CD;

	public int shili;

	public int GeRen;

	public int menpaihuobi;

	public int Type;

	public string name;

	public string ZongMiaoShu;

	public string jiaofurenwu;

	public string jiaofudidian;

	public List<int> XiangXiID = new List<int>();

	public List<int> seid = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.NTaskAllType.list)
		{
			try
			{
				NTaskAllType nTaskAllType = new NTaskAllType();
				nTaskAllType.Id = item["Id"].I;
				nTaskAllType.CD = item["CD"].I;
				nTaskAllType.shili = item["shili"].I;
				nTaskAllType.GeRen = item["GeRen"].I;
				nTaskAllType.menpaihuobi = item["menpaihuobi"].I;
				nTaskAllType.Type = item["Type"].I;
				nTaskAllType.name = item["name"].Str;
				nTaskAllType.ZongMiaoShu = item["ZongMiaoShu"].Str;
				nTaskAllType.jiaofurenwu = item["jiaofurenwu"].Str;
				nTaskAllType.jiaofudidian = item["jiaofudidian"].Str;
				nTaskAllType.XiangXiID = item["XiangXiID"].ToList();
				nTaskAllType.seid = item["seid"].ToList();
				if (DataDict.ContainsKey(nTaskAllType.Id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典NTaskAllType.DataDict添加数据时出现重复的键，Key:{nTaskAllType.Id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(nTaskAllType.Id, nTaskAllType);
				DataList.Add(nTaskAllType);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典NTaskAllType.DataDict添加数据时出现异常，已跳过，请检查配表");
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
