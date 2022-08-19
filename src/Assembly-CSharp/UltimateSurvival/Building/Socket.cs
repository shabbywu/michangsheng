using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000666 RID: 1638
	public class Socket : MonoBehaviour
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x0016C923 File Offset: 0x0016AB23
		public List<BuildingSpace> OccupiedSpaces
		{
			get
			{
				return this.m_OccupiedSpaces;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x0016C92B File Offset: 0x0016AB2B
		public BuildingPiece Piece
		{
			get
			{
				return this.m_Piece;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x0016C933 File Offset: 0x0016AB33
		// (set) Token: 0x06003412 RID: 13330 RVA: 0x0016C93B File Offset: 0x0016AB3B
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

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x0016C944 File Offset: 0x0016AB44
		// (set) Token: 0x06003414 RID: 13332 RVA: 0x0016C94C File Offset: 0x0016AB4C
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

		// Token: 0x06003415 RID: 13333 RVA: 0x0016C955 File Offset: 0x0016AB55
		private void Awake()
		{
			SphereCollider sphereCollider = base.gameObject.AddComponent<SphereCollider>();
			sphereCollider.isTrigger = true;
			sphereCollider.radius = this.Radius;
			this.m_Piece = base.GetComponentInParent<BuildingPiece>();
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x0016C980 File Offset: 0x0016AB80
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

		// Token: 0x06003417 RID: 13335 RVA: 0x0016C9D4 File Offset: 0x0016ABD4
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

		// Token: 0x06003418 RID: 13336 RVA: 0x0016CA48 File Offset: 0x0016AC48
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

		// Token: 0x06003419 RID: 13337 RVA: 0x0016CAD8 File Offset: 0x0016ACD8
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

		// Token: 0x0600341A RID: 13338 RVA: 0x0016CB28 File Offset: 0x0016AD28
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

		// Token: 0x0600341B RID: 13339 RVA: 0x0016CBA8 File Offset: 0x0016ADA8
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

		// Token: 0x0600341C RID: 13340 RVA: 0x0016CC2C File Offset: 0x0016AE2C
		private void OnDrawGizmos()
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.color = new Color(0f, 1f, 0f, 0.8f);
			Gizmos.matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one * 0.15f);
			Gizmos.DrawCube(Vector3.zero, Vector3.one);
			Gizmos.matrix = matrix;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x0016CC9F File Offset: 0x0016AE9F
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.35f);
			Gizmos.DrawSphere(base.transform.position, this.m_Radius);
		}

		// Token: 0x04002E50 RID: 11856
		[SerializeField]
		private List<Socket.PieceOffset> m_PieceOffsets;

		// Token: 0x04002E51 RID: 11857
		[SerializeField]
		private float m_Radius = 1f;

		// Token: 0x04002E52 RID: 11858
		private BuildingPiece m_Piece;

		// Token: 0x04002E53 RID: 11859
		private List<BuildingSpace> m_OccupiedSpaces = new List<BuildingSpace>();

		// Token: 0x04002E54 RID: 11860
		private List<Socket.SpaceOccupier> m_Occupiers = new List<Socket.SpaceOccupier>();

		// Token: 0x020014F0 RID: 5360
		[Serializable]
		public class PieceOffset
		{
			// Token: 0x17000B21 RID: 2849
			// (get) Token: 0x06008275 RID: 33397 RVA: 0x002DB322 File Offset: 0x002D9522
			// (set) Token: 0x06008276 RID: 33398 RVA: 0x002DB32A File Offset: 0x002D952A
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

			// Token: 0x17000B22 RID: 2850
			// (get) Token: 0x06008277 RID: 33399 RVA: 0x002DB333 File Offset: 0x002D9533
			// (set) Token: 0x06008278 RID: 33400 RVA: 0x002DB33B File Offset: 0x002D953B
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

			// Token: 0x17000B23 RID: 2851
			// (get) Token: 0x06008279 RID: 33401 RVA: 0x002DB344 File Offset: 0x002D9544
			public Quaternion RotationOffset
			{
				get
				{
					return Quaternion.Euler(this.m_RotationOffset);
				}
			}

			// Token: 0x17000B24 RID: 2852
			// (get) Token: 0x0600827A RID: 33402 RVA: 0x002DB351 File Offset: 0x002D9551
			// (set) Token: 0x0600827B RID: 33403 RVA: 0x002DB359 File Offset: 0x002D9559
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

			// Token: 0x0600827C RID: 33404 RVA: 0x002DB362 File Offset: 0x002D9562
			public Socket.PieceOffset GetMemberwiseClone()
			{
				return (Socket.PieceOffset)base.MemberwiseClone();
			}

			// Token: 0x04006DD0 RID: 28112
			[SerializeField]
			private BuildingPiece m_Piece;

			// Token: 0x04006DD1 RID: 28113
			[SerializeField]
			private Vector3 m_PositionOffset = Vector3.one;

			// Token: 0x04006DD2 RID: 28114
			[SerializeField]
			private Vector3 m_RotationOffset;
		}

		// Token: 0x020014F1 RID: 5361
		public class SpaceOccupier
		{
			// Token: 0x17000B25 RID: 2853
			// (get) Token: 0x0600827E RID: 33406 RVA: 0x002DB382 File Offset: 0x002D9582
			// (set) Token: 0x0600827F RID: 33407 RVA: 0x002DB38A File Offset: 0x002D958A
			public BuildingSpace OccupiedSpace { get; private set; }

			// Token: 0x17000B26 RID: 2854
			// (get) Token: 0x06008280 RID: 33408 RVA: 0x002DB393 File Offset: 0x002D9593
			// (set) Token: 0x06008281 RID: 33409 RVA: 0x002DB39B File Offset: 0x002D959B
			public BuildingPiece Occupier { get; private set; }

			// Token: 0x06008282 RID: 33410 RVA: 0x002DB3A4 File Offset: 0x002D95A4
			public SpaceOccupier(BuildingSpace occupiedSpace, BuildingPiece occupier)
			{
				this.OccupiedSpace = occupiedSpace;
				this.Occupier = occupier;
			}
		}
	}
}
