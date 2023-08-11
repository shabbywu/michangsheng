using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "To String", "Stores the result of a ToString on given variable in a string.", 0)]
[AddComponentMenu("")]
public class ToString : Command
{
	[Tooltip("Target variable to get String of.")]
	[VariableProperty(new Type[] { })]
	[SerializeField]
	protected Variable variable;

	[Tooltip("Variable to store the result of ToString")]
	[VariableProperty(new Type[] { typeof(StringVariable) })]
	[SerializeField]
	protected StringVariable outValue;

	public override void OnEnter()
	{
		if ((Object)(object)variable != (Object)null && (Object)(object)outValue != (Object)null)
		{
			outValue.Value = ((object)variable).ToString();
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)variable == (Object)null)
		{
			return "Error: Variable not selected";
		}
		if ((Object)(object)outValue == (Object)null)
		{
			return "Error: outValue not set";
		}
		return outValue.Key + " = " + variable.Key + ".ToString";
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)variable == (Object)(object)this.variable))
		{
			return (Object)(object)outValue == (Object)(object)variable;
		}
		return true;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)253, (byte)253, (byte)150, byte.MaxValue));
	}
}
