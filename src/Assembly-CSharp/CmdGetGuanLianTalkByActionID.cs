using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "获取当前NPC的ActionID关联对话", "获取当前NPC的ActionID关联对话，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetGuanLianTalkByActionID : Command
{
	private static bool inited;

	private static Dictionary<int, string> talkDict = new Dictionary<int, string>();

	public override void OnEnter()
	{
		Init();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		if (talkDict.ContainsKey(nowJiaoHuNPC.ActionID))
		{
			GetFlowchart().SetStringVariable("TmpTalkString", talkDict[nowJiaoHuNPC.ActionID].ReplaceTalkWord(nowJiaoHuNPC));
		}
		else
		{
			GetFlowchart().SetStringVariable("TmpTalkString", $"获取ActionID关联对话失败，没有ActionID {nowJiaoHuNPC.ActionID}");
		}
		Continue();
	}

	private static void Init()
	{
		if (inited)
		{
			return;
		}
		foreach (JSONObject item in jsonData.instance.NPCActionDate.list)
		{
			talkDict.Add(item["id"].I, item["GuanLianTalk"].Str);
		}
		inited = true;
	}
}
