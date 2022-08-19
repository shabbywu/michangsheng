using System;
using Fungus;
using UnityEngine;

// Token: 0x02000241 RID: 577
[CommandInfo("YSPlayer", "获取玩家人际关系(未完成)", "获取玩家人际关系", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerRelationship : Command
{
	// Token: 0x0600162A RID: 5674 RVA: 0x0005E3AF File Offset: 0x0005C5AF
	public override void OnEnter()
	{
		this.Continue();
	}

	// Token: 0x04001078 RID: 4216
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
