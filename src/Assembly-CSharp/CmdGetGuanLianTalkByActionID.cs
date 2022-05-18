using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

// Token: 0x0200034E RID: 846
[CommandInfo("YSNPCJiaoHu", "获取当前NPC的ActionID关联对话", "获取当前NPC的ActionID关联对话，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetGuanLianTalkByActionID : Command
{
	// Token: 0x060018BA RID: 6330 RVA: 0x000DD7BC File Offset: 0x000DB9BC
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

	// Token: 0x060018BB RID: 6331 RVA: 0x000DD840 File Offset: 0x000DBA40
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

	// Token: 0x040013B3 RID: 5043
	private static bool inited;

	// Token: 0x040013B4 RID: 5044
	private static Dictionary<int, string> talkDict = new Dictionary<int, string>();
}
