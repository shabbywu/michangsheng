using UnityEngine;

namespace KBEngine;

public class SCEntityObject : SCObject
{
	public int targetID;

	public SCEntityObject(int id)
	{
		targetID = id;
	}

	public override bool valid(Entity caster)
	{
		return true;
	}

	public override Vector3 getPosition()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return KBEngineApp.app.findEntity(targetID)?.position ?? base.getPosition();
	}
}
