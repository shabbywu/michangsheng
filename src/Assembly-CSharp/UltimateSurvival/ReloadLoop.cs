using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008CF RID: 2255
	public class ReloadLoop : StateMachineBehaviour
	{
		// Token: 0x060039FD RID: 14845 RVA: 0x0002A29D File Offset: 0x0002849D
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);
			this.m_NextReloadTime = Time.time + stateInfo.length;
		}

		// Token: 0x060039FE RID: 14846 RVA: 0x001A739C File Offset: 0x001A559C
		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateUpdate(animator, stateInfo, layerIndex);
			if (Time.time >= this.m_NextReloadTime)
			{
				int integer = animator.GetInteger("Reload Loop Count");
				if (integer > 0)
				{
					this.m_NextReloadTime = Time.time + stateInfo.length;
					animator.SetInteger("Reload Loop Count", integer - 1);
				}
			}
		}

		// Token: 0x04003429 RID: 13353
		private float m_NextReloadTime;
	}
}
