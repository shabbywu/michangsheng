using System;
using UnityEngine;

namespace UltimateSurvival.AI;

[Serializable]
public class EntityVitals
{
	[SerializeField]
	private float m_HungerRegenerationTime;

	private float m_LastTimeFed;

	private bool m_IsHungry;

	public bool IsHungry
	{
		get
		{
			return m_IsHungry;
		}
		set
		{
			m_IsHungry = value;
		}
	}

	public float LastTimeFed
	{
		get
		{
			return m_LastTimeFed;
		}
		set
		{
			m_LastTimeFed = value;
		}
	}

	public void Update(AIBrain brain)
	{
		if (!m_IsHungry && Time.time - m_LastTimeFed > m_HungerRegenerationTime)
		{
			m_IsHungry = true;
		}
		StateData.OverrideValue("Is Hungry", m_IsHungry, brain.WorldState);
	}
}
