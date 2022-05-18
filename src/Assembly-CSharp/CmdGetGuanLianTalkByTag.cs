using System;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

// Token: 0x0200034F RID: 847
[CommandInfo("YSNPCJiaoHu", "获取当前NPC的Tag关联对话", "获取当前NPC的Tag关联对话，赋值到TmpTalkString", 0)]
[AddComponentMenu("")]
public class CmdGetGuanLianTalkByTag : Command
{
	// Token: 0x060018BE RID: 6334 RVA: 0x000DD8CC File Offset: 0x000DBACC
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

	// Token: 0x060018BF RID: 6335 RVA: 0x000DD950 File Offset: 0x000DBB50
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

	// Token: 0x040013B5 RID: 5045
	private static bool inited;

	// Token: 0x040013B6 RID: 5046
	private static Dictionary<int, string> talkDict = new Dictionary<int, string>();
}
