using System;
using System.Collections;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x0200086E RID: 2158
	public class RootHeightHandler : PlayerBehaviour
	{
		// Token: 0x060037E7 RID: 14311 RVA: 0x001A1944 File Offset: 0x0019FB44
		private void Start()
		{
			base.Player.Crouch.AddStartListener(new Action(this.OnStart_Crouch));
			base.Player.Crouch.AddStopListener(new Action(this.OnStop_Crouch));
			this.m_InitialHeight = base.transform.localPosition.y;
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000289BA File Offset: 0x00026BBA
		private void OnStart_Crouch()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.SetOffsetOnY(this.m_CrouchOffset));
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000289D5 File Offset: 0x00026BD5
		private void OnStop_Crouch()
		{
			base.StopAllCoroutines();
			base.StartCoroutine(this.SetOffsetOnY(0f));
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000289EF File Offset: 0x00026BEF
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

		// Token: 0x0400322E RID: 12846
		[SerializeField]
		[Clamp(-2f, 0f)]
		private float m_CrouchOffset = -1f;

		// Token: 0x0400322F RID: 12847
		[SerializeField]
		private float m_CrouchSpeed = 5f;

		// Token: 0x04003230 RID: 12848
		private float m_CurrentOffsetOnY;

		// Token: 0x04003231 RID: 12849
		private float m_InitialHeight;
	}
}
