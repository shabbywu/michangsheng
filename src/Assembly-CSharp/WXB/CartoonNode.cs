using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009BC RID: 2492
	public class CartoonNode : RectNode
	{
		// Token: 0x06003F78 RID: 16248 RVA: 0x0002D9D4 File Offset: 0x0002BBD4
		public override float getWidth()
		{
			return (float)((int)(this.width + this.cartoon.space));
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x001B9980 File Offset: 0x001B7B80
		protected override void OnRectRender(RenderCache cache, Line line, Rect rect)
		{
			float space = this.cartoon.space;
			rect.x += space / 2f;
			rect.width -= space;
			cache.cacheCartoon(line, this, this.cartoon, rect);
		}

		// Token: 0x040038E2 RID: 14562
		public Cartoon cartoon;
	}
}
