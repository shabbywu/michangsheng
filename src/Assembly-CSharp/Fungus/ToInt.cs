using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "ToInt", "Command to execute and store the result of a float to int conversion", 0)]
[AddComponentMenu("")]
public class ToInt : Command
{
	public enum Mode
	{
		RoundToInt,
		FloorToInt,
		CeilToInt
	}

	[Tooltip("To integer mode; round, floor or ceil.")]
	[SerializeField]
	protected Mode function;

	[Tooltip("Value to be passed in to the function.")]
	[SerializeField]
	protected FloatData inValue;

	[Tooltip("Where the result of the function is stored.")]
	[SerializeField]
	protected IntegerData outValue;

	public override void OnEnter()
	{
		switch (function)
		{
		case Mode.RoundToInt:
			outValue.Value = Mathf.RoundToInt(inValue.Value);
			break;
		case Mode.FloorToInt:
			outValue.Value = Mathf.FloorToInt(inValue.Value);
			break;
		case Mode.CeilToInt:
			outValue.Value = Mathf.CeilToInt(inValue.Value);
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return function.ToString() + " in: " + (((Object)(object)inValue.floatRef != (Object)null) ? inValue.floatRef.Key : inValue.Value.ToString()) + ", out: " + (((Object)(object)outValue.integerRef != (Object)null) ? outValue.integerRef.Key : outValue.Value.ToString());
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)variable == (Object)(object)inValue.floatRef))
		{
			return (Object)(object)variable == (Object)(object)outValue.integerRef;
		}
		return true;
	}
}
