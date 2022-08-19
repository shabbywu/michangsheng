using System;
using Fungus;
using UnityEngine;

// Token: 0x0200037D RID: 893
[CommandInfo("YSPlayer", "根据变量增加赏金评分", "根据变量增加赏金评分", 0)]
[AddComponentMenu("")]
public class CmdAddShangJinPingFen : Command
{
	// Token: 0x06001DAA RID: 7594 RVA: 0x000D17C2 File Offset: 0x000CF9C2
	public override void OnEnter()
	{
		PlayerEx.AddShangJinPingFen((this.shiLiID as IntegerVariable).Value, (this.addCount as IntegerVariable).Value);
		this.Continue();
	}

	// Token: 0x0400183A RID: 6202
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable shiLiID;

	// Token: 0x0400183B RID: 6203
	[SerializeField]
	[Tooltip("增加量")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable addCount;
}
