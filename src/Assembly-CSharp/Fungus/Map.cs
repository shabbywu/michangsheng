using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Map", "Map a value that exists in 1 range of numbers to another.", 0)]
[AddComponentMenu("")]
public class Map : Command
{
	[SerializeField]
	protected FloatData initialRangeLower = new FloatData(0f);

	[SerializeField]
	protected FloatData initialRangeUpper = new FloatData(1f);

	[SerializeField]
	protected FloatData value;

	[SerializeField]
	protected FloatData newRangeLower = new FloatData(0f);

	[SerializeField]
	protected FloatData newRangeUpper = new FloatData(1f);

	[SerializeField]
	protected FloatData outValue;

	public override void OnEnter()
	{
		float num = (value.Value - initialRangeLower.Value) / (initialRangeUpper.Value - initialRangeLower.Value) * (newRangeUpper.Value - newRangeLower.Value);
		num += newRangeLower.Value;
		outValue.Value = num;
		Continue();
	}

	public override string GetSummary()
	{
		return "Map [" + initialRangeLower.Value + "-" + initialRangeUpper.Value + "] to [" + newRangeLower.Value + "-" + newRangeUpper.Value + "]";
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)initialRangeLower.floatRef == (Object)(object)variable) && !((Object)(object)initialRangeUpper.floatRef == (Object)(object)variable) && !((Object)(object)value.floatRef == (Object)(object)variable) && !((Object)(object)newRangeLower.floatRef == (Object)(object)variable) && !((Object)(object)newRangeUpper.floatRef == (Object)(object)variable))
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
