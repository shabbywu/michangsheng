using System;
using Fungus;
using UnityEngine;

// Token: 0x02000249 RID: 585
[CommandInfo("YSPlayer", "移除当前副本中的某个NPC", "移除当前副本中的某个NPC(在移除前确保已经将NPC移动到其他地图)", 0)]
[AddComponentMenu("")]
public class CmdRemoveNPCFromFuBen : Command
{
	// Token: 0x06001641 RID: 5697 RVA: 0x000969AE File Offset: 0x00094BAE
	public override void OnEnter()
	{
		NPCEx.RemoveNPCFromNowFuBen(this.NPCID.Value);
		this.Continue();
	}

	// Token: 0x04001087 RID: 4231
	[SerializeField]
	[Tooltip("NPCID(老ID或者新ID皆可)")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable NPCID;
}
