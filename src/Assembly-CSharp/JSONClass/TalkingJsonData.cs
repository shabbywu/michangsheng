using System;
using System.Collections.Generic;

namespace JSONClass;

public class TalkingJsonData : IJSONClass
{
	public static Dictionary<int, TalkingJsonData> DataDict = new Dictionary<int, TalkingJsonData>();

	public static List<TalkingJsonData> DataList = new List<TalkingJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int headID;

	public int menu1;

	public int menu2;

	public int menu3;

	public int menu4;

	public int menu5;

	public string sayname;

	public string title;

	public string body;

	public string funcFailMsg;

	public string func1;

	public string func2;

	public string func3;

	public string func4;

	public string funcargs4;

	public string func5;

	public string funcargs5;

	public List<int> funcargs1 = new List<int>();

	public List<int> funcargs2 = new List<int>();

	public List<int> funcargs3 = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.TalkingJsonData.list)
		{
			try
			{
				TalkingJsonData talkingJsonData = new TalkingJsonData();
				talkingJsonData.id = item["id"].I;
				talkingJsonData.headID = item["headID"].I;
				talkingJsonData.menu1 = item["menu1"].I;
				talkingJsonData.menu2 = item["menu2"].I;
				talkingJsonData.menu3 = item["menu3"].I;
				talkingJsonData.menu4 = item["menu4"].I;
				talkingJsonData.menu5 = item["menu5"].I;
				talkingJsonData.sayname = item["sayname"].Str;
				talkingJsonData.title = item["title"].Str;
				talkingJsonData.body = item["body"].Str;
				talkingJsonData.funcFailMsg = item["funcFailMsg"].Str;
				talkingJsonData.func1 = item["func1"].Str;
				talkingJsonData.func2 = item["func2"].Str;
				talkingJsonData.func3 = item["func3"].Str;
				talkingJsonData.func4 = item["func4"].Str;
				talkingJsonData.funcargs4 = item["funcargs4"].Str;
				talkingJsonData.func5 = item["func5"].Str;
				talkingJsonData.funcargs5 = item["funcargs5"].Str;
				talkingJsonData.funcargs1 = item["funcargs1"].ToList();
				talkingJsonData.funcargs2 = item["funcargs2"].ToList();
				talkingJsonData.funcargs3 = item["funcargs3"].ToList();
				if (DataDict.ContainsKey(talkingJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典TalkingJsonData.DataDict添加数据时出现重复的键，Key:{talkingJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(talkingJsonData.id, talkingJsonData);
				DataList.Add(talkingJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典TalkingJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
