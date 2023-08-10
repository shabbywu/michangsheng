using UnityEngine;

namespace Fungus;

[AddComponentMenu("")]
public abstract class BaseUnaryMathCommand : Command
{
	[Tooltip("Value to be passed in to the function.")]
	[SerializeField]
	protected FloatData inValue;

	[Tooltip("Where the result of the function is stored.")]
	[SerializeField]
	protected FloatData outValue;

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override string GetSummary()
	{
		return "in: " + (((Object)(object)inValue.floatRef != (Object)null) ? inValue.floatRef.Key : inValue.Value.ToString()) + ", out: " + (((Object)(object)outValue.floatRef != (Object)null) ? outValue.floatRef.Key : outValue.Value.ToString());
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)variable == (Object)(object)inValue.floatRef))
		{
			return (Object)(object)variable == (Object)(object)outValue.floatRef;
		}
		return true;
	}
}
