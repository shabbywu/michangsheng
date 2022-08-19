using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005AC RID: 1452
	public class CameraAnimator : PlayerBehaviour
	{
		// Token: 0x06002F46 RID: 12102 RVA: 0x001568BB File Offset: 0x00154ABB
		private void Awake()
		{
			base.Player.Run.AddStartListener(new Action(this.OnStart_Run));
			base.Player.Run.AddStopListener(new Action(this.OnStop_Run));
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x001568F5 File Offset: 0x00154AF5
		private void OnStart_Run()
		{
			this.m_Animator.SetBool("Run", true);
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x00156908 File Offset: 0x00154B08
		private void OnStop_Run()
		{
			this.m_Animator.SetBool("Run", false);
		}

		// Token: 0x04002980 RID: 10624
		[SerializeField]
		private Animator m_Animator;
	}
}
