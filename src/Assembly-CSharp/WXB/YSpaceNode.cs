using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x020009D7 RID: 2519
	internal class YSpaceNode : NodeBase
	{
		// Token: 0x0600400F RID: 16399 RVA: 0x0002DFCB File Offset: 0x0002C1CB
		public override float getHeight()
		{
			return this.d_offset;
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x0002DFD3 File Offset: 0x0002C1D3
		public override float getWidth()
		{
			return 0.001f;
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x0002DFDA File Offset: 0x0002C1DA
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			if (this.d_bNewLine)
			{
				yline += 1U;
				x = offsetX;
			}
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		public override void Release()
		{
			base.Release();
			this.d_offset = 0f;
		}

		// Token: 0x04003945 RID: 14661
		public float d_offset;
	}
}
