using System;
using UnityEngine;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x020009AC RID: 2476
	[ExecuteInEditMode]
	public class OutlineDraw : EffectDrawObjec, ICanvasElement
	{
		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06003F13 RID: 16147 RVA: 0x0002D5FA File Offset: 0x0002B7FA
		public override DrawType type
		{
			get
			{
				return DrawType.Outline;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06003F14 RID: 16148 RVA: 0x0002D100 File Offset: 0x0002B300
		// (set) Token: 0x06003F15 RID: 16149 RVA: 0x0002D109 File Offset: 0x0002B309
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

		// Token: 0x06003F16 RID: 16150 RVA: 0x001B8AFC File Offset: 0x001B6CFC
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

		// Token: 0x06003F17 RID: 16151 RVA: 0x001B8BCC File Offset: 0x001B6DCC
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

		// Token: 0x06003F18 RID: 16152 RVA: 0x0002D5FD File Offset: 0x0002B7FD
		public override void UpdateMaterial(Material mat)
		{
			base.UpdateMaterial(mat);
			base.rectTransform.SetAsLastSibling();
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x001B8C68 File Offset: 0x001B6E68
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

		// Token: 0x06003F1A RID: 16154 RVA: 0x0002D611 File Offset: 0x0002B811
		public override void Release()
		{
			base.Release();
			this.m_Data = null;
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x000042DD File Offset: 0x000024DD
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0002D193 File Offset: 0x0002B393
		public bool IsDestroyed()
		{
			return this == null;
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x000042DD File Offset: 0x000024DD
		public void LayoutComplete()
		{
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x0002D1AF File Offset: 0x0002B3AF
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x040038B9 RID: 14521
		private DrawLineStruct m_Data;

		// Token: 0x040038BA RID: 14522
		private float currentWidth;

		// Token: 0x040038BB RID: 14523
		private float maxWidth;
	}
}
