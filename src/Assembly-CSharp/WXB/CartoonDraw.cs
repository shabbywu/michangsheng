using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x02000683 RID: 1667
	[ExecuteInEditMode]
	public class CartoonDraw : EffectDrawObjec, ICanvasElement
	{
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x0016F21F File Offset: 0x0016D41F
		public override DrawType type
		{
			get
			{
				return DrawType.Cartoon;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x0016F222 File Offset: 0x0016D422
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x0016F22A File Offset: 0x0016D42A
		public Cartoon cartoon { get; set; }

		// Token: 0x060034D9 RID: 13529 RVA: 0x0016F234 File Offset: 0x0016D434
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

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060034DA RID: 13530 RVA: 0x0016F2BA File Offset: 0x0016D4BA
		// (set) Token: 0x060034DB RID: 13531 RVA: 0x0016F2C3 File Offset: 0x0016D4C3
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

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060034DC RID: 13532 RVA: 0x0016F2CD File Offset: 0x0016D4CD
		// (set) Token: 0x060034DD RID: 13533 RVA: 0x0016F2D6 File Offset: 0x0016D4D6
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

		// Token: 0x060034DE RID: 13534 RVA: 0x0016F2E0 File Offset: 0x0016D4E0
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

		// Token: 0x060034DF RID: 13535 RVA: 0x0016F30F File Offset: 0x0016D50F
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

		// Token: 0x060034E0 RID: 13536 RVA: 0x0016F334 File Offset: 0x0016D534
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

		// Token: 0x060034E1 RID: 13537 RVA: 0x0016F3DE File Offset: 0x0016D5DE
		public override void Release()
		{
			base.Release();
			this.mData.Clear();
			this.frameIndex = 0;
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x00004095 File Offset: 0x00002295
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x0016F3F8 File Offset: 0x0016D5F8
		public bool IsDestroyed()
		{
			return this == null;
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x00004095 File Offset: 0x00002295
		public void LayoutComplete()
		{
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x0016F414 File Offset: 0x0016D614
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04002EC9 RID: 11977
		private int frameIndex;

		// Token: 0x04002ECA RID: 11978
		private float mDelta;

		// Token: 0x04002ECB RID: 11979
		private List<CartoonDraw.Data> mData = new List<CartoonDraw.Data>();

		// Token: 0x020014F7 RID: 5367
		private class Data
		{
			// Token: 0x0600828C RID: 33420 RVA: 0x002DB3F0 File Offset: 0x002D95F0
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

			// Token: 0x04006DE8 RID: 28136
			public Vector2 leftPos;

			// Token: 0x04006DE9 RID: 28137
			public Color color;

			// Token: 0x04006DEA RID: 28138
			public float width;

			// Token: 0x04006DEB RID: 28139
			public float height;
		}
	}
}
