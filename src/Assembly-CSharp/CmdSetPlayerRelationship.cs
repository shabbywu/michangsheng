using System;
using Fungus;
using UnityEngine;

// Token: 0x0200036C RID: 876
[CommandInfo("YSPlayer", "设置玩家人际关系", "设置玩家人际关系", 0)]
[AddComponentMenu("")]
public class CmdSetPlayerRelationship : Command
{
	// Token: 0x06001907 RID: 6407 RVA: 0x000DF12C File Offset: 0x000DD32C
	public override void OnEnter()
	{
		int npcid = NPCEx.NPCIDToNew(this.NPCID.Value);
		if (this.IsAdd)
		{
			PlayerEx.AddRelationship(npcid, this.Teather, this.DaoLv, this.Brother, this.TuDi);
		}
		else
		{
			PlayerEx.SubRelationship(npcid, this.Teather, this.DaoLv, this.Brother, this.TuDi);
		}
		this.Continue();
	}

	// Token: 0x040013EE RID: 5102
	[Tooltip("是否添加")]
	[SerializeField]
	protected bool IsAdd = true;

	// Token: 0x040013EF RID: 5103
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013F0 RID: 5104
	[Tooltip("师傅")]
	[SerializeField]
	protected bool Teather;

	// Token: 0x040013F1 RID: 5105
	[Tooltip("道侣")]
	[SerializeField]
	protected bool DaoLv;

	// Token: 0x040013F2 RID: 5106
	[Tooltip("兄弟姐妹")]
	[SerializeField]
	protected bool Brother;

	// Token: 0x040013F3 RID: 5107
	[Tooltip("徒弟")]
	[SerializeField]
	protected bool TuDi;
}
