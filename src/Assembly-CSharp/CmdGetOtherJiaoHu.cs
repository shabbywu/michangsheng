using System.Collections.Generic;
using Fungus;
using KBEngine;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "获取其他交互文本", "获取当前NPC的其他交互文本，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetOtherJiaoHu : Command
{
	private static bool isInited;

	private static Dictionary<int, int> _JingJieDict = new Dictionary<int, int>();

	private static Dictionary<int, int> _XingGeDict = new Dictionary<int, int>();

	private static Dictionary<int, Dictionary<string, string>> _TalkDict = new Dictionary<int, Dictionary<string, string>>();

	[Tooltip("以Talk开头的key，获取对应的文本")]
	[SerializeField]
	protected string textKey;

	private static void Init()
	{
		if (isInited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NpcTalkQiTaJiaoHuData.list)
		{
			int i = item["id"].I;
			_JingJieDict.Add(i, item["JingJie"].I);
			_XingGeDict.Add(i, item["XingGe"].I);
			_TalkDict.Add(i, new Dictionary<string, string>());
			foreach (string key in item.keys)
			{
				if (key.StartsWith("Talk"))
				{
					_TalkDict[i].Add(key, item[key].Str);
				}
			}
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
		if (list2.Count > 0)
		{
			if (_TalkDict[list2[0]].ContainsKey(textKey))
			{
				flowchart.SetStringVariable("TmpTalkString", _TalkDict[list2[0]][textKey].ReplaceTalkWord(nowJiaoHuNPC));
			}
			else
			{
				flowchart.SetStringVariable("TmpTalkString", $"读取失败，没有获取到文本(key{list2[0]}不存在)");
			}
		}
		else
		{
			flowchart.SetStringVariable("TmpTalkString", "读取失败，没有获取到文本3");
		}
		Continue();
	}
}
