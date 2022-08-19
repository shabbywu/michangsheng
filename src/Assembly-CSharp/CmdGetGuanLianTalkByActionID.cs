using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

// Token: 0x02000232 RID: 562
[CommandInfo("YSNPCJiaoHu", "获取当前NPC的ActionID关联对话", "获取当前NPC的ActionID关联对话，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetGuanLianTalkByActionID : Command
{
	// Token: 0x06001602 RID: 5634 RVA: 0x00095134 File Offset: 0x00093334
	public override void OnEnter()
	{
		CmdGetGuanLianTalkByActionID.Init();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		if (CmdGetGuanLianTalkByActionID.talkDict.ContainsKey(nowJiaoHuNPC.ActionID))
		{
			this.GetFlowchart().SetStringVariable("TmpTalkString", CmdGetGuanLianTalkByActionID.talkDict[nowJiaoHuNPC.ActionID].ReplaceTalkWord(nowJiaoHuNPC));
		}
		else
		{
			this.GetFlowchart().SetStringVariable("TmpTalkString", string.Format("获取ActionID关联对话失败，没有ActionID {0}", nowJiaoHuNPC.ActionID));
		}
		this.Continue();
	}

	// Token: 0x06001603 RID: 5635 RVA: 0x000951B8 File Offset: 0x000933B8
	private static void Init()
	{
		if (!CmdGetGuanLianTalkByActionID.inited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCActionDate.list)
			{
				CmdGetGuanLianTalkByActionID.talkDict.Add(jsonobject["id"].I, jsonobject["GuanLianTalk"].Str);
			}
			CmdGetGuanLianTalkByActionID.inited = true;
		}
	}

	// Token: 0x0400105B RID: 4187
	private static bool inited;

	// Token: 0x0400105C RID: 4188
	private static Dictionary<int, string> talkDict = new Dictionary<int, string>();
}
