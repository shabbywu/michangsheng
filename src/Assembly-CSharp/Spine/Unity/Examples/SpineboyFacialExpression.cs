using System;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000B00 RID: 2816
	public class SpineboyFacialExpression : MonoBehaviour
	{
		// Token: 0x06004E70 RID: 20080 RVA: 0x00216BBC File Offset: 0x00214DBC
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

		// Token: 0x06004E71 RID: 20081 RVA: 0x00216C60 File Offset: 0x00214E60
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

		// Token: 0x04004DEE RID: 19950
		public SpineboyFootplanter footPlanter;

		// Token: 0x04004DEF RID: 19951
		[SpineSlot("", "", false, true, false)]
		public string eyeSlotName;

		// Token: 0x04004DF0 RID: 19952
		[SpineSlot("", "", false, true, false)]
		public string mouthSlotName;

		// Token: 0x04004DF1 RID: 19953
		[SpineAttachment(true, false, false, "eyeSlotName", "", "", true, false)]
		public string shockEyeName;

		// Token: 0x04004DF2 RID: 19954
		[SpineAttachment(true, false, false, "eyeSlotName", "", "", true, false)]
		public string normalEyeName;

		// Token: 0x04004DF3 RID: 19955
		[SpineAttachment(true, false, false, "mouthSlotName", "", "", true, false)]
		public string shockMouthName;

		// Token: 0x04004DF4 RID: 19956
		[SpineAttachment(true, false, false, "mouthSlotName", "", "", true, false)]
		public string normalMouthName;

		// Token: 0x04004DF5 RID: 19957
		public Slot eyeSlot;

		// Token: 0x04004DF6 RID: 19958
		public Slot mouthSlot;

		// Token: 0x04004DF7 RID: 19959
		public Attachment shockEye;

		// Token: 0x04004DF8 RID: 19960
		public Attachment normalEye;

		// Token: 0x04004DF9 RID: 19961
		public Attachment shockMouth;

		// Token: 0x04004DFA RID: 19962
		public Attachment normalMouth;

		// Token: 0x04004DFB RID: 19963
		public float balanceThreshold = 2.5f;

		// Token: 0x04004DFC RID: 19964
		public float shockDuration = 1f;

		// Token: 0x04004DFD RID: 19965
		[Header("Debug")]
		public float shockTimer;
	}
}
