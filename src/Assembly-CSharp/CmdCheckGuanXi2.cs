using System;
using Fungus;
using UnityEngine;

// Token: 0x0200022F RID: 559
[CommandInfo("YSNPCJiaoHu", "检测是否有某个特殊关系", "检测是否有某个特殊关系", 0)]
[AddComponentMenu("")]
public class CmdCheckGuanXi2 : Command
{
	// Token: 0x060015FC RID: 5628 RVA: 0x00094FCC File Offset: 0x000931CC
	public override void OnEnter()
	{
		int item = NPCEx.NPCIDToNew(this.NPCID.Value);
		if (this.GuanXi == GuanXiType.None)
		{
			this.HasGuanXi.Value = false;
		}
		else if (this.GuanXi == GuanXiType.道侣)
		{
			if (PlayerEx.Player.DaoLvId.ToList().Contains(item))
			{
				this.HasGuanXi.Value = true;
			}
		}
		else if (this.GuanXi == GuanXiType.师傅)
		{
			if (PlayerEx.Player.TeatherId.ToList().Contains(item))
			{
				this.HasGuanXi.Value = true;
			}
		}
		else if (this.GuanXi == GuanXiType.兄弟 && PlayerEx.Player.Brother.ToList().Contains(item))
		{
			this.HasGuanXi.Value = true;
		}
		this.Continue();
	}

	// Token: 0x04001055 RID: 4181
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x04001056 RID: 4182
	[Tooltip("关系")]
	[SerializeField]
	protected GuanXiType GuanXi;

	// Token: 0x04001057 RID: 4183
	[Tooltip("是否有关系")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable)
	})]
	protected BooleanVariable HasGuanXi;
}
