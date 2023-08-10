using System;
using System.Collections.Generic;

namespace JSONClass;

public class RenWuDaLeiYouXianJi : IJSONClass
{
	public static Dictionary<int, RenWuDaLeiYouXianJi> DataDict = new Dictionary<int, RenWuDaLeiYouXianJi>();

	public static List<RenWuDaLeiYouXianJi> DataList = new List<RenWuDaLeiYouXianJi>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int Id;

	public List<int> QuJian = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.RenWuDaLeiYouXianJi.list)
		{
			try
			{
				RenWuDaLeiYouXianJi renWuDaLeiYouXianJi = new RenWuDaLeiYouXianJi();
				renWuDaLeiYouXianJi.Id = item["Id"].I;
				renWuDaLeiYouXianJi.QuJian = item["QuJian"].ToList();
				if (DataDict.ContainsKey(renWuDaLeiYouXianJi.Id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典RenWuDaLeiYouXianJi.DataDict添加数据时出现重复的键，Key:{renWuDaLeiYouXianJi.Id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(renWuDaLeiYouXianJi.Id, renWuDaLeiYouXianJi);
				DataList.Add(renWuDaLeiYouXianJi);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典RenWuDaLeiYouXianJi.DataDict添加数据时出现异常，已跳过，请检查配表");
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
