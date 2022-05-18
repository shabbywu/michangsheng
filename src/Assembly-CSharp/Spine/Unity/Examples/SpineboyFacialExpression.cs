using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E5C RID: 3676
	public class SpineboyFacialExpression : MonoBehaviour
	{
		// Token: 0x06005820 RID: 22560 RVA: 0x00246B94 File Offset: 0x00244D94
		private void Start()
		{
			Skeleton skeleton = base.GetComponent<SkeletonAnimation>().Skeleton;
			this.eyeSlot = skeleton.FindSlot(this.eyeSlotName);
			this.mouthSlot = skeleton.FindSlot(this.mouthSlotName);
			int num = skeleton.FindSlotIndex(this.eyeSlotName);
			this.shockEye = skeleton.GetAttachment(num, this.shockEyeName);
			this.normalEye = skeleton.GetAttachment(num, this.normalEyeName);
			int num2 = skeleton.FindSlotIndex(this.mouthSlotName);
			this.shockMouth = skeleton.GetAttachment(num2, this.shockMouthName);
			this.normalMouth = skeleton.GetAttachment(num2, this.normalMouthName);
		}

		// Token: 0x06005821 RID: 22561 RVA: 0x00246C38 File Offset: 0x00244E38
		private void Update()
		{
			if (Mathf.Abs(this.footPlanter.Balance) > this.balanceThreshold)
			{
				this.shockTimer = this.shockDuration;
			}
			if (this.shockTimer > 0f)
			{
				this.shockTimer -= Time.deltaTime;
			}
			if (this.shockTimer > 0f)
			{
				this.eyeSlot.Attachment = this.shockEye;
				this.mouthSlot.Attachment = this.shockMouth;
				return;
			}
			this.eyeSlot.Attachment = this.normalEye;
			this.mouthSlot.Attachment = this.normalMouth;
		}

		// Token: 0x04005824 RID: 22564
		public SpineboyFootplanter footPlanter;

		// Token: 0x04005825 RID: 22565
		[SpineSlot("", "", false, true, false)]
		public string eyeSlotName;

		// Token: 0x04005826 RID: 22566
		[SpineSlot("", "", false, true, false)]
		public string mouthSlotName;

		// Token: 0x04005827 RID: 22567
		[SpineAttachment(true, false, false, "eyeSlotName", "", "", true, false)]
		public string shockEyeName;

		// Token: 0x04005828 RID: 22568
		[SpineAttachment(true, false, false, "eyeSlotName", "", "", true, false)]
		public string normalEyeName;

		// Token: 0x04005829 RID: 22569
		[SpineAttachment(true, false, false, "mouthSlotName", "", "", true, false)]
		public string shockMouthName;

		// Token: 0x0400582A RID: 22570
		[SpineAttachment(true, false, false, "mouthSlotName", "", "", true, false)]
		public string normalMouthName;

		// Token: 0x0400582B RID: 22571
		public Slot eyeSlot;

		// Token: 0x0400582C RID: 22572
		public Slot mouthSlot;

		// Token: 0x0400582D RID: 22573
		public Attachment shockEye;

		// Token: 0x0400582E RID: 22574
		public Attachment normalEye;

		// Token: 0x0400582F RID: 22575
		public Attachment shockMouth;

		// Token: 0x04005830 RID: 22576
		public Attachment normalMouth;

		// Token: 0x04005831 RID: 22577
		public float balanceThreshold = 2.5f;

		// Token: 0x04005832 RID: 22578
		public float shockDuration = 1f;

		// Token: 0x04005833 RID: 22579
		[Header("Debug")]
		public float shockTimer;
	}
}
