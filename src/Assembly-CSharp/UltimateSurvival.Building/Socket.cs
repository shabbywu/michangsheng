using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building;

public class Socket : MonoBehaviour
{
	[Serializable]
	public class PieceOffset
	{
		[SerializeField]
		private BuildingPiece m_Piece;

		[SerializeField]
		private Vector3 m_PositionOffset = Vector3.one;

		[SerializeField]
		private Vector3 m_RotationOffset;

		public BuildingPiece Piece
		{
			get
			{
				return m_Piece;
			}
			set
			{
				m_Piece = value;
			}
		}

		public Vector3 PositionOffset
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return m_PositionOffset;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				m_PositionOffset = value;
			}
		}

		public Quaternion RotationOffset => Quaternion.Euler(m_RotationOffset);

		public Vector3 RotationOffsetEuler
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return m_RotationOffset;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				m_RotationOffset = value;
			}
		}

		public PieceOffset GetMemberwiseClone()
		{
			return (PieceOffset)MemberwiseClone();
		}
	}

	public class SpaceOccupier
	{
		public BuildingSpace OccupiedSpace { get; private set; }

		public BuildingPiece Occupier { get; private set; }

		public SpaceOccupier(BuildingSpace occupiedSpace, BuildingPiece occupier)
		{
			OccupiedSpace = occupiedSpace;
			Occupier = occupier;
		}
	}

	[SerializeField]
	private List<PieceOffset> m_PieceOffsets;

	[SerializeField]
	private float m_Radius = 1f;

	private BuildingPiece m_Piece;

	private List<BuildingSpace> m_OccupiedSpaces = new List<BuildingSpace>();

	private List<SpaceOccupier> m_Occupiers = new List<SpaceOccupier>();

	public List<BuildingSpace> OccupiedSpaces => m_OccupiedSpaces;

	public BuildingPiece Piece => m_Piece;

	public List<PieceOffset> PieceOffsets
	{
		get
		{
			return m_PieceOffsets;
		}
		set
		{
			m_PieceOffsets = value;
		}
	}

	public float Radius
	{
		get
		{
			return m_Radius;
		}
		set
		{
			m_Radius = value;
		}
	}

	private void Awake()
	{
		SphereCollider obj = ((Component)this).gameObject.AddComponent<SphereCollider>();
		((Collider)obj).isTrigger = true;
		obj.radius = Radius;
		m_Piece = ((Component)this).GetComponentInParent<BuildingPiece>();
	}

	public void OnPieceDeath(BuildingPiece piece)
	{
		for (int i = 0; i < m_Occupiers.Count; i++)
		{
			if ((Object)(object)piece == (Object)(object)m_Occupiers[i].Occupier)
			{
				m_OccupiedSpaces.RemoveAt(i);
				m_Occupiers.RemoveAt(i);
			}
		}
	}

	public bool GetPieceOffsetByName(string name, out PieceOffset offset)
	{
		offset = new PieceOffset();
		for (int i = 0; i < m_PieceOffsets.Count; i++)
		{
			if ((Object)(object)m_PieceOffsets[i].Piece != (Object)null && m_PieceOffsets[i].Piece.Name == name)
			{
				offset = m_PieceOffsets[i];
				return true;
			}
		}
		return false;
	}

	public bool HasSpace(LayerMask mask, BuildingPiece placedPiece)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		Collider[] array = Physics.OverlapSphere(((Component)this).transform.position, Radius, LayerMask.op_Implicit(mask), (QueryTriggerInteraction)1);
		foreach (Collider val in array)
		{
			if ((Object)(object)m_Piece != (Object)(object)placedPiece)
			{
				if (!m_Piece.Building.HasCollider(val) && (Object)(object)((val is TerrainCollider) ? val : null) == (Object)null)
				{
					return false;
				}
			}
			else if (!m_Piece.HasCollider(val) && (Object)(object)((val is TerrainCollider) ? val : null) == (Object)null)
			{
				return false;
			}
		}
		return true;
	}

	public void OccupySpaces(BuildingSpace[] spacesToOccupy, BuildingPiece piece)
	{
		for (int i = 0; i < spacesToOccupy.Length; i++)
		{
			if (!m_OccupiedSpaces.Contains(spacesToOccupy[i]))
			{
				m_OccupiedSpaces.Add(spacesToOccupy[i]);
				m_Occupiers.Add(new SpaceOccupier(spacesToOccupy[i], piece));
			}
		}
	}

	public void OccupyNeighbours(LayerMask freePlacementMask, LayerMask buildingMask, BuildingPiece placedPiece)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		Bounds bounds = placedPiece.Bounds;
		Vector3 center = ((Bounds)(ref bounds)).center;
		bounds = placedPiece.Bounds;
		Collider[] array = Physics.OverlapBox(center, ((Bounds)(ref bounds)).extents, ((Component)placedPiece).transform.rotation, LayerMask.op_Implicit(freePlacementMask), (QueryTriggerInteraction)2);
		for (int i = 0; i < array.Length; i++)
		{
			Socket component = ((Component)array[i]).GetComponent<Socket>();
			if (Object.op_Implicit((Object)(object)component) && component.SupportsPiece(placedPiece) && !component.HasSpace(freePlacementMask, placedPiece))
			{
				component.OccupySpaces(placedPiece.SpacesToOccupy, placedPiece);
			}
		}
	}

	public bool SupportsPiece(BuildingPiece piece)
	{
		for (int i = 0; i < m_PieceOffsets.Count; i++)
		{
			if (m_PieceOffsets[i] != null && (Object)(object)m_PieceOffsets[i].Piece != (Object)null && m_PieceOffsets[i].Piece.Name == piece.Name && !m_OccupiedSpaces.Contains(piece.NeededSpace))
			{
				return true;
			}
		}
		return false;
	}

	private void OnDrawGizmos()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.color = new Color(0f, 1f, 0f, 0.8f);
		Gizmos.matrix = Matrix4x4.TRS(((Component)this).transform.position, ((Component)this).transform.rotation, Vector3.one * 0.15f);
		Gizmos.DrawCube(Vector3.zero, Vector3.one);
		Gizmos.matrix = matrix;
	}

	private void OnDrawGizmosSelected()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.35f);
		Gizmos.DrawSphere(((Component)this).transform.position, m_Radius);
	}
}
