using System;
using System.Collections.Generic;

namespace JSONClass;

public class CreateAvatarJsonData : IJSONClass
{
	public static Dictionary<int, CreateAvatarJsonData> DataDict = new Dictionary<int, CreateAvatarJsonData>();

	public static List<CreateAvatarJsonData> DataList = new List<CreateAvatarJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int fenZu;

	public int feiYong;

	public int fenLeiGuanLian;

	public int jiesuo;

	public string Title;

	public string fenLei;

	public string Desc;

	public string Info;

	public List<int> seid = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CreateAvatarJsonData.list)
		{
			try
			{
				CreateAvatarJsonData createAvatarJsonData = new CreateAvatarJsonData();
				createAvatarJsonData.id = item["id"].I;
				createAvatarJsonData.fenZu = item["fenZu"].I;
				createAvatarJsonData.feiYong = item["feiYong"].I;
				createAvatarJsonData.fenLeiGuanLian = item["fenLeiGuanLian"].I;
				createAvatarJsonData.jiesuo = item["jiesuo"].I;
				createAvatarJsonData.Title = item["Title"].Str;
				createAvatarJsonData.fenLei = item["fenLei"].Str;
				createAvatarJsonData.Desc = item["Desc"].Str;
				createAvatarJsonData.Info = item["Info"].Str;
				createAvatarJsonData.seid = item["seid"].ToList();
				if (DataDict.ContainsKey(createAvatarJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CreateAvatarJsonData.DataDict添加数据时出现重复的键，Key:{createAvatarJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(createAvatarJsonData.id, createAvatarJsonData);
				DataList.Add(createAvatarJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CreateAvatarJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
