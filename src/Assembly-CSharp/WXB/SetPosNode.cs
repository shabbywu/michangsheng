using System;
using System.Collections.Generic;

namespace WXB
{
	// Token: 0x020009CF RID: 2511
	public class SetPosNode : NodeBase
	{
		// Token: 0x06003FEF RID: 16367 RVA: 0x0002DA5B File Offset: 0x0002BC5B
		public override float getHeight()
		{
			return 0f;
		}

		// Token: 0x06003FF0 RID: 16368 RVA: 0x0002DA5B File Offset: 0x0002BC5B
		public override float getWidth()
		{
			return 0f;
		}

		// Token: 0x06003FF1 RID: 16369 RVA: 0x001BB11C File Offset: 0x001B931C
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

		// Token: 0x06003FF2 RID: 16370 RVA: 0x0002DE32 File Offset: 0x0002C032
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			this.AlterX(ref x, maxWidth);
		}

		// Token: 0x06003FF3 RID: 16371 RVA: 0x0002DE3C File Offset: 0x0002C03C
		public override void Release()
		{
			base.Release();
			this.d_value = 0f;
		}

		// Token: 0x04003919 RID: 14617
		public TypePosition type = TypePosition.Relative;

		// Token: 0x0400391A RID: 14618
		public float d_value;
	}
}
