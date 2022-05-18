using System;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x0200096F RID: 2415
	[Serializable]
	public class StabilityPoint
	{
		// Token: 0x06003DC6 RID: 15814 RVA: 0x0002C80F File Offset: 0x0002AA0F
		public StabilityPoint GetClone()
		{
			return (StabilityPoint)base.MemberwiseClone();
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x001B5920 File Offset: 0x001B3B20
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

		// Token: 0x06003DC8 RID: 15816 RVA: 0x001B5998 File Offset: 0x001B3B98
		public void OnDrawGizmosSelected(BuildingPiece piece)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawRay(piece.transform.position + piece.transform.TransformVector(this.m_Position), piece.transform.TransformDirection(this.m_Direction).normalized * this.m_Distance);
		}

		// Token: 0x040037E8 RID: 14312
		[SerializeField]
		private string m_Name;

		// Token: 0x040037E9 RID: 14313
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x040037EA RID: 14314
		[SerializeField]
		private Vector3 m_Direction = Vector3.down;

		// Token: 0x040037EB RID: 14315
		[SerializeField]
		[Clamp(0f, 10f)]
		private float m_Distance = 0.2f;
	}
}
