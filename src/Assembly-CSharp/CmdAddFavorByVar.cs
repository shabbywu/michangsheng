using System;
using Fungus;
using UnityEngine;

// Token: 0x02000346 RID: 838
[CommandInfo("YSNPCJiaoHu", "根据变量名增加好感度", "根据变量增加好感度", 0)]
[AddComponentMenu("")]
public class CmdAddFavorByVar : Command
{
	// Token: 0x060018AA RID: 6314 RVA: 0x000DD498 File Offset: 0x000DB698
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int integerVariable = flowchart.GetIntegerVariable(this.addTargetNPCIDVar);
		int integerVariable2 = flowchart.GetIntegerVariable(this.addCountVar);
		UINPCSVItem.RefreshNPCFavorID = integerVariable;
		NPCEx.AddFavor(integerVariable, integerVariable2, true, true);
		this.Continue();
	}

	// Token: 0x040013A4 RID: 5028
	[SerializeField]
	[Tooltip("目标NPCID的变量名")]
	protected string addTargetNPCIDVar;

	// Token: 0x040013A5 RID: 5029
	[SerializeField]
	[Tooltip("好感度增加量的变量名")]
	protected string addCountVar;
}
