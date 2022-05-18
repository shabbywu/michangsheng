using System;

namespace Fungus
{
	// Token: 0x02001344 RID: 4932
	public static class FungusPrioritySignals
	{
		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060077BD RID: 30653 RVA: 0x0005195E File Offset: 0x0004FB5E
		public static int CurrentPriorityDepth
		{
			get
			{
				return FungusPrioritySignals.activeDepth;
			}
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x060077BE RID: 30654 RVA: 0x002B53A4 File Offset: 0x002B35A4
		// (remove) Token: 0x060077BF RID: 30655 RVA: 0x002B53D8 File Offset: 0x002B35D8
		public static event FungusPrioritySignals.FungusPriorityStartHandler OnFungusPriorityStart;

		// Token: 0x14000061 RID: 97
		// (add) Token: 0x060077C0 RID: 30656 RVA: 0x002B540C File Offset: 0x002B360C
		// (remove) Token: 0x060077C1 RID: 30657 RVA: 0x002B5440 File Offset: 0x002B3640
		public static event FungusPrioritySignals.FungusPriorityEndHandler OnFungusPriorityEnd;

		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060077C2 RID: 30658 RVA: 0x002B5474 File Offset: 0x002B3674
		// (remove) Token: 0x060077C3 RID: 30659 RVA: 0x002B54A8 File Offset: 0x002B36A8
		public static event FungusPrioritySignals.FungusPriorityChangeHandler OnFungusPriorityChange;

		// Token: 0x060077C4 RID: 30660 RVA: 0x002B54DC File Offset: 0x002B36DC
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

		// Token: 0x060077C5 RID: 30661 RVA: 0x002B552C File Offset: 0x002B372C
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

		// Token: 0x060077C6 RID: 30662 RVA: 0x00051965 File Offset: 0x0004FB65
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

		// Token: 0x04006833 RID: 26675
		private static int activeDepth;

		// Token: 0x02001345 RID: 4933
		// (Invoke) Token: 0x060077C8 RID: 30664
		public delegate void FungusPriorityStartHandler();

		// Token: 0x02001346 RID: 4934
		// (Invoke) Token: 0x060077CC RID: 30668
		public delegate void FungusPriorityEndHandler();

		// Token: 0x02001347 RID: 4935
		// (Invoke) Token: 0x060077D0 RID: 30672
		public delegate void FungusPriorityChangeHandler(int previousActiveDepth, int newActiveDepth);
	}
}
