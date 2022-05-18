using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x0200099B RID: 2459
	public class DrawLineStruct : DrawStruct
	{
		// Token: 0x06003ED2 RID: 16082 RVA: 0x001B80BC File Offset: 0x001B62BC
		public void Render(float width, VertexHelper vh)
		{
			for (int i = 0; i < this.lines.Count; i++)
			{
				this.lines[i].Render(vh, ref width);
				if (width <= 0f)
				{
					break;
				}
			}
		}

		// Token: 0x04003895 RID: 14485
		public List<DrawLineStruct.Line> lines = new List<DrawLineStruct.Line>();

		// Token: 0x0200099C RID: 2460
		public struct Line
		{
			// Token: 0x06003ED4 RID: 16084 RVA: 0x001B8100 File Offset: 0x001B6300
			public void Render(VertexHelper vh, ref float curwidth)
			{
				int currentIndexCount = vh.currentIndexCount;
				Color effectColor = this.color;
				if (effectColor.a <= 0.01f)
				{
					return;
				}
				if (curwidth >= this.width)
				{
					Tools.AddLine(vh, this.leftPos, this.uv, this.width, this.height, effectColor);
				}
				else
				{
					Tools.AddLine(vh, this.leftPos, this.uv, curwidth, this.height, effectColor);
				}
				EffectType effectType = this.node.effectType;
				if (effectType != EffectType.Shadow)
				{
					if (effectType == EffectType.Outline)
					{
						effectColor = this.node.effectColor;
						Effect.Outline(vh, currentIndexCount, effectColor, this.node.effectDistance);
					}
				}
				else
				{
					effectColor = this.node.effectColor;
					Effect.Shadow(vh, currentIndexCount, effectColor, this.node.effectDistance);
				}
				curwidth -= this.width;
			}

			// Token: 0x04003896 RID: 14486
			public Vector2 leftPos;

			// Token: 0x04003897 RID: 14487
			public float width;

			// Token: 0x04003898 RID: 14488
			public float height;

			// Token: 0x04003899 RID: 14489
			public Vector2 uv;

			// Token: 0x0400389A RID: 14490
			public Color color;

			// Token: 0x0400389B RID: 14491
			public int dynSpeed;

			// Token: 0x0400389C RID: 14492
			public TextNode node;
		}
	}
}
