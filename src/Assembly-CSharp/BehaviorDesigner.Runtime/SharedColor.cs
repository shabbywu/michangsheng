using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime;

[Serializable]
public class SharedColor : SharedVariable<Color>
{
	public static implicit operator SharedColor(Color value)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return new SharedColor
		{
			mValue = value
		};
	}
}
