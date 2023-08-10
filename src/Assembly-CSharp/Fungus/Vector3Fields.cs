using UnityEngine;

namespace Fungus;

[CommandInfo("Vector3", "Fields", "Get or Set the x,y,z fields of a vector3 via floatvars", 0)]
[AddComponentMenu("")]
public class Vector3Fields : Command
{
	public enum GetSet
	{
		Get,
		Set
	}

	public GetSet getOrSet;

	[SerializeField]
	protected Vector3Data vec3;

	[SerializeField]
	protected FloatData x;

	[SerializeField]
	protected FloatData y;

	[SerializeField]
	protected FloatData z;

	public override void OnEnter()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		switch (getOrSet)
		{
		case GetSet.Get:
		{
			Vector3 value = vec3.Value;
			x.Value = value.x;
			y.Value = value.y;
			z.Value = value.z;
			break;
		}
		case GetSet.Set:
			vec3.Value = new Vector3(x.Value, y.Value, z.Value);
			break;
		}
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)vec3.vector3Ref == (Object)null)
		{
			return "Error: vec3 not set";
		}
		return getOrSet.ToString() + " (" + vec3.vector3Ref.Key + ")";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)vec3.vector3Ref == (Object)(object)variable || (Object)(object)x.floatRef == (Object)(object)variable || (Object)(object)y.floatRef == (Object)(object)variable || (Object)(object)z.floatRef == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
