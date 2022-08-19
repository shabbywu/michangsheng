using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006B2 RID: 1714
	public class XSpaceNode : NodeBase
	{
		// Token: 0x06003608 RID: 13832 RVA: 0x00172CA9 File Offset: 0x00170EA9
		public override float getHeight()
		{
			return 0.01f;
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x00172CB0 File Offset: 0x00170EB0
		public override float getWidth()
		{
			return this.d_offset;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x00172CB8 File Offset: 0x00170EB8
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

		// Token: 0x0600360B RID: 13835 RVA: 0x00172D5E File Offset: 0x00170F5E
		public override void Release()
		{
			base.Release();
			this.d_offset = 0f;
		}

		// Token: 0x04002F56 RID: 12118
		public float d_offset;
	}
}
