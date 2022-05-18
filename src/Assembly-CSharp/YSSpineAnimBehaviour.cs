using System;
using Spine;
using Spine.Unity;
using UnityEngine;

// Token: 0x0200065B RID: 1627
public class YSSpineAnimBehaviour : StateMachineBehaviour
{
	// Token: 0x0600289F RID: 10399 RVA: 0x000B6004 File Offset: 0x000B4204
	private static SkeletonAnimation GetSkeAnim(Animator animator)
	{
		SkeletonAnimation skeletonAnimation = animator.GetComponent<SkeletonAnimation>();
		if (skeletonAnimation == null)
		{
			skeletonAnimation = animator.GetComponentInChildren<SkeletonAnimation>();
		}
		return skeletonAnimation;
	}

	// Token: 0x060028A0 RID: 10400 RVA: 0x0013D594 File Offset: 0x0013B794
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
