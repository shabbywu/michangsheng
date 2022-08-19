using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AEB RID: 2795
	public class MecanimToAnimationHandleExample : StateMachineBehaviour
	{
		// Token: 0x06004E15 RID: 19989 RVA: 0x002154AA File Offset: 0x002136AA
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!this.initialized)
			{
				this.animationHandle = animator.GetComponent<SkeletonAnimationHandleExample>();
				this.initialized = true;
			}
			this.animationHandle.PlayAnimationForState(stateInfo.shortNameHash, layerIndex);
		}

		// Token: 0x04004D81 RID: 19841
		private SkeletonAnimationHandleExample animationHandle;

		// Token: 0x04004D82 RID: 19842
		private bool initialized;
	}
}
