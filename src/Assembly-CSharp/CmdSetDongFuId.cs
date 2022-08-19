using System;
using Fungus;
using UnityEngine;

// Token: 0x0200024A RID: 586
[CommandInfo("YSPlayer", "设置npc去的洞府Id", "设置npc去的洞府Id", 0)]
[AddComponentMenu("")]
public class CmdSetDongFuId : Command
{
	// Token: 0x06001643 RID: 5699 RVA: 0x000969C8 File Offset: 0x00094BC8
	public override void OnEnter()
	{
		jsonData.instance.AvatarJsonData[this.npc.Value.ToString()].SetField("DongFuId", this.dongFu.Value);
		this.Continue();
	}

	// Token: 0x04001088 RID: 4232
	[SerializeField]
	[Tooltip("npcId")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable npc;

	// Token: 0x04001089 RID: 4233
	[SerializeField]
	[Tooltip("洞府ID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable dongFu;
}
