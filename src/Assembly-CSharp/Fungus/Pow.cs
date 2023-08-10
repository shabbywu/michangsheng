using UnityEngine;

namespace Fungus;

[CommandInfo("Math", "Pow", "Raise a value to the power of another.", 0)]
[AddComponentMenu("")]
public class Pow : Command
{
	[SerializeField]
	protected FloatData baseValue;

	[SerializeField]
	protected FloatData exponentValue;

	[Tooltip("Where the result of the function is stored.")]
	[SerializeField]
	protected FloatData outValue;

	public override void OnEnter()
	{
		outValue.Value = Mathf.Pow(baseValue.Value, exponentValue.Value);
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)outValue.floatRef == (Object)null)
		{
			return "Error: No out value selected";
		}
		return outValue.floatRef.Key + " = " + baseValue.Value + "^" + exponentValue.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)baseValue.floatRef == (Object)(object)variable) && !((Object)(object)exponentValue.floatRef == (Object)(object)variable))
		{
			return (Object)(object)outValue.floatRef == (Object)(object)variable;
		}
		return true;
	}
}
