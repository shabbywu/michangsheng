using System;
using Fungus;
using UnityEngine;

// Token: 0x02000250 RID: 592
[CommandInfo("YSPlayer", "设置npc的状态", "设置npc的状态", 0)]
[AddComponentMenu("")]
public class CmdSetNpcStatus : Command
{
	// Token: 0x0600164D RID: 5709 RVA: 0x00096D0D File Offset: 0x00094F0D
	public override void OnEnter()
	{
		NpcJieSuanManager.inst.npcStatus.SetNpcStatus(this.npc.Value, this.state.Value);
		this.Continue();
	}

	// Token: 0x04001099 RID: 4249
	[SerializeField]
	[Tooltip("npcId")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable npc;

	// Token: 0x0400109A RID: 4250
	[SerializeField]
	[Tooltip("状态Id")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable state;
}
