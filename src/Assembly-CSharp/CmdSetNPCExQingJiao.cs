using System;
using Fungus;
using UnityEngine;

// Token: 0x0200024C RID: 588
[CommandInfo("YSNPCJiaoHu", "设置NPC额外请教", "设置NPC额外请教，功法填流水号，神通填神通ID", 0)]
[AddComponentMenu("")]
public class CmdSetNPCExQingJiao : Command
{
	// Token: 0x06001647 RID: 5703 RVA: 0x00096AA0 File Offset: 0x00094CA0
	public override void OnEnter()
	{
		if (this.NPCID.Value == 0)
		{
			Debug.LogError("设置NPC额外请教出错，NPCID不能为0，当前flowchart:" + this.GetFlowchart().GetParentName() + "，当前block:" + this.ParentBlock.BlockName);
		}
		else if (this.QingJiaoType == NPCExQingJiaoType.神通)
		{
			NPCEx.SetNPCExQingJiaoSkill(this.NPCID.Value, this.SkillID);
		}
		else
		{
			NPCEx.SetNPCExQingJiaoStaticSkill(this.NPCID.Value, this.SkillID);
		}
		this.Continue();
	}

	// Token: 0x0400108C RID: 4236
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x0400108D RID: 4237
	[Tooltip("技能ID，功法填流水号，神通填神通ID")]
	[SerializeField]
	protected int SkillID;

	// Token: 0x0400108E RID: 4238
	[Tooltip("类型")]
	[SerializeField]
	protected NPCExQingJiaoType QingJiaoType;
}
