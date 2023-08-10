using System;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateSurvival.GUISystem;

[Serializable]
public class DurabilityBar
{
	[SerializeField]
	private GameObject m_Background;

	[SerializeField]
	private Image m_Bar;

	[SerializeField]
	private Gradient m_ColorGradient;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_Durability;

	private bool m_Active = true;

	public bool Active => m_Active;

	public void SetActive(bool active)
	{
		m_Background.SetActive(active);
		((Behaviour)m_Bar).enabled = active;
		m_Active = active;
	}

	public void SetFillAmount(float fillAmount)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		((Graphic)m_Bar).color = m_ColorGradient.Evaluate(fillAmount);
		m_Bar.fillAmount = fillAmount;
	}
}
