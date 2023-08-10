using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "添加玩家打听过的NPC", "添加玩家打听过的NPC", 0)]
[AddComponentMenu("")]
public class CmdSetPlayerDaTing : Command
{
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	public override void OnEnter()
	{
		PlayerEx.AddDaTingNPC(NPCEx.NPCIDToNew(NPCID.Value));
		Continue();
	}
}
