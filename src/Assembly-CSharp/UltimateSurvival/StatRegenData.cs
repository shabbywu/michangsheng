using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x02000877 RID: 2167
	[Serializable]
	public class StatRegenData
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600380F RID: 14351 RVA: 0x00028BB2 File Offset: 0x00026DB2
		public bool CanRegenerate
		{
			get
			{
				return this.m_Enabled && !this.IsPaused;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x00028BC7 File Offset: 0x00026DC7
		public bool Enabled
		{
			get
			{
				return this.m_Enabled;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06003811 RID: 14353 RVA: 0x00028BCF File Offset: 0x00026DCF
		public bool IsPaused
		{
			get
			{
				return Time.time < this.m_NextRegenTime;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06003812 RID: 14354 RVA: 0x00028BDE File Offset: 0x00026DDE
		public float RegenDelta
		{
			get
			{
				return this.m_Speed * Time.deltaTime;
			}
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x00028BEC File Offset: 0x00026DEC
		public void Pause()
		{
			this.m_NextRegenTime = Time.time + this.m_Pause;
		}

		// Token: 0x04003267 RID: 12903
		[SerializeField]
		private bool m_Enabled = true;

		// Token: 0x04003268 RID: 12904
		[SerializeField]
		private float m_Pause = 2f;

		// Token: 0x04003269 RID: 12905
		[SerializeField]
		[Clamp(0f, 1000f)]
		private float m_Speed = 10f;

		// Token: 0x0400326A RID: 12906
		private float m_NextRegenTime;
	}
}
