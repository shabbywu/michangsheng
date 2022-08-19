using System;
using Spine;
using Spine.Unity;
using UnityEngine;

// Token: 0x0200048C RID: 1164
public class YSSpineAnimBehaviour : StateMachineBehaviour
{
	// Token: 0x060024C5 RID: 9413 RVA: 0x000FE1B8 File Offset: 0x000FC3B8
	private static SkeletonAnimation GetSkeAnim(Animator animator)
	{
		SkeletonAnimation skeletonAnimation = animator.GetComponent<SkeletonAnimation>();
		if (skeletonAnimation == null)
		{
			skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
		}
		return skeletonAnimation;
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x000FE1E0 File Offset: 0x000FC3E0
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		SkeletonAnimation skeAnim = YSSpineAnimBehaviour.GetSkeAnim(animator);
		if (skeAnim == null)
		{
			return;
		}
		foreach (Animation animation in skeAnim.skeletonDataAsset.GetSkeletonData(false).Animations)
		{
			if (stateInfo.IsName(animation.Name))
			{
				skeAnim.AnimationName = animation.Name;
				break;
			}
		}
	}
}
