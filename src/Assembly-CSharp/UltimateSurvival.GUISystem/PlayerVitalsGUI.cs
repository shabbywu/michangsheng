using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

public class PlayerVitalsGUI : GUIBehaviour
{
	[SerializeField]
	private Image m_HealthBar;

	[SerializeField]
	private Image m_StaminaBar;

	[SerializeField]
	private Image m_ThirstBar;

	[SerializeField]
	private Image m_HungerBar;

	private void Start()
	{
		base.Player.Health.AddChangeListener(OnChanged_Health);
		base.Player.Stamina.AddChangeListener(OnChanged_Stamina);
		base.Player.Thirst.AddChangeListener(OnChanged_Thirst);
		base.Player.Hunger.AddChangeListener(OnChanged_Hunger);
	}

	private void OnChanged_Health()
	{
		m_HealthBar.fillAmount = base.Player.Health.Get() / 100f;
	}

	private void OnChanged_Stamina()
	{
		m_StaminaBar.fillAmount = base.Player.Stamina.Get() / 100f;
	}

	private void OnChanged_Thirst()
	{
		m_ThirstBar.fillAmount = base.Player.Thirst.Get() / 100f;
	}

	private void OnChanged_Hunger()
	{
		m_HungerBar.fillAmount = base.Player.Hunger.Get() / 100f;
	}
}
