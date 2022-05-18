using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x02000990 RID: 2448
	[ExecuteInEditMode]
	public class CartoonDraw : EffectDrawObjec, ICanvasElement
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		public override DrawType type
		{
			get
			{
				return DrawType.Cartoon;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06003E87 RID: 16007 RVA: 0x0002D0EF File Offset: 0x0002B2EF
		// (set) Token: 0x06003E88 RID: 16008 RVA: 0x0002D0F7 File Offset: 0x0002B2F7
		public Cartoon cartoon { get; set; }

		// Token: 0x06003E89 RID: 16009 RVA: 0x001B7534 File Offset: 0x001B5734
		private void UpdateAnim(float deltaTime)
		{
			this.mDelta += Mathf.Min(1f, deltaTime);
			float num = 1f / this.cartoon.fps;
			while (num < this.mDelta)
			{
				this.mDelta = ((num > 0f) ? (this.mDelta - num) : 0f);
				int num2 = this.frameIndex + 1;
				this.frameIndex = num2;
				if (num2 >= this.cartoon.sprites.Length)
				{
					this.frameIndex = 0;
				}
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x0002D100 File Offset: 0x0002B300
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x0002D109 File Offset: 0x0002B309
		public bool isOpenAlpha
		{
			get
			{
				return base.GetOpen(0);
			}
			set
			{
				base.SetOpen<AlphaEffect>(0, value);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x0002D113 File Offset: 0x0002B313
		// (set) Token: 0x06003E8D RID: 16013 RVA: 0x0002D11C File Offset: 0x0002B31C
		public bool isOpenOffset
		{
			get
			{
				return base.GetOpen(1);
			}
			set
			{
				base.SetOpen<OffsetEffect>(1, value);
			}
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x0002D126 File Offset: 0x0002B326
		public void Add(Vector2 leftPos, float width, float height, Color color)
		{
			this.mData.Add(new CartoonDraw.Data
			{
				leftPos = leftPos,
				color = color,
				width = width,
				height = height
			});
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x0002D155 File Offset: 0x0002B355
		public override void UpdateSelf(float deltaTime)
		{
			base.UpdateSelf(deltaTime);
			int num = this.frameIndex;
			this.UpdateAnim(deltaTime);
			if (num != this.frameIndex)
			{
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			}
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x001B75BC File Offset: 0x001B57BC
		public void Rebuild(CanvasUpdate executing)
		{
			if (executing != 3)
			{
				return;
			}
			if (this.mData == null)
			{
				return;
			}
			Sprite sprite = this.cartoon.sprites[this.frameIndex];
			Vector4 outerUV = DataUtility.GetOuterUV(this.cartoon.sprites[this.frameIndex]);
			VertexHelper vertexHelper = Tools.vertexHelper;
			vertexHelper.Clear();
			for (int i = 0; i < this.mData.Count; i++)
			{
				this.mData[i].Gen(vertexHelper, outerUV);
			}
			Mesh workerMesh = SymbolText.WorkerMesh;
			vertexHelper.FillMesh(workerMesh);
			base.canvasRenderer.SetMesh(workerMesh);
			base.canvasRenderer.SetTexture(sprite.texture);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x0002D179 File Offset: 0x0002B379
		public override void Release()
		{
			base.Release();
			this.mData.Clear();
			this.frameIndex = 0;
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x000042DD File Offset: 0x000024DD
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x0002D193 File Offset: 0x0002B393
		public bool IsDestroyed()
		{
			return this == null;
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x000042DD File Offset: 0x000024DD
		public void LayoutComplete()
		{
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x0002D1AF File Offset: 0x0002B3AF
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400386F RID: 14447
		private int frameIndex;

		// Token: 0x04003870 RID: 14448
		private float mDelta;

		// Token: 0x04003871 RID: 14449
		private List<CartoonDraw.Data> mData = new List<CartoonDraw.Data>();

		// Token: 0x02000991 RID: 2449
		private class Data
		{
			// Token: 0x06003E97 RID: 16023 RVA: 0x001B7668 File Offset: 0x001B5868
			public void Gen(VertexHelper vh, Vector4 uv)
			{
				int currentVertCount = vh.currentVertCount;
				vh.AddVert(new Vector3(this.leftPos.x, this.leftPos.y), this.color, new Vector2(uv.x, uv.y));
				vh.AddVert(new Vector3(this.leftPos.x, this.leftPos.y + this.height), this.color, new Vector2(uv.x, uv.w));
				vh.AddVert(new Vector3(this.leftPos.x + this.width, this.leftPos.y + this.height), this.color, new Vector2(uv.z, uv.w));
				vh.AddVert(new Vector3(this.leftPos.x + this.width, this.leftPos.y), this.color, new Vector2(uv.z, uv.y));
				vh.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
				vh.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
			}

			// Token: 0x04003872 RID: 14450
			public Vector2 leftPos;

			// Token: 0x04003873 RID: 14451
			public Color color;

			// Token: 0x04003874 RID: 14452
			public float width;

			// Token: 0x04003875 RID: 14453
			public float height;
		}
	}
}
