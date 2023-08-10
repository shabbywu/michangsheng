using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI.Actions;

public class Chase : Action
{
	[SerializeField]
	private SoundPlayer m_InitialScreamAudio;

	[SerializeField]
	private SoundPlayer m_ScreamingAudio;

	[SerializeField]
	private Vector2 m_ScreamInterval = new Vector2(0.7f, 1.2f);

	private Transform m_Target;

	private float m_NextScreamTime;

	public override void OnStart(AIBrain brain)
	{
		m_Priority = 1;
		m_IsInterruptable = false;
		m_RepeatType = ET.ActionRepeatType.Single;
		base.Preconditions.Add("Player in sight", true);
		base.Effects.Add("Can Attack Player", true);
		ResetValues();
	}

	public override bool CanActivate(AIBrain brain)
	{
		if (!brain.Settings.Detection.HasTarget())
		{
			return false;
		}
		m_Target = brain.Settings.Detection.GetRandomTarget();
		brain.Settings.Detection.LastChasedTarget = ((Component)m_Target).gameObject;
		return true;
	}

	public override void Activate(AIBrain brain)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		brain.Settings.Movement.MoveTo(m_Target.position, fastMove: true);
		if (Vector3.Distance(((Component)brain).transform.position, m_Target.position) > 3.5f)
		{
			m_InitialScreamAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, brain.Settings.AudioSource);
		}
	}

	public override void OnUpdate(AIBrain brain)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		brain.Settings.Movement.MoveTo(m_Target.position, fastMove: true);
		NavMeshAgent agent = brain.Settings.Movement.Agent;
		Vector3 velocity = agent.velocity;
		if (((Vector3)(ref velocity)).sqrMagnitude < 0.01f)
		{
			brain.Settings.Animation.ToggleBool("Run", value: false);
			agent.updateRotation = false;
			RotateTowards(((Component)agent).transform, m_Target, 5f);
			return;
		}
		agent.updateRotation = true;
		brain.Settings.Animation.ToggleBool("Run", value: true);
		if (Time.time > m_NextScreamTime)
		{
			m_ScreamingAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, brain.Settings.AudioSource);
			m_NextScreamTime = Time.time + Random.Range(m_ScreamInterval.x, m_ScreamInterval.y);
		}
	}

	public override bool StillValid(AIBrain brain)
	{
		bool num = brain.Settings.Detection.HasTarget();
		if (!num)
		{
			brain.Settings.Animation.ToggleBool("Run", value: false);
			brain.Settings.Movement.Agent.updateRotation = true;
		}
		return num;
	}

	public override bool IsDone(AIBrain brain)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (brain.Settings.Movement.ReachedDestination())
		{
			Vector3 val = ((Component)brain).transform.position - m_Target.position;
			return ((Vector3)(ref val)).sqrMagnitude < brain.Settings.Movement.Agent.stoppingDistance * brain.Settings.Movement.Agent.stoppingDistance;
		}
		return false;
	}

	public override void ResetValues()
	{
		m_Target = null;
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
