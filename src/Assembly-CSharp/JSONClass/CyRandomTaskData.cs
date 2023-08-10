using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyRandomTaskData : IJSONClass
{
	public static Dictionary<int, CyRandomTaskData> DataDict = new Dictionary<int, CyRandomTaskData>();

	public static List<CyRandomTaskData> DataList = new List<CyRandomTaskData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int info;

	public int Type;

	public int NPCxingdong;

	public int TaskID;

	public int TaskType;

	public int Taskvalue;

	public int XingWeiType;

	public int ItemID;

	public int ItemNum;

	public int IsZhongYaoNPC;

	public int IsOnly;

	public string StarTime;

	public string EndTime;

	public List<int> DelayTime = new List<int>();

	public List<int> valueID = new List<int>();

	public List<int> value = new List<int>();

	public List<int> Level = new List<int>();

	public List<int> NPCLevel = new List<int>();

	public List<int> NPCXingGe = new List<int>();

	public List<int> NPCType = new List<int>();

	public List<int> NPCLiuPai = new List<int>();

	public List<int> NPCTag = new List<int>();

	public List<int> NPCXingWei = new List<int>();

	public List<int> NPCGuanXi = new List<int>();

	public List<int> NPCGuanXiNot = new List<int>();

	public List<int> HaoGanDu = new List<int>();

	public List<int> WuDaoType = new List<int>();

	public List<int> WuDaoLevel = new List<int>();

	public List<int> EventValue = new List<int>();

	public List<int> fuhao = new List<int>();

	public List<int> EventValueNum = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyRandomTaskData.list)
		{
			try
			{
				CyRandomTaskData cyRandomTaskData = new CyRandomTaskData();
				cyRandomTaskData.id = item["id"].I;
				cyRandomTaskData.info = item["info"].I;
				cyRandomTaskData.Type = item["Type"].I;
				cyRandomTaskData.NPCxingdong = item["NPCxingdong"].I;
				cyRandomTaskData.TaskID = item["TaskID"].I;
				cyRandomTaskData.TaskType = item["TaskType"].I;
				cyRandomTaskData.Taskvalue = item["Taskvalue"].I;
				cyRandomTaskData.XingWeiType = item["XingWeiType"].I;
				cyRandomTaskData.ItemID = item["ItemID"].I;
				cyRandomTaskData.ItemNum = item["ItemNum"].I;
				cyRandomTaskData.IsZhongYaoNPC = item["IsZhongYaoNPC"].I;
				cyRandomTaskData.IsOnly = item["IsOnly"].I;
				cyRandomTaskData.StarTime = item["StarTime"].Str;
				cyRandomTaskData.EndTime = item["EndTime"].Str;
				cyRandomTaskData.DelayTime = item["DelayTime"].ToList();
				cyRandomTaskData.valueID = item["valueID"].ToList();
				cyRandomTaskData.value = item["value"].ToList();
				cyRandomTaskData.Level = item["Level"].ToList();
				cyRandomTaskData.NPCLevel = item["NPCLevel"].ToList();
				cyRandomTaskData.NPCXingGe = item["NPCXingGe"].ToList();
				cyRandomTaskData.NPCType = item["NPCType"].ToList();
				cyRandomTaskData.NPCLiuPai = item["NPCLiuPai"].ToList();
				cyRandomTaskData.NPCTag = item["NPCTag"].ToList();
				cyRandomTaskData.NPCXingWei = item["NPCXingWei"].ToList();
				cyRandomTaskData.NPCGuanXi = item["NPCGuanXi"].ToList();
				cyRandomTaskData.NPCGuanXiNot = item["NPCGuanXiNot"].ToList();
				cyRandomTaskData.HaoGanDu = item["HaoGanDu"].ToList();
				cyRandomTaskData.WuDaoType = item["WuDaoType"].ToList();
				cyRandomTaskData.WuDaoLevel = item["WuDaoLevel"].ToList();
				cyRandomTaskData.EventValue = item["EventValue"].ToList();
				cyRandomTaskData.fuhao = item["fuhao"].ToList();
				cyRandomTaskData.EventValueNum = item["EventValueNum"].ToList();
				if (DataDict.ContainsKey(cyRandomTaskData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyRandomTaskData.DataDict添加数据时出现重复的键，Key:{cyRandomTaskData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyRandomTaskData.id, cyRandomTaskData);
				DataList.Add(cyRandomTaskData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyRandomTaskData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
