using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000968 RID: 2408
	public class BuildingPiece : MonoBehaviour
	{
		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x0002C4B4 File Offset: 0x0002A6B4
		public PieceState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06003D74 RID: 15732 RVA: 0x0002C4BC File Offset: 0x0002A6BC
		// (set) Token: 0x06003D75 RID: 15733 RVA: 0x0002C4C4 File Offset: 0x0002A6C4
		public BuildingHolder Building { get; set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06003D76 RID: 15734 RVA: 0x0002C4CD File Offset: 0x0002A6CD
		// (set) Token: 0x06003D77 RID: 15735 RVA: 0x0002C4D5 File Offset: 0x0002A6D5
		public BuildingPiece AttachedOn { get; set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06003D78 RID: 15736 RVA: 0x0002C4DE File Offset: 0x0002A6DE
		public string Name
		{
			get
			{
				return this.m_PieceName;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x0002C4E6 File Offset: 0x0002A6E6
		public RequiredItem[] RequiredItems
		{
			get
			{
				return this.m_RequiredItems;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06003D7A RID: 15738 RVA: 0x0002C4EE File Offset: 0x0002A6EE
		public Vector3 RotationAxis
		{
			get
			{
				return this.m_RotationAxis;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06003D7B RID: 15739 RVA: 0x0002C4F6 File Offset: 0x0002A6F6
		public BuildingSpace NeededSpace
		{
			get
			{
				return this.m_NeededSpace;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x0002C4FE File Offset: 0x0002A6FE
		public BuildingSpace[] SpacesToOccupy
		{
			get
			{
				return this.m_SpacesToOccupy;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x0002C506 File Offset: 0x0002A706
		public float OutOfGroundHeight
		{
			get
			{
				return this.m_OutOfGroundHeight;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06003D7E RID: 15742 RVA: 0x0002C50E File Offset: 0x0002A70E
		public bool AllowUnderTerrainMovement
		{
			get
			{
				return this.m_AllowUnderTerrainMovement;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x0002C516 File Offset: 0x0002A716
		public bool RequiresSockets
		{
			get
			{
				return this.m_RequiresSockets;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x0002C51E File Offset: 0x0002A71E
		public SoundPlayer BuildAudio
		{
			get
			{
				return this.m_BuildAudio;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x0002C526 File Offset: 0x0002A726
		public GameObject PlacementFX
		{
			get
			{
				return this.m_PlacementFX;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x0002C52E File Offset: 0x0002A72E
		public Bounds Bounds
		{
			get
			{
				return new Bounds(base.transform.position + base.transform.TransformVector(this.m_Bounds.center), this.m_Bounds.size);
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x0002C566 File Offset: 0x0002A766
		public List<Renderer> Renderers
		{
			get
			{
				return this.m_Renderers;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x0002C56E File Offset: 0x0002A76E
		public MeshFilter MainMesh
		{
			get
			{
				return this.m_MainMesh;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06003D85 RID: 15749 RVA: 0x0002C576 File Offset: 0x0002A776
		public Socket[] Sockets
		{
			get
			{
				return this.m_Sockets;
			}
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x001B4798 File Offset: 0x001B2998
		private void Awake()
		{
			if (this.m_MainMesh == null)
			{
				this.m_MainMesh = base.GetComponentInChildren<MeshFilter>();
			}
			base.GetComponentsInChildren<Renderer>(this.m_Renderers);
			this.m_Renderers.RemoveAll((Renderer r) => this.m_IgnoredRenderers.Contains(r));
			for (int i = 0; i < this.m_Renderers.Count; i++)
			{
				this.m_InitialMaterials.Add(this.m_Renderers[i], this.m_Renderers[i].sharedMaterials);
			}
			base.GetComponentsInChildren<Collider>(this.m_Colliders);
			this.m_Colliders.RemoveAll((Collider col) => this.m_IgnoredColliders.Contains(col));
			for (int j = 0; j < this.m_Colliders.Count; j++)
			{
				for (int k = 0; k < this.m_Colliders.Count; k++)
				{
					if (this.m_Colliders[j] != this.m_Colliders[k])
					{
						Physics.IgnoreCollision(this.m_Colliders[j], this.m_Colliders[k]);
					}
				}
			}
			this.m_Sockets = base.GetComponentsInChildren<Socket>();
			this.m_Initialized = true;
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x001B48C0 File Offset: 0x001B2AC0
		private void Update()
		{
			Collider[] array;
			if (this.m_CheckStability && this.State == PieceState.Placed && !this.HasSupport(out array))
			{
				this.On_SocketDeath();
			}
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x001B48F0 File Offset: 0x001B2AF0
		public void SetState(PieceState state)
		{
			if (!this.m_Initialized)
			{
				this.Awake();
			}
			if (state == PieceState.Preview)
			{
				this.SetMaterials(this.m_PreviewMat);
				foreach (Collider collider in this.m_Colliders)
				{
					if (collider)
					{
						collider.enabled = false;
					}
					else
					{
						Debug.LogError("A collider was found null in the collider list!", this);
					}
				}
				Socket[] sockets = this.m_Sockets;
				for (int i = 0; i < sockets.Length; i++)
				{
					sockets[i].gameObject.SetActive(false);
				}
			}
			else if (state == PieceState.Placed)
			{
				this.SetMaterials(this.m_InitialMaterials);
				foreach (Collider collider2 in this.m_Colliders)
				{
					collider2.enabled = true;
				}
				Socket[] sockets = this.m_Sockets;
				for (int i = 0; i < sockets.Length; i++)
				{
					sockets[i].gameObject.SetActive(true);
				}
				if (this.m_CheckStability && this.AttachedOn != null)
				{
					EntityEventHandler component = this.AttachedOn.GetComponent<EntityEventHandler>();
					if (component != null)
					{
						component.Death.AddListener(new Action(this.On_SocketDeath));
					}
				}
			}
			this.m_State = state;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x001B4A60 File Offset: 0x001B2C60
		private void On_SocketDeath()
		{
			if (base.gameObject != null)
			{
				base.GetComponent<EntityEventHandler>().ChangeHealth.Try(new HealthEventData(float.NegativeInfinity, null, default(Vector3), default(Vector3), 0f));
			}
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x0002C57E File Offset: 0x0002A77E
		private void OnDestroy()
		{
			if (this.AttachedOn != null)
			{
				this.AttachedOn.GetComponent<EntityEventHandler>().Death.RemoveListener(new Action(this.On_SocketDeath));
			}
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x001B4AB0 File Offset: 0x001B2CB0
		private void SetMaterials(Material material)
		{
			for (int i = 0; i < this.m_Renderers.Count; i++)
			{
				Material[] materials = this.m_Renderers[i].materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j] = material;
				}
				this.m_Renderers[i].materials = materials;
			}
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x001B4B0C File Offset: 0x001B2D0C
		private void SetMaterials(Dictionary<Renderer, Material[]> materials)
		{
			for (int i = 0; i < this.m_Renderers.Count; i++)
			{
				Material[] materials2 = this.m_Renderers[i].materials;
				for (int j = 0; j < materials2.Length; j++)
				{
					materials2[j] = materials[this.m_Renderers[i]][j];
				}
				this.m_Renderers[i].materials = materials2;
			}
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x001B4B78 File Offset: 0x001B2D78
		public bool IsBlockedByTerrain()
		{
			if (!this.m_EnableTP)
			{
				return false;
			}
			Collider[] array = Physics.OverlapBox(base.transform.position + base.transform.TransformVector(this.m_TPBox.center), this.m_TPBox.extents, base.transform.rotation, -1, 1);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] as TerrainCollider != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x001B4BF4 File Offset: 0x001B2DF4
		public bool HasCollider(Collider col)
		{
			for (int i = 0; i < this.m_Colliders.Count; i++)
			{
				if (this.m_Colliders[i] == col)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x001B4C30 File Offset: 0x001B2E30
		public bool HasSupport(out Collider[] colliders)
		{
			bool result = true;
			List<Collider> list = new List<Collider>();
			for (int i = 0; i < this.m_StabilityBoxes.Length; i++)
			{
				Bounds bounds = this.m_StabilityBoxes[i];
				Collider[] array = Physics.OverlapBox(base.transform.position + base.transform.TransformVector(bounds.center), bounds.extents, base.transform.rotation, this.m_StabilityCheckMask);
				for (int j = 0; j < array.Length; j++)
				{
					if (!array[j].isTrigger && !this.HasCollider(array[j]))
					{
						list.Add(array[j]);
					}
					else
					{
						result = false;
					}
				}
			}
			colliders = list.ToArray();
			return result;
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x001B4CF8 File Offset: 0x001B2EF8
		private void OnDrawGizmosSelected()
		{
			Matrix4x4 matrix = Gizmos.matrix;
			Gizmos.color = Color.blue;
			if (this.m_ShowBounds)
			{
				Gizmos.matrix = Matrix4x4.TRS(base.transform.position + base.transform.TransformVector(this.m_Bounds.center), base.transform.rotation, this.m_Bounds.size);
				Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
			}
			Gizmos.color = Color.yellow;
			if (this.m_ShowTP)
			{
				Gizmos.matrix = Matrix4x4.TRS(base.transform.position + base.transform.TransformVector(this.m_TPBox.center), base.transform.rotation, this.m_TPBox.size);
				Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
			}
			Gizmos.color = Color.red;
			if (this.m_ShowStabilityBox)
			{
				for (int i = 0; i < this.m_StabilityBoxes.Length; i++)
				{
					Bounds bounds = this.m_StabilityBoxes[i];
					Gizmos.matrix = Matrix4x4.TRS(base.transform.position + base.transform.TransformVector(bounds.center), base.transform.rotation, bounds.size);
					Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
				}
			}
			Gizmos.matrix = matrix;
		}

		// Token: 0x040037AA RID: 14250
		[SerializeField]
		private string m_PieceName;

		// Token: 0x040037AB RID: 14251
		[SerializeField]
		private RequiredItem[] m_RequiredItems;

		// Token: 0x040037AC RID: 14252
		[SerializeField]
		private Vector3 m_RotationAxis = Vector3.forward;

		// Token: 0x040037AD RID: 14253
		[Header("Setup")]
		[SerializeField]
		private bool m_ShowBounds;

		// Token: 0x040037AE RID: 14254
		[SerializeField]
		private Bounds m_Bounds;

		// Token: 0x040037AF RID: 14255
		[SerializeField]
		[Tooltip("If left empty, it will automatically get populated with the first MeshFilter found.")]
		private MeshFilter m_MainMesh;

		// Token: 0x040037B0 RID: 14256
		[SerializeField]
		private List<Renderer> m_IgnoredRenderers;

		// Token: 0x040037B1 RID: 14257
		[SerializeField]
		private List<Collider> m_IgnoredColliders;

		// Token: 0x040037B2 RID: 14258
		[Header("Placing")]
		[SerializeField]
		private BuildingSpace m_NeededSpace;

		// Token: 0x040037B3 RID: 14259
		[SerializeField]
		private BuildingSpace[] m_SpacesToOccupy;

		// Token: 0x040037B4 RID: 14260
		[SerializeField]
		private bool m_RequiresSockets;

		// Token: 0x040037B5 RID: 14261
		[SerializeField]
		private float m_OutOfGroundHeight;

		// Token: 0x040037B6 RID: 14262
		[SerializeField]
		private bool m_AllowUnderTerrainMovement;

		// Token: 0x040037B7 RID: 14263
		[Header("Stability")]
		[SerializeField]
		private bool m_CheckStability = true;

		// Token: 0x040037B8 RID: 14264
		[Space]
		[SerializeField]
		private LayerMask m_StabilityCheckMask;

		// Token: 0x040037B9 RID: 14265
		[SerializeField]
		private bool m_ShowStabilityBox;

		// Token: 0x040037BA RID: 14266
		[SerializeField]
		private Bounds[] m_StabilityBoxes;

		// Token: 0x040037BB RID: 14267
		[Header("Terrain Protection")]
		[SerializeField]
		private bool m_EnableTP;

		// Token: 0x040037BC RID: 14268
		[SerializeField]
		private bool m_ShowTP;

		// Token: 0x040037BD RID: 14269
		[SerializeField]
		private Bounds m_TPBox;

		// Token: 0x040037BE RID: 14270
		[Header("Sound And Effects")]
		[SerializeField]
		private SoundPlayer m_BuildAudio;

		// Token: 0x040037BF RID: 14271
		[SerializeField]
		private GameObject m_PlacementFX;

		// Token: 0x040037C0 RID: 14272
		[Header("Preview")]
		[SerializeField]
		private Material m_PreviewMat;

		// Token: 0x040037C1 RID: 14273
		private Dictionary<Renderer, Material[]> m_InitialMaterials = new Dictionary<Renderer, Material[]>();

		// Token: 0x040037C2 RID: 14274
		private Socket[] m_Sockets = new Socket[0];

		// Token: 0x040037C3 RID: 14275
		private PieceState m_State;

		// Token: 0x040037C4 RID: 14276
		private List<Collider> m_Colliders = new List<Collider>();

		// Token: 0x040037C5 RID: 14277
		private List<Renderer> m_Renderers = new List<Renderer>();

		// Token: 0x040037C6 RID: 14278
		private bool m_Initialized;
	}
}
