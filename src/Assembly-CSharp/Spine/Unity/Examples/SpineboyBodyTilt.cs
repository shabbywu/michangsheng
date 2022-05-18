using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E5B RID: 3675
	public class SpineboyBodyTilt : MonoBehaviour
	{
		// Token: 0x0600581D RID: 22557 RVA: 0x00246A98 File Offset: 0x00244C98
		private void Start()
		{
			SkeletonAnimation component = base.GetComponent<SkeletonAnimation>();
			Skeleton skeleton = component.Skeleton;
			this.hipBone = skeleton.FindBone(this.hip);
			this.headBone = skeleton.FindBone(this.head);
			this.baseHeadRotation = this.headBone.Rotation;
			component.UpdateLocal += new UpdateBonesDelegate(this.UpdateLocal);
		}

		// Token: 0x0600581E RID: 22558 RVA: 0x00246AF8 File Offset: 0x00244CF8
		private void UpdateLocal(ISkeletonAnimation animated)
		{
			this.hipRotationTarget = this.planter.Balance * this.hipTiltScale;
			this.hipRotationSmoothed = Mathf.MoveTowards(this.hipRotationSmoothed, this.hipRotationTarget, Time.deltaTime * this.hipRotationMoveScale * Mathf.Abs(2f * this.planter.Balance / this.planter.offBalanceThreshold));
			this.hipBone.Rotation = this.hipRotationSmoothed;
			this.headBone.Rotation = this.baseHeadRotation + -this.hipRotationSmoothed * this.headTiltScale;
		}

		// Token: 0x04005819 RID: 22553
		[Header("Settings")]
		public SpineboyFootplanter planter;

		// Token: 0x0400581A RID: 22554
		[SpineBone("", "", true, false)]
		public string hip = "hip";

		// Token: 0x0400581B RID: 22555
		[SpineBone("", "", true, false)]
		public string head = "head";

		// Token: 0x0400581C RID: 22556
		public float hipTiltScale = 7f;

		// Token: 0x0400581D RID: 22557
		public float headTiltScale = 0.7f;

		// Token: 0x0400581E RID: 22558
		public float hipRotationMoveScale = 60f;

		// Token: 0x0400581F RID: 22559
		[Header("Debug")]
		public float hipRotationTarget;

		// Token: 0x04005820 RID: 22560
		public float hipRotationSmoothed;

		// Token: 0x04005821 RID: 22561
		public float baseHeadRotation;

		// Token: 0x04005822 RID: 22562
		private Bone hipBone;

		// Token: 0x04005823 RID: 22563
		private Bone headBone;
	}
}
