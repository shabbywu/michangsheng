using UnityEngine;

namespace UltimateSurvival.AI.Actions;

public class Attack : Action
{
	[SerializeField]
	private float m_MinAttackDistance = 1.5f;

	[SerializeField]
	private float m_AttackInterval = 0.8f;

	private bool m_HasKilledPlayer;

	private GameObject m_Target;

	private float m_NextTimeCanAttack;

	public override void OnStart(AIBrain brain)
	{
		m_Priority = 10;
		m_RepeatType = ET.ActionRepeatType.Single;
		m_IsInterruptable = false;
		base.Preconditions.Add("Can Attack Player", true);
		base.Effects.Add("Is Player Dead", true);
	}

	public override bool CanActivate(AIBrain brain)
	{
		return brain.Settings.Detection.HasTarget();
	}

	public override void Activate(AIBrain brain)
	{
		m_Target = brain.Settings.Detection.LastChasedTarget;
	}

	public override void OnUpdate(AIBrain brain)
	{
		if (Time.time > m_NextTimeCanAttack)
		{
			brain.Settings.Animation.SetTrigger("Attack");
			m_NextTimeCanAttack = Time.time + m_AttackInterval;
		}
		RotateTowards(((Component)brain).transform, m_Target.transform, 5f);
	}

	public override bool StillValid(AIBrain brain)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		bool flag = Vector3.Distance(m_Target.transform.position, ((Component)brain).transform.position) < m_MinAttackDistance;
		bool flag2 = Time.time > m_NextTimeCanAttack;
		if (!(brain.Settings.Detection.HasTarget() && flag))
		{
			return !flag2;
		}
		return true;
	}

	public override bool IsDone(AIBrain brain)
	{
		if (m_HasKilledPlayer)
		{
			return Time.time > m_NextTimeCanAttack;
		}
		return false;
	}

	private void RotateTowards(Transform transform, Transform target, float rotationSpeed)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = target.position - transform.position;
		Vector3 normalized = ((Vector3)(ref val)).normalized;
		Quaternion val2 = Quaternion.LookRotation(new Vector3(normalized.x, 0f, normalized.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, val2, Time.deltaTime * rotationSpeed);
	}
}
