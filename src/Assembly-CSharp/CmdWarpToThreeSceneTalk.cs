using System;
using Fungus;
using UnityEngine;

// Token: 0x02000371 RID: 881
[CommandInfo("YSNPCJiaoHu", "跳转到三级场景NPCTalk(TODO)", "跳转到三级场景NPCTalk，并将NPCID赋值到全局变量400", 0)]
[AddComponentMenu("")]
public class CmdWarpToThreeSceneTalk : Command
{
	// Token: 0x06001911 RID: 6417 RVA: 0x000DF198 File Offset: 0x000DD398
	public override void OnEnter()
	{
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		GlobalValue.Set(400, nowJiaoHuNPC.ID, "TODO");
		this.Continue();
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x000DF1CC File Offset: 0x000DD3CC
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

	// Token: 0x040013FB RID: 5115
	private static bool inited;
}
