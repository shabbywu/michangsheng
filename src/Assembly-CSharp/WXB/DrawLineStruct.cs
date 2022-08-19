using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WXB
{
	// Token: 0x0200068A RID: 1674
	public class DrawLineStruct : DrawStruct
	{
		// Token: 0x06003518 RID: 13592 RVA: 0x0016FDF4 File Offset: 0x0016DFF4
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

		// Token: 0x04002EE9 RID: 12009
		public List<DrawLineStruct.Line> lines = new List<DrawLineStruct.Line>();

		// Token: 0x020014FB RID: 5371
		public struct Line
		{
			// Token: 0x06008296 RID: 33430 RVA: 0x002DB634 File Offset: 0x002D9834
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

			// Token: 0x04006DEE RID: 28142
			public Vector2 leftPos;

			// Token: 0x04006DEF RID: 28143
			public float width;

			// Token: 0x04006DF0 RID: 28144
			public float height;

			// Token: 0x04006DF1 RID: 28145
			public Vector2 uv;

			// Token: 0x04006DF2 RID: 28146
			public Color color;

			// Token: 0x04006DF3 RID: 28147
			public int dynSpeed;

			// Token: 0x04006DF4 RID: 28148
			public TextNode node;
		}
	}
}
