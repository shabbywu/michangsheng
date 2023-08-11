using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime;

[Serializable]
public class SharedAnimationCurve : SharedVariable<AnimationCurve>
{
	public static implicit operator SharedAnimationCurve(AnimationCurve value)
	{
		return new SharedAnimationCurve
		{
			mValue = value
		};
	}
}
