using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x0200069F RID: 1695
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	[ExecuteInEditMode]
	public class RenderObject : MonoBehaviour
	{
		// Token: 0x06003578 RID: 13688 RVA: 0x00170CDC File Offset: 0x0016EEDC
		protected virtual void OnTransformParentChanged()
		{
			if (base.isActiveAndEnabled)
			{
				return;
			}
			this.UpdateRect();
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00170CED File Offset: 0x0016EEED
		protected void OnDisable()
		{
			if (this.m_CanvasRender == null)
			{
				return;
			}
			this.m_CanvasRender.Clear();
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x00170D09 File Offset: 0x0016EF09
		protected void Start()
		{
			this.UpdateRect();
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600357B RID: 13691 RVA: 0x00170D11 File Offset: 0x0016EF11
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

		// Token: 0x0600357C RID: 13692 RVA: 0x00170D34 File Offset: 0x0016EF34
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

		// Token: 0x0600357D RID: 13693 RVA: 0x00170DC7 File Offset: 0x0016EFC7
		public void FillMesh(Mesh workerMesh)
		{
			this.canvasRenderer.SetMesh(workerMesh);
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x00170DD5 File Offset: 0x0016EFD5
		public void UpdateMaterial(Material mat, Texture texture)
		{
			this.canvasRenderer.materialCount = 1;
			this.canvasRenderer.SetMaterial(mat, 0);
			this.canvasRenderer.SetTexture(texture);
		}

		// Token: 0x04002F08 RID: 12040
		private RectTransform rect;

		// Token: 0x04002F09 RID: 12041
		[NonSerialized]
		private CanvasRenderer m_CanvasRender;
	}
}
