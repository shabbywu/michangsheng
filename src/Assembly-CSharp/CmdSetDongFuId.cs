using System;
using Fungus;
using UnityEngine;

// Token: 0x02000366 RID: 870
[CommandInfo("YSPlayer", "设置npc去的洞府Id", "设置npc去的洞府Id", 0)]
[AddComponentMenu("")]
public class CmdSetDongFuId : Command
{
	// Token: 0x060018FB RID: 6395 RVA: 0x000DEE9C File Offset: 0x000DD09C
	public override void OnEnter()
	{
		jsonData.instance.AvatarJsonData[this.npc.Value.ToString()].SetField("DongFuId", this.dongFu.Value);
		this.Continue();
	}

	// Token: 0x040013E0 RID: 5088
	[SerializeField]
	[Tooltip("npcId")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable npc;

	// Token: 0x040013E1 RID: 5089
	[SerializeField]
	[Tooltip("洞府ID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable dongFu;
}
