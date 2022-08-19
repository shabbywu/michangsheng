using System;

namespace Fungus
{
	// Token: 0x02000EBA RID: 3770
	public static class SaveManagerSignals
	{
		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06006A84 RID: 27268 RVA: 0x00293318 File Offset: 0x00291518
		// (remove) Token: 0x06006A85 RID: 27269 RVA: 0x0029334C File Offset: 0x0029154C
		public static event SaveManagerSignals.SavePointLoadedHandler OnSavePointLoaded;

		// Token: 0x06006A86 RID: 27270 RVA: 0x0029337F File Offset: 0x0029157F
		public static void DoSavePointLoaded(string savePointKey)
		{
			if (SaveManagerSignals.OnSavePointLoaded != null)
			{
				SaveManagerSignals.OnSavePointLoaded(savePointKey);
			}
		}

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x06006A87 RID: 27271 RVA: 0x00293394 File Offset: 0x00291594
		// (remove) Token: 0x06006A88 RID: 27272 RVA: 0x002933C8 File Offset: 0x002915C8
		public static event SaveManagerSignals.SavePointAddedHandler OnSavePointAdded;

		// Token: 0x06006A89 RID: 27273 RVA: 0x002933FB File Offset: 0x002915FB
		public static void DoSavePointAdded(string savePointKey, string savePointDescription)
		{
			if (SaveManagerSignals.OnSavePointAdded != null)
			{
				SaveManagerSignals.OnSavePointAdded(savePointKey, savePointDescription);
			}
		}

		// Token: 0x14000067 RID: 103
		// (add) Token: 0x06006A8A RID: 27274 RVA: 0x00293410 File Offset: 0x00291610
		// (remove) Token: 0x06006A8B RID: 27275 RVA: 0x00293444 File Offset: 0x00291644
		public static event SaveManagerSignals.SaveResetHandler OnSaveReset;

		// Token: 0x06006A8C RID: 27276 RVA: 0x00293477 File Offset: 0x00291677
		public static void DoSaveReset()
		{
			if (SaveManagerSignals.OnSaveReset != null)
			{
				SaveManagerSignals.OnSaveReset();
			}
		}

		// Token: 0x02001705 RID: 5893
		// (Invoke) Token: 0x060088C8 RID: 35016
		public delegate void SavePointLoadedHandler(string savePointKey);

		// Token: 0x02001706 RID: 5894
		// (Invoke) Token: 0x060088CC RID: 35020
		public delegate void SavePointAddedHandler(string savePointKey, string savePointDescription);

		// Token: 0x02001707 RID: 5895
		// (Invoke) Token: 0x060088D0 RID: 35024
		public delegate void SaveResetHandler();
	}
}
