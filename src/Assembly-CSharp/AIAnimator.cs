using UltimateSurvival;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AIAnimator : EntityBehaviour
{
	private Animator m_Animator;

	private void Start()
	{
		base.Entity.ChangeHealth.AddListener(OnAttempt_HealthChange);
		m_Animator = ((Component)this).GetComponent<Animator>();
	}

	private void OnAttempt_HealthChange(HealthEventData data)
	{
		if (data.Delta < 0f)
		{
			float num = Mathf.Abs(data.Delta) / 100f;
			m_Animator.SetLayerWeight(1, num);
			m_Animator.SetTrigger("Get Hit");
		}
	}
}
