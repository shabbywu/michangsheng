using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A8 RID: 1704
	public abstract class RectNode : NodeBase
	{
		// Token: 0x060035C6 RID: 13766 RVA: 0x00171EAB File Offset: 0x001700AB
		public override float getHeight()
		{
			return this.height;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x00171EB3 File Offset: 0x001700B3
		public override float getWidth()
		{
			return this.width;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x00171EBB File Offset: 0x001700BB
		public override void Release()
		{
			base.Release();
			this.width = 0f;
			this.height = 0f;
		}

		// Token: 0x060035C9 RID: 13769
		protected abstract void OnRectRender(RenderCache cache, Line line, Rect rect);

		// Token: 0x060035CA RID: 13770 RVA: 0x00171EDC File Offset: 0x001700DC
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			float num = this.getWidth();
			float num2 = this.getHeight();
			if (x + num > maxWidth)
			{
				x = 0f;
				yline += 1U;
			}
			float num3 = NodeBase.AlignedFormatting(this.owner, this.formatting, maxWidth, lines[(int)yline].x);
			float num4 = offsetY;
			int num5 = 0;
			while ((long)num5 < (long)((ulong)yline))
			{
				num4 += lines[num5].y;
				num5++;
			}
			Rect rect;
			rect..ctor(x + offsetX + num3, num4, num, num2);
			float num6 = 0f;
			while (!this.owner.around.isContain(rect, out num6))
			{
				rect.x = num6;
				x = num6 - num3 - offsetX;
				if (x + num > maxWidth)
				{
					x = 0f;
					yline += 1U;
					num4 += lines[(int)yline].y;
					rect..ctor(x + offsetX + num3, num4, num, num2);
				}
			}
			this.OnRectRender(cache, lines[(int)yline], rect);
			x += num;
			if (this.d_bNewLine)
			{
				x = 0f;
				yline += 1U;
			}
		}

		// Token: 0x04002F24 RID: 12068
		public float width;

		// Token: 0x04002F25 RID: 12069
		public float height;
	}
}
