using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000B05 RID: 2821
	public class MecanimToAnimationHandle : StateMachineBehaviour
	{
		// Token: 0x06004E81 RID: 20097 RVA: 0x00217274 File Offset: 0x00215474
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

		// Token: 0x04004E2B RID: 20011
		private SkeletonAnimation animationHandle;

		// Token: 0x04004E2C RID: 20012
		private bool initialized;
	}
}
