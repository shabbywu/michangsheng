using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "获取玩家是否打听过某NPC", "获取玩家是否打听过某NPC，赋值到TmpBool", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerDaTing : Command
{
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	public override void OnEnter()
	{
		bool value = PlayerEx.IsDaTing(NPCEx.NPCIDToNew(NPCID.Value));
		GetFlowchart().SetBooleanVariable("TmpBool", value);
		Continue();
	}
}
