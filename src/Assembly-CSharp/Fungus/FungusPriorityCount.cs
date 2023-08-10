using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Priority Signals", "Get Priority Count", "Copy the value of the Priority Count to a local IntegerVariable, intended primarily to assist with debugging use of Priority.", 0)]
public class FungusPriorityCount : Command
{
	[VariableProperty(new Type[] { typeof(IntegerVariable) })]
	public IntegerVariable outVar;

	public override void OnEnter()
	{
		outVar.Value = FungusPrioritySignals.CurrentPriorityDepth;
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)outVar == (Object)null)
		{
			return "Error: No out var supplied";
		}
		return outVar.Key;
	}

	public override bool HasReference(Variable variable)
	{
		return (Object)(object)outVar == (Object)(object)variable;
	}
}
