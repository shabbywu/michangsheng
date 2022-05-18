using System;

namespace Fungus
{
	// Token: 0x02001348 RID: 4936
	public static class MenuSignals
	{
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060077D3 RID: 30675 RVA: 0x002B557C File Offset: 0x002B377C
		// (remove) Token: 0x060077D4 RID: 30676 RVA: 0x002B55B0 File Offset: 0x002B37B0
		public static event MenuSignals.MenuStartHandler OnMenuStart;

		// Token: 0x060077D5 RID: 30677 RVA: 0x0005199D File Offset: 0x0004FB9D
		public static void DoMenuStart(MenuDialog menu)
		{
			if (MenuSignals.OnMenuStart != null)
			{
				MenuSignals.OnMenuStart(menu);
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060077D6 RID: 30678 RVA: 0x002B55E4 File Offset: 0x002B37E4
		// (remove) Token: 0x060077D7 RID: 30679 RVA: 0x002B5618 File Offset: 0x002B3818
		public static event MenuSignals.MenuEndHandler OnMenuEnd;

		// Token: 0x060077D8 RID: 30680 RVA: 0x000519B1 File Offset: 0x0004FBB1
		public static void DoMenuEnd(MenuDialog menu)
		{
			if (MenuSignals.OnMenuEnd != null)
			{
				MenuSignals.OnMenuEnd(menu);
			}
		}

		// Token: 0x02001349 RID: 4937
		// (Invoke) Token: 0x060077DA RID: 30682
		public delegate void MenuStartHandler(MenuDialog menu);

		// Token: 0x0200134A RID: 4938
		// (Invoke) Token: 0x060077DE RID: 30686
		public delegate void MenuEndHandler(MenuDialog menu);
	}
}
