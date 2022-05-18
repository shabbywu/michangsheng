using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building
{
	// Token: 0x02000963 RID: 2403
	[Serializable]
	public class BuildingHelpers
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x0002C39F File Offset: 0x0002A59F
		public BuildingPiece CurrentPreviewPiece
		{
			get
			{
				return this.m_CurrentPreviewPiece;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06003D56 RID: 15702 RVA: 0x0002C3A7 File Offset: 0x0002A5A7
		// (set) Token: 0x06003D57 RID: 15703 RVA: 0x0002C3AF File Offset: 0x0002A5AF
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

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x0002C3B8 File Offset: 0x0002A5B8
		public bool PlacementAllowed
		{
			get
			{
				return this.m_PlacementAllowed;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x0002C3C0 File Offset: 0x0002A5C0
		// (set) Token: 0x06003D5A RID: 15706 RVA: 0x0002C3C8 File Offset: 0x0002A5C8
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

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x0002C3D1 File Offset: 0x0002A5D1
		// (set) Token: 0x06003D5C RID: 15708 RVA: 0x0002C3D9 File Offset: 0x0002A5D9
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

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06003D5D RID: 15709 RVA: 0x0002C3E2 File Offset: 0x0002A5E2
		public GameObject CurrentPreview
		{
			get
			{
				return this.m_CurrentPreview;
			}
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x0002C3EA File Offset: 0x0002A5EA
		public void Initialize(Transform t, PlayerEventHandler pl, AudioSource aS)
		{
			this.m_Transform = t;
			this.m_Pulse = new AlphaPulse(this.m_PreviewColor, this.m_PulseMin, this.m_PulseMax);
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x001B3D80 File Offset: 0x001B1F80
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

		// Token: 0x06003D60 RID: 15712 RVA: 0x001B3DF4 File Offset: 0x001B1FF4
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

		// Token: 0x06003D61 RID: 15713 RVA: 0x001B3F0C File Offset: 0x001B210C
		private void UpdatePreviewColor()
		{
			Color previewColor = this.m_PlacementAllowed ? new Color(0f, 1f, 0f, this.m_PreviewColor.a) : new Color(1f, 0f, 0f, this.m_PreviewColor.a);
			this.m_PreviewColor = previewColor;
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x001B3F6C File Offset: 0x001B216C
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

		// Token: 0x06003D63 RID: 15715 RVA: 0x001B3FC4 File Offset: 0x001B21C4
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

		// Token: 0x06003D64 RID: 15716 RVA: 0x001B4030 File Offset: 0x001B2230
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

		// Token: 0x06003D65 RID: 15717 RVA: 0x001B41C0 File Offset: 0x001B23C0
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

		// Token: 0x06003D66 RID: 15718 RVA: 0x001B4368 File Offset: 0x001B2568
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

		// Token: 0x06003D67 RID: 15719 RVA: 0x001B4414 File Offset: 0x001B2614
		public void SpawnPreview(GameObject prefab)
		{
			this.m_CurrentPreview = Object.Instantiate<GameObject>(prefab);
			this.m_CurrentPreview.transform.position = Vector3.one * 10000f;
			this.m_CurrentPreviewPiece = this.m_CurrentPreview.GetComponent<BuildingPiece>();
			this.m_CurrentPreviewPiece.SetState(PieceState.Preview);
			this.m_CurrentPrefab = prefab.GetComponent<BuildingPiece>();
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x001B4478 File Offset: 0x001B2678
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

		// Token: 0x06003D69 RID: 15721 RVA: 0x001B4630 File Offset: 0x001B2830
		private bool IntersectsSocket(Ray ray, Socket socket)
		{
			Vector3 vector = socket.transform.position - ray.origin;
			float num = Vector3.Dot(vector, ray.direction);
			return num >= 0f && Vector3.Dot(vector, vector) - num * num <= socket.Radius * socket.Radius;
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x0002C410 File Offset: 0x0002A610
		public void ClearPreview()
		{
			if (this.m_CurrentPreview != null)
			{
				Object.Destroy(this.m_CurrentPreview.gameObject);
				this.m_CurrentPreview = null;
				this.m_CurrentPreviewPiece = null;
			}
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x0002C43E File Offset: 0x0002A63E
		public bool PreviewExists()
		{
			return this.m_CurrentPreview;
		}

		// Token: 0x04003785 RID: 14213
		[SerializeField]
		private LayerMask m_BuildingPieceMask;

		// Token: 0x04003786 RID: 14214
		[SerializeField]
		private LayerMask m_FreePlacementMask;

		// Token: 0x04003787 RID: 14215
		[SerializeField]
		private int m_BuildRange;

		// Token: 0x04003788 RID: 14216
		[Header("Preview Pulsing Effect")]
		[SerializeField]
		private bool m_UsePulseEffect = true;

		// Token: 0x04003789 RID: 14217
		[SerializeField]
		private bool m_PulseWhenSnapped = true;

		// Token: 0x0400378A RID: 14218
		[SerializeField]
		private float m_PulseEffectDuration = 2f;

		// Token: 0x0400378B RID: 14219
		[SerializeField]
		private float m_PulseMin = 0.4f;

		// Token: 0x0400378C RID: 14220
		[SerializeField]
		private float m_PulseMax = 0.9f;

		// Token: 0x0400378D RID: 14221
		private BuildingPiece m_CurrentPreviewPiece;

		// Token: 0x0400378E RID: 14222
		private GameObject m_CurrentPreview;

		// Token: 0x0400378F RID: 14223
		private Socket m_LastValidSocket;

		// Token: 0x04003790 RID: 14224
		private bool m_HasSocket;

		// Token: 0x04003791 RID: 14225
		private bool m_PlacementAllowed = true;

		// Token: 0x04003792 RID: 14226
		private Color m_PreviewColor;

		// Token: 0x04003793 RID: 14227
		private float m_RotationOffset;

		// Token: 0x04003794 RID: 14228
		private Transform m_Transform;

		// Token: 0x04003795 RID: 14229
		private AlphaPulse m_Pulse;

		// Token: 0x04003796 RID: 14230
		private BuildingPiece m_CurrentPrefab;
	}
}
