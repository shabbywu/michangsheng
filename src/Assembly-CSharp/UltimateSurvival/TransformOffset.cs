using System;
using UnityEngine;

namespace UltimateSurvival
{
	// Token: 0x020005EF RID: 1519
	[Serializable]
	public class TransformOffset
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x0015D1EB File Offset: 0x0015B3EB
		public Vector3 CurrentPosition
		{
			get
			{
				return this.m_CurrentPosition;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060030D7 RID: 12503 RVA: 0x0015D1F3 File Offset: 0x0015B3F3
		public Vector3 CurrentRotation
		{
			get
			{
				return this.m_CurrentRotation;
			}
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0015D1FB File Offset: 0x0015B3FB
		public TransformOffset GetClone()
		{
			return (TransformOffset)base.MemberwiseClone();
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0015D208 File Offset: 0x0015B408
		public void Reset()
		{
			this.m_CurrentPosition = (this.m_CurrentRotation = Vector3.zero);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x0015D229 File Offset: 0x0015B429
		public void ContinueFrom(Vector3 position, Vector3 rotation)
		{
			this.m_CurrentPosition = position;
			this.m_CurrentRotation = rotation;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x0015D239 File Offset: 0x0015B439
		public void ContinueFrom(TransformOffset state)
		{
			this.m_CurrentPosition = state.CurrentPosition;
			this.m_CurrentRotation = state.CurrentRotation;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x0015D254 File Offset: 0x0015B454
		public void Update(float deltaTime, out Vector3 position, out Quaternion rotation)
		{
			this.m_CurrentPosition = Vector3.Lerp(this.m_CurrentPosition, this.m_Position, deltaTime * this.m_LerpSpeed);
			this.m_CurrentRotation = new Vector3(Mathf.LerpAngle(this.m_CurrentRotation.x, this.m_Rotation.x, deltaTime * this.m_LerpSpeed), Mathf.LerpAngle(this.m_CurrentRotation.y, this.m_Rotation.y, deltaTime * this.m_LerpSpeed), Mathf.LerpAngle(this.m_CurrentRotation.z, this.m_Rotation.z, deltaTime * this.m_LerpSpeed));
			position = this.m_CurrentPosition;
			rotation = Quaternion.Euler(this.m_CurrentRotation);
		}

		// Token: 0x04002B08 RID: 11016
		[SerializeField]
		private float m_LerpSpeed = 5f;

		// Token: 0x04002B09 RID: 11017
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x04002B0A RID: 11018
		[SerializeField]
		private Vector3 m_Rotation;

		// Token: 0x04002B0B RID: 11019
		private Vector3 m_CurrentPosition;

		// Token: 0x04002B0C RID: 11020
		private Vector3 m_CurrentRotation;
	}
}
