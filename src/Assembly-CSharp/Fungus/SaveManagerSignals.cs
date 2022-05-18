using System;

namespace Fungus
{
	// Token: 0x0200134B RID: 4939
	public static class SaveManagerSignals
	{
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060077E1 RID: 30689 RVA: 0x002B564C File Offset: 0x002B384C
		// (remove) Token: 0x060077E2 RID: 30690 RVA: 0x002B5680 File Offset: 0x002B3880
		public static event SaveManagerSignals.SavePointLoadedHandler OnSavePointLoaded;

		// Token: 0x060077E3 RID: 30691 RVA: 0x000519C5 File Offset: 0x0004FBC5
		public static void DoSavePointLoaded(string savePointKey)
		{
			if (SaveManagerSignals.OnSavePointLoaded != null)
			{
				SaveManagerSignals.OnSavePointLoaded(savePointKey);
			}
		}

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060077E4 RID: 30692 RVA: 0x002B56B4 File Offset: 0x002B38B4
		// (remove) Token: 0x060077E5 RID: 30693 RVA: 0x002B56E8 File Offset: 0x002B38E8
		public static event SaveManagerSignals.SavePointAddedHandler OnSavePointAdded;

		// Token: 0x060077E6 RID: 30694 RVA: 0x000519D9 File Offset: 0x0004FBD9
		public static void DoSavePointAdded(string savePointKey, string savePointDescription)
		{
			if (SaveManagerSignals.OnSavePointAdded != null)
			{
				SaveManagerSignals.OnSavePointAdded(savePointKey, savePointDescription);
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x060077E7 RID: 30695 RVA: 0x002B571C File Offset: 0x002B391C
		// (remove) Token: 0x060077E8 RID: 30696 RVA: 0x002B5750 File Offset: 0x002B3950
		public static event SaveManagerSignals.SaveResetHandler OnSaveReset;

		// Token: 0x060077E9 RID: 30697 RVA: 0x000519EE File Offset: 0x0004FBEE
		public static void DoSaveReset()
		{
			if (SaveManagerSignals.OnSaveReset != null)
			{
				SaveManagerSignals.OnSaveReset();
			}
		}

		// Token: 0x0200134C RID: 4940
		// (Invoke) Token: 0x060077EB RID: 30699
		public delegate void SavePointLoadedHandler(string savePointKey);

		// Token: 0x0200134D RID: 4941
		// (Invoke) Token: 0x060077EF RID: 30703
		public delegate void SavePointAddedHandler(string savePointKey, string savePointDescription);

		// Token: 0x0200134E RID: 4942
		// (Invoke) Token: 0x060077F3 RID: 30707
		public delegate void SaveResetHandler();
	}
}
