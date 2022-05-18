using System;
using UnityEngine;

namespace UltimateSurvival.GUISystem
{
	// Token: 0x02000952 RID: 2386
	public class DamageGUI : GUIBehaviour
	{
		// Token: 0x06003CF6 RID: 15606 RVA: 0x0002BEE5 File Offset: 0x0002A0E5
		private void Start()
		{
			base.Player.ChangeHealth.AddListener(new Action<HealthEventData>(this.OnSuccess_ChangeHealth));
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x001B29E4 File Offset: 0x001B0BE4
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

		// Token: 0x06003CF8 RID: 15608 RVA: 0x001B2AA8 File Offset: 0x001B0CA8
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

		// Token: 0x04003737 RID: 14135
		[Header("Blood Screen")]
		[SerializeField]
		private ImageFader m_BloodScreenFader;

		// Token: 0x04003738 RID: 14136
		[Header("Damage Indicator")]
		[SerializeField]
		private RectTransform m_IndicatorRT;

		// Token: 0x04003739 RID: 14137
		[SerializeField]
		private ImageFader m_IndicatorFader;

		// Token: 0x0400373A RID: 14138
		[SerializeField]
		[Clamp(0f, 512f)]
		[Tooltip("Damage indicator distance (in pixels) from the screen center.")]
		private int m_IndicatorDistance = 128;

		// Token: 0x0400373B RID: 14139
		private Vector3 m_LastHitPoint;
	}
}
