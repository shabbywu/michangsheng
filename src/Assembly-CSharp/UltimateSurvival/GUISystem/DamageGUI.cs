using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000652 RID: 1618
	public class DamageGUI : GUIBehaviour
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x001698E3 File Offset: 0x00167AE3
		private void Start()
		{
			base.Player.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnSuccess_ChangeHealth));
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x00169904 File Offset: 0x00167B04
		private void Update()
		{
			if (!this.m_IndicatorFader.Fading)
			{
				return;
			}
			Vector3 normalized = Vector3.ProjectOnPlane(base.Player.LookDirection.Get(), Vector3.up).normalized;
			Vector3 normalized2 = Vector3.ProjectOnPlane(this.m_LastHitPoint - base.Player.transform.position, Vector3.up).normalized;
			Vector3 vector = Vector3.Cross(normalized, Vector3.up);
			float num = Vector3.Angle(normalized, normalized2) * Mathf.Sign(Vector3.Dot(vector, normalized2));
			this.m_IndicatorRT.localEulerAngles = Vector3.forward * num;
			this.m_IndicatorRT.localPosition = this.m_IndicatorRT.up * (float)this.m_IndicatorDistance;
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x001699C8 File Offset: 0x00167BC8
		private void OnSuccess_ChangeHealth(HealthEventData healthEventData)
		{
			if (healthEventData.Delta < 0f)
			{
				this.m_BloodScreenFader.DoFadeCycle(this, healthEventData.Delta / 100f);
				if (healthEventData.HitPoint != Vector3.zero)
				{
					this.m_LastHitPoint = healthEventData.HitPoint;
					this.m_IndicatorFader.DoFadeCycle(this, 1f);
				}
			}
		}

		// Token: 0x04002DBC RID: 11708
		[Header("Blood Screen")]
		[SerializeField]
		private ImageFader m_BloodScreenFader;

		// Token: 0x04002DBD RID: 11709
		[Header("Damage Indicator")]
		[SerializeField]
		private RectTransform m_IndicatorRT;

		// Token: 0x04002DBE RID: 11710
		[SerializeField]
		private ImageFader m_IndicatorFader;

		// Token: 0x04002DBF RID: 11711
		[SerializeField]
		[Clamp(0f, 512f)]
		[Tooltip("Damage indicator distance (in pixels) from the screen center.")]
		private int m_IndicatorDistance = 128;

		// Token: 0x04002DC0 RID: 11712
		private Vector3 m_LastHitPoint;
	}
}
