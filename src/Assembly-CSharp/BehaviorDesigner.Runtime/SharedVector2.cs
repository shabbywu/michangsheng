using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime;

[Serializable]
public class SharedVector2 : SharedVariable<Vector2>
{
	public static implicit operator SharedVector2(Vector2 value)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return new SharedVector2
		{
			mValue = value
		};
	}
}
