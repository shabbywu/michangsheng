using System;
using System.Collections.Generic;

namespace JSONClass;

public class _BuffJsonData : IJSONClass
{
	public static Dictionary<int, _BuffJsonData> DataDict = new Dictionary<int, _BuffJsonData>();

	public static List<_BuffJsonData> DataList = new List<_BuffJsonData>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int buffid;

	public int BuffIcon;

	public int bufftype;

	public int trigger;

	public int removeTrigger;

	public int looptime;

	public int totaltime;

	public int BuffType;

	public int isHide;

	public int ShowOnlyOne;

	public string skillEffect;

	public string name;

	public string descr;

	public string script;

	public List<int> Affix = new List<int>();

	public List<int> seid = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance._BuffJsonData.list)
		{
			try
			{
				_BuffJsonData buffJsonData = new _BuffJsonData();
				buffJsonData.buffid = item["buffid"].I;
				buffJsonData.BuffIcon = item["BuffIcon"].I;
				buffJsonData.bufftype = item["bufftype"].I;
				buffJsonData.trigger = item["trigger"].I;
				buffJsonData.removeTrigger = item["removeTrigger"].I;
				buffJsonData.looptime = item["looptime"].I;
				buffJsonData.totaltime = item["totaltime"].I;
				buffJsonData.BuffType = item["BuffType"].I;
				buffJsonData.isHide = item["isHide"].I;
				buffJsonData.ShowOnlyOne = item["ShowOnlyOne"].I;
				buffJsonData.skillEffect = item["skillEffect"].Str;
				buffJsonData.name = item["name"].Str;
				buffJsonData.descr = item["descr"].Str;
				buffJsonData.script = item["script"].Str;
				buffJsonData.Affix = item["Affix"].ToList();
				buffJsonData.seid = item["seid"].ToList();
				if (DataDict.ContainsKey(buffJsonData.buffid))
				{
					PreloadManager.LogException($"!!!错误!!!向字典_BuffJsonData.DataDict添加数据时出现重复的键，Key:{buffJsonData.buffid}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(buffJsonData.buffid, buffJsonData);
				DataList.Add(buffJsonData);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典_BuffJsonData.DataDict添加数据时出现异常，已跳过，请检查配表");
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
