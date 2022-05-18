using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009BE RID: 2494
	internal class LineNode : NodeBase
	{
		// Token: 0x06003F81 RID: 16257 RVA: 0x0002DA53 File Offset: 0x0002BC53
		public override float getHeight()
		{
			return this.height;
		}

		// Token: 0x06003F82 RID: 16258 RVA: 0x0002DA5B File Offset: 0x0002BC5B
		public override float getWidth()
		{
			return 0f;
		}

		// Token: 0x06003F83 RID: 16259 RVA: 0x001B99CC File Offset: 0x001B7BCC
		public override void fill(ref Vector2 currentpos, List<Line> lines, float maxWidth, float pixelsPerUnit)
		{
			this.height = (float)FontCache.GetLineHeight(this.font, (int)((float)this.fontSize * pixelsPerUnit), this.fs) / pixelsPerUnit;
			lines.Add(new Line(new Vector2(0f, this.height)));
		}

		// Token: 0x06003F84 RID: 16260 RVA: 0x0002DA62 File Offset: 0x0002BC62
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			yline += 1U;
			x = offsetX;
		}

		// Token: 0x06003F85 RID: 16261 RVA: 0x0002DA70 File Offset: 0x0002BC70
		public override void Release()
		{
			base.Release();
			this.height = 0f;
		}

		// Token: 0x040038E6 RID: 14566
		public Font font;

		// Token: 0x040038E7 RID: 14567
		public FontStyle fs;

		// Token: 0x040038E8 RID: 14568
		public int fontSize;

		// Token: 0x040038E9 RID: 14569
		private float height;
	}
}
