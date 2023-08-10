using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Lerp", "Linearly Interpolate from A to B", 0)]
[AddComponentMenu("")]
public class Lerp : Command
{
	public enum Mode
	{
		Lerp,
		LerpUnclamped,
		LerpAngle
	}

	[SerializeField]
	protected Mode mode;

	[SerializeField]
	protected FloatData a = new FloatData(0f);

	[SerializeField]
	protected FloatData b = new FloatData(1f);

	[SerializeField]
	protected FloatData percentage;

	[SerializeField]
	protected FloatData outValue;

	public override void OnEnter()
	{
		switch (mode)
		{
		case Mode.Lerp:
			outValue.Value = Mathf.Lerp(a.Value, b.Value, percentage.Value);
			break;
		case Mode.LerpUnclamped:
			outValue.Value = Mathf.LerpUnclamped(a.Value, b.Value, percentage.Value);
			break;
		case Mode.LerpAngle:
			outValue.Value = Mathf.LerpAngle(a.Value, b.Value, percentage.Value);
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return mode.ToString() + " [" + a.Value + "-" + b.Value + "]";
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)a.floatRef == (Object)(object)variable) && !((Object)(object)b.floatRef == (Object)(object)variable) && !((Object)(object)percentage.floatRef == (Object)(object)variable))
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
