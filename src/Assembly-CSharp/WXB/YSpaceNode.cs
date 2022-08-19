using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x020006B3 RID: 1715
	internal class YSpaceNode : NodeBase
	{
		// Token: 0x0600360D RID: 13837 RVA: 0x00172D71 File Offset: 0x00170F71
		public override float getHeight()
		{
			return this.d_offset;
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x00172D79 File Offset: 0x00170F79
		public override float getWidth()
		{
			return 0.001f;
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x00172D80 File Offset: 0x00170F80
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			if (this.d_bNewLine)
			{
				yline += 1U;
				x = offsetX;
			}
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x00172D96 File Offset: 0x00170F96
		public override void Release()
		{
			base.Release();
			this.d_offset = 0f;
		}

		// Token: 0x04002F57 RID: 12119
		public float d_offset;
	}
}
