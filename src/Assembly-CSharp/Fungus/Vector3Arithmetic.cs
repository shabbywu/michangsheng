using UnityEngine;

namespace Fungus;

[CommandInfo("Vector3", "Arithmetic", "Vector3 add, sub, mul, div arithmetic", 0)]
[AddComponentMenu("")]
public class Vector3Arithmetic : Command
{
	public enum Operation
	{
		Add,
		Sub,
		Mul,
		Div
	}

	[SerializeField]
	protected Vector3Data lhs;

	[SerializeField]
	protected Vector3Data rhs;

	[SerializeField]
	protected Vector3Data output;

	[SerializeField]
	protected Operation operation;

	public override void OnEnter()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		Vector3 value;
		switch (operation)
		{
		case Operation.Add:
			output.Value = lhs.Value + rhs.Value;
			break;
		case Operation.Sub:
			output.Value = lhs.Value - rhs.Value;
			break;
		case Operation.Mul:
			value = lhs.Value;
			((Vector3)(ref value)).Scale(rhs.Value);
			output.Value = value;
			break;
		case Operation.Div:
			value = lhs.Value;
			((Vector3)(ref value)).Scale(new Vector3(1f / rhs.Value.x, 1f / rhs.Value.y, 1f / rhs.Value.z));
			output.Value = value;
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)output.vector3Ref == (Object)null)
		{
			return "Error: no output set";
		}
		return operation.ToString() + ": stored in " + output.vector3Ref.Key;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)lhs.vector3Ref == (Object)(object)variable || (Object)(object)rhs.vector3Ref == (Object)(object)variable || (Object)(object)output.vector3Ref == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
