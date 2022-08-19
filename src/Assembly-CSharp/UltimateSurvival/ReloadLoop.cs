using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005F8 RID: 1528
	public class ReloadLoop : StateMachineBehaviour
	{
		// Token: 0x06003113 RID: 12563 RVA: 0x0015DE48 File Offset: 0x0015C048
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);
			this.m_NextReloadTime = Time.time + stateInfo.length;
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x0015DE68 File Offset: 0x0015C068
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

		// Token: 0x04002B46 RID: 11078
		private float m_NextReloadTime;
	}
}
