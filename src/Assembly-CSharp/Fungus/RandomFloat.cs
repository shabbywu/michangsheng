using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Variable", "Random Float", "Sets an float variable to a random value in the defined range.", 0)]
[AddComponentMenu("")]
public class RandomFloat : Command
{
	[Tooltip("The variable whos value will be set")]
	[VariableProperty(new Type[] { typeof(FloatVariable) })]
	[SerializeField]
	protected FloatVariable variable;

	[Tooltip("Minimum value for random range")]
	[SerializeField]
	protected FloatData minValue;

	[Tooltip("Maximum value for random range")]
	[SerializeField]
	protected FloatData maxValue;

	public override void OnEnter()
	{
		if ((Object)(object)variable != (Object)null)
		{
			variable.Value = Random.Range(minValue.Value, maxValue.Value);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)variable == (Object)null)
		{
			return "Error: Variable not selected";
		}
		return variable.Key;
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)variable == (Object)(object)this.variable) && !((Object)(object)minValue.floatRef == (Object)(object)variable))
		{
			return (Object)(object)maxValue.floatRef == (Object)(object)variable;
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
