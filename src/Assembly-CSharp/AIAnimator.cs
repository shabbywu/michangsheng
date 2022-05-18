using System;
using UltimateSurvival;
using UnityEngine;

// Token: 0x020007AC RID: 1964
[RequireComponent(typeof(Animator))]
public class AIAnimator : EntityBehaviour
{
	// Token: 0x060031FA RID: 12794 RVA: 0x000247AE File Offset: 0x000229AE
	private void Start()
	{
		base.Entity.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnAttempt_HealthChange));
		this.m_Animator = base.GetComponent<Animator>();
	}

	// Token: 0x060031FB RID: 12795 RVA: 0x0018D12C File Offset: 0x0018B32C
	private void OnAttempt_HealthChange(HealthEventData data)
	{
		if (data.Delta < 0f)
		{
			float num = Mathf.Abs(data.Delta) / 100f;
			this.m_Animator.SetLayerWeight(1, num);
			this.m_Animator.SetTrigger("Get Hit");
		}
	}

	// Token: 0x04002E29 RID: 11817
	private Animator m_Animator;
}
