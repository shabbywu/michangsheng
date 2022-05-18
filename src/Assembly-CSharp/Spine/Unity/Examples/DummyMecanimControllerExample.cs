using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E38 RID: 3640
	public class DummyMecanimControllerExample : MonoBehaviour
	{
		// Token: 0x0600578D RID: 22413 RVA: 0x0003E9E8 File Offset: 0x0003CBE8
		private void Awake()
		{
			this.isGrounded = true;
		}

		// Token: 0x0600578E RID: 22414 RVA: 0x00245394 File Offset: 0x00243594
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

		// Token: 0x0600578F RID: 22415 RVA: 0x0003E9F1 File Offset: 0x0003CBF1
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

		// Token: 0x04005783 RID: 22403
		public Animator logicAnimator;

		// Token: 0x04005784 RID: 22404
		public SkeletonAnimationHandleExample animationHandle;

		// Token: 0x04005785 RID: 22405
		[Header("Controls")]
		public KeyCode walkButton = 304;

		// Token: 0x04005786 RID: 22406
		public KeyCode jumpButton = 32;

		// Token: 0x04005787 RID: 22407
		[Header("Animator Properties")]
		public string horizontalSpeedProperty = "Speed";

		// Token: 0x04005788 RID: 22408
		public string verticalSpeedProperty = "VerticalSpeed";

		// Token: 0x04005789 RID: 22409
		public string groundedProperty = "Grounded";

		// Token: 0x0400578A RID: 22410
		[Header("Fake Physics")]
		public float jumpDuration = 1.5f;

		// Token: 0x0400578B RID: 22411
		public Vector2 speed;

		// Token: 0x0400578C RID: 22412
		public bool isGrounded;
	}
}
