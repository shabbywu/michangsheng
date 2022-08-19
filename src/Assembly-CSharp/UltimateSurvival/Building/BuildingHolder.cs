using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000660 RID: 1632
	public class BuildingHolder : MonoBehaviour
	{
		// Token: 0x060033DC RID: 13276 RVA: 0x0016B898 File Offset: 0x00169A98
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

		// Token: 0x060033DD RID: 13277 RVA: 0x0016B8D4 File Offset: 0x00169AD4
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

		// Token: 0x060033DE RID: 13278 RVA: 0x0016B93C File Offset: 0x00169B3C
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

		// Token: 0x04002E10 RID: 11792
		private List<BuildingPiece> m_Pieces = new List<BuildingPiece>();
	}
}
