using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000663 RID: 1635
	public class BuildingPiece : MonoBehaviour
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x0016B9B6 File Offset: 0x00169BB6
		public PieceState State
		{
			get
			{
				return this.m_State;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x0016B9BE File Offset: 0x00169BBE
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x0016B9C6 File Offset: 0x00169BC6
		public BuildingHolder Building { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060033E3 RID: 13283 RVA: 0x0016B9CF File Offset: 0x00169BCF
		// (set) Token: 0x060033E4 RID: 13284 RVA: 0x0016B9D7 File Offset: 0x00169BD7
		public BuildingPiece AttachedOn { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060033E5 RID: 13285 RVA: 0x0016B9E0 File Offset: 0x00169BE0
		public string Name
		{
			get
			{
				return this.m_PieceName;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060033E6 RID: 13286 RVA: 0x0016B9E8 File Offset: 0x00169BE8
		public RequiredItem[] RequiredItems
		{
			get
			{
				return this.m_RequiredItems;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060033E7 RID: 13287 RVA: 0x0016B9F0 File Offset: 0x00169BF0
		public Vector3 RotationAxis
		{
			get
			{
				return this.m_RotationAxis;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060033E8 RID: 13288 RVA: 0x0016B9F8 File Offset: 0x00169BF8
		public BuildingSpace NeededSpace
		{
			get
			{
				return this.m_NeededSpace;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x0016BA00 File Offset: 0x00169C00
		public BuildingSpace[] SpacesToOccupy
		{
			get
			{
				return this.m_SpacesToOccupy;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060033EA RID: 13290 RVA: 0x0016BA08 File Offset: 0x00169C08
		public float OutOfGroundHeight
		{
			get
			{
				return this.m_OutOfGroundHeight;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x0016BA10 File Offset: 0x00169C10
		public bool AllowUnderTerrainMovement
		{
			get
			{
				return this.m_AllowUnderTerrainMovement;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060033EC RID: 13292 RVA: 0x0016BA18 File Offset: 0x00169C18
		public bool RequiresSockets
		{
			get
			{
				return this.m_RequiresSockets;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x0016BA20 File Offset: 0x00169C20
		public SoundPlayer BuildAudio
		{
			get
			{
				return this.m_BuildAudio;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060033EE RID: 13294 RVA: 0x0016BA28 File Offset: 0x00169C28
		public GameObject PlacementFX
		{
			get
			{
				return this.m_PlacementFX;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x0016BA30 File Offset: 0x00169C30
		public Bounds Bounds
		{
			get
			{
				return new Bounds(base.transform.position + base.transform.TransformVector(this.m_Bounds.center), this.m_Bounds.size);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060033F0 RID: 13296 RVA: 0x0016BA68 File Offset: 0x00169C68
		public List<Renderer> Renderers
		{
			get
			{
				return this.m_Renderers;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060033F1 RID: 13297 RVA: 0x0016BA70 File Offset: 0x00169C70
		public MeshFilter MainMesh
		{
			get
			{
				return this.m_MainMesh;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x0016BA78 File Offset: 0x00169C78
		public Socket[] Sockets
		{
			get
			{
				return this.m_Sockets;
			}
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x0016BA80 File Offset: 0x00169C80
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

		// Token: 0x060033F4 RID: 13300 RVA: 0x0016BBA8 File Offset: 0x00169DA8
		private void Update()
		{
			Collider[] array;
			if (this.m_CheckStability && this.State == PieceState.Placed && !this.HasSupport(out array))
			{
				this.On_SocketDeath();
			}
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x0016BBD8 File Offset: 0x00169DD8
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

		// Token: 0x060033F6 RID: 13302 RVA: 0x0016BD48 File Offset: 0x00169F48
		private void On_SocketDeath()
		{
			if (base.gameObject != null)
			{
				base.GetComponent<EntityEventHandler>().ChangeHealth.Try(new HealthEventData(float.NegativeInfinity, null, default(Vector3), default(Vector3), 0f));
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x0016BD96 File Offset: 0x00169F96
		private void OnDestroy()
		{
			if (this.AttachedOn != null)
			{
				this.AttachedOn.GetComponent<EntityEventHandler>().Death.RemoveListener(new Action(this.On_SocketDeath));
			}
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x0016BDC8 File Offset: 0x00169FC8
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

		// Token: 0x060033F9 RID: 13305 RVA: 0x0016BE24 File Offset: 0x0016A024
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

		// Token: 0x060033FA RID: 13306 RVA: 0x0016BE90 File Offset: 0x0016A090
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

		// Token: 0x060033FB RID: 13307 RVA: 0x0016BF0C File Offset: 0x0016A10C
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

		// Token: 0x060033FC RID: 13308 RVA: 0x0016BF48 File Offset: 0x0016A148
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

		// Token: 0x060033FD RID: 13309 RVA: 0x0016C010 File Offset: 0x0016A210
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

		// Token: 0x04002E21 RID: 11809
		[SerializeField]
		private string m_PieceName;

		// Token: 0x04002E22 RID: 11810
		[SerializeField]
		private RequiredItem[] m_RequiredItems;

		// Token: 0x04002E23 RID: 11811
		[SerializeField]
		private Vector3 m_RotationAxis = Vector3.forward;

		// Token: 0x04002E24 RID: 11812
		[Header("Setup")]
		[SerializeField]
		private bool m_ShowBounds;

		// Token: 0x04002E25 RID: 11813
		[SerializeField]
		private Bounds m_Bounds;

		// Token: 0x04002E26 RID: 11814
		[SerializeField]
		[Tooltip("If left empty, it will automatically get populated with the first MeshFilter found.")]
		private MeshFilter m_MainMesh;

		// Token: 0x04002E27 RID: 11815
		[SerializeField]
		private List<Renderer> m_IgnoredRenderers;

		// Token: 0x04002E28 RID: 11816
		[SerializeField]
		private List<Collider> m_IgnoredColliders;

		// Token: 0x04002E29 RID: 11817
		[Header("Placing")]
		[SerializeField]
		private BuildingSpace m_NeededSpace;

		// Token: 0x04002E2A RID: 11818
		[SerializeField]
		private BuildingSpace[] m_SpacesToOccupy;

		// Token: 0x04002E2B RID: 11819
		[SerializeField]
		private bool m_RequiresSockets;

		// Token: 0x04002E2C RID: 11820
		[SerializeField]
		private float m_OutOfGroundHeight;

		// Token: 0x04002E2D RID: 11821
		[SerializeField]
		private bool m_AllowUnderTerrainMovement;

		// Token: 0x04002E2E RID: 11822
		[Header("Stability")]
		[SerializeField]
		private bool m_CheckStability = true;

		// Token: 0x04002E2F RID: 11823
		[Space]
		[SerializeField]
		private LayerMask m_StabilityCheckMask;

		// Token: 0x04002E30 RID: 11824
		[SerializeField]
		private bool m_ShowStabilityBox;

		// Token: 0x04002E31 RID: 11825
		[SerializeField]
		private Bounds[] m_StabilityBoxes;

		// Token: 0x04002E32 RID: 11826
		[Header("Terrain Protection")]
		[SerializeField]
		private bool m_EnableTP;

		// Token: 0x04002E33 RID: 11827
		[SerializeField]
		private bool m_ShowTP;

		// Token: 0x04002E34 RID: 11828
		[SerializeField]
		private Bounds m_TPBox;

		// Token: 0x04002E35 RID: 11829
		[Header("Sound And Effects")]
		[SerializeField]
		private SoundPlayer m_BuildAudio;

		// Token: 0x04002E36 RID: 11830
		[SerializeField]
		private GameObject m_PlacementFX;

		// Token: 0x04002E37 RID: 11831
		[Header("Preview")]
		[SerializeField]
		private Material m_PreviewMat;

		// Token: 0x04002E38 RID: 11832
		private Dictionary<Renderer, Material[]> m_InitialMaterials = new Dictionary<Renderer, Material[]>();

		// Token: 0x04002E39 RID: 11833
		private Socket[] m_Sockets = new Socket[0];

		// Token: 0x04002E3A RID: 11834
		private PieceState m_State;

		// Token: 0x04002E3B RID: 11835
		private List<Collider> m_Colliders = new List<Collider>();

		// Token: 0x04002E3C RID: 11836
		private List<Renderer> m_Renderers = new List<Renderer>();

		// Token: 0x04002E3D RID: 11837
		private bool m_Initialized;
	}
}
