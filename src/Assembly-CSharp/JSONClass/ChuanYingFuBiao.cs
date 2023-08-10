using System;
using System.Collections.Generic;

namespace JSONClass;

public class ChuanYingFuBiao : IJSONClass
{
	public static Dictionary<int, ChuanYingFuBiao> DataDict = new Dictionary<int, ChuanYingFuBiao>();

	public static List<ChuanYingFuBiao> DataList = new List<ChuanYingFuBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int AvatarID;

	public int Type;

	public int TaskID;

	public int WeiTuo;

	public int ItemID;

	public int SPvalueID;

	public int HaoGanDu;

	public int IsOnly;

	public int IsAdd;

	public int IsDelete;

	public int IsAlive;

	public string info;

	public string StarTime;

	public string EndTime;

	public string fuhao;

	public List<int> DelayTime = new List<int>();

	public List<int> TaskIndex = new List<int>();

	public List<int> valueID = new List<int>();

	public List<int> value = new List<int>();

	public List<int> Level = new List<int>();

	public List<int> EventValue = new List<int>();

	public List<int> NPCLevel = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.ChuanYingFuBiao.list)
		{
			try
			{
				ChuanYingFuBiao chuanYingFuBiao = new ChuanYingFuBiao();
				chuanYingFuBiao.id = item["id"].I;
				chuanYingFuBiao.AvatarID = item["AvatarID"].I;
				chuanYingFuBiao.Type = item["Type"].I;
				chuanYingFuBiao.TaskID = item["TaskID"].I;
				chuanYingFuBiao.WeiTuo = item["WeiTuo"].I;
				chuanYingFuBiao.ItemID = item["ItemID"].I;
				chuanYingFuBiao.SPvalueID = item["SPvalueID"].I;
				chuanYingFuBiao.HaoGanDu = item["HaoGanDu"].I;
				chuanYingFuBiao.IsOnly = item["IsOnly"].I;
				chuanYingFuBiao.IsAdd = item["IsAdd"].I;
				chuanYingFuBiao.IsDelete = item["IsDelete"].I;
				chuanYingFuBiao.IsAlive = item["IsAlive"].I;
				chuanYingFuBiao.info = item["info"].Str;
				chuanYingFuBiao.StarTime = item["StarTime"].Str;
				chuanYingFuBiao.EndTime = item["EndTime"].Str;
				chuanYingFuBiao.fuhao = item["fuhao"].Str;
				chuanYingFuBiao.DelayTime = item["DelayTime"].ToList();
				chuanYingFuBiao.TaskIndex = item["TaskIndex"].ToList();
				chuanYingFuBiao.valueID = item["valueID"].ToList();
				chuanYingFuBiao.value = item["value"].ToList();
				chuanYingFuBiao.Level = item["Level"].ToList();
				chuanYingFuBiao.EventValue = item["EventValue"].ToList();
				chuanYingFuBiao.NPCLevel = item["NPCLevel"].ToList();
				if (DataDict.ContainsKey(chuanYingFuBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典ChuanYingFuBiao.DataDict添加数据时出现重复的键，Key:{chuanYingFuBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(chuanYingFuBiao.id, chuanYingFuBiao);
				DataList.Add(chuanYingFuBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典ChuanYingFuBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
