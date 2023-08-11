using System;
using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building;

[Serializable]
public class BuildingHelpers
{
	[SerializeField]
	private LayerMask m_BuildingPieceMask;

	[SerializeField]
	private LayerMask m_FreePlacementMask;

	[SerializeField]
	private int m_BuildRange;

	[Header("Preview Pulsing Effect")]
	[SerializeField]
	private bool m_UsePulseEffect = true;

	[SerializeField]
	private bool m_PulseWhenSnapped = true;

	[SerializeField]
	private float m_PulseEffectDuration = 2f;

	[SerializeField]
	private float m_PulseMin = 0.4f;

	[SerializeField]
	private float m_PulseMax = 0.9f;

	private BuildingPiece m_CurrentPreviewPiece;

	private GameObject m_CurrentPreview;

	private Socket m_LastValidSocket;

	private bool m_HasSocket;

	private bool m_PlacementAllowed = true;

	private Color m_PreviewColor;

	private float m_RotationOffset;

	private Transform m_Transform;

	private AlphaPulse m_Pulse;

	private BuildingPiece m_CurrentPrefab;

	public BuildingPiece CurrentPreviewPiece => m_CurrentPreviewPiece;

	public bool HasSocket
	{
		get
		{
			return m_HasSocket;
		}
		set
		{
			m_HasSocket = value;
		}
	}

	public bool PlacementAllowed => m_PlacementAllowed;

	public Color PreviewColor
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_PreviewColor;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			m_PreviewColor = value;
		}
	}

	public float RotationOffset
	{
		get
		{
			return m_RotationOffset;
		}
		set
		{
			m_RotationOffset = value;
		}
	}

	public GameObject CurrentPreview => m_CurrentPreview;

	public void Initialize(Transform t, PlayerEventHandler pl, AudioSource aS)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		m_Transform = t;
		m_Pulse = new AlphaPulse(m_PreviewColor, m_PulseMin, m_PulseMax);
	}

	public void ManagePreview()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		ManageCollision();
		if (m_UsePulseEffect)
		{
			ApplyPulse();
		}
		List<Renderer> renderers = m_CurrentPreviewPiece.Renderers;
		for (int i = 0; i < renderers.Count; i++)
		{
			Material[] materials = renderers[i].materials;
			for (int j = 0; j < materials.Length; j++)
			{
				materials[j].color = m_PreviewColor;
			}
			renderers[i].materials = materials;
		}
	}

	private void ManageCollision()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		bool flag = m_CurrentPreviewPiece.IsBlockedByTerrain();
		if (!flag)
		{
			Bounds bounds = m_CurrentPreviewPiece.Bounds;
			Vector3 center = ((Bounds)(ref bounds)).center;
			bounds = m_CurrentPreviewPiece.Bounds;
			Collider[] array = Physics.OverlapBox(center, ((Bounds)(ref bounds)).extents, ((Component)m_CurrentPreviewPiece).transform.rotation, LayerMask.op_Implicit(m_FreePlacementMask), (QueryTriggerInteraction)1);
			for (int i = 0; i < array.Length; i++)
			{
				if (m_CurrentPreviewPiece.HasCollider(array[i]))
				{
					continue;
				}
				Debug.Log((object)((Object)array[i]).name);
				Collider obj = array[i];
				if ((Object)(object)((obj is TerrainCollider) ? obj : null) == (Object)null)
				{
					BuildingPiece component = ((Component)array[i]).GetComponent<BuildingPiece>();
					if (!Object.op_Implicit((Object)(object)component) || !m_HasSocket || !((Object)(object)component.Building == (Object)(object)m_LastValidSocket.Piece.Building))
					{
						flag = true;
						break;
					}
				}
			}
		}
		if (m_HasSocket)
		{
			m_PlacementAllowed = !flag;
		}
		else
		{
			m_PlacementAllowed = !m_CurrentPreviewPiece.RequiresSockets && !flag;
		}
		UpdatePreviewColor();
	}

	private void UpdatePreviewColor()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		Color previewColor = (m_PlacementAllowed ? new Color(0f, 1f, 0f, m_PreviewColor.a) : new Color(1f, 0f, 0f, m_PreviewColor.a));
		m_PreviewColor = previewColor;
	}

	private void ApplyPulse()
	{
		if (!m_PulseWhenSnapped && m_HasSocket)
		{
			m_PreviewColor.a = 1f;
			return;
		}
		m_Pulse.StartPulse(m_PulseEffectDuration);
		m_PreviewColor.a = m_Pulse.UpdatePulse();
	}

	public void LookForSnaps()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		m_CurrentPreview.gameObject.SetActive(GameController.LocalPlayer.CanShowObjectPreview.Get());
		Collider[] array = Physics.OverlapSphere(m_Transform.position, (float)m_BuildRange, LayerMask.op_Implicit(m_BuildingPieceMask), (QueryTriggerInteraction)1);
		if (array.Length != 0)
		{
			HandleSnapPreview(array);
		}
		else if (!RaycastAndPlace())
		{
			HandleFreePreview();
		}
	}

	private void HandleFreePreview()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		Transform val = ((m_CurrentPreviewPiece.OutOfGroundHeight == 0f) ? m_Transform : ((Component)GameController.WorldCamera).transform);
		Vector3 val2 = val.position + val.forward * (float)m_BuildRange;
		if (m_CurrentPreviewPiece.OutOfGroundHeight == 0f)
		{
			RaycastHit val3 = default(RaycastHit);
			if (Physics.Raycast(m_CurrentPreview.transform.position + new Vector3(0f, 0.25f, 0f), Vector3.down, ref val3, 1f, LayerMask.op_Implicit(m_FreePlacementMask), (QueryTriggerInteraction)1))
			{
				val2.y = ((RaycastHit)(ref val3)).point.y;
			}
		}
		else
		{
			float num = (m_CurrentPreviewPiece.AllowUnderTerrainMovement ? (m_Transform.position.y - m_CurrentPreviewPiece.OutOfGroundHeight) : 0f);
			val2.y = Mathf.Clamp(val2.y, num, m_Transform.position.y + m_CurrentPreviewPiece.OutOfGroundHeight);
		}
		m_CurrentPreview.transform.position = val2;
		m_CurrentPreview.transform.rotation = m_Transform.rotation * ((Component)m_CurrentPrefab).transform.localRotation * Quaternion.Euler(m_CurrentPreviewPiece.RotationAxis * m_RotationOffset);
		m_LastValidSocket = null;
		m_HasSocket = false;
	}

	private void HandleSnapPreview(Collider[] buildingPieces)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		Ray val = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
		float num = float.PositiveInfinity;
		Socket socket = null;
		for (int i = 0; i < buildingPieces.Length; i++)
		{
			BuildingPiece component = ((Component)buildingPieces[i]).GetComponent<BuildingPiece>();
			if ((Object)(object)component == (Object)null || component.Sockets.Length == 0)
			{
				continue;
			}
			for (int j = 0; j < component.Sockets.Length; j++)
			{
				Socket socket2 = component.Sockets[j];
				if (!socket2.SupportsPiece(m_CurrentPreviewPiece))
				{
					continue;
				}
				Vector3 val2 = ((Component)socket2).transform.position - m_Transform.position;
				if (((Vector3)(ref val2)).sqrMagnitude < Mathf.Pow((float)m_BuildRange, 2f))
				{
					float num2 = Vector3.Angle(((Ray)(ref val)).direction, ((Component)socket2).transform.position - ((Ray)(ref val)).origin);
					if (num2 < num && num2 < 35f)
					{
						num = num2;
						socket = socket2;
					}
				}
			}
		}
		if ((Object)(object)socket != (Object)null && socket.GetPieceOffsetByName(m_CurrentPrefab.Name, out var offset))
		{
			m_CurrentPreview.transform.position = ((Component)socket).transform.position + ((Component)socket).transform.TransformVector(offset.PositionOffset);
			m_CurrentPreview.transform.rotation = ((Component)socket).transform.rotation * offset.RotationOffset;
			m_LastValidSocket = socket;
			m_HasSocket = true;
		}
		else if (!RaycastAndPlace())
		{
			HandleFreePreview();
		}
	}

	private bool RaycastAndPlace()
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		RaycastHit val = default(RaycastHit);
		if (Physics.Raycast(Camera.main.ViewportPointToRay(Vector3.one * 0.5f), ref val, (float)m_BuildRange, LayerMask.op_Implicit(m_FreePlacementMask), (QueryTriggerInteraction)1))
		{
			m_CurrentPreview.transform.position = ((RaycastHit)(ref val)).point;
			m_CurrentPreview.transform.rotation = m_Transform.rotation * ((Component)m_CurrentPrefab).transform.localRotation * Quaternion.Euler(m_CurrentPreviewPiece.RotationAxis * m_RotationOffset);
			return true;
		}
		return false;
	}

	public void SpawnPreview(GameObject prefab)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		m_CurrentPreview = Object.Instantiate<GameObject>(prefab);
		m_CurrentPreview.transform.position = Vector3.one * 10000f;
		m_CurrentPreviewPiece = m_CurrentPreview.GetComponent<BuildingPiece>();
		m_CurrentPreviewPiece.SetState(PieceState.Preview);
		m_CurrentPrefab = prefab.GetComponent<BuildingPiece>();
	}

	public void PlacePiece()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)m_CurrentPreview == (Object)null)
		{
			return;
		}
		GameObject obj = Object.Instantiate<GameObject>(((Component)m_CurrentPrefab).gameObject, m_CurrentPreview.transform.position, m_CurrentPreview.transform.rotation);
		obj.transform.SetParent((Transform)null);
		BuildingPiece component = obj.GetComponent<BuildingPiece>();
		if (Object.op_Implicit((Object)(object)m_LastValidSocket) && (Object)(object)m_LastValidSocket.Piece.Building != (Object)null)
		{
			((Component)component).transform.SetParent(((Component)m_LastValidSocket.Piece.Building).transform, true);
			component.AttachedOn = m_LastValidSocket.Piece;
			component.SetState(PieceState.Placed);
			m_LastValidSocket.OccupyNeighbours(m_FreePlacementMask, m_BuildingPieceMask, component);
			component.Building = m_LastValidSocket.Piece.Building;
			component.Building.AddPiece(component);
		}
		else
		{
			GameObject val = new GameObject("Building", new Type[1] { typeof(BuildingHolder) });
			((Component)component).transform.SetParent(val.transform, true);
			component.Building = val.GetComponent<BuildingHolder>();
			component.Building.AddPiece(component);
			if (component.HasSupport(out var colliders))
			{
				BuildingPiece component2 = ((Component)colliders[0]).GetComponent<BuildingPiece>();
				if ((Object)(object)component2 != (Object)null)
				{
					component.AttachedOn = component2;
				}
			}
			component.SetState(PieceState.Placed);
		}
		m_RotationOffset = 0f;
		if (Object.op_Implicit((Object)(object)component.PlacementFX))
		{
			Object.Instantiate<GameObject>(component.PlacementFX, ((Component)component).transform.position, ((Component)component).transform.rotation);
		}
		m_LastValidSocket = null;
		m_HasSocket = false;
	}

	private bool IntersectsSocket(Ray ray, Socket socket)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Vector3 val = ((Component)socket).transform.position - ((Ray)(ref ray)).origin;
		float num = Vector3.Dot(val, ((Ray)(ref ray)).direction);
		if (num < 0f)
		{
			return false;
		}
		if (Vector3.Dot(val, val) - num * num > socket.Radius * socket.Radius)
		{
			return false;
		}
		return true;
	}

	public void ClearPreview()
	{
		if ((Object)(object)m_CurrentPreview != (Object)null)
		{
			Object.Destroy((Object)(object)m_CurrentPreview.gameObject);
			m_CurrentPreview = null;
			m_CurrentPreviewPiece = null;
		}
	}

	public bool PreviewExists()
	{
		if (Object.op_Implicit((Object)(object)m_CurrentPreview))
		{
			return true;
		}
		return false;
	}
}
