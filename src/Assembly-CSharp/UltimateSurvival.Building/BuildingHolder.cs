using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building;

public class BuildingHolder : MonoBehaviour
{
	private List<BuildingPiece> m_Pieces = new List<BuildingPiece>();

	public bool HasCollider(Collider col)
	{
		for (int i = 0; i < m_Pieces.Count; i++)
		{
			if (m_Pieces[i].HasCollider(col))
			{
				return true;
			}
		}
		return false;
	}

	public void AddPiece(BuildingPiece piece)
	{
		if (!m_Pieces.Contains(piece))
		{
			m_Pieces.Add(piece);
			((Component)piece).GetComponent<EntityEventHandler>().Death.AddListener(delegate
			{
				OnPieceDeath(piece);
			});
		}
	}

	private void OnPieceDeath(BuildingPiece piece)
	{
		m_Pieces.Remove(piece);
		for (int i = 0; i < m_Pieces.Count; i++)
		{
			for (int j = 0; j < m_Pieces[i].Sockets.Length; j++)
			{
				m_Pieces[i].Sockets[j].OnPieceDeath(piece);
			}
		}
	}
}
