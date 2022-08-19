using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

// Token: 0x02000233 RID: 563
[CommandInfo("YSNPCJiaoHu", "获取当前NPC的Tag关联对话", "获取当前NPC的Tag关联对话，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetGuanLianTalkByTag : Command
{
	// Token: 0x06001606 RID: 5638 RVA: 0x00095250 File Offset: 0x00093450
	public override void OnEnter()
	{
		CmdGetGuanLianTalkByTag.Init();
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		if (CmdGetGuanLianTalkByTag.talkDict.ContainsKey(nowJiaoHuNPC.Tag))
		{
			this.GetFlowchart().SetStringVariable("TmpTalkString", CmdGetGuanLianTalkByTag.talkDict[nowJiaoHuNPC.Tag].ReplaceTalkWord(nowJiaoHuNPC));
		}
		else
		{
			this.GetFlowchart().SetStringVariable("TmpTalkString", string.Format("获取Tag关联对话失败，没有Tag {0}", nowJiaoHuNPC.Tag));
		}
		this.Continue();
	}

	// Token: 0x06001607 RID: 5639 RVA: 0x000952D4 File Offset: 0x000934D4
	private static void Init()
	{
		if (!CmdGetGuanLianTalkByTag.inited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.NPCTagDate.list)
			{
				CmdGetGuanLianTalkByTag.talkDict.Add(jsonobject["id"].I, jsonobject["GuanLianTalk"].Str);
			}
			CmdGetGuanLianTalkByTag.inited = true;
		}
	}

	// Token: 0x0400105D RID: 4189
	private static bool inited;

	// Token: 0x0400105E RID: 4190
	private static Dictionary<int, string> talkDict = new Dictionary<int, string>();
}
