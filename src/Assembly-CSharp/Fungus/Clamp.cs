using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Clamp", "Command to contain a value between a lower and upper bound, with optional wrapping modes", 0)]
[AddComponentMenu("")]
public class Clamp : Command
{
	public enum Mode
	{
		Clamp,
		Repeat,
		PingPong
	}

	[SerializeField]
	protected Mode mode;

	[SerializeField]
	protected FloatData lower;

	[SerializeField]
	protected FloatData upper;

	[SerializeField]
	protected FloatData value;

	[Tooltip("Result put here, if using pingpong don't use the same var for value as outValue.")]
	[SerializeField]
	protected FloatData outValue;

	public override void OnEnter()
	{
		float num = lower.Value;
		float num2 = upper.Value;
		float num3 = value.Value;
		switch (mode)
		{
		case Mode.Clamp:
			outValue.Value = Mathf.Clamp(value.Value, lower.Value, upper.Value);
			break;
		case Mode.Repeat:
			outValue.Value = Mathf.Repeat(num3 - num, num2 - num) + num;
			break;
		case Mode.PingPong:
			outValue.Value = Mathf.PingPong(num3 - num, num2 - num) + num;
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)outValue.floatRef == (Object)null)
		{
			return "Error: no output value selected";
		}
		return outValue.floatRef.Key + " = " + Mode.Clamp.ToString() + ((mode != 0) ? (" & " + mode) : "");
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)lower.floatRef == (Object)(object)variable) && !((Object)(object)upper.floatRef == (Object)(object)variable) && !((Object)(object)value.floatRef == (Object)(object)variable))
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
