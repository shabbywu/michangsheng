using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E27 RID: 3623
	public class SpineBeginnerTwo : MonoBehaviour
	{
		// Token: 0x06005749 RID: 22345 RVA: 0x0003E5FB File Offset: 0x0003C7FB
		private void Start()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			this.spineAnimationState = this.skeletonAnimation.AnimationState;
			this.skeleton = this.skeletonAnimation.Skeleton;
			base.StartCoroutine(this.DoDemoRoutine());
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x0003E638 File Offset: 0x0003C838
		private IEnumerator DoDemoRoutine()
		{
			for (;;)
			{
				this.spineAnimationState.SetAnimation(0, this.walkAnimationName, true);
				yield return new WaitForSeconds(this.runWalkDuration);
				this.spineAnimationState.SetAnimation(0, this.runAnimationName, true);
				yield return new WaitForSeconds(this.runWalkDuration);
				this.spineAnimationState.SetAnimation(0, this.runToIdleAnimationName, false);
				this.spineAnimationState.AddAnimation(0, this.idleAnimationName, true, 0f);
				yield return new WaitForSeconds(1f);
				this.skeleton.ScaleX = -1f;
				this.spineAnimationState.SetAnimation(0, this.idleTurnAnimationName, false);
				this.spineAnimationState.AddAnimation(0, this.idleAnimationName, true, 0f);
				yield return new WaitForSeconds(0.5f);
				this.skeleton.ScaleX = 1f;
				this.spineAnimationState.SetAnimation(0, this.idleTurnAnimationName, false);
				this.spineAnimationState.AddAnimation(0, this.idleAnimationName, true, 0f);
				yield return new WaitForSeconds(0.5f);
			}
			yield break;
		}

		// Token: 0x04005727 RID: 22311
		[SpineAnimation("", "", true, false)]
		public string runAnimationName;

		// Token: 0x04005728 RID: 22312
		[SpineAnimation("", "", true, false)]
		public string idleAnimationName;

		// Token: 0x04005729 RID: 22313
		[SpineAnimation("", "", true, false)]
		public string walkAnimationName;

		// Token: 0x0400572A RID: 22314
		[SpineAnimation("", "", true, false)]
		public string shootAnimationName;

		// Token: 0x0400572B RID: 22315
		[Header("Transitions")]
		[SpineAnimation("", "", true, false)]
		public string idleTurnAnimationName;

		// Token: 0x0400572C RID: 22316
		[SpineAnimation("", "", true, false)]
		public string runToIdleAnimationName;

		// Token: 0x0400572D RID: 22317
		public float runWalkDuration = 1.5f;

		// Token: 0x0400572E RID: 22318
		private SkeletonAnimation skeletonAnimation;

		// Token: 0x0400572F RID: 22319
		public AnimationState spineAnimationState;

		// Token: 0x04005730 RID: 22320
		public Skeleton skeleton;
	}
}
