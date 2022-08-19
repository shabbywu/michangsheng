using System;
using UnityEngine;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x02000698 RID: 1688
	[ExecuteInEditMode]
	public class OutlineDraw : EffectDrawObjec, ICanvasElement
	{
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x001709C1 File Offset: 0x0016EBC1
		public override DrawType type
		{
			get
			{
				return DrawType.Outline;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x0016F2BA File Offset: 0x0016D4BA
		// (set) Token: 0x06003557 RID: 13655 RVA: 0x0016F2C3 File Offset: 0x0016D4C3
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

		// Token: 0x06003558 RID: 13656 RVA: 0x001709C4 File Offset: 0x0016EBC4
		public override void UpdateSelf(float deltaTime)
		{
			base.UpdateSelf(deltaTime);
			if (this.currentWidth >= this.maxWidth || this.m_Data == null)
			{
				return;
			}
			float num = this.currentWidth;
			for (int i = 0; i < this.m_Data.lines.Count; i++)
			{
				DrawLineStruct.Line line = this.m_Data.lines[i];
				if (num >= line.width)
				{
					num -= line.width;
				}
				else
				{
					float num2 = (line.width - num) / (float)line.dynSpeed;
					if (num2 >= deltaTime)
					{
						this.currentWidth += deltaTime * (float)line.dynSpeed;
						break;
					}
					this.currentWidth += (float)line.dynSpeed * num2;
					deltaTime -= num2;
					num -= (float)line.dynSpeed * num2;
				}
			}
			CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x00170A94 File Offset: 0x0016EC94
		public void AddLine(TextNode n, Vector2 left, float width, float height, Color color, Vector2 uv, int speed)
		{
			if (this.m_Data == null)
			{
				this.m_Data = new DrawLineStruct();
				this.maxWidth = 0f;
				this.currentWidth = 0f;
			}
			this.maxWidth += width;
			this.m_Data.lines.Add(new DrawLineStruct.Line
			{
				leftPos = left,
				width = width,
				height = height,
				color = color,
				uv = uv,
				node = n,
				dynSpeed = speed
			});
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x00170B2D File Offset: 0x0016ED2D
		public override void UpdateMaterial(Material mat)
		{
			base.UpdateMaterial(mat);
			base.rectTransform.SetAsLastSibling();
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x00170B44 File Offset: 0x0016ED44
		public void Rebuild(CanvasUpdate executing)
		{
			if (this.m_Data == null)
			{
				return;
			}
			if (executing != 3)
			{
				return;
			}
			float width = this.currentWidth;
			VertexHelper vertexHelper = Tools.vertexHelper;
			vertexHelper.Clear();
			this.m_Data.Render(width, vertexHelper);
			Mesh workerMesh = SymbolText.WorkerMesh;
			vertexHelper.FillMesh(workerMesh);
			base.canvasRenderer.SetMesh(workerMesh);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x00170B98 File Offset: 0x0016ED98
		public override void Release()
		{
			base.Release();
			this.m_Data = null;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x00004095 File Offset: 0x00002295
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x0016F3F8 File Offset: 0x0016D5F8
		public bool IsDestroyed()
		{
			return this == null;
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x00004095 File Offset: 0x00002295
		public void LayoutComplete()
		{
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x0016F414 File Offset: 0x0016D614
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04002F00 RID: 12032
		private DrawLineStruct m_Data;

		// Token: 0x04002F01 RID: 12033
		private float currentWidth;

		// Token: 0x04002F02 RID: 12034
		private float maxWidth;
	}
}
