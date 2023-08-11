using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "获取首次聊天文本", "获取当前NPC的初次聊天内容，赋值到TmpTalkString，好感度变化赋值到TmpValue", 0)]
[AddComponentMenu("")]
public class CmdGetShouCiJiaoTan : Command
{
	private static bool isInited;

	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _ShengWangDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _FavorDict = new Dictionary<int, int>();

	private static Dictionary<int, string> _FirstTalkDict = new Dictionary<int, string>();

	private static void Init()
	{
		if (isInited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NpcTalkShouCiJiaoTanData.list)
		{
			int i = item["id"].I;
			_JingJieDict.Add(i, item["JingJie"].I);
			_ShengWangDict.Add(i, item["ShengWang"].I);
			_XingGeDict.Add(i, item["XingGe"].I);
			_FavorDict.Add(i, item["HaoGanDu"].I);
			_FirstTalkDict.Add(i, item["FirstTalk"].Str);
		}
		isInited = true;
	}

	public override void OnEnter()
	{
		Init();
		Flowchart flowchart = GetFlowchart();
		Avatar player = Tools.instance.getPlayer();
		List<int> list = new List<int>();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
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
		int num = ((!nowJiaoHuNPC.IsNingZhouNPC) ? PlayerEx.GetSeaShengWangLevel() : PlayerEx.GetNingZhouShengWangLevel());
		List<int> list2 = new List<int>();
		foreach (int item2 in list)
		{
			if (_ShengWangDict[item2] == num)
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
			flowchart.SetStringVariable("TmpTalkString", _FirstTalkDict[list3[0]].ReplaceTalkWord(nowJiaoHuNPC));
			flowchart.SetIntegerVariable("TmpValue", _FavorDict[list3[0]]);
			_ = _FavorDict[list3[0]];
		}
		else
		{
			flowchart.SetStringVariable("TmpTalkString", "读取失败，没有获取到文本1");
			flowchart.SetIntegerVariable("TmpValue", 0);
		}
		Continue();
	}
}
