using System;
using Fungus;
using UnityEngine;

// Token: 0x020001DF RID: 479
[Serializable]
public class ShengPingArg
{
	// Token: 0x04000EFB RID: 3835
	[Tooltip("变量名")]
	public string ArgName;

	// Token: 0x04000EFC RID: 3836
	[Tooltip("变量值")]
	[VariableProperty(new Type[]
	{
		typeof(StringVariable)
	})]
	public StringVariable Var;
}
