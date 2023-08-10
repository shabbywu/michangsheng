using System;

namespace Fungus;

[Serializable]
public class FungusVariable
{
	public VariableScope scope;

	public string key = "";

	public FungusVariable(VariableScope scope, string key)
	{
		this.scope = scope;
		this.key = key;
	}
}
