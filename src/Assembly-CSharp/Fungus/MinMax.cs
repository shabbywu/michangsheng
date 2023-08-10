using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "MinMax", "Command to store the min or max of 2 values", 0)]
[AddComponentMenu("")]
public class MinMax : Command
{
	public enum Function
	{
		Min,
		Max
	}

	[Tooltip("Min Or Max")]
	[SerializeField]
	protected Function function;

	[SerializeField]
	protected FloatData inLHSValue;

	[SerializeField]
	protected FloatData inRHSValue;

	[SerializeField]
	protected FloatData outValue;

	public override void OnEnter()
	{
		switch (function)
		{
		case Function.Min:
			outValue.Value = Mathf.Min(inLHSValue.Value, inRHSValue.Value);
			break;
		case Function.Max:
			outValue.Value = Mathf.Max(inLHSValue.Value, inRHSValue.Value);
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return function.ToString() + " out: " + (((Object)(object)outValue.floatRef != (Object)null) ? outValue.floatRef.Key : outValue.Value.ToString()) + " [" + inLHSValue.Value + " - " + inRHSValue.Value + "]";
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)inLHSValue.floatRef == (Object)(object)variable) && !((Object)(object)inRHSValue.floatRef == (Object)(object)variable))
		{
			return (Object)(object)outValue.floatRef == (Object)(object)variable;
		}
		return true;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
