using System;
using Fungus;
using UnityEngine;

// Token: 0x020002AA RID: 682
[CommandInfo("渡劫", "设置NPC受邀参观渡劫", "设置NPC受邀参观渡劫", 0)]
[AddComponentMenu("")]
public class CmdSetNPCCanGuanDuJie : Command
{
	// Token: 0x06001829 RID: 6185 RVA: 0x000A8B24 File Offset: 0x000A6D24
	public override void OnEnter()
	{
		int num = NPCEx.NPCIDToNew(this.NPCID.Value);
		if (!PlayerEx.Player.TianJieCanGuanNPCs.HasItem(num))
		{
			PlayerEx.Player.TianJieCanGuanNPCs.Add(num);
		}
		this.Continue();
	}

	// Token: 0x0400133A RID: 4922
	[Tooltip("NPCID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
