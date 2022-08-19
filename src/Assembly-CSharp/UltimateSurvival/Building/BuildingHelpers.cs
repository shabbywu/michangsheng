using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x0200065F RID: 1631
	[Serializable]
	public class BuildingHelpers
	{
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x0016AE9C File Offset: 0x0016909C
		public BuildingPiece CurrentPreviewPiece
		{
			get
			{
				return this.m_CurrentPreviewPiece;
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x0016AEA4 File Offset: 0x001690A4
		// (set) Token: 0x060033C6 RID: 13254 RVA: 0x0016AEAC File Offset: 0x001690AC
		public bool HasSocket
		{
			get
			{
				return this.m_HasSocket;
			}
			set
			{
				this.m_HasSocket = value;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x0016AEB5 File Offset: 0x001690B5
		public bool PlacementAllowed
		{
			get
			{
				return this.m_PlacementAllowed;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060033C8 RID: 13256 RVA: 0x0016AEBD File Offset: 0x001690BD
		// (set) Token: 0x060033C9 RID: 13257 RVA: 0x0016AEC5 File Offset: 0x001690C5
		public Color PreviewColor
		{
			get
			{
				return this.m_PreviewColor;
			}
			set
			{
				this.m_PreviewColor = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060033CA RID: 13258 RVA: 0x0016AECE File Offset: 0x001690CE
		// (set) Token: 0x060033CB RID: 13259 RVA: 0x0016AED6 File Offset: 0x001690D6
		public float RotationOffset
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

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060033CC RID: 13260 RVA: 0x0016AEDF File Offset: 0x001690DF
		public GameObject CurrentPreview
		{
			get
			{
				return this.m_CurrentPreview;
			}
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x0016AEE7 File Offset: 0x001690E7
		public void Initialize(Transform t, PlayerEventHandler pl, AudioSource aS)
		{
			this.m_Transform = t;
			this.m_Pulse = new AlphaPulse(this.m_PreviewColor, this.m_PulseMin, this.m_PulseMax);
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x0016AF10 File Offset: 0x00169110
		public void ManagePreview()
		{
			this.ManageCollision();
			if (this.m_UsePulseEffect)
			{
				this.ApplyPulse();
			}
			List<Renderer> renderers = this.m_CurrentPreviewPiece.Renderers;
			for (int i = 0; i < renderers.Count; i++)
			{
				Material[] materials = renderers[i].materials;
				for (int j = 0; j < materials.Length; j++)
				{
					materials[j].color = this.m_PreviewColor;
				}
				renderers[i].materials = materials;
			}
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x0016AF84 File Offset: 0x00169184
		private void ManageCollision()
		{
			bool flag = this.m_CurrentPreviewPiece.IsBlockedByTerrain();
			if (!flag)
			{
				Collider[] array = Physics.OverlapBox(this.m_CurrentPreviewPiece.Bounds.center, this.m_CurrentPreviewPiece.Bounds.extents, this.m_CurrentPreviewPiece.transform.rotation, this.m_FreePlacementMask, 1);
				for (int i = 0; i < array.Length; i++)
				{
					if (!this.m_CurrentPreviewPiece.HasCollider(array[i]))
					{
						Debug.Log(array[i].name);
						if (array[i] as TerrainCollider == null)
						{
							BuildingPiece component = array[i].GetComponent<BuildingPiece>();
							if (!component || !this.m_HasSocket || !(component.Building == this.m_LastValidSocket.Piece.Building))
							{
								flag = true;
								break;
							}
						}
					}
				}
			}
			if (this.m_HasSocket)
			{
				this.m_PlacementAllowed = !flag;
			}
			else
			{
				this.m_PlacementAllowed = (!this.m_CurrentPreviewPiece.RequiresSockets && !flag);
			}
			this.UpdatePreviewColor();
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x0016B09C File Offset: 0x0016929C
		private void UpdatePreviewColor()
		{
			Color previewColor = this.m_PlacementAllowed ? new Color(0f, 1f, 0f, this.m_PreviewColor.a) : new Color(1f, 0f, 0f, this.m_PreviewColor.a);
			this.m_PreviewColor = previewColor;
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x0016B0FC File Offset: 0x001692FC
		private void ApplyPulse()
		{
			if (!this.m_PulseWhenSnapped && this.m_HasSocket)
			{
				this.m_PreviewColor.a = 1f;
				return;
			}
			this.m_Pulse.StartPulse(this.m_PulseEffectDuration);
			this.m_PreviewColor.a = this.m_Pulse.UpdatePulse();
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x0016B154 File Offset: 0x00169354
		public void LookForSnaps()
		{
			this.m_CurrentPreview.gameObject.SetActive(GameController.LocalPlayer.CanShowObjectPreview.Get());
			Collider[] array = Physics.OverlapSphere(this.m_Transform.position, (float)this.m_BuildRange, this.m_BuildingPieceMask, 1);
			if (array.Length != 0)
			{
				this.HandleSnapPreview(array);
				return;
			}
			if (!this.RaycastAndPlace())
			{
				this.HandleFreePreview();
			}
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x0016B1C0 File Offset: 0x001693C0
		private void HandleFreePreview()
		{
			Transform transform = (this.m_CurrentPreviewPiece.OutOfGroundHeight == 0f) ? this.m_Transform : GameController.WorldCamera.transform;
			Vector3 vector = transform.position + transform.forward * (float)this.m_BuildRange;
			if (this.m_CurrentPreviewPiece.OutOfGroundHeight == 0f)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(this.m_CurrentPreview.transform.position + new Vector3(0f, 0.25f, 0f), Vector3.down, ref raycastHit, 1f, this.m_FreePlacementMask, 1))
				{
					vector.y = raycastHit.point.y;
				}
			}
			else
			{
				float num = this.m_CurrentPreviewPiece.AllowUnderTerrainMovement ? (this.m_Transform.position.y - this.m_CurrentPreviewPiece.OutOfGroundHeight) : 0f;
				vector.y = Mathf.Clamp(vector.y, num, this.m_Transform.position.y + this.m_CurrentPreviewPiece.OutOfGroundHeight);
			}
			this.m_CurrentPreview.transform.position = vector;
			this.m_CurrentPreview.transform.rotation = this.m_Transform.rotation * this.m_CurrentPrefab.transform.localRotation * Quaternion.Euler(this.m_CurrentPreviewPiece.RotationAxis * this.m_RotationOffset);
			this.m_LastValidSocket = null;
			this.m_HasSocket = false;
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x0016B350 File Offset: 0x00169550
		private void HandleSnapPreview(Collider[] buildingPieces)
		{
			Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
			float num = float.PositiveInfinity;
			Socket socket = null;
			for (int i = 0; i < buildingPieces.Length; i++)
			{
				BuildingPiece component = buildingPieces[i].GetComponent<BuildingPiece>();
				if (!(component == null) && component.Sockets.Length != 0)
				{
					for (int j = 0; j < component.Sockets.Length; j++)
					{
						Socket socket2 = component.Sockets[j];
						if (socket2.SupportsPiece(this.m_CurrentPreviewPiece) && (socket2.transform.position - this.m_Transform.position).sqrMagnitude < Mathf.Pow((float)this.m_BuildRange, 2f))
						{
							float num2 = Vector3.Angle(ray.direction, socket2.transform.position - ray.origin);
							if (num2 < num && num2 < 35f)
							{
								num = num2;
								socket = socket2;
							}
						}
					}
				}
			}
			Socket.PieceOffset pieceOffset;
			if (socket != null && socket.GetPieceOffsetByName(this.m_CurrentPrefab.Name, out pieceOffset))
			{
				this.m_CurrentPreview.transform.position = socket.transform.position + socket.transform.TransformVector(pieceOffset.PositionOffset);
				this.m_CurrentPreview.transform.rotation = socket.transform.rotation * pieceOffset.RotationOffset;
				this.m_LastValidSocket = socket;
				this.m_HasSocket = true;
				return;
			}
			if (!this.RaycastAndPlace())
			{
				this.HandleFreePreview();
			}
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x0016B4F8 File Offset: 0x001696F8
		private bool RaycastAndPlace()
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(Camera.main.ViewportPointToRay(Vector3.one * 0.5f), ref raycastHit, (float)this.m_BuildRange, this.m_FreePlacementMask, 1))
			{
				this.m_CurrentPreview.transform.position = raycastHit.point;
				this.m_CurrentPreview.transform.rotation = this.m_Transform.rotation * this.m_CurrentPrefab.transform.localRotation * Quaternion.Euler(this.m_CurrentPreviewPiece.RotationAxis * this.m_RotationOffset);
				return true;
			}
			return false;
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x0016B5A4 File Offset: 0x001697A4
		public void SpawnPreview(GameObject prefab)
		{
			this.m_CurrentPreview = Object.Instantiate<GameObject>(prefab);
			this.m_CurrentPreview.transform.position = Vector3.one * 10000f;
			this.m_CurrentPreviewPiece = this.m_CurrentPreview.GetComponent<BuildingPiece>();
			this.m_CurrentPreviewPiece.SetState(PieceState.Preview);
			this.m_CurrentPrefab = prefab.GetComponent<BuildingPiece>();
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x0016B608 File Offset: 0x00169808
		public void PlacePiece()
		{
			if (this.m_CurrentPreview == null)
			{
				return;
			}
			GameObject gameObject = Object.Instantiate<GameObject>(this.m_CurrentPrefab.gameObject, this.m_CurrentPreview.transform.position, this.m_CurrentPreview.transform.rotation);
			gameObject.transform.SetParent(null);
			BuildingPiece component = gameObject.GetComponent<BuildingPiece>();
			if (this.m_LastValidSocket && this.m_LastValidSocket.Piece.Building != null)
			{
				component.transform.SetParent(this.m_LastValidSocket.Piece.Building.transform, true);
				component.AttachedOn = this.m_LastValidSocket.Piece;
				component.SetState(PieceState.Placed);
				this.m_LastValidSocket.OccupyNeighbours(this.m_FreePlacementMask, this.m_BuildingPieceMask, component);
				component.Building = this.m_LastValidSocket.Piece.Building;
				component.Building.AddPiece(component);
			}
			else
			{
				GameObject gameObject2 = new GameObject("Building", new Type[]
				{
					typeof(BuildingHolder)
				});
				component.transform.SetParent(gameObject2.transform, true);
				component.Building = gameObject2.GetComponent<BuildingHolder>();
				component.Building.AddPiece(component);
				Collider[] array;
				if (component.HasSupport(out array))
				{
					BuildingPiece component2 = array[0].GetComponent<BuildingPiece>();
					if (component2 != null)
					{
						component.AttachedOn = component2;
					}
				}
				component.SetState(PieceState.Placed);
			}
			this.m_RotationOffset = 0f;
			if (component.PlacementFX)
			{
				Object.Instantiate<GameObject>(component.PlacementFX, component.transform.position, component.transform.rotation);
			}
			this.m_LastValidSocket = null;
			this.m_HasSocket = false;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x0016B7C0 File Offset: 0x001699C0
		private bool IntersectsSocket(Ray ray, Socket socket)
		{
			Vector3 vector = socket.transform.position - ray.origin;
			float num = Vector3.Dot(vector, ray.direction);
			return num >= 0f && Vector3.Dot(vector, vector) - num * num <= socket.Radius * socket.Radius;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x0016B81A File Offset: 0x00169A1A
		public void ClearPreview()
		{
			if (this.m_CurrentPreview != null)
			{
				Object.Destroy(this.m_CurrentPreview.gameObject);
				this.m_CurrentPreview = null;
				this.m_CurrentPreviewPiece = null;
			}
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x0016B848 File Offset: 0x00169A48
		public bool PreviewExists()
		{
			return this.m_CurrentPreview;
		}

		// Token: 0x04002DFE RID: 11774
		[SerializeField]
		private LayerMask m_BuildingPieceMask;

		// Token: 0x04002DFF RID: 11775
		[SerializeField]
		private LayerMask m_FreePlacementMask;

		// Token: 0x04002E00 RID: 11776
		[SerializeField]
		private int m_BuildRange;

		// Token: 0x04002E01 RID: 11777
		[Header("Preview Pulsing Effect")]
		[SerializeField]
		private bool m_UsePulseEffect = true;

		// Token: 0x04002E02 RID: 11778
		[SerializeField]
		private bool m_PulseWhenSnapped = true;

		// Token: 0x04002E03 RID: 11779
		[SerializeField]
		private float m_PulseEffectDuration = 2f;

		// Token: 0x04002E04 RID: 11780
		[SerializeField]
		private float m_PulseMin = 0.4f;

		// Token: 0x04002E05 RID: 11781
		[SerializeField]
		private float m_PulseMax = 0.9f;

		// Token: 0x04002E06 RID: 11782
		private BuildingPiece m_CurrentPreviewPiece;

		// Token: 0x04002E07 RID: 11783
		private GameObject m_CurrentPreview;

		// Token: 0x04002E08 RID: 11784
		private Socket m_LastValidSocket;

		// Token: 0x04002E09 RID: 11785
		private bool m_HasSocket;

		// Token: 0x04002E0A RID: 11786
		private bool m_PlacementAllowed = true;

		// Token: 0x04002E0B RID: 11787
		private Color m_PreviewColor;

		// Token: 0x04002E0C RID: 11788
		private float m_RotationOffset;

		// Token: 0x04002E0D RID: 11789
		private Transform m_Transform;

		// Token: 0x04002E0E RID: 11790
		private AlphaPulse m_Pulse;

		// Token: 0x04002E0F RID: 11791
		private BuildingPiece m_CurrentPrefab;
	}
}
