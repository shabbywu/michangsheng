using System;
using Fungus;
using UnityEngine;

// Token: 0x02000247 RID: 583
[CommandInfo("YSNPCJiaoHu", "打开NPCPop", "打开当前NPC的Pop", 0)]
[AddComponentMenu("")]
public class CmdOpenNPCPop : Command
{
	// Token: 0x0600163D RID: 5693 RVA: 0x00096928 File Offset: 0x00094B28
	public override void OnEnter()
	{
		UINPCData nowJiaoHuNPC = UINPCJiaoHu.Inst.NowJiaoHuNPC;
		for (int i = 0; i < UINPCJiaoHu.Inst.NPCList.SVTransform.childCount; i++)
		{
			UINPCSVItem component = UINPCJiaoHu.Inst.NPCList.SVTransform.GetChild(i).GetComponent<UINPCSVItem>();
			if (component.NPCData.ID == nowJiaoHuNPC.ID)
			{
				component.OnClick();
				break;
			}
		}
		this.Continue();
	}
}
