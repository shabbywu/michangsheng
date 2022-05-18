using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E3A RID: 3642
	public class MecanimToAnimationHandleExample : StateMachineBehaviour
	{
		// Token: 0x06005797 RID: 22423 RVA: 0x0003EA17 File Offset: 0x0003CC17
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!this.initialized)
			{
				this.animationHandle = animator.GetComponent<SkeletonAnimationHandleExample>();
				this.initialized = true;
			}
			this.animationHandle.PlayAnimationForState(stateInfo.shortNameHash, layerIndex);
		}

		// Token: 0x04005791 RID: 22417
		private SkeletonAnimationHandleExample animationHandle;

		// Token: 0x04005792 RID: 22418
		private bool initialized;
	}
}
