using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020008C3 RID: 2243
	[Serializable]
	public class TransformOffset
	{
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x00029E26 File Offset: 0x00028026
		public Vector3 CurrentPosition
		{
			get
			{
				return this.m_CurrentPosition;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060039B5 RID: 14773 RVA: 0x00029E2E File Offset: 0x0002802E
		public Vector3 CurrentRotation
		{
			get
			{
				return this.m_CurrentRotation;
			}
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x00029E36 File Offset: 0x00028036
		public TransformOffset GetClone()
		{
			return (TransformOffset)base.MemberwiseClone();
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x001A6878 File Offset: 0x001A4A78
		public void Reset()
		{
			this.m_CurrentPosition = (this.m_CurrentRotation = Vector3.zero);
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x00029E43 File Offset: 0x00028043
		public void ContinueFrom(Vector3 position, Vector3 rotation)
		{
			this.m_CurrentPosition = position;
			this.m_CurrentRotation = rotation;
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x00029E53 File Offset: 0x00028053
		public void ContinueFrom(TransformOffset state)
		{
			this.m_CurrentPosition = state.CurrentPosition;
			this.m_CurrentRotation = state.CurrentRotation;
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x001A689C File Offset: 0x001A4A9C
		public void Update(float deltaTime, out Vector3 position, out Quaternion rotation)
		{
			this.m_CurrentPosition = Vector3.Lerp(this.m_CurrentPosition, this.m_Position, deltaTime * this.m_LerpSpeed);
			this.m_CurrentRotation = new Vector3(Mathf.LerpAngle(this.m_CurrentRotation.x, this.m_Rotation.x, deltaTime * this.m_LerpSpeed), Mathf.LerpAngle(this.m_CurrentRotation.y, this.m_Rotation.y, deltaTime * this.m_LerpSpeed), Mathf.LerpAngle(this.m_CurrentRotation.z, this.m_Rotation.z, deltaTime * this.m_LerpSpeed));
			position = this.m_CurrentPosition;
			rotation = Quaternion.Euler(this.m_CurrentRotation);
		}

		// Token: 0x040033DE RID: 13278
		[SerializeField]
		private float m_LerpSpeed = 5f;

		// Token: 0x040033DF RID: 13279
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x040033E0 RID: 13280
		[SerializeField]
		private Vector3 m_Rotation;

		// Token: 0x040033E1 RID: 13281
		private Vector3 m_CurrentPosition;

		// Token: 0x040033E2 RID: 13282
		private Vector3 m_CurrentRotation;
	}
}
