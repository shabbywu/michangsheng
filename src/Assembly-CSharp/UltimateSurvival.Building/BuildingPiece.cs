using System.Collections.Generic;
using UnityEngine;

namespace UltimateSurvival.Building;

public class BuildingPiece : MonoBehaviour
{
	[SerializeField]
	private string m_PieceName;

	[SerializeField]
	private RequiredItem[] m_RequiredItems;

	[SerializeField]
	private Vector3 m_RotationAxis = Vector3.forward;

	[Header("Setup")]
	[SerializeField]
	private bool m_ShowBounds;

	[SerializeField]
	private Bounds m_Bounds;

	[SerializeField]
	[Tooltip("If left empty, it will automatically get populated with the first MeshFilter found.")]
	private MeshFilter m_MainMesh;

	[SerializeField]
	private List<Renderer> m_IgnoredRenderers;

	[SerializeField]
	private List<Collider> m_IgnoredColliders;

	[Header("Placing")]
	[SerializeField]
	private BuildingSpace m_NeededSpace;

	[SerializeField]
	private BuildingSpace[] m_SpacesToOccupy;

	[SerializeField]
	private bool m_RequiresSockets;

	[SerializeField]
	private float m_OutOfGroundHeight;

	[SerializeField]
	private bool m_AllowUnderTerrainMovement;

	[Header("Stability")]
	[SerializeField]
	private bool m_CheckStability = true;

	[Space]
	[SerializeField]
	private LayerMask m_StabilityCheckMask;

	[SerializeField]
	private bool m_ShowStabilityBox;

	[SerializeField]
	private Bounds[] m_StabilityBoxes;

	[Header("Terrain Protection")]
	[SerializeField]
	private bool m_EnableTP;

	[SerializeField]
	private bool m_ShowTP;

	[SerializeField]
	private Bounds m_TPBox;

	[Header("Sound And Effects")]
	[SerializeField]
	private SoundPlayer m_BuildAudio;

	[SerializeField]
	private GameObject m_PlacementFX;

	[Header("Preview")]
	[SerializeField]
	private Material m_PreviewMat;

	private Dictionary<Renderer, Material[]> m_InitialMaterials = new Dictionary<Renderer, Material[]>();

	private Socket[] m_Sockets = new Socket[0];

	private PieceState m_State;

	private List<Collider> m_Colliders = new List<Collider>();

	private List<Renderer> m_Renderers = new List<Renderer>();

	private bool m_Initialized;

	public PieceState State => m_State;

	public BuildingHolder Building { get; set; }

	public BuildingPiece AttachedOn { get; set; }

	public string Name => m_PieceName;

	public RequiredItem[] RequiredItems => m_RequiredItems;

	public Vector3 RotationAxis => m_RotationAxis;

	public BuildingSpace NeededSpace => m_NeededSpace;

	public BuildingSpace[] SpacesToOccupy => m_SpacesToOccupy;

	public float OutOfGroundHeight => m_OutOfGroundHeight;

	public bool AllowUnderTerrainMovement => m_AllowUnderTerrainMovement;

	public bool RequiresSockets => m_RequiresSockets;

	public SoundPlayer BuildAudio => m_BuildAudio;

	public GameObject PlacementFX => m_PlacementFX;

	public Bounds Bounds => new Bounds(((Component)this).transform.position + ((Component)this).transform.TransformVector(((Bounds)(ref m_Bounds)).center), ((Bounds)(ref m_Bounds)).size);

	public List<Renderer> Renderers => m_Renderers;

	public MeshFilter MainMesh => m_MainMesh;

	public Socket[] Sockets => m_Sockets;

	private void Awake()
	{
		if ((Object)(object)m_MainMesh == (Object)null)
		{
			m_MainMesh = ((Component)this).GetComponentInChildren<MeshFilter>();
		}
		((Component)this).GetComponentsInChildren<Renderer>(m_Renderers);
		m_Renderers.RemoveAll((Renderer r) => m_IgnoredRenderers.Contains(r));
		for (int i = 0; i < m_Renderers.Count; i++)
		{
			m_InitialMaterials.Add(m_Renderers[i], m_Renderers[i].sharedMaterials);
		}
		((Component)this).GetComponentsInChildren<Collider>(m_Colliders);
		m_Colliders.RemoveAll((Collider col) => m_IgnoredColliders.Contains(col));
		for (int j = 0; j < m_Colliders.Count; j++)
		{
			for (int k = 0; k < m_Colliders.Count; k++)
			{
				if ((Object)(object)m_Colliders[j] != (Object)(object)m_Colliders[k])
				{
					Physics.IgnoreCollision(m_Colliders[j], m_Colliders[k]);
				}
			}
		}
		m_Sockets = ((Component)this).GetComponentsInChildren<Socket>();
		m_Initialized = true;
	}

	private void Update()
	{
		if (m_CheckStability && State == PieceState.Placed && !HasSupport(out var _))
		{
			On_SocketDeath();
		}
	}

	public void SetState(PieceState state)
	{
		if (!m_Initialized)
		{
			Awake();
		}
		switch (state)
		{
		case PieceState.Preview:
		{
			SetMaterials(m_PreviewMat);
			foreach (Collider collider in m_Colliders)
			{
				if (Object.op_Implicit((Object)(object)collider))
				{
					collider.enabled = false;
				}
				else
				{
					Debug.LogError((object)"A collider was found null in the collider list!", (Object)(object)this);
				}
			}
			Socket[] sockets = m_Sockets;
			for (int i = 0; i < sockets.Length; i++)
			{
				((Component)sockets[i]).gameObject.SetActive(false);
			}
			break;
		}
		case PieceState.Placed:
		{
			SetMaterials(m_InitialMaterials);
			foreach (Collider collider2 in m_Colliders)
			{
				collider2.enabled = true;
			}
			Socket[] sockets = m_Sockets;
			for (int i = 0; i < sockets.Length; i++)
			{
				((Component)sockets[i]).gameObject.SetActive(true);
			}
			if (m_CheckStability && (Object)(object)AttachedOn != (Object)null)
			{
				EntityEventHandler component = ((Component)AttachedOn).GetComponent<EntityEventHandler>();
				if ((Object)(object)component != (Object)null)
				{
					component.Death.AddListener(On_SocketDeath);
				}
			}
			break;
		}
		}
		m_State = state;
	}

	private void On_SocketDeath()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)((Component)this).gameObject != (Object)null)
		{
			((Component)this).GetComponent<EntityEventHandler>().ChangeHealth.Try(new HealthEventData(float.NegativeInfinity));
		}
	}

	private void OnDestroy()
	{
		if ((Object)(object)AttachedOn != (Object)null)
		{
			((Component)AttachedOn).GetComponent<EntityEventHandler>().Death.RemoveListener(On_SocketDeath);
		}
	}

	private void SetMaterials(Material material)
	{
		for (int i = 0; i < m_Renderers.Count; i++)
		{
			Material[] materials = m_Renderers[i].materials;
			for (int j = 0; j < materials.Length; j++)
			{
				materials[j] = material;
			}
			m_Renderers[i].materials = materials;
		}
	}

	private void SetMaterials(Dictionary<Renderer, Material[]> materials)
	{
		for (int i = 0; i < m_Renderers.Count; i++)
		{
			Material[] materials2 = m_Renderers[i].materials;
			for (int j = 0; j < materials2.Length; j++)
			{
				materials2[j] = materials[m_Renderers[i]][j];
			}
			m_Renderers[i].materials = materials2;
		}
	}

	public bool IsBlockedByTerrain()
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (!m_EnableTP)
		{
			return false;
		}
		Collider[] array = Physics.OverlapBox(((Component)this).transform.position + ((Component)this).transform.TransformVector(((Bounds)(ref m_TPBox)).center), ((Bounds)(ref m_TPBox)).extents, ((Component)this).transform.rotation, -1, (QueryTriggerInteraction)1);
		foreach (Collider obj in array)
		{
			if ((Object)(object)((obj is TerrainCollider) ? obj : null) != (Object)null)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasCollider(Collider col)
	{
		for (int i = 0; i < m_Colliders.Count; i++)
		{
			if ((Object)(object)m_Colliders[i] == (Object)(object)col)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasSupport(out Collider[] colliders)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		bool result = true;
		List<Collider> list = new List<Collider>();
		for (int i = 0; i < m_StabilityBoxes.Length; i++)
		{
			Bounds val = m_StabilityBoxes[i];
			Collider[] array = Physics.OverlapBox(((Component)this).transform.position + ((Component)this).transform.TransformVector(((Bounds)(ref val)).center), ((Bounds)(ref val)).extents, ((Component)this).transform.rotation, LayerMask.op_Implicit(m_StabilityCheckMask));
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j].isTrigger && !HasCollider(array[j]))
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

	private void OnDrawGizmosSelected()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		Matrix4x4 matrix = Gizmos.matrix;
		Gizmos.color = Color.blue;
		if (m_ShowBounds)
		{
			Gizmos.matrix = Matrix4x4.TRS(((Component)this).transform.position + ((Component)this).transform.TransformVector(((Bounds)(ref m_Bounds)).center), ((Component)this).transform.rotation, ((Bounds)(ref m_Bounds)).size);
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
		}
		Gizmos.color = Color.yellow;
		if (m_ShowTP)
		{
			Gizmos.matrix = Matrix4x4.TRS(((Component)this).transform.position + ((Component)this).transform.TransformVector(((Bounds)(ref m_TPBox)).center), ((Component)this).transform.rotation, ((Bounds)(ref m_TPBox)).size);
			Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
		}
		Gizmos.color = Color.red;
		if (m_ShowStabilityBox)
		{
			for (int i = 0; i < m_StabilityBoxes.Length; i++)
			{
				Bounds val = m_StabilityBoxes[i];
				Gizmos.matrix = Matrix4x4.TRS(((Component)this).transform.position + ((Component)this).transform.TransformVector(((Bounds)(ref val)).center), ((Component)this).transform.rotation, ((Bounds)(ref val)).size);
				Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
			}
		}
		Gizmos.matrix = matrix;
	}
}
