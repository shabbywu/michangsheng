using System;
using System.Collections.Generic;

namespace JSONClass;

public class MenPaiFengLuBiao : IJSONClass
{
	public static Dictionary<int, MenPaiFengLuBiao> DataDict = new Dictionary<int, MenPaiFengLuBiao>();

	public static List<MenPaiFengLuBiao> DataList = new List<MenPaiFengLuBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int MenKan;

	public int CD;

	public int money;

	public string Name;

	public List<int> RenWu = new List<int>();

	public List<int> haogandu = new List<int>();

	public List<int> addMoney = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.MenPaiFengLuBiao.list)
		{
			try
			{
				MenPaiFengLuBiao menPaiFengLuBiao = new MenPaiFengLuBiao();
				menPaiFengLuBiao.id = item["id"].I;
				menPaiFengLuBiao.MenKan = item["MenKan"].I;
				menPaiFengLuBiao.CD = item["CD"].I;
				menPaiFengLuBiao.money = item["money"].I;
				menPaiFengLuBiao.Name = item["Name"].Str;
				menPaiFengLuBiao.RenWu = item["RenWu"].ToList();
				menPaiFengLuBiao.haogandu = item["haogandu"].ToList();
				menPaiFengLuBiao.addMoney = item["addMoney"].ToList();
				if (DataDict.ContainsKey(menPaiFengLuBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典MenPaiFengLuBiao.DataDict添加数据时出现重复的键，Key:{menPaiFengLuBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(menPaiFengLuBiao.id, menPaiFengLuBiao);
				DataList.Add(menPaiFengLuBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典MenPaiFengLuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
