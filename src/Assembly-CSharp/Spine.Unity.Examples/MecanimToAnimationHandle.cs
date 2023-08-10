using System;
using UnityEngine;

namespace Spine.Unity.Examples;

public class MecanimToAnimationHandle : StateMachineBehaviour
{
	private SkeletonAnimation animationHandle;

	private bool initialized;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			animationHandle = ((Component)((Component)animator).transform).GetComponentInChildren<SkeletonAnimation>();
			initialized = true;
		}
		if (!((Object)(object)animationHandle != (Object)null))
		{
			return;
		}
		Enumerator<Animation> enumerator = ((SkeletonRenderer)animationHandle).Skeleton.Data.Animations.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Animation current = enumerator.Current;
				if (Animator.StringToHash(current.Name) == ((AnimatorStateInfo)(ref stateInfo)).shortNameHash && animationHandle.AnimationName != current.Name)
				{
					animationHandle.AnimationName = current.Name;
					break;
				}
			}
		}
		finally
		{
			((IDisposable)enumerator).Dispose();
		}
	}
}
