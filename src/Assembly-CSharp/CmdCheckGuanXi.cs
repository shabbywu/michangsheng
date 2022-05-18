using System;
using Fungus;
using UnityEngine;

// Token: 0x0200034A RID: 842
[CommandInfo("YSNPCJiaoHu", "检测是否有特殊关系", "检测是否有特殊关系，赋值到TmpValue，0无关系 1师傅 2兄弟 3道侣 4徒弟", 0)]
[AddComponentMenu("")]
public class CmdCheckGuanXi : Command
{
	// Token: 0x060018B2 RID: 6322 RVA: 0x000DD5B8 File Offset: 0x000DB7B8
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

	// Token: 0x040013AC RID: 5036
	[Tooltip("NPC的ID")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
