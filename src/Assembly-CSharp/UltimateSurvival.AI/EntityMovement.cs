using System;
using UnityEngine;
using UnityEngine.AI;

namespace UltimateSurvival.AI;

[Serializable]
public class EntityMovement
{
	[SerializeField]
	[Tooltip("Normal speed the agent will use.")]
	private float m_WalkSpeed;

	[Tooltip("Speed the agent will only use whenever an action requires it to hurry.")]
	[SerializeField]
	private float m_RunSpeed;

	private Vector3 m_CurrentDestination;

	private ET.AIMovementState m_MovementState;

	private AIBrain m_Brain;

	private NavMeshAgent m_Agent;

	public Vector3 CurrentDestination => m_CurrentDestination;

	public ET.AIMovementState MovementState => m_MovementState;

	public NavMeshAgent Agent => m_Agent;

	public void Initialize(AIBrain brain)
	{
		m_Brain = brain;
		m_Agent = ((Component)m_Brain).GetComponent<NavMeshAgent>();
	}

	public void Update(Transform transform)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = m_Agent.nextPosition - transform.position;
		if (((Vector3)(ref val)).magnitude > m_Agent.radius)
		{
			m_Agent.nextPosition = transform.position + 0.9f * val;
		}
	}

	public NavMeshPath MoveTo(Vector3 position, bool fastMove = false)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_0081: Expected O, but got Unknown
		NavMeshPath val = new NavMeshPath();
		m_Agent.SetDestination(position);
		m_CurrentDestination = position;
		bool num = fastMove && m_Brain.Settings.Animation.ParameterExists("Run");
		bool flag = m_Brain.Settings.Animation.ParameterExists("Walk");
		if (num)
		{
			ChangeMovementState(m_RunSpeed, "Run", animValue: true, ET.AIMovementState.Running);
			return val;
		}
		if (flag)
		{
			ChangeMovementState(m_WalkSpeed, "Walk", animValue: true, ET.AIMovementState.Walking);
		}
		return val;
	}

	private void ChangeMovementState(float speed, string animName, bool animValue, ET.AIMovementState newState)
	{
		m_Agent.speed = speed;
		m_Brain.Settings.Animation.ToggleBool(animName, animValue);
		m_MovementState = newState;
	}

	public bool ReachedDestination(bool isStop = true)
	{
		if (m_Agent.remainingDistance <= m_Agent.stoppingDistance)
		{
			if (isStop)
			{
				string paramName = ((m_MovementState == ET.AIMovementState.Running) ? "Run" : "Walk");
				m_Brain.Settings.Animation.ToggleBool(paramName, value: false);
				m_MovementState = ET.AIMovementState.Idle;
			}
			return true;
		}
		return false;
	}
}
