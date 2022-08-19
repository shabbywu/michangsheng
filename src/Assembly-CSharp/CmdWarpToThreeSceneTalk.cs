using System;
using Fungus;
using UnityEngine;

// Token: 0x02000258 RID: 600
[CommandInfo("YSNPCJiaoHu", "跳转到三级场景NPCTalk(TODO)", "跳转到三级场景NPCTalk，并将NPCID赋值到全局变量400", 0)]
[AddComponentMenu("")]
public class CmdWarpToThreeSceneTalk : Command
{
	// Token: 0x0600165D RID: 5725 RVA: 0x00096E70 File Offset: 0x00095070
	public override void OnEnter()
	{
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		GlobalValue.Set(400, nowJiaoHuNPC.ID, "TODO");
		this.Continue();
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x00096EA4 File Offset: 0x000950A4
	private static void Init()
	{
		if (!CmdWarpToThreeSceneTalk.inited)
		{
			foreach (JSONObject jsonobject in jsonData.instance.ThreeSenceJsonData.list)
			{
			}
			CmdWarpToThreeSceneTalk.inited = true;
		}
	}

	// Token: 0x040010A9 RID: 4265
	private static bool inited;
}
