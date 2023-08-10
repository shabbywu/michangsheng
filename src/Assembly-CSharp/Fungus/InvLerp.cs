using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "InvLerp", "Calculates the inverse lerp, the percentage a value is between two others.", 0)]
[AddComponentMenu("")]
public class InvLerp : Command
{
	[Tooltip("Clamp percentage to 0-1?")]
	[SerializeField]
	protected bool clampResult = true;

	[SerializeField]
	protected FloatData a;

	[SerializeField]
	protected FloatData b;

	[SerializeField]
	protected FloatData value;

	[SerializeField]
	protected FloatData outValue;

	public override void OnEnter()
	{
		if (clampResult)
		{
			outValue.Value = Mathf.InverseLerp(a.Value, b.Value, value.Value);
		}
		else
		{
			outValue.Value = (value.Value - a.Value) / (b.Value - a.Value);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)outValue.floatRef == (Object)null)
		{
			return "Error: no out value selected";
		}
		return outValue.floatRef.Key + " = [" + a.Value + "-" + b.Value + "]";
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)a.floatRef == (Object)(object)variable) && !((Object)(object)b.floatRef == (Object)(object)variable) && !((Object)(object)value.floatRef == (Object)(object)variable))
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
