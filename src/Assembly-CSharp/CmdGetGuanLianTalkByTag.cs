using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "获取当前NPC的Tag关联对话", "获取当前NPC的Tag关联对话，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetGuanLianTalkByTag : Command
{
	private static bool inited;

	private static Dictionary<int, string> talkDict = new Dictionary<int, string>();

	public override void OnEnter()
	{
		Init();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		if (talkDict.ContainsKey(nowJiaoHuNPC.Tag))
		{
			GetFlowchart().SetStringVariable("TmpTalkString", talkDict[nowJiaoHuNPC.Tag].ReplaceTalkWord(nowJiaoHuNPC));
		}
		else
		{
			GetFlowchart().SetStringVariable("TmpTalkString", $"获取Tag关联对话失败，没有Tag {nowJiaoHuNPC.Tag}");
		}
		Continue();
	}

	private static void Init()
	{
		if (inited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NPCTagDate.list)
		{
			talkDict.Add(item["id"].I, item["GuanLianTalk"].Str);
		}
		inited = true;
	}
}
