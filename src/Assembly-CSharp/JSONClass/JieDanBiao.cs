using System;
using System.Collections.Generic;

namespace JSONClass;

public class JieDanBiao : IJSONClass
{
	public static Dictionary<int, JieDanBiao> DataDict = new Dictionary<int, JieDanBiao>();

	public static List<JieDanBiao> DataList = new List<JieDanBiao>();

	public static Action OnInitFinishAction = OnInitFinish;

	public int id;

	public int JinDanQuality;

	public int HP;

	public int EXP;

	public string name;

	public string desc;

	public List<int> JinDanType = new List<int>();

	public List<int> LinGengType = new List<int>();

	public List<int> LinGengZongShu = new List<int>();

	public List<int> seid = new List<int>();

	public static void InitDataDict()
	{
		foreach (JSONObject item in jsonData.instance.JieDanBiao.list)
		{
			try
			{
				JieDanBiao jieDanBiao = new JieDanBiao();
				jieDanBiao.id = item["id"].I;
				jieDanBiao.JinDanQuality = item["JinDanQuality"].I;
				jieDanBiao.HP = item["HP"].I;
				jieDanBiao.EXP = item["EXP"].I;
				jieDanBiao.name = item["name"].Str;
				jieDanBiao.desc = item["desc"].Str;
				jieDanBiao.JinDanType = item["JinDanType"].ToList();
				jieDanBiao.LinGengType = item["LinGengType"].ToList();
				jieDanBiao.LinGengZongShu = item["LinGengZongShu"].ToList();
				jieDanBiao.seid = item["seid"].ToList();
				if (DataDict.ContainsKey(jieDanBiao.id))
				{
					PreloadManager.LogException($"!!!错误!!!向字典JieDanBiao.DataDict添加数据时出现重复的键，Key:{jieDanBiao.id}，已跳过，请检查配表");
					continue;
				}
				DataDict.Add(jieDanBiao.id, jieDanBiao);
				DataList.Add(jieDanBiao);
			}
			catch (Exception arg)
			{
				PreloadManager.LogException("!!!错误!!!向字典JieDanBiao.DataDict添加数据时出现异常，已跳过，请检查配表");
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
