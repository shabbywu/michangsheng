using System;
using Fungus;
using UnityEngine;

// Token: 0x020002F2 RID: 754
[Serializable]
public class ShengPingArg
{
	// Token: 0x04001239 RID: 4665
	[Tooltip("变量名")]
	public string ArgName;

	// Token: 0x0400123A RID: 4666
	[Tooltip("变量值")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	public StringVariable Var;
}
