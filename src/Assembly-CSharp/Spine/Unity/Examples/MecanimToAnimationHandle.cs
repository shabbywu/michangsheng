using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E65 RID: 3685
	public class MecanimToAnimationHandle : StateMachineBehaviour
	{
		// Token: 0x06005845 RID: 22597 RVA: 0x002476E4 File Offset: 0x002458E4
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!this.initialized)
			{
				this.animationHandle = animator.transform.GetComponentInChildren<SkeletonAnimation>();
				this.initialized = true;
			}
			if (this.animationHandle != null)
			{
				foreach (Animation animation in this.animationHandle.Skeleton.Data.Animations)
				{
					if (Animator.StringToHash(animation.Name) == stateInfo.shortNameHash && this.animationHandle.AnimationName != animation.Name)
					{
						this.animationHandle.AnimationName = animation.Name;
						break;
					}
				}
			}
		}

		// Token: 0x04005874 RID: 22644
		private SkeletonAnimation animationHandle;

		// Token: 0x04005875 RID: 22645
		private bool initialized;
	}
}
