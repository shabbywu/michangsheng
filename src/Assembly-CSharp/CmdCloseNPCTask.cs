using System;
using Fungus;
using UnityEngine;

// Token: 0x02000230 RID: 560
[CommandInfo("YSNPCJiaoHu", "关闭NPC任务", "关闭NPC任务", 0)]
[AddComponentMenu("")]
public class CmdCloseNPCTask : Command
{
	// Token: 0x060015FE RID: 5630 RVA: 0x00095094 File Offset: 0x00093294
	public override void OnEnter()
	{
		int integerVariable = this.GetFlowchart().GetIntegerVariable(this.TargetNPCIDVar);
		jsonData.instance.AvatarJsonData[integerVariable.ToString()].SetField("IsNeedHelp", false);
		UINPCSVItem.RefreshNPCTaskID = integerVariable;
		this.Continue();
	}

	// Token: 0x04001058 RID: 4184
	[SerializeField]
	[Tooltip("目标NPCID的变量名")]
	protected string TargetNPCIDVar;
}
