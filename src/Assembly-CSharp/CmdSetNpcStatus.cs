using System;
using Fungus;
using UnityEngine;

// Token: 0x0200036A RID: 874
[CommandInfo("YSPlayer", "设置npc的状态", "设置npc的状态", 0)]
[AddComponentMenu("")]
public class CmdSetNpcStatus : Command
{
	// Token: 0x06001903 RID: 6403 RVA: 0x0001572A File Offset: 0x0001392A
	public override void OnEnter()
	{
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(this.npc.Value, this.state.Value);
		this.Continue();
	}

	// Token: 0x040013EB RID: 5099
	[SerializeField]
	[Tooltip("npcId")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable npc;

	// Token: 0x040013EC RID: 5100
	[SerializeField]
	[Tooltip("状态Id")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable state;
}
