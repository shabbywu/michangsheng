using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009D6 RID: 2518
	public class XSpaceNode : NodeBase
	{
		// Token: 0x0600400A RID: 16394 RVA: 0x0002DFA9 File Offset: 0x0002C1A9
		public override float getHeight()
		{
			return 0.01f;
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x0002DFB0 File Offset: 0x0002C1B0
		public override float getWidth()
		{
			return this.d_offset;
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x001BB934 File Offset: 0x001B9B34
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			Vector2 vector;
			vector..ctor(x + offsetX, offsetY);
			int num = 0;
			while ((long)num < (long)((ulong)yline))
			{
				vector.y += lines[num].y;
				num++;
			}
			float num2 = NodeBase.AlignedFormatting(this.owner, this.formatting, maxWidth, lines[(int)yline].x);
			if (x + this.d_offset + num2 > maxWidth)
			{
				yline += 1U;
				x = 0f;
			}
			else
			{
				x += this.d_offset;
			}
			if (this.d_bNewLine)
			{
				yline += 1U;
				x = 0f;
			}
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x0002DFB8 File Offset: 0x0002C1B8
		public override void Release()
		{
			base.Release();
			this.d_offset = 0f;
		}

		// Token: 0x04003944 RID: 14660
		public float d_offset;
	}
}
