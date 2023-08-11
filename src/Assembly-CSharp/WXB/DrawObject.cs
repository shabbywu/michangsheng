using UnityEngine;

namespace WXB;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[ExecuteInEditMode]
public class DrawObject : MonoBehaviour, Draw
{
	private Material m_Material;

	private Texture m_Texture;

	public RectTransform rectTransform { get; private set; }

	public virtual DrawType type => DrawType.Default;

	public virtual long key { get; set; }

	public CanvasRenderer canvasRenderer { get; private set; }

	public Material srcMat
	{
		get
		{
			return m_Material;
		}
		set
		{
			m_Material = value;
		}
	}

	public Texture texture
	{
		get
		{
			return m_Texture;
		}
		set
		{
			m_Texture = value;
		}
	}

	protected virtual void OnTransformParentChanged()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		if (((Behaviour)this).isActiveAndEnabled)
		{
			UpdateRect(Vector2.zero);
		}
	}

	protected virtual void OnDisable()
	{
		if (!((Object)(object)canvasRenderer == (Object)null))
		{
			canvasRenderer.Clear();
		}
	}

	protected void OnEnable()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		UpdateRect(Vector2.zero);
	}

	public void OnInit()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		((Behaviour)this).enabled = true;
		UpdateRect(Vector2.zero);
	}

	protected void Start()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		UpdateRect(Vector2.zero);
	}

	protected virtual void Init()
	{
	}

	protected void Awake()
	{
		canvasRenderer = ((Component)this).GetComponent<CanvasRenderer>();
		rectTransform = ((Component)this).GetComponent<RectTransform>();
		Init();
	}

	protected void UpdateRect(Vector2 offset)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Tools.UpdateRect(rectTransform, offset);
	}

	public virtual void UpdateSelf(float deltaTime)
	{
	}

	public void FillMesh(Mesh workerMesh)
	{
		canvasRenderer.SetMesh(workerMesh);
	}

	public virtual void UpdateMaterial(Material mat)
	{
		canvasRenderer.materialCount = 1;
		canvasRenderer.SetMaterial(mat, 0);
		canvasRenderer.SetTexture(m_Texture);
	}

	public virtual void Release()
	{
		m_Material = null;
		m_Texture = null;
		key = 0L;
		if ((Object)(object)canvasRenderer != (Object)null)
		{
			canvasRenderer.Clear();
		}
	}

	public void DestroySelf()
	{
		Tools.Destroy((Object)(object)((Component)this).gameObject);
	}
}
