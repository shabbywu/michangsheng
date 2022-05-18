using System;
using Fungus;
using UnityEngine;

// Token: 0x02000363 RID: 867
[CommandInfo("YSNPCJiaoHu", "打开NPCPop", "打开当前NPC的Pop", 0)]
[AddComponentMenu("")]
public class CmdOpenNPCPop : Command
{
	// Token: 0x060018F5 RID: 6389 RVA: 0x000DEE28 File Offset: 0x000DD028
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
