using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000868 RID: 2152
	public class CameraAnimator : PlayerBehaviour
	{
		// Token: 0x060037C4 RID: 14276 RVA: 0x0002874E File Offset: 0x0002694E
		private void Awake()
		{
			base.Player.Run.AddStartListener(new Action(this.OnStart_Run));
			base.Player.Run.AddStopListener(new Action(this.OnStop_Run));
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x00028788 File Offset: 0x00026988
		private void OnStart_Run()
		{
			this.m_Animator.SetBool("Run", true);
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x0002879B File Offset: 0x0002699B
		private void OnStop_Run()
		{
			this.m_Animator.SetBool("Run", false);
		}

		// Token: 0x04003201 RID: 12801
		[SerializeField]
		private Animator m_Animator;
	}
}
