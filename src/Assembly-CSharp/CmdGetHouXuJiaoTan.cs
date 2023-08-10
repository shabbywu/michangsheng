using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "获取后续聊天文本", "获取当前NPC的后续聊天内容，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetHouXuJiaoTan : Command
{
	private static bool isInited;

	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _FavorDict = new Dictionary<int, int>();

	private static Dictionary<int, string> _TalkDict = new Dictionary<int, string>();

	private static void Init()
	{
		if (isInited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NpcTalkHouXuJiaoTanData.list)
		{
			int i = item["id"].I;
			_JingJieDict.Add(i, item["JingJie"].I);
			_XingGeDict.Add(i, item["XingGe"].I);
			_FavorDict.Add(i, item["HaoGanDu"].I);
			_TalkDict.Add(i, item["OtherTalk"].Str);
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
		foreach (KeyValuePair<int, int> item in _JingJieDict)
		{
			if (item.Value == 1 && nowJiaoHuNPC.BigLevel < levelType)
			{
				list.Add(item.Key);
			}
			if (item.Value == 2 && nowJiaoHuNPC.BigLevel == levelType)
			{
				list.Add(item.Key);
			}
			if (item.Value == 3 && nowJiaoHuNPC.BigLevel > levelType)
			{
				list.Add(item.Key);
			}
		}
		List<int> list2 = new List<int>();
		foreach (int item2 in list)
		{
			if (_XingGeDict[item2] == nowJiaoHuNPC.XingGe)
			{
				list2.Add(item2);
			}
		}
		List<int> list3 = new List<int>();
		foreach (int item3 in list2)
		{
			if (_FavorDict[item3] == nowJiaoHuNPC.FavorLevel)
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
