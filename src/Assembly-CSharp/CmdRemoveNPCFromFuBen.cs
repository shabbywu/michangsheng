using System;
using Fungus;
using UnityEngine;

[CommandInfo("YSPlayer", "移除当前副本中的某个NPC", "移除当前副本中的某个NPC(在移除前确保已经将NPC移动到其他地图)", 0)]
[AddComponentMenu("")]
public class CmdRemoveNPCFromFuBen : Command
{
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	public override void OnEnter()
	{
		NPCEx.RemoveNPCFromNowFuBen(NPCID.Value);
		Continue();
	}
}
