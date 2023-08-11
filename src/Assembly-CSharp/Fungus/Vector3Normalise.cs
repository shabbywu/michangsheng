using UnityEngine;

namespace Fungus;

[CommandInfo("Vector3", "Normalise", "Normalise a Vector3", 0)]
[AddComponentMenu("")]
public class Vector3Normalise : Command
{
	[SerializeField]
	protected Vector3Data vec3In;

	[SerializeField]
	protected Vector3Data vec3Out;

	public override void OnEnter()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		ref Vector3Data reference = ref vec3Out;
		Vector3 value = vec3In.Value;
		reference.Value = ((Vector3)(ref value)).normalized;
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)vec3Out.vector3Ref == (Object)null)
		{
			return "";
		}
		return vec3Out.vector3Ref.Key;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)vec3In.vector3Ref == (Object)(object)variable || (Object)(object)vec3Out.vector3Ref == (Object)(object)variable)
		{
			return true;
		}
		return false;
	}
}
