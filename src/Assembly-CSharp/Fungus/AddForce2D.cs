using UnityEngine;

namespace Fungus;

[CommandInfo("Rigidbody2D", "AddForce2D", "Add force to a Rigidbody2D", 0)]
[AddComponentMenu("")]
public class AddForce2D : Command
{
	public enum ForceFunction
	{
		AddForce,
		AddForceAtPosition,
		AddRelativeForce
	}

	[SerializeField]
	protected Rigidbody2DData rb;

	[SerializeField]
	protected ForceMode2D forceMode;

	[SerializeField]
	protected ForceFunction forceFunction;

	[Tooltip("Vector of force to be added")]
	[SerializeField]
	protected Vector2Data force;

	[Tooltip("Scale factor to be applied to force as it is used.")]
	[SerializeField]
	protected FloatData forceScaleFactor = new FloatData(1f);

	[Tooltip("World position the force is being applied from. Used only in AddForceAtPosition")]
	[SerializeField]
	protected Vector2Data atPosition;

	public override void OnEnter()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		switch (forceFunction)
		{
		case ForceFunction.AddForce:
			rb.Value.AddForce(force.Value * forceScaleFactor.Value, forceMode);
			break;
		case ForceFunction.AddForceAtPosition:
			rb.Value.AddForceAtPosition(force.Value * forceScaleFactor.Value, atPosition.Value, forceMode);
			break;
		case ForceFunction.AddRelativeForce:
			rb.Value.AddRelativeForce(force.Value * forceScaleFactor.Value, forceMode);
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		return ((object)(ForceMode2D)(ref forceMode)).ToString() + ": " + force;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)rb.rigidbody2DRef == (Object)(object)variable || (Object)(object)force.vector2Ref == (Object)(object)variable || (Object)(object)forceScaleFactor.floatRef == (Object)(object)variable || (Object)(object)atPosition.vector2Ref == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
