using System;
using System.Collections.Generic;

namespace JSONClass;

public class _FaBaoFirstNameJsonData : IJSONClass
{
	public static Dictionary<int, _FaBaoFirstNameJsonData> DataDict = new Dictionary<int, _FaBaoFirstNameJsonData>();

	public static List<_FaBaoFirstNameJsonData> DataList = new List<_FaBaoFirstNameJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int PosReverse;

	public string FirstName;

	public List<int> Type = new List<int>();

	public List<int> quality = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance._FaBaoFirstNameJsonData.list)
		{
			try
			{
				_FaBaoFirstNameJsonData faBaoFirstNameJsonData = new _FaBaoFirstNameJsonData();
				faBaoFirstNameJsonData.id = item["id"].I;
				faBaoFirstNameJsonData.PosReverse = item["PosReverse"].I;
				faBaoFirstNameJsonData.FirstName = item["FirstName"].Str;
				faBaoFirstNameJsonData.Type = item["Type"].ToList();
				faBaoFirstNameJsonData.quality = item["quality"].ToList();
				if (DataDict.ContainsKey(faBaoFirstNameJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典_FaBaoFirstNameJsonData.DataDict添加数据时出现重复的键，Key:{faBaoFirstNameJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(faBaoFirstNameJsonData.id, faBaoFirstNameJsonData);
				DataList.Add(faBaoFirstNameJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典_FaBaoFirstNameJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
