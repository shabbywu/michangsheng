using UnityEngine;

namespace UltimateSurvival;

public class GenericVitals : EntityBehaviour
{
	[Header("Health & Damage")]
	[SerializeField]
	[Tooltip("The health to start with.")]
	private float m_MaxHealth = 100f;

	[SerializeField]
	private StatRegenData m_HealthRegeneration;

	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("0 -> the damage received will not be decreased, \n1 -> the damage will be reduced to 0 (GOD mode).")]
	private float m_Resistance = 0.1f;

	[Header("Audio")]
	[SerializeField]
	protected AudioSource m_AudioSource;

	protected float m_HealthDelta;

	private float m_NextRegenTime;

	private void Start()
	{
		base.Entity.ChangeHealth.SetTryer(Try_ChangeHealth);
		m_MaxHealth *= 5f;
	}

	protected virtual void Update()
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (m_HealthRegeneration.CanRegenerate && base.Entity.Health.Get() < 100f && base.Entity.Health.Get() > 0f)
		{
			HealthEventData arg = new HealthEventData(m_HealthRegeneration.RegenDelta);
			base.Entity.ChangeHealth.Try(arg);
		}
	}

	protected virtual bool Try_ChangeHealth(HealthEventData healthEventData)
	{
		if (base.Entity.Health.Get() == 0f)
		{
			return false;
		}
		if (healthEventData.Delta > 0f && base.Entity.Health.Get() == 100f)
		{
			return false;
		}
		float num = healthEventData.Delta;
		if (num < 0f)
		{
			num *= 1f - m_Resistance;
		}
		float value = Mathf.Clamp(base.Entity.Health.Get() + num, 0f, 100f);
		base.Entity.Health.Set(value);
		if (num < 0f)
		{
			m_HealthRegeneration.Pause();
		}
		return true;
	}
}
