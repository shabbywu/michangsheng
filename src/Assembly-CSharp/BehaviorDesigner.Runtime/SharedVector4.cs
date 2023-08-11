using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime;

[Serializable]
public class SharedVector4 : SharedVariable<Vector4>
{
	public static implicit operator SharedVector4(Vector4 value)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return new SharedVector4
		{
			mValue = value
		};
	}
}
