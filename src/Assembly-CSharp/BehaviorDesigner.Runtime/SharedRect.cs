using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime;

[Serializable]
public class SharedRect : SharedVariable<Rect>
{
	public static implicit operator SharedRect(Rect value)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return new SharedRect
		{
			mValue = value
		};
	}
}
