using System;
using System.Collections.Generic;

namespace JSONClass;

public class DFZhenYanLevel : IJSONClass
{
	public static Dictionary<int, DFZhenYanLevel> DataDict = new Dictionary<int, DFZhenYanLevel>();

	public static List<DFZhenYanLevel> DataList = new List<DFZhenYanLevel>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int zhenpanlevel;

	public int wudaolevel;

	public int buzhenxiaohao;

	public int xiuliansudu;

	public int lingtiansudu;

	public int lingtiancuishengsudu;

	public string name;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.DFZhenYanLevel.list)
		{
			try
			{
				DFZhenYanLevel dFZhenYanLevel = new DFZhenYanLevel();
				dFZhenYanLevel.id = item["id"].I;
				dFZhenYanLevel.zhenpanlevel = item["zhenpanlevel"].I;
				dFZhenYanLevel.wudaolevel = item["wudaolevel"].I;
				dFZhenYanLevel.buzhenxiaohao = item["buzhenxiaohao"].I;
				dFZhenYanLevel.xiuliansudu = item["xiuliansudu"].I;
				dFZhenYanLevel.lingtiansudu = item["lingtiansudu"].I;
				dFZhenYanLevel.lingtiancuishengsudu = item["lingtiancuishengsudu"].I;
				dFZhenYanLevel.name = item["name"].Str;
				if (DataDict.ContainsKey(dFZhenYanLevel.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典DFZhenYanLevel.DataDict添加数据时出现重复的键，Key:{dFZhenYanLevel.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(dFZhenYanLevel.id, dFZhenYanLevel);
				DataList.Add(dFZhenYanLevel);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典DFZhenYanLevel.DataDict添加数据时出现异常，已跳过，请检查配表");
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
