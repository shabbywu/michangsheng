using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AFF RID: 2815
	public class SpineboyBodyTilt : MonoBehaviour
	{
		// Token: 0x06004E6D RID: 20077 RVA: 0x00216A80 File Offset: 0x00214C80
		private void Start()
		{
			SkeletonAnimation component = base.GetComponent<SkeletonAnimation>();
			Skeleton skeleton = component.Skeleton;
			this.hipBone = skeleton.FindBone(this.hip);
			this.headBone = skeleton.FindBone(this.head);
			this.baseHeadRotation = this.headBone.Rotation;
			component.UpdateLocal += new UpdateBonesDelegate(this.UpdateLocal);
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x00216AE0 File Offset: 0x00214CE0
		private void UpdateLocal(ISkeletonAnimation animated)
		{
			this.hipRotationTarget = this.planter.Balance * this.hipTiltScale;
			this.hipRotationSmoothed = Mathf.MoveTowards(this.hipRotationSmoothed, this.hipRotationTarget, Time.deltaTime * this.hipRotationMoveScale * Mathf.Abs(2f * this.planter.Balance / this.planter.offBalanceThreshold));
			this.hipBone.Rotation = this.hipRotationSmoothed;
			this.headBone.Rotation = this.baseHeadRotation + -this.hipRotationSmoothed * this.headTiltScale;
		}

		// Token: 0x04004DE3 RID: 19939
		[Header("Settings")]
		public SpineboyFootplanter planter;

		// Token: 0x04004DE4 RID: 19940
		[SpineBone("", "", true, false)]
		public string hip = "hip";

		// Token: 0x04004DE5 RID: 19941
		[SpineBone("", "", true, false)]
		public string head = "head";

		// Token: 0x04004DE6 RID: 19942
		public float hipTiltScale = 7f;

		// Token: 0x04004DE7 RID: 19943
		public float headTiltScale = 0.7f;

		// Token: 0x04004DE8 RID: 19944
		public float hipRotationMoveScale = 60f;

		// Token: 0x04004DE9 RID: 19945
		[Header("Debug")]
		public float hipRotationTarget;

		// Token: 0x04004DEA RID: 19946
		public float hipRotationSmoothed;

		// Token: 0x04004DEB RID: 19947
		public float baseHeadRotation;

		// Token: 0x04004DEC RID: 19948
		private Bone hipBone;

		// Token: 0x04004DED RID: 19949
		private Bone headBone;
	}
}
