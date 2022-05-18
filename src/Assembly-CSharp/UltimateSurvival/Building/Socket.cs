using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x0200096C RID: 2412
	public class Socket : MonoBehaviour
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x0002C6BB File Offset: 0x0002A8BB
		public List<BuildingSpace> OccupiedSpaces
		{
			get
			{
				return this.m_OccupiedSpaces;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x0002C6C3 File Offset: 0x0002A8C3
		public BuildingPiece Piece
		{
			get
			{
				return this.m_Piece;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x0002C6CB File Offset: 0x0002A8CB
		// (set) Token: 0x06003DAB RID: 15787 RVA: 0x0002C6D3 File Offset: 0x0002A8D3
		public List<Socket.PieceOffset> PieceOffsets
		{
			get
			{
				return this.m_PieceOffsets;
			}
			set
			{
				this.m_PieceOffsets = value;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x0002C6DC File Offset: 0x0002A8DC
		// (set) Token: 0x06003DAD RID: 15789 RVA: 0x0002C6E4 File Offset: 0x0002A8E4
		public float Radius
		{
			get
			{
				return this.m_Radius;
			}
			set
			{
				this.m_Radius = value;
			}
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x0002C6ED File Offset: 0x0002A8ED
		private void Awake()
		{
			SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
			sphereCollider.isTrigger = true;
			sphereCollider.radius = this.Radius;
			this.m_Piece = base.GetComponentInParent<BuildingPiece>();
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x001B5600 File Offset: 0x001B3800
		public void OnPieceDeath(BuildingPiece piece)
		{
			for (int i = 0; i < this.m_Occupiers.Count; i++)
			{
				if (piece == this.m_Occupiers[i].Occupier)
				{
					this.m_OccupiedSpaces.RemoveAt(i);
					this.m_Occupiers.RemoveAt(i);
				}
			}
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x001B5654 File Offset: 0x001B3854
		public bool GetPieceOffsetByName(string name, out Socket.PieceOffset offset)
		{
			offset = new Socket.PieceOffset();
			for (int i = 0; i < this.m_PieceOffsets.Count; i++)
			{
				if (this.m_PieceOffsets[i].Piece != null && this.m_PieceOffsets[i].Piece.Name == name)
				{
					offset = this.m_PieceOffsets[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x001B56C8 File Offset: 0x001B38C8
		public bool HasSpace(LayerMask mask, BuildingPiece placedPiece)
		{
			foreach (Collider collider in Physics.OverlapSphere(base.transform.position, this.Radius, mask, 1))
			{
				if (this.m_Piece != placedPiece)
				{
					if (!this.m_Piece.Building.HasCollider(collider) && collider as TerrainCollider == null)
					{
						return false;
					}
				}
				else if (!this.m_Piece.HasCollider(collider) && collider as TerrainCollider == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x001B5758 File Offset: 0x001B3958
		public void OccupySpaces(BuildingSpace[] spacesToOccupy, BuildingPiece piece)
		{
			for (int i = 0; i < spacesToOccupy.Length; i++)
			{
				if (!this.m_OccupiedSpaces.Contains(spacesToOccupy[i]))
				{
					this.m_OccupiedSpaces.Add(spacesToOccupy[i]);
					this.m_Occupiers.Add(new Socket.SpaceOccupier(spacesToOccupy[i], piece));
				}
			}
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x001B57A8 File Offset: 0x001B39A8
		public void OccupyNeighbours(LayerMask freePlacementMask, LayerMask buildingMask, BuildingPiece placedPiece)
		{
			Collider[] array = Physics.OverlapBox(placedPiece.Bounds.center, placedPiece.Bounds.extents, placedPiece.transform.rotation, freePlacementMask, 2);
			for (int i = 0; i < array.Length; i++)
			{
				Socket component = array[i].GetComponent<Socket>();
				if (component && component.SupportsPiece(placedPiece) && !component.HasSpace(freePlacementMask, placedPiece))
				{
					component.OccupySpaces(placedPiece.SpacesToOccupy, placedPiece);
				}
			}
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x001B5828 File Offset: 0x001B3A28
		public bool SupportsPiece(BuildingPiece piece)
		{
			for (int i = 0; i < this.m_PieceOffsets.Count; i++)
			{
				if (this.m_PieceOffsets[i] != null && this.m_PieceOffsets[i].Piece != null && this.m_PieceOffsets[i].Piece.Name == piece.Name && !this.m_OccupiedSpaces.Contains(piece.NeededSpace))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x001B58AC File Offset: 0x001B3AAC
		private void OnDrawGizmos()
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.color = new Color(0f, 1f, 0f, 0.8f);
			Gizmos.matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one * 0.15f);
			Gizmos.DrawCube(Vector3.zero, Vector3.one);
			Gizmos.matrix = matrix;
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x0002C718 File Offset: 0x0002A918
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.35f);
			Gizmos.DrawSphere(base.transform.position, this.m_Radius);
		}

		// Token: 0x040037DE RID: 14302
		[SerializeField]
		private List<Socket.PieceOffset> m_PieceOffsets;

		// Token: 0x040037DF RID: 14303
		[SerializeField]
		private float m_Radius = 1f;

		// Token: 0x040037E0 RID: 14304
		private BuildingPiece m_Piece;

		// Token: 0x040037E1 RID: 14305
		private List<BuildingSpace> m_OccupiedSpaces = new List<BuildingSpace>();

		// Token: 0x040037E2 RID: 14306
		private List<Socket.SpaceOccupier> m_Occupiers = new List<Socket.SpaceOccupier>();

		// Token: 0x0200096D RID: 2413
		[Serializable]
		public class PieceOffset
		{
			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x06003DB8 RID: 15800 RVA: 0x0002C777 File Offset: 0x0002A977
			// (set) Token: 0x06003DB9 RID: 15801 RVA: 0x0002C77F File Offset: 0x0002A97F
			public BuildingPiece Piece
			{
				get
				{
					return this.m_Piece;
				}
				set
				{
					this.m_Piece = value;
				}
			}

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x06003DBA RID: 15802 RVA: 0x0002C788 File Offset: 0x0002A988
			// (set) Token: 0x06003DBB RID: 15803 RVA: 0x0002C790 File Offset: 0x0002A990
			public Vector3 PositionOffset
			{
				get
				{
					return this.m_PositionOffset;
				}
				set
				{
					this.m_PositionOffset = value;
				}
			}

			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x06003DBC RID: 15804 RVA: 0x0002C799 File Offset: 0x0002A999
			public Quaternion RotationOffset
			{
				get
				{
					return Quaternion.Euler(this.m_RotationOffset);
				}
			}

			// Token: 0x170006CA RID: 1738
			// (get) Token: 0x06003DBD RID: 15805 RVA: 0x0002C7A6 File Offset: 0x0002A9A6
			// (set) Token: 0x06003DBE RID: 15806 RVA: 0x0002C7AE File Offset: 0x0002A9AE
			public Vector3 RotationOffsetEuler
			{
				get
				{
					return this.m_RotationOffset;
				}
				set
				{
					this.m_RotationOffset = value;
				}
			}

			// Token: 0x06003DBF RID: 15807 RVA: 0x0002C7B7 File Offset: 0x0002A9B7
			public Socket.PieceOffset GetMemberwiseClone()
			{
				return (Socket.PieceOffset)base.MemberwiseClone();
			}

			// Token: 0x040037E3 RID: 14307
			[SerializeField]
			private BuildingPiece m_Piece;

			// Token: 0x040037E4 RID: 14308
			[SerializeField]
			private Vector3 m_PositionOffset = Vector3.one;

			// Token: 0x040037E5 RID: 14309
			[SerializeField]
			private Vector3 m_RotationOffset;
		}

		// Token: 0x0200096E RID: 2414
		public class SpaceOccupier
		{
			// Token: 0x170006CB RID: 1739
			// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x0002C7D7 File Offset: 0x0002A9D7
			// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x0002C7DF File Offset: 0x0002A9DF
			public BuildingSpace OccupiedSpace { get; private set; }

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x0002C7E8 File Offset: 0x0002A9E8
			// (set) Token: 0x06003DC4 RID: 15812 RVA: 0x0002C7F0 File Offset: 0x0002A9F0
			public BuildingPiece Occupier { get; private set; }

			// Token: 0x06003DC5 RID: 15813 RVA: 0x0002C7F9 File Offset: 0x0002A9F9
			public SpaceOccupier(BuildingSpace occupiedSpace, BuildingPiece occupier)
			{
				this.OccupiedSpace = occupiedSpace;
				this.Occupier = occupier;
			}
		}
	}
}
