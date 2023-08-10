using System;
using System.Collections.Generic;

namespace JSONClass;

public class MessageJsonData : IJSONClass
{
	public static Dictionary<int, MessageJsonData> DataDict = new Dictionary<int, MessageJsonData>();

	public static List<MessageJsonData> DataList = new List<MessageJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int headID;

	public string messageInfo;

	public string title;

	public string body;

	public string func1;

	public string funcargs1;

	public string func2;

	public string funcargs2;

	public string func3;

	public string funcargs3;

	public string func4;

	public string funcargs4;

	public string func5;

	public string funcargs5;

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.MessageJsonData.list)
		{
			try
			{
				MessageJsonData messageJsonData = new MessageJsonData();
				messageJsonData.id = item["id"].I;
				messageJsonData.headID = item["headID"].I;
				messageJsonData.messageInfo = item["messageInfo"].Str;
				messageJsonData.title = item["title"].Str;
				messageJsonData.body = item["body"].Str;
				messageJsonData.func1 = item["func1"].Str;
				messageJsonData.funcargs1 = item["funcargs1"].Str;
				messageJsonData.func2 = item["func2"].Str;
				messageJsonData.funcargs2 = item["funcargs2"].Str;
				messageJsonData.func3 = item["func3"].Str;
				messageJsonData.funcargs3 = item["funcargs3"].Str;
				messageJsonData.func4 = item["func4"].Str;
				messageJsonData.funcargs4 = item["funcargs4"].Str;
				messageJsonData.func5 = item["func5"].Str;
				messageJsonData.funcargs5 = item["funcargs5"].Str;
				if (DataDict.ContainsKey(messageJsonData.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典MessageJsonData.DataDict添加数据时出现重复的键，Key:{messageJsonData.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(messageJsonData.id, messageJsonData);
				DataList.Add(messageJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典MessageJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
