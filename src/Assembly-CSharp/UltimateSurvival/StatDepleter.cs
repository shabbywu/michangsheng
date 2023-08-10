using System;
using UnityEngine;

namespace UltimateSurvival;

[Serializable]
public class StatDepleter
{
	[SerializeField]
	[Clamp(0f, 100f)]
	private float m_DepletionRate = 1f;

	[SerializeField]
	[Clamp(0f, 100f)]
	[Tooltip("Damage applied when this stat reaches 0.")]
	private float m_Damage;

	[SerializeField]
	[Clamp(0f, 100f)]
	[Tooltip("How frequent damage will be applied.")]
	private float m_DamagePeriod = 3f;

	private float m_LastDamageTime;

	public void Update(Value<float> statValue, Attempt<HealthEventData> healthChanger)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (statValue.Is(0f) && Time.time - m_LastDamageTime > m_DamagePeriod)
		{
			m_LastDamageTime = Time.time;
			healthChanger.Try(new HealthEventData(-0.01f));
		}
	}
}
