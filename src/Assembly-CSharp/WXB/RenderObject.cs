using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009B4 RID: 2484
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	[ExecuteInEditMode]
	public class RenderObject : MonoBehaviour
	{
		// Token: 0x06003F39 RID: 16185 RVA: 0x0002D6E1 File Offset: 0x0002B8E1
		protected virtual void OnTransformParentChanged()
		{
			if (base.isActiveAndEnabled)
			{
				return;
			}
			this.UpdateRect();
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x0002D6F2 File Offset: 0x0002B8F2
		protected void OnDisable()
		{
			if (this.m_CanvasRender == null)
			{
				return;
			}
			this.m_CanvasRender.Clear();
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x0002D70E File Offset: 0x0002B90E
		protected void Start()
		{
			this.UpdateRect();
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06003F3C RID: 16188 RVA: 0x0002D716 File Offset: 0x0002B916
		public CanvasRenderer canvasRenderer
		{
			get
			{
				if (this.m_CanvasRender == null)
				{
					this.m_CanvasRender = base.GetComponent<CanvasRenderer>();
				}
				return this.m_CanvasRender;
			}
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x001B8D44 File Offset: 0x001B6F44
		private void UpdateRect()
		{
			if (this.rect == null)
			{
				this.rect = base.GetComponent<RectTransform>();
			}
			RectTransform rectTransform = this.rect.parent as RectTransform;
			if (rectTransform == null)
			{
				return;
			}
			this.rect.pivot = rectTransform.pivot;
			this.rect.anchorMin = Vector2.zero;
			this.rect.anchorMax = Vector2.one;
			this.rect.offsetMax = Vector2.zero;
			this.rect.offsetMin = Vector2.zero;
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x0002D738 File Offset: 0x0002B938
		public void FillMesh(Mesh workerMesh)
		{
			this.canvasRenderer.SetMesh(workerMesh);
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x0002D746 File Offset: 0x0002B946
		public void UpdateMaterial(Material mat, Texture texture)
		{
			this.canvasRenderer.materialCount = 1;
			this.canvasRenderer.SetMaterial(mat, 0);
			this.canvasRenderer.SetTexture(texture);
		}

		// Token: 0x040038C3 RID: 14531
		private RectTransform rect;

		// Token: 0x040038C4 RID: 14532
		[NonSerialized]
		private CanvasRenderer m_CanvasRender;
	}
}
