using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000E2C RID: 3628
	[SelectionBase]
	public class SpineboyBeginnerModel : MonoBehaviour
	{
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x0600575D RID: 22365 RVA: 0x00244B8C File Offset: 0x00242D8C
		// (remove) Token: 0x0600575E RID: 22366 RVA: 0x00244BC4 File Offset: 0x00242DC4
		public event Action ShootEvent;

		// Token: 0x0600575F RID: 22367 RVA: 0x0003E6FA File Offset: 0x0003C8FA
		public void TryJump()
		{
			base.StartCoroutine(this.JumpRoutine());
		}

		// Token: 0x06005760 RID: 22368 RVA: 0x00244BFC File Offset: 0x00242DFC
		public void TryShoot()
		{
			float time = Time.time;
			if (time - this.lastShootTime > this.shootInterval)
			{
				this.lastShootTime = time;
				if (this.ShootEvent != null)
				{
					this.ShootEvent();
				}
			}
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x00244C3C File Offset: 0x00242E3C
		public void TryMove(float speed)
		{
			this.currentSpeed = speed;
			if (speed != 0f)
			{
				bool flag = speed < 0f;
				this.facingLeft = flag;
			}
			if (this.state != SpineBeginnerBodyState.Jumping)
			{
				this.state = ((speed == 0f) ? SpineBeginnerBodyState.Idle : SpineBeginnerBodyState.Running);
			}
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x0003E709 File Offset: 0x0003C909
		private IEnumerator JumpRoutine()
		{
			if (this.state == SpineBeginnerBodyState.Jumping)
			{
				yield break;
			}
			this.state = SpineBeginnerBodyState.Jumping;
			Vector3 pos = base.transform.localPosition;
			for (float t = 0f; t < 0.6f; t += Time.deltaTime)
			{
				float num = 20f * (0.6f - t);
				base.transform.Translate(num * Time.deltaTime * Vector3.up);
				yield return null;
			}
			for (float t = 0f; t < 0.6f; t += Time.deltaTime)
			{
				float num2 = 20f * t;
				base.transform.Translate(num2 * Time.deltaTime * Vector3.down);
				yield return null;
			}
			base.transform.localPosition = pos;
			pos = default(Vector3);
			this.state = SpineBeginnerBodyState.Idle;
			yield break;
		}

		// Token: 0x04005740 RID: 22336
		[Header("Current State")]
		public SpineBeginnerBodyState state;

		// Token: 0x04005741 RID: 22337
		public bool facingLeft;

		// Token: 0x04005742 RID: 22338
		[Range(-1f, 1f)]
		public float currentSpeed;

		// Token: 0x04005743 RID: 22339
		[Header("Balance")]
		public float shootInterval = 0.12f;

		// Token: 0x04005744 RID: 22340
		private float lastShootTime;
	}
}
