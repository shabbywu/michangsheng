using System;
using Fungus;
using UnityEngine;

// Token: 0x020004FF RID: 1279
[CommandInfo("YSPlayer", "根据变量增加赏金评分", "根据变量增加赏金评分", 0)]
[AddComponentMenu("")]
public class CmdAddShangJinPingFen : Command
{
	// Token: 0x06002121 RID: 8481 RVA: 0x0001B46E File Offset: 0x0001966E
	public override void OnEnter()
	{
		PlayerEx.AddShangJinPingFen((this.shiLiID as IntegerVariable).Value, (this.addCount as IntegerVariable).Value);
		this.Continue();
	}

	// Token: 0x04001C93 RID: 7315
	[SerializeField]
	[Tooltip("目标势力的ID")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable shiLiID;

	// Token: 0x04001C94 RID: 7316
	[SerializeField]
	[Tooltip("增加量")]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected Variable addCount;
}
