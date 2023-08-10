using UnityEngine;

namespace KBEngine;

public class SCPositionObject : SCObject
{
	public Vector3 targetPos;

	public SCPositionObject(Vector3 position)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		targetPos = position;
	}

	public override bool valid(Entity caster)
	{
		return true;
	}

	public override Vector3 getPosition()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return targetPos;
	}
}
