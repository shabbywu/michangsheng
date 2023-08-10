using UnityEngine;

namespace Fungus;

[CommandInfo("Rigidbody2D", "AddTorque2D", "Add Torque to a Rigidbody2D", 0)]
[AddComponentMenu("")]
public class AddTorque2D : Command
{
	[SerializeField]
	protected Rigidbody2DData rb;

	[SerializeField]
	protected ForceMode2D forceMode;

	[Tooltip("Amount of torque to be added")]
	[SerializeField]
	protected FloatData force;

	public override void OnEnter()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		rb.Value.AddTorque(force.Value, forceMode);
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)rb.Value == (Object)null)
		{
			return "Error: rb not set";
		}
		return ((object)(ForceMode2D)(ref forceMode)).ToString() + ": " + force.Value + (((Object)(object)force.floatRef != (Object)null) ? (" (" + force.floatRef.Key + ")") : "");
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)rb.rigidbody2DRef == (Object)(object)variable || (Object)(object)force.floatRef == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
