using System;

namespace Fungus
{
	// Token: 0x02000EB8 RID: 3768
	public static class FungusPrioritySignals
	{
		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06006A74 RID: 27252 RVA: 0x00293006 File Offset: 0x00291206
		public static int CurrentPriorityDepth
		{
			get
			{
				return FungusPrioritySignals.activeDepth;
			}
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06006A75 RID: 27253 RVA: 0x00293010 File Offset: 0x00291210
		// (remove) Token: 0x06006A76 RID: 27254 RVA: 0x00293044 File Offset: 0x00291244
		public static event FungusPrioritySignals.FungusPriorityStartHandler OnFungusPriorityStart;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x06006A77 RID: 27255 RVA: 0x00293078 File Offset: 0x00291278
		// (remove) Token: 0x06006A78 RID: 27256 RVA: 0x002930AC File Offset: 0x002912AC
		public static event FungusPrioritySignals.FungusPriorityEndHandler OnFungusPriorityEnd;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06006A79 RID: 27257 RVA: 0x002930E0 File Offset: 0x002912E0
		// (remove) Token: 0x06006A7A RID: 27258 RVA: 0x00293114 File Offset: 0x00291314
		public static event FungusPrioritySignals.FungusPriorityChangeHandler OnFungusPriorityChange;

		// Token: 0x06006A7B RID: 27259 RVA: 0x00293148 File Offset: 0x00291348
		public static void DoIncreasePriorityDepth()
		{
			if (FungusPrioritySignals.activeDepth == 0 && FungusPrioritySignals.OnFungusPriorityStart != null)
			{
				FungusPrioritySignals.OnFungusPriorityStart();
			}
			if (FungusPrioritySignals.OnFungusPriorityChange != null)
			{
				FungusPrioritySignals.OnFungusPriorityChange(FungusPrioritySignals.activeDepth, FungusPrioritySignals.activeDepth + 1);
			}
			FungusPrioritySignals.activeDepth++;
		}

		// Token: 0x06006A7C RID: 27260 RVA: 0x00293198 File Offset: 0x00291398
		public static void DoDecreasePriorityDepth()
		{
			if (FungusPrioritySignals.OnFungusPriorityChange != null)
			{
				FungusPrioritySignals.OnFungusPriorityChange(FungusPrioritySignals.activeDepth, FungusPrioritySignals.activeDepth - 1);
			}
			if (FungusPrioritySignals.activeDepth == 1 && FungusPrioritySignals.OnFungusPriorityEnd != null)
			{
				FungusPrioritySignals.OnFungusPriorityEnd();
			}
			FungusPrioritySignals.activeDepth--;
		}

		// Token: 0x06006A7D RID: 27261 RVA: 0x002931E7 File Offset: 0x002913E7
		public static void DoResetPriority()
		{
			if (FungusPrioritySignals.activeDepth == 0)
			{
				return;
			}
			if (FungusPrioritySignals.OnFungusPriorityChange != null)
			{
				FungusPrioritySignals.OnFungusPriorityChange(FungusPrioritySignals.activeDepth, 0);
			}
			if (FungusPrioritySignals.OnFungusPriorityEnd != null)
			{
				FungusPrioritySignals.OnFungusPriorityEnd();
			}
			FungusPrioritySignals.activeDepth = 0;
		}

		// Token: 0x040059EC RID: 23020
		private static int activeDepth;

		// Token: 0x02001700 RID: 5888
		// (Invoke) Token: 0x060088B4 RID: 34996
		public delegate void FungusPriorityStartHandler();

		// Token: 0x02001701 RID: 5889
		// (Invoke) Token: 0x060088B8 RID: 35000
		public delegate void FungusPriorityEndHandler();

		// Token: 0x02001702 RID: 5890
		// (Invoke) Token: 0x060088BC RID: 35004
		public delegate void FungusPriorityChangeHandler(int previousActiveDepth, int newActiveDepth);
	}
}
