using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples
{
	// Token: 0x02000AE0 RID: 2784
	[SelectionBase]
	public class SpineboyBeginnerModel : MonoBehaviour
	{
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06004DE7 RID: 19943 RVA: 0x002149F8 File Offset: 0x00212BF8
		// (remove) Token: 0x06004DE8 RID: 19944 RVA: 0x00214A30 File Offset: 0x00212C30
		public event Action ShootEvent;

		// Token: 0x06004DE9 RID: 19945 RVA: 0x00214A65 File Offset: 0x00212C65
		public void TryJump()
		{
			base.StartCoroutine(this.JumpRoutine());
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x00214A74 File Offset: 0x00212C74
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

		// Token: 0x06004DEB RID: 19947 RVA: 0x00214AB4 File Offset: 0x00212CB4
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

		// Token: 0x06004DEC RID: 19948 RVA: 0x00214AFB File Offset: 0x00212CFB
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

		// Token: 0x04004D3C RID: 19772
		[Header("Current State")]
		public SpineBeginnerBodyState state;

		// Token: 0x04004D3D RID: 19773
		public bool facingLeft;

		// Token: 0x04004D3E RID: 19774
		[Range(-1f, 1f)]
		public float currentSpeed;

		// Token: 0x04004D3F RID: 19775
		[Header("Balance")]
		public float shootInterval = 0.12f;

		// Token: 0x04004D40 RID: 19776
		private float lastShootTime;
	}
}
