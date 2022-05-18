using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009D0 RID: 2512
	public class SpriteNode : RectNode
	{
		// Token: 0x06003FF5 RID: 16373 RVA: 0x0002DE5E File Offset: 0x0002C05E
		protected override void OnRectRender(RenderCache cache, Line line, Rect rect)
		{
			cache.cacheSprite(line, this, this.sprite, rect);
		}

		// Token: 0x0400391B RID: 14619
		public Sprite sprite;
	}
}
