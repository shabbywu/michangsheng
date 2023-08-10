using UnityEngine;

namespace UltimateSurvival;

public class ReloadLoop : StateMachineBehaviour
{
	private float m_NextReloadTime;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((StateMachineBehaviour)this).OnStateEnter(animator, stateInfo, layerIndex);
		m_NextReloadTime = Time.time + ((AnimatorStateInfo)(ref stateInfo)).length;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		((StateMachineBehaviour)this).OnStateUpdate(animator, stateInfo, layerIndex);
		if (Time.time >= m_NextReloadTime)
		{
			int integer = animator.GetInteger("Reload Loop Count");
			if (integer > 0)
			{
				m_NextReloadTime = Time.time + ((AnimatorStateInfo)(ref stateInfo)).length;
				animator.SetInteger("Reload Loop Count", integer - 1);
			}
		}
	}
}
