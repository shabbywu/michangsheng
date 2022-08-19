using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005B9 RID: 1465
	[Serializable]
	public class StatRegenData
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06002F8B RID: 12171 RVA: 0x00158085 File Offset: 0x00156285
		public bool CanRegenerate
		{
			get
			{
				return this.m_Enabled && !this.IsPaused;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x0015809A File Offset: 0x0015629A
		public bool Enabled
		{
			get
			{
				return this.m_Enabled;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x001580A2 File Offset: 0x001562A2
		public bool IsPaused
		{
			get
			{
				return Time.time < this.m_NextRegenTime;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x001580B1 File Offset: 0x001562B1
		public float RegenDelta
		{
			get
			{
				return this.m_Speed * Time.deltaTime;
			}
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x001580BF File Offset: 0x001562BF
		public void Pause()
		{
			this.m_NextRegenTime = Time.time + this.m_Pause;
		}

		// Token: 0x040029DD RID: 10717
		[SerializeField]
		private bool m_Enabled = true;

		// Token: 0x040029DE RID: 10718
		[SerializeField]
		private float m_Pause = 2f;

		// Token: 0x040029DF RID: 10719
		[SerializeField]
		[Clamp(0f, 1000f)]
		private float m_Speed = 10f;

		// Token: 0x040029E0 RID: 10720
		private float m_NextRegenTime;
	}
}
