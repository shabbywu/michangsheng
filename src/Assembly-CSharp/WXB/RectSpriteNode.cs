using System;
using System.Collections.Generic;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A9 RID: 1705
	public class RectSpriteNode : NodeBase
	{
		// Token: 0x060035CC RID: 13772 RVA: 0x00172002 File Offset: 0x00170202
		public override float getHeight()
		{
			return this.rect.height;
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x0017200F File Offset: 0x0017020F
		public override float getWidth()
		{
			return this.rect.width;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x0017201C File Offset: 0x0017021C
		public override void render(float maxWidth, RenderCache cache, ref float x, ref uint yline, List<Line> lines, float offsetX, float offsetY)
		{
			cache.cacheSprite(null, this, this.sprite, this.rect);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x00004095 File Offset: 0x00002295
		public override void fill(ref Vector2 currentpos, List<Line> Lines, float maxWidth, float pixelsPerUnit)
		{
		}

		// Token: 0x04002F26 RID: 12070
		public Rect rect;

		// Token: 0x04002F27 RID: 12071
		public Sprite sprite;

		// Token: 0x04002F28 RID: 12072
		public Color color;
	}
}
