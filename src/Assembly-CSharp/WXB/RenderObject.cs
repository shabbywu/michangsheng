using System;
using UnityEngine;

namespace WXB;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasRenderer))]
[ExecuteInEditMode]
public class RenderObject : MonoBehaviour
{
	private RectTransform rect;

	[NonSerialized]
	private CanvasRenderer m_CanvasRender;

	public CanvasRenderer canvasRenderer
	{
		get
		{
			if ((Object)(object)m_CanvasRender == (Object)null)
			{
				m_CanvasRender = ((Component)this).GetComponent<CanvasRenderer>();
			}
			return m_CanvasRender;
		}
	}

	protected virtual void OnTransformParentChanged()
	{
		if (!((Behaviour)this).isActiveAndEnabled)
		{
			UpdateRect();
		}
	}

	protected void OnDisable()
	{
		if (!((Object)(object)m_CanvasRender == (Object)null))
		{
			m_CanvasRender.Clear();
		}
	}

	protected void Start()
	{
		UpdateRect();
	}

	private void UpdateRect()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		if ((Object)(object)rect == (Object)null)
		{
			rect = ((Component)this).GetComponent<RectTransform>();
		}
		Transform parent = ((Transform)rect).parent;
		RectTransform val = (RectTransform)(object)((parent is RectTransform) ? parent : null);
		if (!((Object)(object)val == (Object)null))
		{
			rect.pivot = val.pivot;
			rect.anchorMin = Vector2.zero;
			rect.anchorMax = Vector2.one;
			rect.offsetMax = Vector2.zero;
			rect.offsetMin = Vector2.zero;
		}
	}

	public void FillMesh(Mesh workerMesh)
	{
		canvasRenderer.SetMesh(workerMesh);
	}

	public void UpdateMaterial(Material mat, Texture texture)
	{
		canvasRenderer.materialCount = 1;
		canvasRenderer.SetMaterial(mat, 0);
		canvasRenderer.SetTexture(texture);
	}
}
