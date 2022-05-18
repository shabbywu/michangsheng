using System;
using Fungus;
using UnityEngine;

// Token: 0x0200034B RID: 843
[CommandInfo("YSNPCJiaoHu", "检测是否有某个特殊关系", "检测是否有某个特殊关系", 0)]
[AddComponentMenu("")]
public class CmdCheckGuanXi2 : Command
{
	// Token: 0x060018B4 RID: 6324 RVA: 0x000DD654 File Offset: 0x000DB854
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

	// Token: 0x040013AD RID: 5037
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;

	// Token: 0x040013AE RID: 5038
	[Tooltip("关系")]
	[SerializeField]
	protected GuanXiType GuanXi;

	// Token: 0x040013AF RID: 5039
	[Tooltip("是否有关系")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(BooleanVariable)
	})]
	protected BooleanVariable HasGuanXi;
}
