using System;
using UltimateSurvival;
using UnityEngine;

// Token: 0x02000518 RID: 1304
[RequireComponent(typeof(Animator))]
public class AIAnimator : EntityBehaviour
{
	// Token: 0x060029E7 RID: 10727 RVA: 0x0013FE8A File Offset: 0x0013E08A
	private void Start()
	{
		base.Entity.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnAttempt_HealthChange));
		this.m_Animator = base.GetComponent<Animator>();
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x0013FEB4 File Offset: 0x0013E0B4
	private void OnAttempt_HealthChange(HealthEventData data)
	{
		if (data.Delta < 0f)
		{
			float num = Mathf.Abs(data.Delta) / 100f;
			this.m_Animator.SetLayerWeight(1, num);
			this.m_Animator.SetTrigger("Get Hit");
		}
	}

	// Token: 0x04002639 RID: 9785
	private Animator m_Animator;
}
