using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AD9 RID: 2777
	public class FootSoldierExample : MonoBehaviour
	{
		// Token: 0x06004DCC RID: 19916 RVA: 0x002140E5 File Offset: 0x002122E5
		private void Awake()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			this.skeletonAnimation.OnRebuild += new SkeletonRenderer.SkeletonRendererDelegate(this.Apply);
		}

		// Token: 0x06004DCD RID: 19917 RVA: 0x0021410A File Offset: 0x0021230A
		private void Apply(SkeletonRenderer skeletonRenderer)
		{
			base.StartCoroutine("Blink");
		}

		// Token: 0x06004DCE RID: 19918 RVA: 0x00214118 File Offset: 0x00212318
		private void Update()
		{
			if (Input.GetKey(this.attackKey))
			{
				this.skeletonAnimation.AnimationName = this.attackAnimation;
				return;
			}
			if (Input.GetKey(this.rightKey))
			{
				this.skeletonAnimation.AnimationName = this.moveAnimation;
				this.skeletonAnimation.Skeleton.ScaleX = 1f;
				base.transform.Translate(this.moveSpeed * Time.deltaTime, 0f, 0f);
				return;
			}
			if (Input.GetKey(this.leftKey))
			{
				this.skeletonAnimation.AnimationName = this.moveAnimation;
				this.skeletonAnimation.Skeleton.ScaleX = -1f;
				base.transform.Translate(-this.moveSpeed * Time.deltaTime, 0f, 0f);
				return;
			}
			this.skeletonAnimation.AnimationName = this.idleAnimation;
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x00214200 File Offset: 0x00212400
		private IEnumerator Blink()
		{
			for (;;)
			{
				yield return new WaitForSeconds(Random.Range(0.25f, 3f));
				this.skeletonAnimation.Skeleton.SetAttachment(this.eyesSlot, this.blinkAttachment);
				yield return new WaitForSeconds(this.blinkDuration);
				this.skeletonAnimation.Skeleton.SetAttachment(this.eyesSlot, this.eyesOpenAttachment);
			}
			yield break;
		}

		// Token: 0x04004CFE RID: 19710
		[SpineAnimation("Idle", "", true, false)]
		public string idleAnimation;

		// Token: 0x04004CFF RID: 19711
		[SpineAnimation("", "", true, false)]
		public string attackAnimation;

		// Token: 0x04004D00 RID: 19712
		[SpineAnimation("", "", true, false)]
		public string moveAnimation;

		// Token: 0x04004D01 RID: 19713
		[SpineSlot("", "", false, true, false)]
		public string eyesSlot;

		// Token: 0x04004D02 RID: 19714
		[SpineAttachment(true, false, false, "eyesSlot", "", "", true, false)]
		public string eyesOpenAttachment;

		// Token: 0x04004D03 RID: 19715
		[SpineAttachment(true, false, false, "eyesSlot", "", "", true, false)]
		public string blinkAttachment;

		// Token: 0x04004D04 RID: 19716
		[Range(0f, 0.2f)]
		public float blinkDuration = 0.05f;

		// Token: 0x04004D05 RID: 19717
		public KeyCode attackKey = 323;

		// Token: 0x04004D06 RID: 19718
		public KeyCode rightKey = 100;

		// Token: 0x04004D07 RID: 19719
		public KeyCode leftKey = 97;

		// Token: 0x04004D08 RID: 19720
		public float moveSpeed = 3f;

		// Token: 0x04004D09 RID: 19721
		private SkeletonAnimation skeletonAnimation;
	}
}
