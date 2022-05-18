using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000999 RID: 2457
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	[ExecuteInEditMode]
	public class DrawObject : MonoBehaviour, Draw
	{
		// Token: 0x06003EB8 RID: 16056 RVA: 0x0002D233 File Offset: 0x0002B433
		protected virtual void OnTransformParentChanged()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.UpdateRect(Vector2.zero);
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0002D249 File Offset: 0x0002B449
		protected virtual void OnDisable()
		{
			if (this.canvasRenderer == null)
			{
				return;
			}
			this.canvasRenderer.Clear();
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x0002D265 File Offset: 0x0002B465
		protected void OnEnable()
		{
			this.UpdateRect(Vector2.zero);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x0002D272 File Offset: 0x0002B472
		public void OnInit()
		{
			base.enabled = true;
			this.UpdateRect(Vector2.zero);
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x0002D265 File Offset: 0x0002B465
		protected void Start()
		{
			this.UpdateRect(Vector2.zero);
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000042DD File Offset: 0x000024DD
		protected virtual void Init()
		{
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x0002D286 File Offset: 0x0002B486
		protected void Awake()
		{
			this.canvasRenderer = base.GetComponent<CanvasRenderer>();
			this.rectTransform = base.GetComponent<RectTransform>();
			this.Init();
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x0002D2A6 File Offset: 0x0002B4A6
		// (set) Token: 0x06003EC0 RID: 16064 RVA: 0x0002D2AE File Offset: 0x0002B4AE
		public RectTransform rectTransform { get; private set; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06003EC1 RID: 16065 RVA: 0x00004050 File Offset: 0x00002250
		public virtual DrawType type
		{
			get
			{
				return DrawType.Default;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x0002D2B7 File Offset: 0x0002B4B7
		// (set) Token: 0x06003EC3 RID: 16067 RVA: 0x0002D2BF File Offset: 0x0002B4BF
		public virtual long key { get; set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x0002D2C8 File Offset: 0x0002B4C8
		// (set) Token: 0x06003EC5 RID: 16069 RVA: 0x0002D2D0 File Offset: 0x0002B4D0
		public CanvasRenderer canvasRenderer { get; private set; }

		// Token: 0x06003EC6 RID: 16070 RVA: 0x0002D2D9 File Offset: 0x0002B4D9
		protected void UpdateRect(Vector2 offset)
		{
			Tools.UpdateRect(this.rectTransform, offset);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x000042DD File Offset: 0x000024DD
		public virtual void UpdateSelf(float deltaTime)
		{
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x0002D2E7 File Offset: 0x0002B4E7
		// (set) Token: 0x06003EC9 RID: 16073 RVA: 0x0002D2EF File Offset: 0x0002B4EF
		public Material srcMat
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x0002D2F8 File Offset: 0x0002B4F8
		// (set) Token: 0x06003ECB RID: 16075 RVA: 0x0002D300 File Offset: 0x0002B500
		public Texture texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				this.m_Texture = value;
			}
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x0002D309 File Offset: 0x0002B509
		public void FillMesh(Mesh workerMesh)
		{
			this.canvasRenderer.SetMesh(workerMesh);
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0002D317 File Offset: 0x0002B517
		public virtual void UpdateMaterial(Material mat)
		{
			this.canvasRenderer.materialCount = 1;
			this.canvasRenderer.SetMaterial(mat, 0);
			this.canvasRenderer.SetTexture(this.m_Texture);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x0002D343 File Offset: 0x0002B543
		public virtual void Release()
		{
			this.m_Material = null;
			this.m_Texture = null;
			this.key = 0L;
			if (this.canvasRenderer != null)
			{
				this.canvasRenderer.Clear();
			}
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x0002D374 File Offset: 0x0002B574
		public void DestroySelf()
		{
			Tools.Destroy(base.gameObject);
		}

		// Token: 0x04003893 RID: 14483
		private Material m_Material;

		// Token: 0x04003894 RID: 14484
		private Texture m_Texture;
	}
}
