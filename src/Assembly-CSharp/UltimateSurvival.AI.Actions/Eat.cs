using UnityEngine;

namespace UltimateSurvival.AI.Actions;

public class Eat : Action
{
	[SerializeField]
	[Tooltip("Determines the time it will take for the AI to eat.")]
	private float m_EatTime;

	private bool m_IsDoneEating;

	private float m_EatStartTime;

	public override void OnStart(AIBrain brain)
	{
		m_Priority = 8;
		m_IsInterruptable = true;
		m_RepeatType = ET.ActionRepeatType.Single;
		base.Preconditions.Add("Next To Food", true);
		base.Effects.Add("Is Hungry", false);
	}

	public override void Activate(AIBrain brain)
	{
		ResetValues();
		brain.Settings.Animation.ToggleBool("Eat", value: true);
		m_EatStartTime = Time.time;
	}

	public override void OnUpdate(AIBrain brain)
	{
		if (Time.time - m_EatStartTime > m_EatTime)
		{
			brain.Settings.Vitals.LastTimeFed = Time.time;
			brain.Settings.Vitals.IsHungry = false;
			m_IsDoneEating = true;
			brain.Settings.Animation.ToggleBool("Eat", value: false);
		}
	}

	public override void OnDeactivation(AIBrain brain)
	{
		brain.Settings.Animation.ToggleBool("Eat", value: false);
	}

	public override bool IsDone(AIBrain brain)
	{
		return m_IsDoneEating;
	}

	public override void ResetValues()
	{
		m_IsDoneEating = false;
		m_EatStartTime = 0f;
	}
}
