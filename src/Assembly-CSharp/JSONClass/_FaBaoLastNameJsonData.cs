using System;
using System.Collections.Generic;

namespace JSONClass;

public class _FaBaoLastNameJsonData : IJSONClass
{
	public static Dictionary<int, _FaBaoLastNameJsonData> DataDict = new Dictionary<int, _FaBaoLastNameJsonData>();

	public static List<_FaBaoLastNameJsonData> DataList = new List<_FaBaoLastNameJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int PosReverse;

	public string LastName;

	public List<int> Type = new List<int>();

	public List<int> quality = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance._FaBaoLastNameJsonData.list)
		{
			try
			{
				_FaBaoLastNameJsonData faBaoLastNameJsonData = new _FaBaoLastNameJsonData();
				faBaoLastNameJsonData.id = item["id"].I;
				faBaoLastNameJsonData.PosReverse = item["PosReverse"].I;
				faBaoLastNameJsonData.LastName = item["LastName"].Str;
				faBaoLastNameJsonData.Type = item["Type"].ToList();
				faBaoLastNameJsonData.quality = item["quality"].ToList();
				if (DataDict.ContainsKey(faBaoLastNameJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典_FaBaoLastNameJsonData.DataDict添加数据时出现重复的键，Key:{faBaoLastNameJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(faBaoLastNameJsonData.id, faBaoLastNameJsonData);
				DataList.Add(faBaoLastNameJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典_FaBaoLastNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
