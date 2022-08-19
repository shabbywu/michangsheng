using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000ADD RID: 2781
	public class SpineBeginnerTwo : MonoBehaviour
	{
		// Token: 0x06004DDF RID: 19935 RVA: 0x002148BD File Offset: 0x00212ABD
		private void Start()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			this.spineAnimationState = this.skeletonAnimation.AnimationState;
			this.skeleton = this.skeletonAnimation.Skeleton;
			base.StartCoroutine(this.DoDemoRoutine());
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x002148FA File Offset: 0x00212AFA
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

		// Token: 0x04004D2A RID: 19754
		[SpineAnimation("", "", true, false)]
		public string runAnimationName;

		// Token: 0x04004D2B RID: 19755
		[SpineAnimation("", "", true, false)]
		public string idleAnimationName;

		// Token: 0x04004D2C RID: 19756
		[SpineAnimation("", "", true, false)]
		public string walkAnimationName;

		// Token: 0x04004D2D RID: 19757
		[SpineAnimation("", "", true, false)]
		public string shootAnimationName;

		// Token: 0x04004D2E RID: 19758
		[Header("Transitions")]
		[SpineAnimation("", "", true, false)]
		public string idleTurnAnimationName;

		// Token: 0x04004D2F RID: 19759
		[SpineAnimation("", "", true, false)]
		public string runToIdleAnimationName;

		// Token: 0x04004D30 RID: 19760
		public float runWalkDuration = 1.5f;

		// Token: 0x04004D31 RID: 19761
		private SkeletonAnimation skeletonAnimation;

		// Token: 0x04004D32 RID: 19762
		public AnimationState spineAnimationState;

		// Token: 0x04004D33 RID: 19763
		public Skeleton skeleton;
	}
}
