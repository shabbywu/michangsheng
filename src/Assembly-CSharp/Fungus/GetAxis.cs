using UnityEngine;

namespace Fungus;

[CommandInfo("Input", "GetAxis", "Store Input.GetAxis in a variable", 0)]
[AddComponentMenu("")]
public class GetAxis : Command
{
	[SerializeField]
	protected StringData axisName;

	[Tooltip("If true, calls GetAxisRaw instead of GetAxis")]
	[SerializeField]
	protected bool axisRaw;

	[Tooltip("Float to store the value of the GetAxis")]
	[SerializeField]
	protected FloatData outValue;

	public override void OnEnter()
	{
		if (axisRaw)
		{
			outValue.Value = Input.GetAxisRaw(axisName.Value);
		}
		else
		{
			outValue.Value = Input.GetAxis(axisName.Value);
		}
		Continue();
	}

	public override string GetSummary()
	{
		return string.Concat(axisName, axisRaw ? " Raw" : "");
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)axisName.stringRef == (Object)(object)variable || (Object)(object)outValue.floatRef == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
