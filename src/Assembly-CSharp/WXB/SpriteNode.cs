using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020006AE RID: 1710
	public class SpriteNode : RectNode
	{
		// Token: 0x060035F8 RID: 13816 RVA: 0x00172903 File Offset: 0x00170B03
		protected override void OnRectRender(RenderCache cache, Line line, Rect rect)
		{
			cache.cacheSprite(line, this, this.sprite, rect);
		}

		// Token: 0x04002F3E RID: 12094
		public Sprite sprite;
	}
}
