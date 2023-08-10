using UnityEngine;

namespace UltimateSurvival;

public class PlayerVitals : EntityVitals
{
	[Header("Stamina")]
	[SerializeField]
	[Clamp(0f, 300f)]
	private float m_StaminaDepletionRate = 30f;

	[SerializeField]
	private StatRegenData m_StaminaRegeneration;

	[SerializeField]
	private SoundPlayer m_BreathingHeavyAudio;

	[SerializeField]
	private float m_BreathingHeavyDuration = 11f;

	[SerializeField]
	[Clamp(0f, 100f)]
	private float m_JumpStaminaTake = 15f;

	[Header("Thirst")]
	[SerializeField]
	private StatDepleter m_ThirstDepletion;

	[Header("Hunger")]
	[SerializeField]
	private StatDepleter m_HungerDepletion;

	[Header("Sleeping")]
	[SerializeField]
	private bool m_SleepRestoresHealth = true;

	private PlayerEventHandler m_Player;

	private float m_LastHeavyBreathTime;

	public PlayerEventHandler Player
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)m_Player))
			{
				m_Player = ((Component)this).GetComponent<PlayerEventHandler>();
			}
			if (!Object.op_Implicit((Object)(object)m_Player))
			{
				m_Player = ((Component)this).GetComponentInParent<PlayerEventHandler>();
			}
			return m_Player;
		}
	}

	protected override void Update()
	{
		m_ThirstDepletion.Update(Player.Thirst, Player.ChangeHealth);
		m_HungerDepletion.Update(Player.Hunger, Player.ChangeHealth);
	}

	protected override bool Try_ChangeHealth(HealthEventData healthEventData)
	{
		healthEventData.Delta *= 1f - (float)Player.Defense.Get() / 100f;
		return base.Try_ChangeHealth(healthEventData);
	}

	private void Start()
	{
		Player.Run.AddStartTryer(delegate
		{
			m_StaminaRegeneration.Pause();
			return Player.Stamina.Get() > 0f;
		});
		Player.Jump.AddStartListener(delegate
		{
			ModifyStamina(0f - m_JumpStaminaTake);
		});
		if (m_SleepRestoresHealth)
		{
			Player.Sleep.AddStopListener(delegate
			{
				//IL_0013: Unknown result type (might be due to invalid IL or missing references)
				//IL_0019: Unknown result type (might be due to invalid IL or missing references)
				//IL_001c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0022: Unknown result type (might be due to invalid IL or missing references)
				Player.ChangeHealth.Try(new HealthEventData(100f));
			});
		}
	}

	private void ModifyStamina(float delta)
	{
		float num = Player.Stamina.Get() + delta;
		num = Mathf.Clamp(num, 0f, 100f);
		Player.Stamina.Set(num);
	}
}
