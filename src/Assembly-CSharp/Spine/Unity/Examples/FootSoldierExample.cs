using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E20 RID: 3616
	public class FootSoldierExample : MonoBehaviour
	{
		// Token: 0x0600572A RID: 22314 RVA: 0x0003E515 File Offset: 0x0003C715
		private void Awake()
		{
			this.skeletonAnimation = base.GetComponent<SkeletonAnimation>();
			this.skeletonAnimation.OnRebuild += new SkeletonRenderer.SkeletonRendererDelegate(this.Apply);
		}

		// Token: 0x0600572B RID: 22315 RVA: 0x0003E53A File Offset: 0x0003C73A
		private void Apply(SkeletonRenderer skeletonRenderer)
		{
			base.StartCoroutine("Blink");
		}

		// Token: 0x0600572C RID: 22316 RVA: 0x00244014 File Offset: 0x00242214
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

		// Token: 0x0600572D RID: 22317 RVA: 0x0003E548 File Offset: 0x0003C748
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

		// Token: 0x040056EC RID: 22252
		[SpineAnimation("Idle", "", true, false)]
		public string idleAnimation;

		// Token: 0x040056ED RID: 22253
		[SpineAnimation("", "", true, false)]
		public string attackAnimation;

		// Token: 0x040056EE RID: 22254
		[SpineAnimation("", "", true, false)]
		public string moveAnimation;

		// Token: 0x040056EF RID: 22255
		[SpineSlot("", "", false, true, false)]
		public string eyesSlot;

		// Token: 0x040056F0 RID: 22256
		[SpineAttachment(true, false, false, "eyesSlot", "", "", true, false)]
		public string eyesOpenAttachment;

		// Token: 0x040056F1 RID: 22257
		[SpineAttachment(true, false, false, "eyesSlot", "", "", true, false)]
		public string blinkAttachment;

		// Token: 0x040056F2 RID: 22258
		[Range(0f, 0.2f)]
		public float blinkDuration = 0.05f;

		// Token: 0x040056F3 RID: 22259
		public KeyCode attackKey = 323;

		// Token: 0x040056F4 RID: 22260
		public KeyCode rightKey = 100;

		// Token: 0x040056F5 RID: 22261
		public KeyCode leftKey = 97;

		// Token: 0x040056F6 RID: 22262
		public float moveSpeed = 3f;

		// Token: 0x040056F7 RID: 22263
		private SkeletonAnimation skeletonAnimation;
	}
}
