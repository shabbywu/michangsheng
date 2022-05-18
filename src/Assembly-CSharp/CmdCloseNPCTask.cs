using System;
using Fungus;
using UnityEngine;

// Token: 0x0200034C RID: 844
[CommandInfo("YSNPCJiaoHu", "关闭NPC任务", "关闭NPC任务", 0)]
[AddComponentMenu("")]
public class CmdCloseNPCTask : Command
{
	// Token: 0x060018B6 RID: 6326 RVA: 0x000DD71C File Offset: 0x000DB91C
	public override void OnEnter()
	{
		int integerVariable = this.GetFlowchart().GetIntegerVariable(this.TargetNPCIDVar);
		jsonData.instance.AvatarJsonData[integerVariable.ToString()].SetField("IsNeedHelp", false);
		UINPCSVItem.RefreshNPCTaskID = integerVariable;
		this.Continue();
	}

	// Token: 0x040013B0 RID: 5040
	[SerializeField]
	[Tooltip("目标NPCID的变量名")]
	protected string TargetNPCIDVar;
}
