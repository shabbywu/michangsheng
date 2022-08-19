using System;

namespace Fungus
{
	// Token: 0x02000EB9 RID: 3769
	public static class MenuSignals
	{
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06006A7E RID: 27262 RVA: 0x00293220 File Offset: 0x00291420
		// (remove) Token: 0x06006A7F RID: 27263 RVA: 0x00293254 File Offset: 0x00291454
		public static event MenuSignals.MenuStartHandler OnMenuStart;

		// Token: 0x06006A80 RID: 27264 RVA: 0x00293287 File Offset: 0x00291487
		public static void DoMenuStart(MenuDialog menu)
		{
			if (MenuSignals.OnMenuStart != null)
			{
				MenuSignals.OnMenuStart(menu);
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06006A81 RID: 27265 RVA: 0x0029329C File Offset: 0x0029149C
		// (remove) Token: 0x06006A82 RID: 27266 RVA: 0x002932D0 File Offset: 0x002914D0
		public static event MenuSignals.MenuEndHandler OnMenuEnd;

		// Token: 0x06006A83 RID: 27267 RVA: 0x00293303 File Offset: 0x00291503
		public static void DoMenuEnd(MenuDialog menu)
		{
			if (MenuSignals.OnMenuEnd != null)
			{
				MenuSignals.OnMenuEnd(menu);
			}
		}

		// Token: 0x02001703 RID: 5891
		// (Invoke) Token: 0x060088C0 RID: 35008
		public delegate void MenuStartHandler(MenuDialog menu);

		// Token: 0x02001704 RID: 5892
		// (Invoke) Token: 0x060088C4 RID: 35012
		public delegate void MenuEndHandler(MenuDialog menu);
	}
}
