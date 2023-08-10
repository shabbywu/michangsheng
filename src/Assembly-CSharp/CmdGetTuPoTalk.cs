using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "获取关于突破关联文本", "获取关于突破关联文本，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetTuPoTalk : Command
{
	private static bool isInited;

	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _PlayerJingJieDict = new Dictionary<int, int>();

	private static Dictionary<int, string> _TalkDict = new Dictionary<int, string>();

	private static void Init()
	{
		if (isInited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NpcTalkGuanYuTuPoData.list)
		{
			int i = item["id"].I;
			_PlayerJingJieDict.Add(i, item["WanJiaJingJie"].I);
			_JingJieDict.Add(i, item["JingJie"].I);
			_XingGeDict.Add(i, item["XingGe"].I);
			_TalkDict.Add(i, item["TuPoTalk"].Str);
		}
		isInited = true;
	}

	public override void OnEnter()
	{
		Init();
		Flowchart flowchart = GetFlowchart();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int>();
		int levelType = player.getLevelType();
		foreach (KeyValuePair<int, int> item in _PlayerJingJieDict)
		{
			if (item.Value == levelType)
			{
				list.Add(item.Key);
			}
		}
		List<int> list2 = new List<int>();
		foreach (int item2 in list)
		{
			if (_JingJieDict[item2] == 1 && nowJiaoHuNPC.BigLevel < levelType)
			{
				list2.Add(item2);
			}
			if (_JingJieDict[item2] == 2 && nowJiaoHuNPC.BigLevel == levelType)
			{
				list2.Add(item2);
			}
			if (_JingJieDict[item2] == 3 && nowJiaoHuNPC.BigLevel > levelType)
			{
				list2.Add(item2);
			}
		}
		List<int> list3 = new List<int>();
		foreach (int item3 in list2)
		{
			if (_XingGeDict[item3] == nowJiaoHuNPC.XingGe)
			{
				list3.Add(item3);
			}
		}
		if (list3.Count > 0)
		{
			flowchart.SetStringVariable("TmpTalkString", _TalkDict[list3[0]].ReplaceTalkWord(nowJiaoHuNPC));
		}
		else
		{
			flowchart.SetStringVariable("TmpTalkString", "读取失败，没有获取到文本2");
		}
		Continue();
	}
}
