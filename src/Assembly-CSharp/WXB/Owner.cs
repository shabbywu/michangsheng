using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x02000699 RID: 1689
	public interface Owner
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06003562 RID: 13666
		// (set) Token: 0x06003563 RID: 13667
		int minLineHeight { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06003564 RID: 13668
		Around around { get; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06003565 RID: 13669
		RenderCache renderCache { get; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06003566 RID: 13670
		Anchor anchor { get; }

		// Token: 0x06003567 RID: 13671
		void SetRenderDirty();

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06003568 RID: 13672
		ElementSegment elementSegment { get; }

		// Token: 0x06003569 RID: 13673
		Draw GetDraw(DrawType type, long key, Action<Draw, object> onCreate, object para = null);

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x0600356A RID: 13674
		Material material { get; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600356B RID: 13675
		LineAlignment lineAlignment { get; }
	}
}
