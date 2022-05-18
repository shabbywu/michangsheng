using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009C0 RID: 2496
	public class RectSpriteNode : NodeBase
	{
		// Token: 0x06003F8D RID: 16269 RVA: 0x0002DAB9 File Offset: 0x0002BCB9
		public override float getHeight()
		{
			return this.rect.height;
		}

		// Token: 0x06003F8E RID: 16270 RVA: 0x0002DAC6 File Offset: 0x0002BCC6
		public override float getWidth()
		{
			return this.rect.width;
		}

		// Token: 0x06003F8F RID: 16271 RVA: 0x0002DAD3 File Offset: 0x0002BCD3
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			cache.cacheSprite(null, this, this.sprite, this.rect);
		}

		// Token: 0x06003F90 RID: 16272 RVA: 0x000042DD File Offset: 0x000024DD
		public override void fill(ref Vector2 currentpos, List<Line> Lines, float maxWidth, float pixelsPerUnit)
		{
		}

		// Token: 0x040038EC RID: 14572
		public Rect rect;

		// Token: 0x040038ED RID: 14573
		public Sprite sprite;

		// Token: 0x040038EE RID: 14574
		public Color color;
	}
}
