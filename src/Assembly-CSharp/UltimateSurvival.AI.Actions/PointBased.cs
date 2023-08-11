using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.AI.Actions;

[Serializable]
public class PointBased : Action
{
	[SerializeField]
	[Tooltip("Determines if the AI will run to the waypoints or just walk.")]
	protected bool m_IsUrgent;

	[SerializeField]
	[Tooltip("Determines whether the AI will go to the points at random or in order.")]
	private ET.PointOrder m_PointSequence;

	[SerializeField]
	[Tooltip("Use predefined points? If not, we will create some at the start.")]
	private bool m_UsePredefinedPoints;

	[SerializeField]
	[Tooltip("This is the tag the predefined points need to have in order to be detected.")]
	private string m_PointTag;

	[Header("Procedural Point Creation")]
	[SerializeField]
	[Tooltip("Determines the amount of points that will be randomly created if we don't want to use predefined ones.")]
	private int m_PointAmount;

	[SerializeField]
	[Tooltip("Determines the max distance from the AI at which the points will be created.")]
	private int m_PointMaxRadius;

	protected int m_CurrentIndex;

	protected List<Vector3> m_PointPositions;

	public List<Vector3> PointPositions => m_PointPositions;

	public override void OnStart(AIBrain brain)
	{
		if (m_UsePredefinedPoints)
		{
			if (!ScriptUtilities.GetTransformsPositionsByTag(m_PointTag, out m_PointPositions))
			{
				Debug.LogError((object)("No waypoints with tag " + m_PointTag + " have been setup"));
			}
		}
		else
		{
			m_PointPositions = ScriptUtilities.GetRandomPositionsAroundTransform(((Component)brain.Settings).transform, m_PointAmount, m_PointMaxRadius);
		}
	}

	public override bool CanActivate(AIBrain brain)
	{
		if (m_PointPositions.Count > 0)
		{
			return true;
		}
		return false;
	}

	public override void Activate(AIBrain brain)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		ChangePatrolPoint();
		brain.Settings.Movement.MoveTo(m_PointPositions[m_CurrentIndex], m_IsUrgent);
	}

	public override void OnUpdate(AIBrain brain)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		brain.Settings.Movement.MoveTo(m_PointPositions[m_CurrentIndex], m_IsUrgent);
	}

	public override void OnDeactivation(AIBrain brain)
	{
		string paramName = (m_IsUrgent ? "Run" : "Walk");
		brain.Settings.Animation.ToggleBool(paramName, value: false);
	}

	public override bool IsDone(AIBrain brain)
	{
		return brain.Settings.Movement.ReachedDestination();
	}

	public void ChangePatrolPoint()
	{
		int num = 0;
		if (m_PointSequence == ET.PointOrder.Sequenced)
		{
			num = (m_CurrentIndex + 1) % m_PointPositions.Count;
		}
		else if (m_PointSequence == ET.PointOrder.Random)
		{
			num = Random.Range(0, m_PointPositions.Count);
		}
		if (num == m_CurrentIndex)
		{
			ChangePatrolPoint();
		}
		else
		{
			m_CurrentIndex = num;
		}
	}
}
