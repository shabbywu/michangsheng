using System;
using Fungus;
using UnityEngine;

// Token: 0x020004F6 RID: 1270
[CommandInfo("YSSea", "增加海域探索度", "增加海域探索度", 0)]
[AddComponentMenu("")]
public class CmdAddSeaTanSuoDu : Command
{
	// Token: 0x06002103 RID: 8451 RVA: 0x0001B371 File Offset: 0x00019571
	public override void OnEnter()
	{
		PlayerEx.AddSeaTanSuoDu(this.SeaID.Value, this.TanSuoDu.Value);
		this.Continue();
	}

	// Token: 0x04001C79 RID: 7289
	[Tooltip("海域ID(大海域)")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable SeaID;

	// Token: 0x04001C7A RID: 7290
	[Tooltip("增加的探索度")]
	[SerializeField]
	[VariableProperty(new Type[]
	{
		typeof(IntegerVariable)
	})]
	protected IntegerVariable TanSuoDu;
}
