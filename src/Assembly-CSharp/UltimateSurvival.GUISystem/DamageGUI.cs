using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class DamageGUI : GUIBehaviour
{
	[Header("Blood Screen")]
	[SerializeField]
	private ImageFader m_BloodScreenFader;

	[Header("Damage Indicator")]
	[SerializeField]
	private RectTransform m_IndicatorRT;

	[SerializeField]
	private ImageFader m_IndicatorFader;

	[SerializeField]
	[Clamp(0f, 512f)]
	[Tooltip("Damage indicator distance (in pixels) from the screen center.")]
	private int m_IndicatorDistance = 128;

	private Vector3 m_LastHitPoint;

	private void Start()
	{
		base.Player.ChangeHealth.AddListener(OnSuccess_ChangeHealth);
	}

	private void Update()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		if (m_IndicatorFader.Fading)
		{
			Vector3 val = Vector3.ProjectOnPlane(base.Player.LookDirection.Get(), Vector3.up);
			Vector3 normalized = ((Vector3)(ref val)).normalized;
			val = Vector3.ProjectOnPlane(m_LastHitPoint - ((Component)base.Player).transform.position, Vector3.up);
			Vector3 normalized2 = ((Vector3)(ref val)).normalized;
			Vector3 val2 = Vector3.Cross(normalized, Vector3.up);
			float num = Vector3.Angle(normalized, normalized2) * Mathf.Sign(Vector3.Dot(val2, normalized2));
			((Transform)m_IndicatorRT).localEulerAngles = Vector3.forward * num;
			((Transform)m_IndicatorRT).localPosition = ((Transform)m_IndicatorRT).up * (float)m_IndicatorDistance;
		}
	}

	private void OnSuccess_ChangeHealth(HealthEventData healthEventData)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (healthEventData.Delta < 0f)
		{
			m_BloodScreenFader.DoFadeCycle((MonoBehaviour)(object)this, healthEventData.Delta / 100f);
			if (healthEventData.HitPoint != Vector3.zero)
			{
				m_LastHitPoint = healthEventData.HitPoint;
				m_IndicatorFader.DoFadeCycle((MonoBehaviour)(object)this, 1f);
			}
		}
	}
}
