using System;
using Fungus;
using UnityEngine;

// Token: 0x0200022E RID: 558
[CommandInfo("YSNPCJiaoHu", "检测是否有特殊关系", "检测是否有特殊关系，赋值到TmpValue，0无关系 1师傅 2兄弟 3道侣 4徒弟", 0)]
[AddComponentMenu("")]
public class CmdCheckGuanXi : Command
{
	// Token: 0x060015FA RID: 5626 RVA: 0x00094F30 File Offset: 0x00093130
	public override void OnEnter()
	{
		int value = 0;
		int item = NPCEx.NPCIDToNew(this.NPCID.Value);
		if (PlayerEx.Player.TeatherId.ToList().Contains(item))
		{
			value = 1;
		}
		if (PlayerEx.Player.Brother.ToList().Contains(item))
		{
			value = 2;
		}
		if (PlayerEx.Player.DaoLvId.ToList().Contains(item))
		{
			value = 3;
		}
		if (PlayerEx.Player.TuDiId.ToList().Contains(item))
		{
			value = 4;
		}
		this.GetFlowchart().SetIntegerVariable("TmpValue", value);
		this.Continue();
	}

	// Token: 0x04001054 RID: 4180
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
