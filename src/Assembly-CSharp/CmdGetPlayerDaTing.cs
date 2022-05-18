using System;
using Fungus;
using UnityEngine;

// Token: 0x02000359 RID: 857
[CommandInfo("YSPlayer", "获取玩家是否打听过某NPC", "获取玩家是否打听过某NPC，赋值到TmpBool", 0)]
[AddComponentMenu("")]
public class CmdGetPlayerDaTing : Command
{
	// Token: 0x060018DA RID: 6362 RVA: 0x000DE4A8 File Offset: 0x000DC6A8
	public override void OnEnter()
	{
		bool value = PlayerEx.IsDaTing(NPCEx.NPCIDToNew(this.NPCID.Value));
		this.GetFlowchart().SetBooleanVariable("TmpBool", value);
		this.Continue();
	}

	// Token: 0x040013CD RID: 5069
	[Tooltip("NPC的ID(新老都行)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
