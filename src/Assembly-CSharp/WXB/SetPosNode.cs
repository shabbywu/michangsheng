using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x020006AD RID: 1709
	public class SetPosNode : NodeBase
	{
		// Token: 0x060035F2 RID: 13810 RVA: 0x00171E2D File Offset: 0x0017002D
		public override float getHeight()
		{
			return 0f;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x00171E2D File Offset: 0x0017002D
		public override float getWidth()
		{
			return 0f;
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x001728A8 File Offset: 0x00170AA8
		protected override void AlterX(ref float x, float maxWidth)
		{
			TypePosition typePosition = this.type;
			if (typePosition == TypePosition.Absolute)
			{
				x = this.d_value;
				return;
			}
			if (typePosition != TypePosition.Relative)
			{
				return;
			}
			x = maxWidth * this.d_value;
		}

		// Token: 0x060035F5 RID: 13813 RVA: 0x001728D7 File Offset: 0x00170AD7
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			this.AlterX(ref x, maxWidth);
		}

		// Token: 0x060035F6 RID: 13814 RVA: 0x001728E1 File Offset: 0x00170AE1
		public override void Release()
		{
			base.Release();
			this.d_value = 0f;
		}

		// Token: 0x04002F3C RID: 12092
		public TypePosition type = TypePosition.Relative;

		// Token: 0x04002F3D RID: 12093
		public float d_value;
	}
}
