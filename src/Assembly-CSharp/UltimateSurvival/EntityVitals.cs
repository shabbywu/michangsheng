using UnityEngine;

namespace UltimateSurvival;

public class EntityVitals : GenericVitals
{
	[Header("Fall Damage")]
	[SerializeField]
	[Range(1f, 15f)]
	[Tooltip("At which landing speed, the entity will start taking damage.")]
	private float m_MinFallSpeed = 4f;

	[SerializeField]
	[Range(10f, 50f)]
	[Tooltip("At which landing speed, the entity will die, if it has no defense.")]
	private float m_MaxFallSpeed = 15f;

	[Header("Audio")]
	[SerializeField]
	[Tooltip("The sounds that will be played when this entity receives damage.")]
	private SoundPlayer m_HurtAudio;

	[SerializeField]
	private float m_TimeBetweenScreams = 1f;

	[SerializeField]
	private SoundPlayer m_FallDamageAudio;

	[Header("Animation")]
	[SerializeField]
	private Animator m_Animator;

	[SerializeField]
	private float m_GetHitMax = 30f;

	private float m_NextTimeCanScream;

	private void Awake()
	{
		base.Entity.ChangeHealth.SetTryer(Try_ChangeHealth);
		base.Entity.Land.AddListener(On_Landed);
		base.Entity.Health.AddChangeListener(OnChanged_Health);
	}

	private void OnChanged_Health()
	{
		float num = base.Entity.Health.Get() - base.Entity.Health.GetLastValue();
		if (num < 0f)
		{
			if ((Object)(object)m_Animator != (Object)null)
			{
				m_Animator.SetFloat("Get Hit Amount", Mathf.Abs(num / m_GetHitMax));
				m_Animator.SetTrigger("Get Hit");
			}
			if (num < 0f && Time.time > m_NextTimeCanScream)
			{
				m_HurtAudio.Play(ItemSelectionMethod.RandomlyButExcludeLast, m_AudioSource);
				m_NextTimeCanScream = Time.time + m_TimeBetweenScreams;
			}
		}
	}

	private void On_Landed(float landSpeed)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (landSpeed >= m_MinFallSpeed)
		{
			base.Entity.ChangeHealth.Try(new HealthEventData(-100f * (landSpeed / m_MaxFallSpeed)));
			m_FallDamageAudio.Play(ItemSelectionMethod.Randomly, m_AudioSource);
		}
	}
}
