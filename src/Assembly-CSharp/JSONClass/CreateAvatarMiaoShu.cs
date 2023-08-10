using System;
using System.Collections.Generic;

namespace JSONClass;

public class CreateAvatarMiaoShu : IJSONClass
{
	public static Dictionary<int, CreateAvatarMiaoShu> DataDict = new Dictionary<int, CreateAvatarMiaoShu>();

	public static List<CreateAvatarMiaoShu> DataList = new List<CreateAvatarMiaoShu>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public string title;

	public string Info;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.CreateAvatarMiaoShu.list)
		{
			try
			{
				CreateAvatarMiaoShu createAvatarMiaoShu = new CreateAvatarMiaoShu();
				createAvatarMiaoShu.id = item["id"].I;
				createAvatarMiaoShu.title = item["title"].Str;
				createAvatarMiaoShu.Info = item["Info"].Str;
				if (DataDict.ContainsKey(createAvatarMiaoShu.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典CreateAvatarMiaoShu.DataDict添加数据时出现重复的键，Key:{createAvatarMiaoShu.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(createAvatarMiaoShu.id, createAvatarMiaoShu);
				DataList.Add(createAvatarMiaoShu);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典CreateAvatarMiaoShu.DataDict添加数据时出现异常，已跳过，请检查配表");
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
