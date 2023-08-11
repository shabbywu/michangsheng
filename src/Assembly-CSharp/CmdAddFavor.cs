using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSNPCJiaoHu", "根据变量增加好感度", "根据变量增加好感度", 0)]
[AddComponentMenu("")]
public class CmdAddFavor : Command
{
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	[SerializeField]
	[Tooltip("增加的好感度")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable Favor;

	[SerializeField]
	[Tooltip("是否显示提示")]
	protected bool ShowTips;

	public override void OnEnter()
	{
		NPCEx.AddFavor(UINPCSVItem.RefreshNPCFavorID = NPCEx.NPCIDToNew(NPCID.Value), Favor.Value, addQingFen: true, ShowTips);
		Continue();
	}
}
