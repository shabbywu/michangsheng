using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000964 RID: 2404
	public class BuildingHolder : MonoBehaviour
	{
		// Token: 0x06003D6D RID: 15725 RVA: 0x001B468C File Offset: 0x001B288C
		public bool HasCollider(Collider col)
		{
			for (int i = 0; i < this.m_Pieces.Count; i++)
			{
				if (this.m_Pieces[i].HasCollider(col))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x001B46C8 File Offset: 0x001B28C8
		public void AddPiece(BuildingPiece piece)
		{
			if (!this.m_Pieces.Contains(piece))
			{
				this.m_Pieces.Add(piece);
				piece.GetComponent<EntityEventHandler>().Death.AddListener(delegate
				{
					this.OnPieceDeath(piece);
				});
			}
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x001B4730 File Offset: 0x001B2930
		private void OnPieceDeath(BuildingPiece piece)
		{
			this.m_Pieces.Remove(piece);
			for (int i = 0; i < this.m_Pieces.Count; i++)
			{
				for (int j = 0; j < this.m_Pieces[i].Sockets.Length; j++)
				{
					this.m_Pieces[i].Sockets[j].OnPieceDeath(piece);
				}
			}
		}

		// Token: 0x04003797 RID: 14231
		private List<BuildingPiece> m_Pieces = new List<BuildingPiece>();
	}
}
