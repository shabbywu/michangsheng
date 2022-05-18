using System;
using UnityEngine;

namespace WXB
{
	// Token: 0x020009AD RID: 2477
	public interface Owner
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06003F20 RID: 16160
		// (set) Token: 0x06003F21 RID: 16161
		int minLineHeight { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06003F22 RID: 16162
		Around around { get; }

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06003F23 RID: 16163
		RenderCache renderCache { get; }

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06003F24 RID: 16164
		Anchor anchor { get; }

		// Token: 0x06003F25 RID: 16165
		void SetRenderDirty();

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06003F26 RID: 16166
		ElementSegment elementSegment { get; }

		// Token: 0x06003F27 RID: 16167
		Draw GetDraw(DrawType type, long key, Action<Draw, object> onCreate, object para = null);

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06003F28 RID: 16168
		Material material { get; }

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06003F29 RID: 16169
		LineAlignment lineAlignment { get; }
	}
}
