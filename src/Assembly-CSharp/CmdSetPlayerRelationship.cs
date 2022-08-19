using System;
using Fungus;
using UnityEngine;

// Token: 0x02000252 RID: 594
[CommandInfo("YSPlayer", "设置玩家人际关系", "设置玩家人际关系", 0)]
[AddComponentMenu("")]
public class CmdSetPlayerRelationship : Command
{
	// Token: 0x06001651 RID: 5713 RVA: 0x00096D58 File Offset: 0x00094F58
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

	// Token: 0x0400109C RID: 4252
	[Tooltip("是否添加")]
	[SerializeField]
	protected bool IsAdd = true;

	// Token: 0x0400109D RID: 4253
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x0400109E RID: 4254
	[Tooltip("师傅")]
	[SerializeField]
	protected bool Teather;

	// Token: 0x0400109F RID: 4255
	[Tooltip("道侣")]
	[SerializeField]
	protected bool DaoLv;

	// Token: 0x040010A0 RID: 4256
	[Tooltip("兄弟姐妹")]
	[SerializeField]
	protected bool Brother;

	// Token: 0x040010A1 RID: 4257
	[Tooltip("徒弟")]
	[SerializeField]
	protected bool TuDi;
}
