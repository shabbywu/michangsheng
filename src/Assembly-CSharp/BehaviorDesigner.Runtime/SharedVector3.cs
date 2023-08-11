using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime;

[Serializable]
public class SharedVector3 : SharedVariable<Vector3>
{
	public static implicit operator SharedVector3(Vector3 value)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return new SharedVector3
		{
			mValue = value
		};
	}
}
