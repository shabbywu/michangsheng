using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009BF RID: 2495
	public abstract class RectNode : NodeBase
	{
		// Token: 0x06003F87 RID: 16263 RVA: 0x0002DA8B File Offset: 0x0002BC8B
		public override float getHeight()
		{
			return this.height;
		}

		// Token: 0x06003F88 RID: 16264 RVA: 0x0002DA93 File Offset: 0x0002BC93
		public override float getWidth()
		{
			return this.width;
		}

		// Token: 0x06003F89 RID: 16265 RVA: 0x0002DA9B File Offset: 0x0002BC9B
		public override void Release()
		{
			base.Release();
			this.width = 0f;
			this.height = 0f;
		}

		// Token: 0x06003F8A RID: 16266
		protected abstract void OnRectRender(RenderCache cache, Line line, Rect rect);

		// Token: 0x06003F8B RID: 16267 RVA: 0x001B9A1C File Offset: 0x001B7C1C
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

		// Token: 0x040038EA RID: 14570
		public float width;

		// Token: 0x040038EB RID: 14571
		public float height;
	}
}
