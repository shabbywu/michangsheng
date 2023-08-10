using System;
using Fungus;
using UnityEngine;

[CommandInfo("渡劫", "设置NPC受邀参观渡劫", "设置NPC受邀参观渡劫", 0)]
[AddComponentMenu("")]
public class CmdSetNPCCanGuanDuJie : Command
{
	[Tooltip("NPCID")]
	[SerializeField]
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	protected IntegerVariable NPCID;

	public override void OnEnter()
	{
		int num = NPCEx.NPCIDToNew(NPCID.Value);
		if (!PlayerEx.Player.TianJieCanGuanNPCs.HasItem(num))
		{
			PlayerEx.Player.TianJieCanGuanNPCs.Add(num);
		}
		Continue();
	}
}
