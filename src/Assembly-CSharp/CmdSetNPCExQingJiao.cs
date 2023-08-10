using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "设置NPC额外请教", "设置NPC额外请教，功法填流水号，神通填神通ID", 0)]
[AddComponentMenu("")]
public class CmdSetNPCExQingJiao : Command
{
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[Tooltip("技能ID，功法填流水号，神通填神通ID")]
	[SerializeField]
	protected int SkillID;

	[Tooltip("类型")]
	[SerializeField]
	protected NPCExQingJiaoType QingJiaoType;

	public override void OnEnter()
	{
		if (NPCID.Value == 0)
		{
			Debug.LogError((object)("设置NPC额外请教出错，NPCID不能为0，当前flowchart:" + GetFlowchart().GetParentName() + "，当前block:" + ParentBlock.BlockName));
		}
		else if (QingJiaoType == NPCExQingJiaoType.神通)
		{
			NPCEx.SetNPCExQingJiaoSkill(NPCID.Value, SkillID);
		}
		else
		{
			NPCEx.SetNPCExQingJiaoStaticSkill(NPCID.Value, SkillID);
		}
		Continue();
	}
}
