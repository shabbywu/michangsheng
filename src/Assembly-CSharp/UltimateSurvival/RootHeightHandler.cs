using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005B2 RID: 1458
	public class RootHeightHandler : PlayerBehaviour
	{
		// Token: 0x06002F69 RID: 12137 RVA: 0x00157540 File Offset: 0x00155740
		private void Start()
		{
			base.Player.Crouch.AddStartListener(new Action(this.OnStart_Crouch));
			base.Player.Crouch.AddStopListener(new Action(this.OnStop_Crouch));
			this.m_InitialHeight = base.transform.localPosition.y;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x0015759B File Offset: 0x0015579B
		private void OnStart_Crouch()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.SetOffsetOnY(this.m_CrouchOffset));
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x001575B6 File Offset: 0x001557B6
		private void OnStop_Crouch()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.SetOffsetOnY(0f));
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x001575D0 File Offset: 0x001557D0
		private IEnumerator SetOffsetOnY(float targetY)
		{
			WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
			while (Mathf.Abs(targetY - this.m_CurrentOffsetOnY) > Mathf.Epsilon)
			{
				this.m_CurrentOffsetOnY = Mathf.MoveTowards(this.m_CurrentOffsetOnY, targetY, Time.deltaTime * this.m_CrouchSpeed);
				base.transform.localPosition = Vector3.up * (this.m_CurrentOffsetOnY + this.m_InitialHeight);
				yield return waitForFixedUpdate;
			}
			yield break;
		}

		// Token: 0x040029AD RID: 10669
		[SerializeField]
		[Clamp(-2f, 0f)]
		private float m_CrouchOffset = -1f;

		// Token: 0x040029AE RID: 10670
		[SerializeField]
		private float m_CrouchSpeed = 5f;

		// Token: 0x040029AF RID: 10671
		private float m_CurrentOffsetOnY;

		// Token: 0x040029B0 RID: 10672
		private float m_InitialHeight;
	}
}
