using System;
using Fungus;
using UnityEngine;

[Serializable]
public class ShengPingArg
{
	[Tooltip("变量名")]
	public string ArgName;

	[Tooltip("变量值")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	public StringVariable Var;
}
