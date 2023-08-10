using System;
using System.Collections.Generic;

namespace JSONClass;

public class CyNpcSendData : IJSONClass
{
	public static Dictionary<int, CyNpcSendData> DataDict = new Dictionary<int, CyNpcSendData>();

	public static List<CyNpcSendData> DataList = new List<CyNpcSendData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int IsChuFa;

	public int NPCshenfen;

	public int HaoGanDu;

	public int IsOnly;

	public int XiaoXiType;

	public int Rate;

	public int XingWeiType;

	public int ItemJiaGe;

	public int GuoQiShiJian;

	public int HaoGanDuChange;

	public int DuiBaiType;

	public int QingFen;

	public string fuhao1;

	public string fuhao2;

	public string StarTime;

	public string EndTime;

	public List<int> NPCXingWei = new List<int>();

	public List<int> NPCLevel = new List<int>();

	public List<int> EventValue = new List<int>();

	public List<int> ZhuangTaiInfo = new List<int>();

	public List<int> RandomItemID = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CyNpcSendData.list)
		{
			try
			{
				CyNpcSendData cyNpcSendData = new CyNpcSendData();
				cyNpcSendData.id = item["id"].I;
				cyNpcSendData.IsChuFa = item["IsChuFa"].I;
				cyNpcSendData.NPCshenfen = item["NPCshenfen"].I;
				cyNpcSendData.HaoGanDu = item["HaoGanDu"].I;
				cyNpcSendData.IsOnly = item["IsOnly"].I;
				cyNpcSendData.XiaoXiType = item["XiaoXiType"].I;
				cyNpcSendData.Rate = item["Rate"].I;
				cyNpcSendData.XingWeiType = item["XingWeiType"].I;
				cyNpcSendData.ItemJiaGe = item["ItemJiaGe"].I;
				cyNpcSendData.GuoQiShiJian = item["GuoQiShiJian"].I;
				cyNpcSendData.HaoGanDuChange = item["HaoGanDuChange"].I;
				cyNpcSendData.DuiBaiType = item["DuiBaiType"].I;
				cyNpcSendData.QingFen = item["QingFen"].I;
				cyNpcSendData.fuhao1 = item["fuhao1"].Str;
				cyNpcSendData.fuhao2 = item["fuhao2"].Str;
				cyNpcSendData.StarTime = item["StarTime"].Str;
				cyNpcSendData.EndTime = item["EndTime"].Str;
				cyNpcSendData.NPCXingWei = item["NPCXingWei"].ToList();
				cyNpcSendData.NPCLevel = item["NPCLevel"].ToList();
				cyNpcSendData.EventValue = item["EventValue"].ToList();
				cyNpcSendData.ZhuangTaiInfo = item["ZhuangTaiInfo"].ToList();
				cyNpcSendData.RandomItemID = item["RandomItemID"].ToList();
				if (DataDict.ContainsKey(cyNpcSendData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CyNpcSendData.DataDict添加数据时出现重复的键，Key:{cyNpcSendData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(cyNpcSendData.id, cyNpcSendData);
				DataList.Add(cyNpcSendData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CyNpcSendData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
