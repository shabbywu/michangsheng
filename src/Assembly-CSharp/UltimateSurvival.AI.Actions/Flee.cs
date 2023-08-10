using UnityEngine;

namespace UltimateSurvival.AI.Actions;

public class Flee : Action
{
	[SerializeField]
	[Clamp(0f, 100f)]
	private float m_MinFleeDistance = 3f;

	[SerializeField]
	private SoundPlayer m_ScreamAudio;

	[SerializeField]
	[Clamp(0f, 15f)]
	private float m_ScreamInterval = 3f;

	private Vector3 m_FleePosition;

	private float m_LastTimeScreamed;

	public override void OnStart(AIBrain brain)
	{
		m_Priority = 12;
		m_IsInterruptable = true;
		m_RepeatType = ET.ActionRepeatType.Single;
		base.Preconditions.Add("Player in sight", true);
		base.Effects.Add("Player in sight", false);
	}

	public override bool CanActivate(AIBrain brain)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (brain.Settings.Detection.HasTarget())
		{
			Vector3 position = ((Component)brain).transform.position;
			Vector3 position2 = ((Component)brain.Settings.Detection.GetRandomTarget()).transform.position;
			Vector3 val = position - position2;
			return ((Vector3)(ref val)).magnitude < m_MinFleeDistance;
		}
		return false;
	}

	public override void Activate(AIBrain brain)
	{
	}

	public override void OnUpdate(AIBrain brain)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Vector3 position = ((Component)brain).transform.position;
		Vector3 position2 = ((Component)brain.Settings.Detection.GetRandomTarget()).transform.position;
		m_FleePosition = position - position2;
		m_FleePosition += ((Component)brain).transform.position;
		brain.Settings.Movement.MoveTo(m_FleePosition, fastMove: true);
		if (Time.time > m_LastTimeScreamed + m_ScreamInterval)
		{
			m_ScreamAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, brain.Settings.AudioSource);
			m_LastTimeScreamed = Time.time;
		}
	}

	public override void OnDeactivation(AIBrain brain)
	{
		brain.Settings.Animation.ToggleBool("Run", value: false);
	}

	public override bool StillValid(AIBrain brain)
	{
		return brain.Settings.Detection.HasTarget();
	}

	public override bool IsDone(AIBrain brain)
	{
		return !brain.Settings.Detection.HasTarget();
	}
}
