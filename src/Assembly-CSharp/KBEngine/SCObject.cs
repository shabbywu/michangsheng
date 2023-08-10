using UnityEngine;

namespace KBEngine;

public class SCObject
{
	public virtual bool valid(Entity caster)
	{
		return true;
	}

	public virtual Vector3 getPosition()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Vector3.zero;
	}
}
