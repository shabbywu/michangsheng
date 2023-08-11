using System;
using Spine;
using Spine.Unity;
using UnityEngine;

public class YSSpineAnimBehaviour : StateMachineBehaviour
{
	private static SkeletonAnimation GetSkeAnim(Animator animator)
	{
		SkeletonAnimation val = ((Component)animator).GetComponent<SkeletonAnimation>();
		if ((Object)(object)val == (Object)null)
		{
			val = ((Component)animator).GetComponentInChildren<SkeletonAnimation>();
		}
		return val;
	}

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		SkeletonAnimation skeAnim = GetSkeAnim(animator);
		if ((Object)(object)skeAnim == (Object)null)
		{
			return;
		}
		Enumerator<Animation> enumerator = ((SkeletonRenderer)skeAnim).skeletonDataAsset.GetSkeletonData(false).Animations.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Animation current = enumerator.Current;
				if (((AnimatorStateInfo)(ref stateInfo)).IsName(current.Name))
				{
					skeAnim.AnimationName = current.Name;
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
