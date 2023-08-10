using UnityEngine;

namespace Fungus;

[CommandInfo("Vector3", "ToVector2", "Convert Fungus Vector3 to Fungus Vector2", 0)]
[AddComponentMenu("")]
public class Vector3ToVector2 : Command
{
	[SerializeField]
	protected Vector3Data vec3;

	[SerializeField]
	protected Vector2Data vec2;

	public override void OnEnter()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		vec2.Value = Vector2.op_Implicit(vec3.Value);
		Continue();
	}

	public override string GetSummary()
	{
		if ((Object)(object)vec3.vector3Ref != (Object)null && (Object)(object)vec2.vector2Ref != (Object)null)
		{
			return "Converting " + vec3.vector3Ref.Key + " to " + vec2.vector2Ref.Key;
		}
		return "Error: variables not set";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if ((Object)(object)variable == (Object)(object)vec3.vector3Ref || (Object)(object)variable == (Object)(object)vec2.vector2Ref)
		{
			return true;
		}
		return false;
	}
}
