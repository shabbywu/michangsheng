using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A7 RID: 1703
	internal class LineNode : NodeBase
	{
		// Token: 0x060035C0 RID: 13760 RVA: 0x00171E25 File Offset: 0x00170025
		public override float getHeight()
		{
			return this.height;
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x00171E2D File Offset: 0x0017002D
		public override float getWidth()
		{
			return 0f;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x00171E34 File Offset: 0x00170034
		public override void fill(ref Vector2 currentpos, List<Line> lines, float maxWidth, float pixelsPerUnit)
		{
			this.height = (float)FontCache.GetLineHeight(this.font, (int)((float)this.fontSize * pixelsPerUnit), this.fs) / pixelsPerUnit;
			lines.Add(new Line(new Vector2(0f, this.height)));
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x00171E82 File Offset: 0x00170082
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			yline += 1U;
			x = offsetX;
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x00171E90 File Offset: 0x00170090
		public override void Release()
		{
			base.Release();
			this.height = 0f;
		}

		// Token: 0x04002F20 RID: 12064
		public Font font;

		// Token: 0x04002F21 RID: 12065
		public FontStyle fs;

		// Token: 0x04002F22 RID: 12066
		public int fontSize;

		// Token: 0x04002F23 RID: 12067
		private float height;
	}
}
