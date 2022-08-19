using System;
using Fungus;
using UnityEngine;

// Token: 0x0200022A RID: 554
[CommandInfo("YSNPCJiaoHu", "根据变量名增加好感度", "根据变量增加好感度", 0)]
[AddComponentMenu("")]
public class CmdAddFavorByVar : Command
{
	// Token: 0x060015F2 RID: 5618 RVA: 0x00094DBC File Offset: 0x00092FBC
	public override void OnEnter()
	{
		Flowchart flowchart = this.GetFlowchart();
		int integerVariable = flowchart.GetIntegerVariable(this.addTargetNPCIDVar);
		int integerVariable2 = flowchart.GetIntegerVariable(this.addCountVar);
		UINPCSVItem.RefreshNPCFavorID = integerVariable;
		NPCEx.AddFavor(integerVariable, integerVariable2, true, true);
		this.Continue();
	}

	// Token: 0x0400104C RID: 4172
	[SerializeField]
	[Tooltip("目标NPCID的变量名")]
	protected string addTargetNPCIDVar;

	// Token: 0x0400104D RID: 4173
	[SerializeField]
	[Tooltip("好感度增加量的变量名")]
	protected string addCountVar;
}
