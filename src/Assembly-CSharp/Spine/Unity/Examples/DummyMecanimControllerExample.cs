using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AEA RID: 2794
	public class DummyMecanimControllerExample : MonoBehaviour
	{
		// Token: 0x06004E11 RID: 19985 RVA: 0x0021537A File Offset: 0x0021357A
		private void Awake()
		{
			this.isGrounded = true;
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x00215384 File Offset: 0x00213584
		private void Update()
		{
			float num = Input.GetAxisRaw("Horizontal");
			if (Input.GetKey(this.walkButton))
			{
				num += 0.4f;
			}
			this.speed.x = num;
			if (num != 0f)
			{
				this.animationHandle.SetFlip(num);
			}
			if (Input.GetKeyDown(this.jumpButton) && this.isGrounded)
			{
				base.StartCoroutine(this.FakeJump());
			}
			this.logicAnimator.SetFloat(this.horizontalSpeedProperty, Mathf.Abs(this.speed.x));
			this.logicAnimator.SetFloat(this.verticalSpeedProperty, this.speed.y);
			this.logicAnimator.SetBool(this.groundedProperty, this.isGrounded);
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x00215447 File Offset: 0x00213647
		private IEnumerator FakeJump()
		{
			this.isGrounded = false;
			this.speed.y = 10f;
			float durationLeft = this.jumpDuration * 0.5f;
			while (durationLeft > 0f)
			{
				durationLeft -= Time.deltaTime;
				if (!Input.GetKey(this.jumpButton))
				{
					break;
				}
				yield return null;
			}
			this.speed.y = -10f;
			float num = this.jumpDuration * 0.5f - durationLeft;
			yield return new WaitForSeconds(num);
			this.speed.y = 0f;
			this.isGrounded = true;
			yield return null;
			yield break;
		}

		// Token: 0x04004D77 RID: 19831
		public Animator logicAnimator;

		// Token: 0x04004D78 RID: 19832
		public SkeletonAnimationHandleExample animationHandle;

		// Token: 0x04004D79 RID: 19833
		[Header("Controls")]
		public KeyCode walkButton = 304;

		// Token: 0x04004D7A RID: 19834
		public KeyCode jumpButton = 32;

		// Token: 0x04004D7B RID: 19835
		[Header("Animator Properties")]
		public string horizontalSpeedProperty = "Speed";

		// Token: 0x04004D7C RID: 19836
		public string verticalSpeedProperty = "VerticalSpeed";

		// Token: 0x04004D7D RID: 19837
		public string groundedProperty = "Grounded";

		// Token: 0x04004D7E RID: 19838
		[Header("Fake Physics")]
		public float jumpDuration = 1.5f;

		// Token: 0x04004D7F RID: 19839
		public Vector2 speed;

		// Token: 0x04004D80 RID: 19840
		public bool isGrounded;
	}
}
