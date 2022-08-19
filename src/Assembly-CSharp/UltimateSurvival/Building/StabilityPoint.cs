using System;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000667 RID: 1639
	[Serializable]
	public class StabilityPoint
	{
		// Token: 0x0600341F RID: 13343 RVA: 0x0016CCFE File Offset: 0x0016AEFE
		public StabilityPoint GetClone()
		{
			return (StabilityPoint)base.MemberwiseClone();
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x0016CD0C File Offset: 0x0016AF0C
		public bool IsStable(BuildingPiece piece, LayerMask mask)
		{
			RaycastHit[] array = Physics.RaycastAll(piece.transform.position + piece.transform.TransformVector(this.m_Position), piece.transform.TransformDirection(this.m_Direction), this.m_Distance, mask, 1);
			for (int i = 0; i < array.Length; i++)
			{
				if (!piece.HasCollider(array[i].collider))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x0016CD84 File Offset: 0x0016AF84
		public void OnDrawGizmosSelected(BuildingPiece piece)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(piece.transform.position + piece.transform.TransformVector(this.m_Position), piece.transform.TransformDirection(this.m_Direction).normalized * this.m_Distance);
		}

		// Token: 0x04002E55 RID: 11861
		[SerializeField]
		private string m_Name;

		// Token: 0x04002E56 RID: 11862
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x04002E57 RID: 11863
		[SerializeField]
		private Vector3 m_Direction = Vector3.down;

		// Token: 0x04002E58 RID: 11864
		[SerializeField]
		[Clamp(0f, 10f)]
		private float m_Distance = 0.2f;
	}
}
