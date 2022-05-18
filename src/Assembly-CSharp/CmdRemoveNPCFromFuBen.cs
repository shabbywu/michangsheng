using System;
using Fungus;
using UnityEngine;

// Token: 0x02000365 RID: 869
[CommandInfo("YSPlayer", "移除当前副本中的某个NPC", "移除当前副本中的某个NPC(在移除前确保已经将NPC移动到其他地图)", 0)]
[AddComponentMenu("")]
public class CmdRemoveNPCFromFuBen : Command
{
	// Token: 0x060018F9 RID: 6393 RVA: 0x000156E0 File Offset: 0x000138E0
	public override void OnEnter()
	{
		NPCEx.RemoveNPCFromNowFuBen(this.NPCID.Value);
		this.Continue();
	}

	// Token: 0x040013DF RID: 5087
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
