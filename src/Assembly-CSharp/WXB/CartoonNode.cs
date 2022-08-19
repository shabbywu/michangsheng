using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006A5 RID: 1701
	public class CartoonNode : RectNode
	{
		// Token: 0x060035B7 RID: 13751 RVA: 0x00171D5A File Offset: 0x0016FF5A
		public override float getWidth()
		{
			return (float)((int)(this.width + this.cartoon.space));
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x00171D70 File Offset: 0x0016FF70
		protected override void OnRectRender(RenderCache cache, Line line, Rect rect)
		{
			float space = this.cartoon.space;
			rect.x += space / 2f;
			rect.width -= space;
			cache.cacheCartoon(line, this, this.cartoon, rect);
		}

		// Token: 0x04002F1C RID: 12060
		public Cartoon cartoon;
	}
}
