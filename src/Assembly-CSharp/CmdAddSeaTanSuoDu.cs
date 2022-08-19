using System;
using Fungus;
using UnityEngine;

// Token: 0x02000377 RID: 887
[CommandInfo("YSSea", "增加海域探索度", "增加海域探索度", 0)]
[AddComponentMenu("")]
public class CmdAddSeaTanSuoDu : Command
{
	// Token: 0x06001D9A RID: 7578 RVA: 0x000D1446 File Offset: 0x000CF646
	public override void OnEnter()
	{
		PlayerEx.AddSeaTanSuoDu(this.SeaID.Value, this.TanSuoDu.Value);
		this.Continue();
	}

	// Token: 0x04001829 RID: 6185
	[Tooltip("海域ID(大海域)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable SeaID;

	// Token: 0x0400182A RID: 6186
	[Tooltip("增加的探索度")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable TanSuoDu;
}
