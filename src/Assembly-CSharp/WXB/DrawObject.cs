using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000688 RID: 1672
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	[ExecuteInEditMode]
	public class DrawObject : MonoBehaviour, Draw
	{
		// Token: 0x060034FE RID: 13566 RVA: 0x0016FCA6 File Offset: 0x0016DEA6
		protected virtual void OnTransformParentChanged()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			this.UpdateRect(Vector2.zero);
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x0016FCBC File Offset: 0x0016DEBC
		protected virtual void OnDisable()
		{
			if (this.canvasRenderer == null)
			{
				return;
			}
			this.canvasRenderer.Clear();
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x0016FCD8 File Offset: 0x0016DED8
		protected void OnEnable()
		{
			this.UpdateRect(Vector2.zero);
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x0016FCE5 File Offset: 0x0016DEE5
		public void OnInit()
		{
			base.enabled = true;
			this.UpdateRect(Vector2.zero);
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x0016FCD8 File Offset: 0x0016DED8
		protected void Start()
		{
			this.UpdateRect(Vector2.zero);
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x00004095 File Offset: 0x00002295
		protected virtual void Init()
		{
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x0016FCF9 File Offset: 0x0016DEF9
		protected void Awake()
		{
			this.canvasRenderer = base.GetComponent<CanvasRenderer>();
			this.rectTransform = base.GetComponent<RectTransform>();
			this.Init();
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06003505 RID: 13573 RVA: 0x0016FD19 File Offset: 0x0016DF19
		// (set) Token: 0x06003506 RID: 13574 RVA: 0x0016FD21 File Offset: 0x0016DF21
		public RectTransform rectTransform { get; private set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06003507 RID: 13575 RVA: 0x0000280F File Offset: 0x00000A0F
		public virtual DrawType type
		{
			get
			{
				return DrawType.Default;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06003508 RID: 13576 RVA: 0x0016FD2A File Offset: 0x0016DF2A
		// (set) Token: 0x06003509 RID: 13577 RVA: 0x0016FD32 File Offset: 0x0016DF32
		public virtual long key { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x0016FD3B File Offset: 0x0016DF3B
		// (set) Token: 0x0600350B RID: 13579 RVA: 0x0016FD43 File Offset: 0x0016DF43
		public CanvasRenderer canvasRenderer { get; private set; }

		// Token: 0x0600350C RID: 13580 RVA: 0x0016FD4C File Offset: 0x0016DF4C
		protected void UpdateRect(Vector2 offset)
		{
			Tools.UpdateRect(this.rectTransform, offset);
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x00004095 File Offset: 0x00002295
		public virtual void UpdateSelf(float deltaTime)
		{
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x0016FD5A File Offset: 0x0016DF5A
		// (set) Token: 0x0600350F RID: 13583 RVA: 0x0016FD62 File Offset: 0x0016DF62
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

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x0016FD6B File Offset: 0x0016DF6B
		// (set) Token: 0x06003511 RID: 13585 RVA: 0x0016FD73 File Offset: 0x0016DF73
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

		// Token: 0x06003512 RID: 13586 RVA: 0x0016FD7C File Offset: 0x0016DF7C
		public void FillMesh(Mesh workerMesh)
		{
			this.canvasRenderer.SetMesh(workerMesh);
		}

		// Token: 0x06003513 RID: 13587 RVA: 0x0016FD8A File Offset: 0x0016DF8A
		public virtual void UpdateMaterial(Material mat)
		{
			this.canvasRenderer.materialCount = 1;
			this.canvasRenderer.SetMaterial(mat, 0);
			this.canvasRenderer.SetTexture(this.m_Texture);
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x0016FDB6 File Offset: 0x0016DFB6
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

		// Token: 0x06003515 RID: 13589 RVA: 0x0016FDE7 File Offset: 0x0016DFE7
		public void DestroySelf()
		{
			Tools.Destroy(base.gameObject);
		}

		// Token: 0x04002EE7 RID: 12007
		private Material m_Material;

		// Token: 0x04002EE8 RID: 12008
		private Texture m_Texture;
	}
}
