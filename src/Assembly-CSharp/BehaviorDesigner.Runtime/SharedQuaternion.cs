using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime;

[Serializable]
public class SharedQuaternion : SharedVariable<Quaternion>
{
	public static implicit operator SharedQuaternion(Quaternion value)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return new SharedQuaternion
		{
			mValue = value
		};
	}
}
